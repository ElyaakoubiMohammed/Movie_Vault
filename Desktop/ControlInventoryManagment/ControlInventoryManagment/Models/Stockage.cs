using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ControlInventoryManagment.Models
{
    public class Stockage
    {
        public int Id { get; set; }
        public required List<Product> Products { get; set; }
        public int EtagetId { get; set; }
        public required Etage Etage { get; set; }
    }
}
