using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sale_Management.Articles;
using Sale_Management.Clients;
using Sale_Management.Entities;
using Sale_Management.EntityFrameworkCore;
using Sale_Management.VenteLines;
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
        public VenteDto CreateVente(string venteCode, DateTime dateVente, int clientId, List<VenteLinesDto> venteLines)
        {
            //get client
            var client = _dbContext.Clients.Find(clientId);
            if (client == null)
            {
                throw new Exception("Client Not found");
            }
            var vente = _mapper.Map<Vente>(new VenteDto
            {
                Id = venteCode,
                DateVente = dateVente,
                clientId = clientId,
                VenteLines = new List<VenteLinesDto>()
            });


            int _qtyTotal = 0;
            double _totalAmount = 0;
            foreach (var venteline in venteLines)
            {
                var article = _dbContext.Articles.Find(venteline.articleId);
                if (article == null || article.QuantityinStock >= venteline.QtySold)
                {
                    var VenteLinedto = _mapper.Map<VenteLine>(new VenteLinesDto
                    {
                        VenteCode = vente.Id,
                        articleId = article.Id,
                        QtySold = venteline.QtySold,
                        TotalPrice = venteline.QtySold * article.Price
                    });
                    article.QuantityinStock -= venteline.QtySold;
                    vente.VenteLines.Add(VenteLinedto);
                    _qtyTotal += venteline.QtySold;
                    _totalAmount += VenteLinedto.TotalPrice;

                }
                else
                {
                    throw new Exception("Vente line doesn't valid");
                }

            }
            vente.QtyTotal = _qtyTotal;
            vente.TotalAmount = _totalAmount;
            _dbContext.Ventes.Add(vente);
            _dbContext.SaveChanges();
            var ventedto = _mapper.Map<VenteDto>(vente);
            return ventedto;
        }

        // Edit vente
        public VenteDto EditVente(string venteCode, DateTime newDateVente, int newcClientId, List<VenteLinesDto> venteLines)
        {
            var existVente = GetVenteDetails(venteCode);
            if (existVente == null)
            {
                throw new Exception("not exist");
            }
            else
            {
                existVente.DateVente = newDateVente;
                existVente.clientId = newcClientId;
                foreach(var venteline in venteLines)
                {
                    var existVenteLine = existVente.VenteLines.SingleOrDefault(vl => vl.Id == venteline.Id);
                    if (existVenteLine != null)
                    {
                        existVenteLine.VenteCode = venteline.VenteCode;
                        existVenteLine.articleId = venteline.articleId;
                        existVenteLine.QtySold = venteline.QtySold;
                        existVenteLine.TotalPrice = venteline.TotalPrice;
                    }
                    else
                    {
                        var article = _dbContext.Articles.Find(venteline.articleId);
                        var newVenteLine = new VenteLinesDto
                        {
                            Id=venteline.Id,
                            articleId = venteline.articleId,
                            QtySold = venteline.QtySold,
                            TotalPrice = venteline.QtySold*article.Price 
                        };
                        article.QuantityinStock -= newVenteLine.QtySold;
                        existVente.VenteLines.Add(newVenteLine);
                        
                    }

                }
            }
            existVente.QtyTotal = existVente.VenteLines.Sum(vl => vl.QtySold);
                existVente.TotalAmount = existVente.VenteLines.Sum(vl => vl.TotalPrice);
                _dbContext.SaveChanges();
            return existVente;
        }

        // Get Ventes 
       public List<GetVenteDto> GetAllVentes()
        {
            var ventes = _dbContext.Ventes.Include(c=>c.client).ToList();
            var ventedto = ventes.Select(v => new GetVenteDto
            {
                Id = v.Id,
                DateVente = v.DateVente,
                clientName = v.client.FName+" "+ v.client.LName,
                QtyTotal = v.QtyTotal,
                TotalAmount = v.TotalAmount
            }).ToList();
            return ventedto;
        }
       
        // get vente Details
        public VenteDto GetVenteDetails(string codeVente)
        {
            var vente = _dbContext.Ventes.Include(c => c.client).
                Include(v => v.VenteLines).ThenInclude(vl=>vl.Article)
                .FirstOrDefault(v => v.Id == codeVente);
            var venteDto = new VenteDto
            {
                Id = vente.Id,
                DateVente = vente.DateVente,
                client = new ClientDto
                {
                    Id=vente.client.Id,
                    FName = vente.client.FName,
                    LName = vente.client.LName,
                    Email = vente.client.Email,
                    PhoneNumber = vente.client.PhoneNumber
                },
                VenteLines=vente.VenteLines.Select(vl=>new VenteLinesDto
                {
                    Id=vl.Id,
                    VenteCode = vl.VenteCode,
                    articleId = vl.articleId,
                    articlelebelle=vl.Article !=null ? vl.Article.Libelle : null,
                    QtySold = vl.QtySold,
                    TotalPrice = vl.TotalPrice,
                }).ToList(),
                QtyTotal = vente.QtyTotal,
                TotalAmount = vente.TotalAmount
            };
            return venteDto;
        }
    }
}
