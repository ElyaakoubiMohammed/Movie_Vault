namespace ControlInventoryManagment.DTOs.Categorie
{
    public class CategorieCreateDTO
    {
        public required int Id { get; set; }
        public required string NameCategorie { get; set; }
        public required string Description { get; set; }
    }
}
