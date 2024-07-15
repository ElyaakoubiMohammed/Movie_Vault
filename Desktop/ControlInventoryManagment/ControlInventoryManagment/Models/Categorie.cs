using System.ComponentModel.DataAnnotations;

namespace ControlInventoryManagment.Models
{
    public class Categorie
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public required string Description { get; set; }

        public required IEnumerable<Product> Products { get; set; }
    }
}
