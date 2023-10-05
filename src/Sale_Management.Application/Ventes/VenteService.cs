using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sale_Management.Articles;
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
                DateVente = DateTime.Now,
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
    }
}
