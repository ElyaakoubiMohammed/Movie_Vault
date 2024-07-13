using AutoMapper;
using ControlInventoryManagment.Models;
using ControlInventoryManagment.DTOs.Categorie;
using ControlInventoryManagment.DTOs.City;
using ControlInventoryManagment.DTOs.Entreprise;
using ControlInventoryManagment.DTOs.Etage;
using ControlInventoryManagment.DTOs.Local;
using ControlInventoryManagment.DTOs.Operation;
using ControlInventoryManagment.DTOs.Product;
using ControlInventoryManagment.DTOs.Stockage;
using ControlInventoryManagment.DTOs.TypeEtage;

namespace ControlInventoryManagment.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Categorie, CategorieCreateDTO>().ReverseMap();
            CreateMap<Categorie, CategorieReadDTO>().ReverseMap();
            CreateMap<Categorie, CategorieUpdateDTO>().ReverseMap();

            CreateMap<City, CityCreateDTO>().ReverseMap();
            CreateMap<City, CityUpdateDTO>().ReverseMap();
            CreateMap<City, CityReadDTO>().ReverseMap();
            
            CreateMap<Entreprise, EntrepriseCreateDTO>().ReverseMap();
            CreateMap<Entreprise, EntrepriseUpdateDTO>().ReverseMap();
            CreateMap<Entreprise, EntrepriseReadDTO>().ReverseMap();

            CreateMap<Etage, EtageCreateDTO>().ReverseMap();
            CreateMap<Etage, EtageUpdateDTO>().ReverseMap();
            CreateMap<Etage, EtageReadDTO>().ReverseMap();

            CreateMap<Local, LocalCreateDTO>().ReverseMap();
            CreateMap<Local, LocalUpdateDTO>().ReverseMap();
            CreateMap<Local, LocalReadDTO>().ReverseMap();

            CreateMap<Operation, OperationCreateDTO>().ReverseMap();
            CreateMap<Operation, OperationUpdateDTO>().ReverseMap();
            CreateMap<Operation, OperationReadDTO>().ReverseMap();
            
            CreateMap<Product, ProductCreateDTO>().ReverseMap();
            CreateMap<Product, ProductUpdateDTO>().ReverseMap();
            CreateMap<Product, ProductReadDTO>().ReverseMap();

            CreateMap<Stockage, StockageCreateDTO>().ReverseMap();
            CreateMap<Local, StockageUpdateDTO>().ReverseMap();
            CreateMap<Local, StockageReadDTO>().ReverseMap();

            CreateMap<TypeEtage, TypeEtageCreateDTO>().ReverseMap();
            CreateMap<TypeEtage, TypeEtageUpdateDTO>().ReverseMap();
            CreateMap<TypeEtage, TypeEtageReadDTO>().ReverseMap();



        }
    }
}
