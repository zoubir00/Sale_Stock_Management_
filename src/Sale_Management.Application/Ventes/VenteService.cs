using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sale_Management.Articles;
using Sale_Management.Clients;
using Sale_Management.Entities;
using Sale_Management.EntityFrameworkCore;
using Sale_Management.VenteLines;
using Sale_Management.Ventes.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Application.Dtos;
using Sale_Management.Clients.Repository;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Entities;
using Sale_Management.Articles.Repository;
using SendGrid.Helpers.Errors.Model;
using SaleManagement.Migrations;

namespace Sale_Management.Ventes
{
    public class VenteService : ApplicationService ,IVenteService
    {
        //private readonly Sale_ManagementDbContext _dbContext;
        public readonly IVenteRepository _venteRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IArticleRepository _articleRepository;
        private readonly IRepository<VenteLine,Guid> _venteLineRepository;

        //public readonly VenteManager _venteManager;

        public VenteService(IVenteRepository venteRepository, IClientRepository clientRepository, IRepository<VenteLine, Guid> venteLineRepository, IArticleRepository articleRepository)
        {

            _venteRepository = venteRepository;
            _clientRepository = clientRepository;
            _venteLineRepository = venteLineRepository;
            _articleRepository = articleRepository;
            //_venteManager = venteManager;
        }
        //Get Ventes
        public async Task<PagedResultDto<GetVenteDto>> GetAllVentes()
        {
            
            try
            {
                var queryable = await _venteRepository.GetQueryableAsync();
                var ventedto = await queryable.Include(vente => vente.client)
                    .Select(vente => new GetVenteDto
                    {
                        Id = vente.Id,
                        DateVente = vente.DateVente,
                        ClientId=vente.client.Id,
                        clientName = vente.client.FName + " " + vente.client.LName,
                        QtyTotal = vente.QtyTotal,
                        TotalAmount = vente.TotalAmount
                    }).ToListAsync();

                var totalCount = await _venteRepository.CountAsync();
                return new PagedResultDto<GetVenteDto>(
               totalCount,
              ventedto
               );
              
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
       // get vente Details
        public async Task<VenteDto> GetVenteDetails(Guid codeVente)
        {
            var queryable = await _venteRepository.GetQueryableAsync();

            var venteDto = await queryable
                .Include(vente => vente.client)
                .Include(vente => vente.VenteLines).ThenInclude(a => a.Article)
                .FirstOrDefaultAsync(vente => vente.Id == codeVente);
                
                var vente =new VenteDto
                {
                    Id = venteDto.Id,
                    DateVente = venteDto.DateVente,
                    clientId = venteDto.clientId,
                    clientName = venteDto.client.FName + " " + venteDto.client.LName,

                    VenteLines = venteDto.VenteLines.Select(vl => new VenteLinesDto
                    {
                        Id = vl.Id,
                        VenteCode = venteDto.Id,
                        articleId = vl.articleId,
                        articlelebelle = vl.Article.Libelle,
                        QtySold = vl.QtySold,
                        SalePrice = vl.TotalPrice / vl.QtySold,
                        TotalPrice = vl.TotalPrice
                    }).ToList(),
                    QtyTotal = venteDto.QtyTotal,
                    TotalAmount = venteDto.TotalAmount
                };
                 
                return vente;
        }
        //
        public async Task<VenteDto> CreateVente(DateTime dateVente, Guid clientId, List<VenteLinesDto> venteLines)
        {
            // get client
            var client = await _clientRepository.GetAsync(clientId);
            if (client == null)
            {
                throw new Exception("Client Not found");
            }
            var vente = new Vente
            (
                dateVente,
                clientId
            );
            
            int _qtyTotal = 0;
            double _totalAmount = 0;
            foreach(var venteLine in venteLines)
            {
                var article = await _articleRepository.GetAsync(venteLine.articleId);
                if (article != null || article.QuantityinStock >= venteLine.QtySold)
                {
                    var _venteline = new VenteLine
                    ( 
                       vente.Id,
                       article.Id,
                       venteLine.QtySold,
                       article.Price,
                       venteLine.QtySold * article.Price
                    );
                    article.QuantityinStock -= venteLine.QtySold;
                    vente.VenteLines.Add(_venteline);
                    _qtyTotal += venteLine.QtySold;
                    _totalAmount += _venteline.TotalPrice;
                }
                else
                {
                    throw new Exception("Vente line doesn't valid");
                }
            }
            vente.QtyTotal = _qtyTotal;
            vente.TotalAmount = _totalAmount;
            await _venteRepository.InsertAsync(vente);

            return ObjectMapper.Map<Vente, VenteDto>(vente);
           
        }
        // valid a sale 
        public async Task<string> validCreate(Guid codeVente)
        {
            var message = "Valid Sale";
            var queryable = await _venteRepository.GetQueryableAsync();

            var venteDto = await queryable
                .Include(vente => vente.client)
                .Include(vente => vente.VenteLines).ThenInclude(a => a.Article)
                .FirstOrDefaultAsync(vente => vente.Id == codeVente);
            foreach(var venteline in venteDto.VenteLines)
            {
                var article = await _articleRepository.GetAsync(venteline.articleId);
                if(article!=null || article.QuantityinStock >= venteline.QtySold)
                {
                    article.QuantityinStock -= venteline.QtySold;
                }
            }
            return message;
        }
        // Invalid a sale
        public async Task<string> InvalidCreate(Guid codeVente)
        {
            var message = "Invalid Sale";
            var queryable = await _venteRepository.GetQueryableAsync();

            var venteDto = await queryable
                .Include(vente => vente.client)
                .Include(vente => vente.VenteLines).ThenInclude(a => a.Article)
                .FirstOrDefaultAsync(vente => vente.Id == codeVente);
            foreach (var venteline in venteDto.VenteLines)
            {
                var article = await _articleRepository.GetAsync(venteline.articleId);
                if (article != null || article.QuantityinStock >= venteline.QtySold)
                {
                    article.QuantityinStock += venteline.QtySold;
                }
            }
            return message;
        }

        public async Task<VenteDto> UpdateVenteAsync(Guid venteCode, DateTime newDateVente, Guid newClientId, List<VenteLinesDto> updatedVenteLines)
        {
            try
            {

                var queryable = await _venteRepository.GetQueryableAsync();
                // get vente
                var existingVente = await queryable.Include(v => v.VenteLines).
                        FirstOrDefaultAsync(v => v.Id == venteCode);
            if (existingVente == null)
            {
                throw new NotFoundException("Vente not found");
            }
            existingVente.DateVente = newDateVente;
            existingVente.clientId = newClientId;

            foreach(var venteline in updatedVenteLines)
            {
                var existingVenteLine = existingVente.VenteLines.FirstOrDefault(vl => vl.Id == venteline.Id);
                if (existingVenteLine !=null)
                {
                    int diff = venteline.QtySold - existingVenteLine.QtySold;
                     
                    existingVenteLine.QtySold = venteline.QtySold;
                    existingVenteLine.SalePrice = venteline.SalePrice;
                    existingVenteLine.TotalPrice = venteline.SalePrice * venteline.QtySold;
                        var article = await _articleRepository.GetAsync(venteline.articleId);
                        if(article != null && article.QuantityinStock >= venteline.QtySold)
                        {
                            article.QuantityinStock -= diff;
                        }      
                }
                else
                {
                    var article = await _articleRepository.GetAsync(venteline.articleId);
                    if(article!=null && article.QuantityinStock >= venteline.QtySold)
                    {
                        var newVenteLine = new VenteLine
                        (   existingVente.Id,
                            article.Id,
                            venteline.QtySold,
                           article.Price,
                            venteline.SalePrice*venteline.QtySold
                         );
                          article.QuantityinStock -= newVenteLine.QtySold;
                        existingVente.VenteLines.Add(newVenteLine);

                    }
                    else
                    {
                        throw new Exception("Vente line doesn't valid");
                    }
                }
            }
            existingVente.QtyTotal = existingVente.VenteLines.Sum(vl => vl.QtySold);
            existingVente.TotalAmount = existingVente.VenteLines.Sum(vl => vl.TotalPrice);
            await _venteRepository.UpdateAsync(existingVente);
            return ObjectMapper.Map<Vente, VenteDto>(existingVente);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        // delte venteLine 
        public async Task DeleteVenteLine(Guid codeVente, Guid venteLineId)
        {
            var _vente = await _venteRepository.GetQueryableAsync();
            var vente = await _vente.Include(vl => vl.VenteLines).ThenInclude(a => a.Article).FirstOrDefaultAsync(v=>v.Id==codeVente);
            if (vente == null)
            {
                throw new NotFoundException("sale not found");
            }
            var venteline = vente.VenteLines.SingleOrDefault(vl => vl.Id == venteLineId);
            if (venteline == null)
            {
                throw new NotFoundException("sale not found");
            }

            venteline.Article.QuantityinStock += venteline.QtySold;

            await _venteLineRepository.DeleteAsync(venteline);

        }

        // delete vente
        public async Task DeleteVente(Guid venteCode)
        {
            var _vente = await _venteRepository.GetQueryableAsync();
            var existingVente = await _vente.Include(vl => vl.VenteLines).FirstOrDefaultAsync(v => v.Id == venteCode);
            if (existingVente == null)
            {
                throw new NotFoundException("Sale not found");
            }
            foreach(var venteline in existingVente.VenteLines)
            {
                var article = await _articleRepository.FindAsync(venteline.articleId);
                if (article != null)
                {
                    article.QuantityinStock += venteline.QtySold;
                }
                await _venteLineRepository.DeleteAsync(venteline);
            }
            await _venteRepository.DeleteAsync(existingVente);
           
        }


    }
}
