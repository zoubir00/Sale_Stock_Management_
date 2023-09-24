using AutoMapper;
using Sale_Management.Articles;
using Sale_Management.Clients;
using Sale_Management.Entities;
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
        CreateMap<Vente, VenteDto>();
        
        CreateMap<VenteDto, CreateUpdateVenteDto>();
    }
}
