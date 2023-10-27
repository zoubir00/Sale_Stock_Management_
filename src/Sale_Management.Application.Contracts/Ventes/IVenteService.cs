using Sale_Management.Articles;
using Sale_Management.Clients;
using Sale_Management.VenteLines;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Sale_Management.Ventes
{
    public interface IVenteService
    {
        //VenteDto CreateVente(DateTime dateVente, int clientId, List<VenteLinesDto> venteLines);
        Task<PagedResultDto<GetVenteDto>> GetAllVentes();
        Task<VenteDto> GetVenteDetails(Guid codeVente);
        Task<VenteDto> CreateVente(DateTime dateVente, Guid clientId, List<VenteLinesDto> venteLines);
        Task<VenteDto> UpdateVenteAsync(Guid venteCode, DateTime newDateVente, Guid newClientId, List<VenteLinesDto> updatedVenteLines);
        //VenteDto EditVente(Guid venteCode, DateTime newDateVente, Guid newClientId, List<VenteLinesDto> updatedVenteLines);
        Task DeleteVente(Guid venteCode);
        ////delte vente line
        Task DeleteVenteLine(Guid codeVente, Guid venteLineId);
        //void AddVenteLineToVente(Guid venteCode, VenteLinesDto newVenteLineDto);

        //VenteDto AddVente(int clientId, int articleId, int quantite);
        //VenteSummaryDto CreateVente(int clientId, List<int> articleIds, List<int> quantities);
        //Task<List<VenteDto>> GetAllVentesAsync();
        //Task<List<VenteDto>> GetVentesByClientNameAsync(string clientFName, string clientLName);
        //Task deleteVente(int id);
        //List<VenteSummaryDto> GetVenteSummaries();
    }
}
