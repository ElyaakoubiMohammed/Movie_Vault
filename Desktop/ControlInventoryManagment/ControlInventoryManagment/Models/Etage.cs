using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ControlInventoryManagment.Models
{
    public class Etage
    {
        public int Id { get; set; }

        [Required]
        public string TypeEtage { get; set; }

        public int LocalId { get; set; }

        [Required]
        public Local Local { get; set; }

        [Required]
        public List<Stockage> Stockages { get; set; }

        public int TypeId { get; set; } // Assuming this is a required property
    }
}
