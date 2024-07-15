using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ControlInventoryManagment.Models
{
    public class Operation
    {
        public int Id { get; set; }

        public required DateTime DateDebut { get; set; }

        public required DateTime DateFin { get; set; }

        public required List<Product> Products { get; set; }
    }
}
