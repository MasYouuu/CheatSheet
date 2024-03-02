using CheatSheet.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

var option = new DbContextOptionsBuilder<GardenContext>()
    .UseSqlite("Data Source=Garden.db")
    .EnableSensitiveDataLogging()
    .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information)
.Options
;

using (var db = new GardenContext(option))
{
    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();
    await db.SeedAsync();
}