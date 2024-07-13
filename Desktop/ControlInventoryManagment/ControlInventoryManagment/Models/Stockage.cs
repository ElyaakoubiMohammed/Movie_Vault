using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ControlInventoryManagment.Models
{
    public class Stockage
    {
        public int Id { get; set; }

        [Required]
        public List<Product> Products { get; set; }

        public int EtagetId { get; set; }

        [Required]
        public Etage Etage { get; set; }
    }
}
