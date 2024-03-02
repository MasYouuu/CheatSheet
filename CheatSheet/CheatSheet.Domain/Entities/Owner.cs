using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CheatSheet.Domain.Entities
{
    [Table("o_owner")]
    public class Owner
    {
        [Key]
        [Column("o_id")]
        public Guid  ID { get; set; }
        [Column("o_firstname")]
        public string Firstname { get; set; }
        [Column("o_lastname")]
        public  string Lastname { get; set; }


        public Owner() { }


        public Owner (string firstName, string lastName)
        {
            Firstname = firstName;
            Lastname = lastName;
        }

        public Owner(Guid id, string firstName, string lastName)
        {
            ID = id;
            Firstname = firstName;
            Lastname = lastName;
        }
    }
}
