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

        public VenteService(Sale_ManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // get client vente
        public List<Vente> GetClientVentes(string ClientfName, string ClientlName)
        {
            var client = _dbContext.Clients.FirstOrDefault(n => n.FName == ClientfName && n.LName == ClientlName);
            var AllVentes = _dbContext.Ventes.ToList();
            if (client != null)
            {
                var _ClientVente = _dbContext.Ventes.Where(v => v.clientId == client.Id).ToList();
                return _ClientVente;
            }
            return AllVentes;
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
