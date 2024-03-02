using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheatSheet.Domain.Entities
{
    public class Plant
    {
        [Key]
        [Column("p_id")]
        public Guid ID { get; set; }
        [Column("p_name")]
        public string Name { get; set; }
        [Column("p_gardens")]
        public List<Garden> Gardens { get; set; } = new();
        [Column("p_species")]
        public Species Species { get; set; }


        public Plant() { }


        public Plant(string name, List<Garden> gardens, Species species)
        {
            Name = name;
            Gardens = gardens;
            Species = species;
        }
    }

    public enum Species
    {
        Ginkgo, Mosses, Flowering_Plants, Ferns
    }
}
