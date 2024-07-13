using System.ComponentModel.DataAnnotations;

namespace ControlInventoryManagment.Models
{
    public class TypeEtage
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
