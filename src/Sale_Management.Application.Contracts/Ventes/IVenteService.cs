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
        void AddVente(int client, int article, int quantite);
        
    }
}
