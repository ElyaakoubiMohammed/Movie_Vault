using System;
using System.Collections.Generic;
using ControlInventoryManagment.Models;

namespace ControlInventoryManagment.DTOs.Etage
{
    public class EtageCreateDTO
    {
        public  required string TypeEtage { get; set; }
        public int LocalId { get; set; }
        public required List<Stockage> Stockages { get; set; } // Assuming you are passing the IDs of the PetitEms
        public int IdType { get; set; }
    }
}
