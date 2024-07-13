using System.ComponentModel.DataAnnotations;

namespace ControlInventoryManagment.Models
{
    public class Categorie
    {
        public int Id { get; set; }

        [Required]
        public string NameCategorie { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
