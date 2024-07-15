using AutoMapper;
using ControlInventoryManagment.Models;
using ControlInventoryManagment.DTOs.Categorie;
using ControlInventoryManagment.DTOs.City;
using ControlInventoryManagment.DTOs;
using ControlInventoryManagment.DTOs.Etage;
using ControlInventoryManagment.DTOs.Local;
using ControlInventoryManagment.DTOs.Product;
using ControlInventoryManagment.DTOs.Stockage;
using ControlInventoryManagment.DTOs.TypeEtage;

namespace ControlInventoryManagment.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Categorie, CategorieCreateDTO>();
            CreateMap<Categorie, CategorieReadDTO>();
            CreateMap<Categorie, CategorieUpdateDTO>();

            CreateMap<City, CityCreateDTO>();
            CreateMap<City, CityUpdateDTO>();
            CreateMap<City, CityReadDTO>();

            CreateMap<Entreprise, EntrepriseCreateDTO>();
            CreateMap<Entreprise, EntrepriseUpdateDTO>();
            CreateMap<Entreprise, EntrepriseReadDTO>();

            CreateMap<Etage, EtageCreateDTO>();
            CreateMap<Etage, EtageUpdateDTO>();
            CreateMap<Etage, EtageReadDTO>();

            CreateMap<Local, LocalCreateDTO>();
            CreateMap<Local, LocalUpdateDTO>();
            CreateMap<Local, LocalReadDTO>();

            CreateMap<Operation, OperationCreateDTO>();
            CreateMap<Operation, OperationUpdateDTO>();
            CreateMap<Operation, OperationReadDTO>();

            CreateMap<Product, ProductCreateDTO>();
            CreateMap<Product, ProductUpdateDTO>();
            CreateMap<Product, ProductReadDTO>();

            CreateMap<Stockage, StockageCreateDTO>();
            CreateMap<Stockage, StockageUpdateDTO>();
            CreateMap<Stockage, StockageReadDTO>();

            CreateMap<TypeEtage, TypeEtageCreateDTO>();
            CreateMap<TypeEtage, TypeEtageUpdateDTO>();
            CreateMap<TypeEtage, TypeEtageReadDTO>();
        }
    }
}
