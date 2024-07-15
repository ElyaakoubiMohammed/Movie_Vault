using System.ComponentModel.DataAnnotations;

namespace ControlInventoryManagment.Models
{
    public class City
    {
        public int Id { get; set; }
        
        public required string Name { get; set; }

        public required List<Local> Locals { get; set; } 
    }
}
