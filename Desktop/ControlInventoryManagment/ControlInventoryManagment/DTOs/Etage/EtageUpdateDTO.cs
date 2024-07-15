using System;
using System.Collections.Generic;
using ControlInventoryManagment.Models;

namespace ControlInventoryManagment.DTOs.Etage
{
    public class EtageUpdateDTO
    {
        public required string TypeEtage { get; set; }
        public int LocalId { get; set; }
        public required List<ControlInventoryManagment.Models.Stockage> Stockages { get; set; } // Assuming you are passing the IDs of the PetitEms
        public int TypeId { get; set; }
    }
}
