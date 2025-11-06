using Microsoft.EntityFrameworkCore;
using Taller.Backend.Repositories.Implementations;
using System.Text.Json.Serialization;
using Taller.Backend.Data;
using Taller.Backend.Repositories.Interfaces;
using Taller.Backend.UnitOfWork.Implementations;
using Taller.Backend.UnitOfWork.Interfaces;
using Taller.Backend.UnitsOfWork.Implementations;
using Taller.Backend.UnitsOfWork.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer("name=localConnection"));

builder.Services.AddTransient<SeedDb>();
builder.Services.AddScoped<ICountriesRepository, CountriesRepository>();
builder.Services.AddScoped<IStatesRepository, StatesRepository>();
builder.Services.AddScoped<ICitiesRepository, CitiesRepository>();

builder.Services.AddScoped<ICitiesUnitOfWork, CitiesUnitOfWork>();
builder.Services.AddScoped<ICountriesUnitOfWork, CountriesUnitOfWork>();
builder.Services.AddScoped<IStatesUnitOfWork, StatesUnitOfWork>();


builder.Services.AddScoped(typeof(IGenericUnitOfWork<>), typeof(GenericUnitOfWork<>));
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));


var app = builder.Build();
SeedData(app);

void SeedData(WebApplication app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory!.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<SeedDb>();
        service!.SeedDbAsync().Wait();
    }

}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
