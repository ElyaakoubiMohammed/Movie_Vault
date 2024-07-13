using ControlInventoryManagment.DTOs;
using ControlInventoryManagment.DTOs.City;
using ControlInventoryManagment.DTOs.Etage;

namespace ControlInventoryManagment.DTOs.Local;

    public class LocalReadDTO
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public required CityReadDTO City { get; set; }
        public required List<EtageReadDTO> Etages { get; set; }
        public required string Address { get; set; }
    }

   
    

