using System;
using System.Collections.Generic;
using ControlInventoryManagment.Models;

namespace ControlInventoryManagment.DTOs.Entreprise
{
    public class EntrepriseCreateDTO
    {
        public required string Name { get; set; }
        public int CityId { get; set; }
        public required string Adresse { get; set; }
        public  required List<Local > Locals { get; set; }  //
        public required List< ControlInventoryManagment.Models.Etage> Etages { get; set; }  // 
    }
}
