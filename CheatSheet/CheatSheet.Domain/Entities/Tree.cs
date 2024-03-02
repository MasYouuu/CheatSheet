using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheatSheet.Domain.Entities
{
    [Table("t_tree")]
    public class Tree : Plant
    {
        [Column("t_barktype")]
        public string BarkType { get; set; }
        [Column("t_leaftype")]
        public string LeafType { get; set; }


        public Tree() { }


        public  Tree(string  name, List<Garden> gardens, Species species, string barkType, string leafType) : base(name, gardens,  species) 
        {
            Name = name;
            Gardens = gardens;
            Species = species;
            BarkType = barkType;
            LeafType = leafType;
        }

        public Tree(string name, List<Garden> gardens, Species species, Guid id, string barkType, string leafType) : base(name, gardens, species)
        {
            Name = name;
            Gardens = gardens;
            Species = species;
            ID = id;
            BarkType = barkType;
            LeafType = leafType;
        }
    }
}
