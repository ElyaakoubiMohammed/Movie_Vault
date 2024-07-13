using System;
using System.Collections.Generic;
using ControlInventoryManagment.Models;

namespace ControlInventoryManagment.DTOs.Entreprise
{
    public class EntrepriseReadDTO
    {
        public int CityId { get; set; }
        public required string Name { get; set; }
        public required List<Local> Locals { get; set; }
        public int IdLocal { get; set; }
        public required ControlInventoryManagment.Models.City City { get; set; }
        public required List<ControlInventoryManagment.Models.Etage> Etages { get; set; }
        public required string Adresse { get; set; }
    }
}
