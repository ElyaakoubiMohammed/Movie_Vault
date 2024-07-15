using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ControlInventoryManagment.Models
{
    public class Product
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public int CategorieId { get; set; }

        public int TagId { get; set; }

        public int QrId { get; set; }

        public required string SerialNumber { get; set; }

        public required List<Operation> Operations { get; set; }

        public required Categorie Categorie { get; set; }
    }
}
