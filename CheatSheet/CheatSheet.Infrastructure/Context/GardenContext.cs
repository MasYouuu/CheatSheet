using Bogus;
using CheatSheet.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CheatSheet.Infrastructure.Context
{
    public class GardenContext : DbContext
    {
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Garden> Gardens { get; set; }
        public DbSet<Tree> Trees { get; set; }
        public DbSet<Flower> Flowers { get; set; }


        public GardenContext(DbContextOptions opt) : base(new DbContextOptionsBuilder<GardenContext>().UseSqlite("Data Source=Garden.db").Options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Garden>() //Foreing Key Constraint
            .HasMany(g => g.Plants) //Ein Garden hat mehrere Plants
            .WithMany(p => p.Gardens); //Eine Plant hat mehrere Gardens


            //Es soll für jedes Entity in einem anderen Entity ein AutoInclude gemacht werden
            //Garden.cs:
            //Owner Owner { get; set; } --> Owner ist ein Entity in einem anderen Entity und soll deswegen AutoIncludes werden
            modelBuilder.Entity<Plant>().Navigation(p => p.Gardens).AutoInclude(); //Gardens werden AutoIncluded, when der Plant table geloaded wird
            //modelBuilder.Entity<Garden>().Navigation(g => g.Plants).AutoInclude(); //Plants werden AutoIncluded, when der Garden table geloaded wird
            modelBuilder.Entity<Garden>().Navigation(g => g.Owner).AutoInclude(); //Owner werden AutoIncluded, when der Garden table geloaded wird
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }


        public async Task SeedAsync()
        {
            //So machst du ein faker für ein Entity, das eine 1 : 1 Beziehung hat
            var fakerOwners = new Faker<Owner>().CustomInstantiator(f =>
            {
                return new Owner()
                {
                    Firstname = f.Person.FirstName,
                    Lastname = f.Person.LastName,
                };
            }).Generate(20);


            //Die Gardens List wird nicht instanziert, die wird dann wenn man eine Plant in Garden added, in Garden geupdated
            var fakerTrees = new Faker<Tree>().CustomInstantiator(f =>
            {
                return new Tree()
                {
                    Name = f.Commerce.ProductName(),
                    Species = f.PickRandom<Species>(),
                    BarkType = f.Commerce.ProductName(),
                    LeafType = f.Commerce.ProductName(),
                };
            }).Generate(20);


            var fakerFlower = new Faker<Flower>().CustomInstantiator(f =>
            {
                return new Flower()
                {
                    Name = f.Commerce.ProductName(),
                    Species = f.PickRandom<Species>(),
                    BloomTime = f.Date.Month().ToString(),
                    PetalColor = f.Commerce.ProductName(),
                };
            }).Generate(20);


            //Wenn du willst, dass ein Owner nur ein Garden haben kann musst du das in einem Dictionary speichern
            var ownerGardenMap = new Dictionary<Owner, Garden>();
            var fakerGardens = new Faker<Garden>().CustomInstantiator(f =>
            {
                var owner = f.PickRandom(fakerOwners);
                do
                {
                    owner = f.PickRandom(fakerOwners);
                }
                while (ownerGardenMap.ContainsKey(owner)); //Schaut ob der Owner schon ein Garden hat, wenn ja sucht er einen neuen Owner

                var garden = new Garden()
                {
                    Location = f.Address.StreetName().ToString(),
                    Owner = owner
                };

                for(int i = 0; i < 5; i++) //Fügt 5 Trees und 5 Flowers in die Plants List hinzu und updated die Gardens List der Plants
                {
                    garden.AddPlantToGarden(f.PickRandom(fakerFlower));
                    garden.AddPlantToGarden(f.PickRandom(fakerTrees));
                }
                
                ownerGardenMap.Add(owner, garden); //Fügt den Owner und Garden zu der List hinzu
                return garden;
            }).Generate(20);


            Owners.AddRange(fakerOwners);
            Gardens.AddRange(fakerGardens);
            Trees.AddRange(fakerTrees);
            Flowers.AddRange(fakerFlower);
            await SaveChangesAsync();
        }
    }
}
