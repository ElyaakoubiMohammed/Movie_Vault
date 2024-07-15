using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ControlInventoryManagment.Models
{
    public class Entreprise
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public int CityId { get; set; }

        public required List<Local> Locals { get; set; }

        public int LocalId { get; set; }

        public required City City { get; set; }

        public required List<Etage> Etages { get; set; }
    }
}
