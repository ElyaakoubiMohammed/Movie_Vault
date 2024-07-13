
using ControlInventoryManagment.DTOs.Etage;
using ControlInventoryManagment.DTOs.Product;

namespace ControlInventoryManagment.DTOs.Stockage;

    public class StockageReadDTO
    {
        public Guid Id { get; set; }
        public required List<ProductReadDTO> Products { get; set; }
        public int IdEmplacement { get; set; }
        public required EtageReadDTO Etage { get; set; }
    }

   

