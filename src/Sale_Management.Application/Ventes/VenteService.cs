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

        // effectuer la vente
        public VenteSummaryDto CreateVente(int clientId, List<int> articleIds, List<int> quantities)
        {
            // Get the client
            var client = _dbContext.Clients.Find(clientId);

            // Check the existence of the client
            if (client == null)
            {
                throw new Exception("Client not found!");
            }

            // Create a list to store individual sale details
            List<VenteDto> individualSales = new List<VenteDto>();

            // Initialize total quantity and total amount
            int totalQuantity = 0;
            double totalAmount = 0;

            // Loop through each article and quantity
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

                    // Create and add an individual sale DTO to the list
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

                    // Update total quantity and total amount
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

            // Create and return a summary DTO containing individual sales and totals
            var summaryDto = new VenteSummaryDto
            {
                IndividualSales = individualSales,
                TotalQuantity = totalQuantity,
                TotalAmount = totalAmount
            };

            return summaryDto;
        }

        // get the ventes
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
                articleVendue = vente.articleVendue.Libelle, // Include the Libelle of the Article
                QuantityVendue = vente.QuantityVendue,
                prixTotal = vente.PrixTotal(vente.articleVendue.Price) // Assuming you have a CalculatePrixTotal method
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
                    articleVendue = vente.articleVendue.Libelle, // Include the Libelle of the Article
                    QuantityVendue = vente.QuantityVendue,
                    prixTotal = vente.PrixTotal(vente.articleVendue.Price) // Assuming you have a CalculatePrixTotal method
                };
                return venteDto;
            }).ToList();
            return venteDtos;
        }

        // effectuer la vente
        public VenteDto AddVente(int clientId, int articleId, int quantite)
        {
            // Get the article
            var article = _dbContext.Articles.Find(articleId);
            // Get the client
            var client = _dbContext.Clients.Find(clientId);

            // Check the existence of client and article and the availability of quantity in stock
            if (article != null && client != null && quantite <= article.QuantityinStock)
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
                _dbContext.SaveChanges();

                // Fetch the client's first name and last name
                var clientFName = client.FName;
                var clientLName = client.LName;

                // Fetch the article's Libelle
                var articleLibelle = article.Libelle;

                // Create and return a VenteDto
                var venteDto = new VenteDto
                {
                    Id = vente.Id,
                    DateVente = vente.DateVente,
                    clientFName = clientFName,
                    clientLName = clientLName,
                    articleVendue = articleLibelle,
                    QuantityVendue = vente.QuantityVendue,
                    prixTotal = vente.PrixTotal(article.Price) // Assuming you have a CalculatePrixTotal method
                };

                return venteDto;
            }
            else
            {
                throw new Exception("Vente couldn't be done!");
            }
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
        

    }
}
