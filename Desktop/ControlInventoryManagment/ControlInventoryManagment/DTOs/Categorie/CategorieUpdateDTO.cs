namespace ControlInventoryManagment.DTOs.Categorie
{
    public class CategorieUpdateDTO
    {
        public int Id { get; set; }
        public required string NameCategorie { get; set; }
        public required string Description { get; set; }
    }
}
