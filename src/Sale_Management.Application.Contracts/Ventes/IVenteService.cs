using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sale_Management.Ventes
{
    public interface IVenteService
    {
        Task<VenteDto> AddVente(int clientId, CreateUpdateVenteDto venteDto);
    }
}
