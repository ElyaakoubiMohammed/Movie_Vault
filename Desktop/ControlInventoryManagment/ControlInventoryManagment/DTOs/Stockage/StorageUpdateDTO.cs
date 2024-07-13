namespace ControlInventoryManagment.DTOs.Stockage;

    public class StockageUpdateDTO
    {
        public required List<Guid> ProductIds { get; set; }
        public int IdEmplacement { get; set; }
        public Guid EtageId { get; set; }
    }

