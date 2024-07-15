using ControlInventoryManagment.DTOs.Etage;

namespace ControlInventoryManagment.DTOs.Local;
    public class LocalCreateDTO
    {
        public int CityId { get; set; }
        public required List<EtageCreateDTO> Etage { get; set; }
        public required string Address { get; set; }
    }

