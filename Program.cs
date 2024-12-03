using _8bits_app_api.Interfaces;
using _8bits_app_api.Models;
using _8bits_app_api.Repositories;
using _8bits_app_api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<mydbcontext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

#region Dependency Injection ile Servisleri Tanımlama
builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
builder.Services.AddScoped<IRecipeReadingService, RecipeReadingService>();

builder.Services.AddScoped<IIngredientReadingService, IngredientReadingService>();
builder.Services.AddScoped<IIngredientRepository, IngredientRepository>();

builder.Services.AddScoped<IRecipeImagesReadingService, RecipeImageReadingService>();
builder.Services.AddScoped<IRecipeImagesRepository, RecipeImagesRepository>();

builder.Services.AddScoped<IRecipeRateReadingService, RecipeRateReadingService>();
builder.Services.AddScoped<IRecipeRateRepository, RecipeRateRepository>();
#endregion


builder.Services.AddControllers();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();
app.UseHttpsRedirection();
app.Run();

