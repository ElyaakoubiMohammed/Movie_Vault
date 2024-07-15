using System;
using System.Collections.Generic;
using ControlInventoryManagment.Models;

namespace ControlInventoryManagment.DTOs;

    public class EntrepriseReadDTO
    {
        public int Id { get; set;}
        public int CityId { get; set; }
        public required string Name { get; set; }
        public required List<ControlInventoryManagment.Models.Local> Locals { get; set; }
        public int IdLocal { get; set; }
        public required ControlInventoryManagment.Models.City City { get; set; }
        public required List<ControlInventoryManagment.Models.Etage> Etages { get; set; }
        public required string Adresse { get; set; }
    }

