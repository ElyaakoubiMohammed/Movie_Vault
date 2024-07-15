using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ControlInventoryManagment.Models
{
    public class Etage
    {
        public int Id { get; set; }

        public required string TypeEtage { get; set; }

        public int LocalId { get; set; }

        public required Local Local { get; set; }

        public required List<Stockage> Stockages { get; set; }

        public int TypeId { get; set; }
    }
}
