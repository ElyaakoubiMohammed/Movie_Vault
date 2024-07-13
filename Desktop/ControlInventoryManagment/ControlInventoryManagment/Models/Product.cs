using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ControlInventoryManagment.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int CategorieId { get; set; }

        public int TagId { get; set; }

        public int QrId { get; set; }

        [Required]
        public string SerialNum { get; set; }

        [Required]
        public List<Operation> Operations { get; set; }

        [Required]
        public Categorie Categorie { get; set; }
    }
}
