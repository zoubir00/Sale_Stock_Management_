using AutoMapper;
using Sale_Management.Articles;
using Sale_Management.Clients;
using Sale_Management.Entities;

using Sale_Management.VenteLines;
using Sale_Management.Ventes;

namespace Sale_Management;

public class Sale_ManagementApplicationAutoMapperProfile : Profile
{
    public Sale_ManagementApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<Client, ClientDto>();
        CreateMap<Article, ArticleDto>();
        CreateMap<Article, CreateArticleDto>();
        CreateMap<Vente, VenteDto>();
        CreateMap<VenteDto, Vente>(); // Map from VenteDto to Vente
        // Map from Vente to VenteDto
        CreateMap<VenteLine, VenteLinesDto>();
        CreateMap<VenteLinesDto, VenteLine>(); // Map from VenteLinesDto to VenteLines (if needed)
       



    }
}
