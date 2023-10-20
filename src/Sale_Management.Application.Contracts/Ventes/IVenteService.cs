using Sale_Management.Articles;
using Sale_Management.Clients;
using Sale_Management.VenteLines;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sale_Management.Ventes
{
    public interface IVenteService
    {
        VenteDto CreateVente(DateTime dateVente, int clientId, List<VenteLinesDto> venteLines);
        List<GetVenteDto> GetAllVentes();
        VenteDto GetVenteDetails(Guid codeVente);
        VenteDto EditVente(Guid venteCode, DateTime newDateVente, int newClientId, List<VenteLinesDto> updatedVenteLines);
        void DeleteVente(Guid venteCode);
        //delte vente line
        void DeleteVenteLine(Guid codeVente, int venteLineId);
        void AddVenteLineToVente(Guid venteCode, VenteLinesDto newVenteLineDto);

        //VenteDto AddVente(int clientId, int articleId, int quantite);
        //VenteSummaryDto CreateVente(int clientId, List<int> articleIds, List<int> quantities);
        //Task<List<VenteDto>> GetAllVentesAsync();
        //Task<List<VenteDto>> GetVentesByClientNameAsync(string clientFName, string clientLName);
        //Task deleteVente(int id);
        //List<VenteSummaryDto> GetVenteSummaries();
    }
}
