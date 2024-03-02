using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CheatSheet.Domain.Entities
{
    [Table("g_garden")]
    public class Garden
    {
        [Key]
        [Column("g_id")]
        public Guid ID { get; set; }
        [Column("g_location")]
        public  string  Location { get; set; }
        [Column("g_plants")]
        public readonly List<Plant> Plants = new();
        [Column("g_owner")]
        public Owner Owner { get; set; }


        public Garden() { }

        public Garden(string location, List<Plant> plants, Owner owner)
        {
            Location = location;
            Plants = plants;
            Owner = owner;
        }


        public Garden(Guid id, string location, List<Plant> plants, Owner owner)
        {
            ID = id;
            Location = location;
            Plants = plants;
            Owner = owner;
        }


        public void AddPlantToGarden(Plant plant)
        {
            plant.Gardens.Add(this);
            Plants.Add(plant);
        }

        public void RemovePlant(Plant plant)
        {
            if(Plants.Count(p => p == plant) == 1)
                plant.Gardens.Remove(this);

            Plants.Remove(plant);
        }
    }
}
