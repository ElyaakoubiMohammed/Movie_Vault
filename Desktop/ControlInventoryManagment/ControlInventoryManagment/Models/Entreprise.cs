using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ControlInventoryManagment.Models
{
    public class Entreprise
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int CityId { get; set; }

        public List<Local> Locals { get; set; }

        public int LocalId { get; set; }

        [Required]
        public City City { get; set; }

        public List<Etage> Etages { get; set; }
    }
}
