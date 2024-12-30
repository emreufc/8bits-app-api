using System.Text;
using _8bits_app_api.Interfaces;
using _8bits_app_api.Models;
using _8bits_app_api.Repositories;
using _8bits_app_api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer abc123\""
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddDbContext<mydbcontext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

#region Dependency Injection ile Servisleri Tanımlama
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        var jwtSettings = builder.Configuration.GetSection("JwtSettings");
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]))
        };
    });

// DI for services and repositories
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IRecipeStepRepository, RecipeStepRepository>();
builder.Services.AddScoped<IRecipeStepReadingService, RecipeStepReadingService>();

builder.Services.AddScoped<IAllergenRepository, AllergenRepository>();
builder.Services.AddScoped<IAllergenReadingService, AllergenReadingService>();

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IDietTypeRepository, DietTypeRepository>();
builder.Services.AddScoped<IDietTypeReadingService, DietTypeReadingService>();

builder.Services.AddScoped<IPasswordService, PasswordService>();

builder.Services.AddScoped<IRecipeIngredientRepository, RecipeIngredientRepository>();
builder.Services.AddScoped<IRecipeIngredientReadingService, RecipeIngredientReadingService>();

builder.Services.AddScoped<IIngredientReadingService, IngredientReadingService>();
builder.Services.AddScoped<IIngredientRepository, IngredientRepository>();

builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
builder.Services.AddScoped<IRecipeReadingService, RecipeReadingService>();

builder.Services.AddScoped<IUserRefreshTokenService, UserRefreshTokenService>();
builder.Services.AddScoped<IUserRefreshTokenRepository, UserRefreshTokenRepository>();

builder.Services.AddScoped<IShoppingListRepository, ShoppingListRepository>();
builder.Services.AddScoped<IShoppingListService, ShoppingListService>();

builder.Services.AddScoped<IFavoriteRecipeRepository, FavoriteRecipeRepository>();
builder.Services.AddScoped<IFavoriteRecipeService, FavoriteRecipeService>();

builder.Services.AddScoped<IUserInventoryRepository, UserInventoryRepository>();
builder.Services.AddScoped<IUserInventoryService, UserInvertoryService>();

builder.Services.AddScoped<IDietPreferenceRepository, DietPreferenceRepository>();
builder.Services.AddScoped<IDietPreferenceReadingService, DietPreferenceReadingService>();
#endregion

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
builder.Services.AddAuthorization();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowAll");
app.Run();

