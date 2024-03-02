using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheatSheet.Domain.Entities
{
    [Table("f_flowers")]
    public class Flower : Plant
    {
        [Column("f_petalcolor")]
        public string PetalColor { get; set; }
        [Column("f_bloomtime")]
        public string BloomTime { get; set; }


        public Flower() { }


        public Flower(string name, List<Garden> gardens, Species species, string petalColor, string bloomTime) : base(name, gardens, species)
        {
            Name = name;
            Gardens = gardens;
            Species = species;
            PetalColor = petalColor;
            BloomTime = bloomTime;
        }

        public Flower(string name, List<Garden> gardens, Species species, Guid id, string petalColor, string bloomTime) : base(name, gardens, species)
        {
            Name = name;
            Gardens = gardens;
            Species = species;
            ID = id;
            PetalColor = petalColor;
            BloomTime = bloomTime;
        }
    }
}
