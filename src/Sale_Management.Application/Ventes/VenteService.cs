using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sale_Management.Articles;
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

        // create vente
        public VenteSummaryDto CreateVente(int clientId, List<int> articleIds, List<int> quantities)
        {
            // Get the client
            var client = _dbContext.Clients.Find(clientId);

            // Check the existence of the client
            if (client == null)
            {
                throw new Exception("Client not found!");
            }

            //  save individual vente details
            List<VenteDto> individualSales = new List<VenteDto>();

            // Initialize total quantity and total amount
            int totalQuantity = 0;
            double totalAmount = 0;

            // Loop for each article and quantity
            for (int i = 0; i < articleIds.Count; i++)
            {
                int articleId = articleIds[i];
                int quantite = quantities[i];

                // Get the article
                var article = _dbContext.Articles.Find(articleId);

                // Check the existence of the article and the availability of quantity in stock
                if (article != null && quantite <= article.QuantityinStock)
                {
                    // Add Vente
                    var vente = new Vente
                    {
                        DateVente = DateTime.Now,
                        articleId = articleId,
                        clientId = clientId,
                        QuantityVendue = quantite
                    };

                    // Update stock quantity
                    article.QuantityinStock -= quantite;
                    _dbContext.Ventes.Add(vente);

                    // Calculate individual sale total
                    double individualTotal = vente.PrixTotal(article.Price);

                    // add sale to list
                    individualSales.Add(new VenteDto
                    {
                        Id = vente.Id,
                        DateVente = vente.DateVente,
                        clientFName = client.FName,
                        clientLName = client.LName,
                        articleVendue = article.Libelle,
                        QuantityVendue = vente.QuantityVendue,
                        prixTotal = individualTotal
                    });

                    // Update total quantity & total amount
                    totalQuantity += quantite;
                    totalAmount += individualTotal;
                }
                else
                {
                    throw new Exception("Vente couldn't be done for one or more articles!");
                }
            }

            // Save changes to the database
            _dbContext.SaveChanges();

            // summary of the vente with the ventes and totalQuantity and totalAmount
            var summaryDto = new VenteSummaryDto
            {
                IndividualSales = individualSales,
                TotalQuantity = totalQuantity,
                TotalAmount = totalAmount
            };

            return summaryDto;
        }

        
        public async Task<List<VenteDto>> GetAllVentesAsync()
        {
            var ventes = await _dbContext.Ventes
                .Include(vente => vente.articleVendue)
                .Include(vente => vente.client)
                .ToListAsync();

            var venteDtos = ventes.Select(vente => new VenteDto
            {
                Id = vente.Id,
                DateVente = vente.DateVente,
                clientFName = vente.client.FName,
                clientLName = vente.client.LName,
                articleVendue = vente.articleVendue.Libelle, 
                QuantityVendue = vente.QuantityVendue,
                prixTotal = vente.PrixTotal(vente.articleVendue.Price) 
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
                var venteDto = new VenteDto
                {
                    Id = vente.Id,
                    DateVente = vente.DateVente,
                    clientFName = vente.client.FName,
                    clientLName = vente.client.LName,
                    articleVendue = vente.articleVendue.Libelle, 
                    QuantityVendue = vente.QuantityVendue,
                    prixTotal = vente.PrixTotal(vente.articleVendue.Price) 
                };
                return venteDto;
            }).ToList();
            return venteDtos;
        }

        // delete
        public async Task deleteVente(int id)
        {
            var venteTodelete =await _dbContext.Ventes.FindAsync(id);
            if (venteTodelete == null)
            {
                throw new Exception("No vente found");
            }
            else
            {
                _dbContext.Ventes.Remove(venteTodelete);
                await _dbContext.SaveChangesAsync();
            }
        }

        // get the ventes
        public List<VenteSummaryDto> GetVenteSummaries()
        {
            var venteSummaries = _dbContext.Ventes
                .Include(vente => vente.client)
                .Include(vente => vente.articleVendue)
                .GroupBy(vente => new { vente.DateVente, vente.client.FName, vente.client.LName })
                .Select(group => new VenteSummaryDto
                {
                    IndividualSales = group.Select(vente => new VenteDto
                    {
                        Id = vente.Id,
                        DateVente = vente.DateVente,
                        clientFName = vente.client.FName,
                        clientLName = vente.client.LName,
                        articleVendue = vente.articleVendue.Libelle,
                        QuantityVendue = vente.QuantityVendue,
                        prixTotal = vente.PrixTotal(vente.articleVendue.Price)
                    }).ToList(),
                    TotalQuantity = group.Sum(vente => vente.QuantityVendue),
                    TotalAmount = group.Sum(vente => vente.QuantityVendue * vente.articleVendue.Price)
                })
                .ToList();

            return venteSummaries;
        }

    }
}
