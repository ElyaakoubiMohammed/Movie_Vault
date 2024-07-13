using System.ComponentModel.DataAnnotations;

namespace ControlInventoryManagment.Models
{
    public class City
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; } // 'required' should be 'Required' to match attribute usage

        public List<Local> Locals { get; set; } // Corrected 'locals' to 'Locals'
    }
}
