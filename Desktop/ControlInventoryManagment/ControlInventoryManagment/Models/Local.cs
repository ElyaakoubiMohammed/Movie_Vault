using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ControlInventoryManagment.Models
{
    public class Local
    {
        public int Id { get; set; }

        public int CityId { get; set; }

        [Required]
        public City City { get; set; }

        [Required]
        public List<Etage> Etages { get; set; }

        [Required]
        public string Address { get; set; }
    }
}
