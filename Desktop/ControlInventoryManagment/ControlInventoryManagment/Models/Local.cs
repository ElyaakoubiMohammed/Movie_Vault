using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ControlInventoryManagment.Models
{
    public class Local
    {
        public int Id { get; set; }

        public int CityId { get; set; }

        public required City City { get; set; }

        public required List<Etage> Etages { get; set; }

        public required string Address { get; set; }
    }
}
