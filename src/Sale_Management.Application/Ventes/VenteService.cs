using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sale_Management.Entities;
using Sale_Management.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Sale_Management.Ventes
{
    public class VenteService : IVenteService,IScopedDependency
    {
        private readonly Sale_ManagementDbContext _dbContext;
        private readonly IMapper _mapper;

        public VenteService(Sale_ManagementDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        // get the ventes
        public async Task<List<VenteDto>> GetAllVentesAsync()
        {
            var ventes = await _dbContext.Ventes
                .Include(vente => vente.articleVendue)
                .Include(vente => vente.client)
                .ToListAsync();

            var venteDtos = ventes.Select(vente =>
            {
                var venteDto = _mapper.Map<Vente, VenteDto>(vente);
                venteDto.prixTotal = vente.PrixTotal(vente.articleVendue.Price);
                return venteDto;
            }).ToList();

            return venteDtos;
        }
        // get client vente
        public async Task<List<VenteDto>> GetVentesByClientNameAsync(string clientFName, string clientLName)
        {
            var ventes = await _dbContext.Ventes
                .Include(vente => vente.articleVendue)
                .Include(vente => vente.client)
                .Where(vente =>
                    vente.client.FName == clientFName &&
                    vente.client.LName == clientLName)
                .ToListAsync();

            var venteDtos = ventes.Select(vente =>
            {
                var venteDto = _mapper.Map<Vente, VenteDto>(vente);
                venteDto.prixTotal = vente.PrixTotal(vente.articleVendue.Price);
                return venteDto;
            }).ToList();

            return venteDtos;
        }

        // effectuer la vente
        public void AddVente(int clientId, int articleId, int quantite)
        {
            // get the article
            var article = _dbContext.Articles.Find(articleId);
            // get the client
            var client = _dbContext.Clients.Find(clientId);
            // check the existance of client and article and the availibility of quantity in stock
            if(article.Id==articleId && client.Id==clientId && quantite <= article.QuantityinStock)
            {
                // Add Vente
               var vente = new Vente
               {
                    articleId = articleId,
                    clientId = clientId,
                    QuantityVendue = quantite
               };
                //update stock quantity
                article.QuantityinStock -= quantite;
                _dbContext.Ventes.Add(vente);
                _dbContext.SaveChanges();
            }
            else
            {
                throw new Exception("Vente couldn't be done!");
            }
            
        }
    }
}
