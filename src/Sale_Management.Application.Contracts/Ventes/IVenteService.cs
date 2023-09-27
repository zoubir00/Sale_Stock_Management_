using Sale_Management.Articles;
using Sale_Management.Clients;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sale_Management.Ventes
{
    public interface IVenteService
    {
        VenteDto AddVente(int clientId, int articleId, int quantite);
        VenteSummaryDto CreateVente(int clientId, List<int> articleIds, List<int> quantities);
        Task<List<VenteDto>> GetAllVentesAsync();
        Task<List<VenteDto>> GetVentesByClientNameAsync(string clientFName, string clientLName);
        Task deleteVente(int id);
        List<VenteSummaryDto> GetVenteSummaries();
    }
}
