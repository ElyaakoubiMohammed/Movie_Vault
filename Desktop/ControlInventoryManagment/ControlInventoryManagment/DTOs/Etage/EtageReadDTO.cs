using System;
using System.Collections.Generic;
using ControlInventoryManagment.Models;

namespace ControlInventoryManagment.DTOs.Etage
{
    public class EtageReadDTO
    {
        public int EtageId { get; set; }
        public required string TypeEtage { get; set; }
        public int LocalId { get; set; }
        public required Local Local { get; set; }
        public required List<Stockage> Stockages { get; set; }
        public int TypeId { get; set; }
    }
}
