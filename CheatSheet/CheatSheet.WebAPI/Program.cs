using CheatSheet.Application.IRepos;
using CheatSheet.Application.Mapper;
using CheatSheet.Infrastructure.Context;
using CheatSheet.Infrastructure.Repos;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddScoped<IGardenRepo, GardenRepo>();
builder.Services.AddScoped<IFlowerRepo, FlowerRepo>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connection = "Username=postgres;Password=postgres;Server=localhost;Port=5432;Database=Garden";

builder.Services.AddDbContext<GardenContext>(c => {
    c.UseSqlite(connection)
                .EnableSensitiveDataLogging()
                .LogTo(Console.WriteLine, LogLevel.Information);
}
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using (var scope = app.Services.CreateScope())
    {
        using (var db = scope.ServiceProvider.GetService<GardenContext>())
        {
            if (db is null)
                throw new Exception("No DB!");
            //New DB!!!
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            await db.SeedAsync();
        }
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();