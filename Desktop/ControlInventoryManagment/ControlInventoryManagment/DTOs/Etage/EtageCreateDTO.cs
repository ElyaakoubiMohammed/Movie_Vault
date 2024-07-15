using System;
using System.Collections.Generic;
using ControlInventoryManagment.DTOs.Stockage;
using ControlInventoryManagment.Models;

namespace ControlInventoryManagment.DTOs.Etage
{
    public class EtageCreateDTO
    {
        public  required string TypeEtage { get; set; }
        public int LocalId { get; set; }
        public required List<StockageCreateDTO> Stockages { get; set; } 
        public int IdType { get; set; }
    }
}
