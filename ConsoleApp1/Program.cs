
using ConsoleApp1.Data;
using ConsoleApp1.Repositories.Implementations;
using ConsoleApp1.Repositories.Interfaces;
using ConsoleApp1.Services.Implementations;
using ConsoleApp1.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ConsoleApp1Context>(options =>
    options.UseSqlite(
        builder.Configuration["ConnectionStrings:ProductsDBConnectionString"]
    ));

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Redirige automáticamente a Swagger
app.MapGet("/", () => Results.Redirect("/swagger"));

app.MapControllers();

app.Run();