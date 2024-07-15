namespace ControlInventoryManagment.DTOs.Local;
    public class LocalUpdateDTO
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public required List<ControlInventoryManagment.Models.Etage> Etage { get; set; }
        public required string Address { get; set; }
    }

