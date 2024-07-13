namespace ControlInventoryManagment.DTOs.Stockage;

    public class StockageCreateDTO
    {
        public required List<int> ProductIds { get; set; }
        public int IdEmplacement { get; set; }
        public int EtageId { get; set; }
    }

