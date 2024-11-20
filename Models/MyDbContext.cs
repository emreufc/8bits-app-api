using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace _8bits_app_api.Models;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Allergy> Allergies { get; set; }

    public virtual DbSet<DietPreference> DietPreferences { get; set; }

    public virtual DbSet<DietType> DietTypes { get; set; }

    public virtual DbSet<FavoriteRecipe> FavoriteRecipes { get; set; }

    public virtual DbSet<Ingredient> Ingredients { get; set; }

    public virtual DbSet<IngredientImage> IngredientImages { get; set; }

    public virtual DbSet<Recipe> Recipes { get; set; }

    public virtual DbSet<RecipeImage> RecipeImages { get; set; }

    public virtual DbSet<RecipeIngredient> RecipeIngredients { get; set; }

    public virtual DbSet<RecipeRate> RecipeRates { get; set; }

    public virtual DbSet<RecipeStep> RecipeSteps { get; set; }

    public virtual DbSet<ShoppingList> ShoppingLists { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAllergy> UserAllergies { get; set; }

    public virtual DbSet<UserInventory> UserInventories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:sqlserver7roeurp45oi7c.database.windows.net,1433;Initial Catalog=8bits;Persist Security Info=False;User ID=8_bits_admin;Password=dirsen-qafjy8-poqfEd;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Allergy>(entity =>
        {
            entity.HasKey(e => e.AllerjiId);

            entity.ToTable("allergies");

            entity.Property(e => e.AllerjiId)
                .ValueGeneratedNever()
                .HasColumnName("allerji_id");
            entity.Property(e => e.AllerjiBilgisi)
                .HasMaxLength(50)
                .HasColumnName("allerji_bilgisi");
        });

        modelBuilder.Entity<DietPreference>(entity =>
        {
            entity.HasKey(e => e.DietPreferenceId).HasName("PK__diet_pre__379065036CE7E69F");

            entity.ToTable("diet_preferences");

            entity.Property(e => e.DietPreferenceId).HasColumnName("diet_preference_id");
            entity.Property(e => e.DietTypeId).HasColumnName("diet_type_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.DietType).WithMany(p => p.DietPreferences)
                .HasForeignKey(d => d.DietTypeId)
                .HasConstraintName("FK__diet_pref__diet___7B5B524B");
        });

        modelBuilder.Entity<DietType>(entity =>
        {
            entity.HasKey(e => e.DietTypeId).HasName("PK__diet_typ__B5E6540376F83958");

            entity.ToTable("diet_types");

            entity.Property(e => e.DietTypeId).HasColumnName("diet_type_id");
            entity.Property(e => e.DietTypeExplanation)
                .HasColumnType("text")
                .HasColumnName("diet_type_explanation");
            entity.Property(e => e.DietTypeName)
                .HasMaxLength(50)
                .HasColumnName("diet_type_name");
        });

        modelBuilder.Entity<FavoriteRecipe>(entity =>
        {
            entity.HasKey(e => e.FavId).HasName("PK__favorite__37AAF6FE57FC32CC");

            entity.ToTable("favorite_recipes");

            entity.Property(e => e.FavId).HasColumnName("fav_id");
            entity.Property(e => e.RecipeId).HasColumnName("recipe_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Recipe).WithMany(p => p.FavoriteRecipes)
                .HasForeignKey(d => d.RecipeId)
                .HasConstraintName("FK__favorite___recip__2B0A656D");

            entity.HasOne(d => d.User).WithMany(p => p.FavoriteRecipes)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__favorite___user___282DF8C2");
        });

        modelBuilder.Entity<Ingredient>(entity =>
        {
            entity.ToTable("ingredients");

            entity.Property(e => e.IngredientId)
                .ValueGeneratedNever()
                .HasColumnName("ingredient_id");
            entity.Property(e => e.DetailedAllergenInfoTr)
                .HasMaxLength(50)
                .HasColumnName("detailed_allergen_info_tr");
            entity.Property(e => e.IngredientName)
                .HasMaxLength(50)
                .HasColumnName("ingredient_name");
            entity.Property(e => e.PageUrl)
                .HasMaxLength(100)
                .HasColumnName("page_url");
        });

        modelBuilder.Entity<IngredientImage>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ingredient_images");

            entity.Property(e => e.IngredientId).HasColumnName("ingredient_id");
            entity.Property(e => e.IngredientName)
                .HasMaxLength(50)
                .HasColumnName("ingredient_name");
            entity.Property(e => e.IngredientNameEn)
                .HasMaxLength(50)
                .HasColumnName("ingredient_name_en");
            entity.Property(e => e.PageUrl)
                .HasMaxLength(100)
                .HasColumnName("page_url");
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.ToTable("recipes");

            entity.Property(e => e.RecipeId)
                .ValueGeneratedNever()
                .HasColumnName("recipe_id");
            entity.Property(e => e.Calorie)
                .HasMaxLength(50)
                .HasColumnName("calorie");
            entity.Property(e => e.Carbohydrates).HasColumnName("carbohydrates");
            entity.Property(e => e.Category)
                .HasMaxLength(50)
                .HasColumnName("category");
            entity.Property(e => e.CookTime)
                .HasMaxLength(50)
                .HasColumnName("cook_time");
            entity.Property(e => e.Fat).HasColumnName("fat");
            entity.Property(e => e.PreparationTime)
                .HasMaxLength(50)
                .HasColumnName("preparation_time");
            entity.Property(e => e.Protein).HasColumnName("protein");
            entity.Property(e => e.RecipeName)
                .HasMaxLength(100)
                .HasColumnName("recipe_name");
            entity.Property(e => e.Serving)
                .HasMaxLength(50)
                .HasColumnName("serving");
        });

        modelBuilder.Entity<RecipeImage>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("recipe_images");

            entity.Property(e => e.ImageLink)
                .HasMaxLength(150)
                .HasColumnName("image_link");
            entity.Property(e => e.RecipeId).HasColumnName("recipe_id");
            entity.Property(e => e.RecipeName)
                .HasMaxLength(100)
                .HasColumnName("recipe_name");

            entity.HasOne(d => d.Recipe).WithMany()
                .HasForeignKey(d => d.RecipeId)
                .HasConstraintName("FK__recipe_im__recip__236943A5");
        });

        modelBuilder.Entity<RecipeIngredient>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("recipe_ingredients");

            entity.Property(e => e.IngredientId).HasColumnName("ingredient_id");
            entity.Property(e => e.IngredientName)
                .HasMaxLength(100)
                .HasColumnName("ingredient_name");
            entity.Property(e => e.Quantity)
                .HasMaxLength(50)
                .HasColumnName("quantity");
            entity.Property(e => e.QuantityType)
                .HasMaxLength(50)
                .HasColumnName("quantity_type");
            entity.Property(e => e.RecipeId).HasColumnName("recipe_id");

            entity.HasOne(d => d.Ingredient).WithMany()
                .HasForeignKey(d => d.IngredientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__recipe_in__ingre__160F4887");

            entity.HasOne(d => d.Recipe).WithMany()
                .HasForeignKey(d => d.RecipeId)
                .HasConstraintName("FK__recipe_in__recip__1DB06A4F");
        });

        modelBuilder.Entity<RecipeRate>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("recipe_rates");

            entity.Property(e => e.RecipeId).HasColumnName("recipe_id");
            entity.Property(e => e.RecipeName)
                .HasMaxLength(50)
                .HasColumnName("recipe_name");
            entity.Property(e => e.RecipeRate1).HasColumnName("recipe_rate");

            entity.HasOne(d => d.Recipe).WithMany()
                .HasForeignKey(d => d.RecipeId)
                .HasConstraintName("FK__recipe_ra__recip__25518C17");
        });

        modelBuilder.Entity<RecipeStep>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("recipe_steps");

            entity.Property(e => e.RecipeId).HasColumnName("recipe_id");
            entity.Property(e => e.RecipeName)
                .HasMaxLength(100)
                .HasColumnName("recipe_name");
            entity.Property(e => e.Step)
                .HasMaxLength(800)
                .HasColumnName("step");
            entity.Property(e => e.StepNum)
                .HasMaxLength(30)
                .HasColumnName("step_num");

            entity.HasOne(d => d.Recipe).WithMany()
                .HasForeignKey(d => d.RecipeId)
                .HasConstraintName("FK__recipe_st__recip__1F98B2C1");
        });

        modelBuilder.Entity<ShoppingList>(entity =>
        {
            entity.HasKey(e => e.ShoppingListId).HasName("PK__shopping__0659AC3A320C8AF2");

            entity.ToTable("shopping_list");

            entity.Property(e => e.ShoppingListId).HasColumnName("shopping_list_id");
            entity.Property(e => e.IngredientId).HasColumnName("ingredient_id");
            entity.Property(e => e.Quantity)
                .HasMaxLength(50)
                .HasColumnName("quantity");
            entity.Property(e => e.QuantityTypeId).HasColumnName("quantity_type_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Ingredient).WithMany(p => p.ShoppingLists)
                .HasForeignKey(d => d.IngredientId)
                .HasConstraintName("FK__shopping___ingre__123EB7A3");

            entity.HasOne(d => d.User).WithMany(p => p.ShoppingLists)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__shopping___user___114A936A");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__users__B9BE370FE2493D1B");

            entity.ToTable("users");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.AllergyId).HasColumnName("allergy_id");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.DietPreferenceId).HasColumnName("diet_preference_id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash");

            entity.HasOne(d => d.Allergy).WithMany(p => p.Users)
                .HasForeignKey(d => d.AllergyId)
                .HasConstraintName("FK__users__allergy_i__2CF2ADDF");

            entity.HasOne(d => d.DietPreference).WithMany(p => p.Users)
                .HasForeignKey(d => d.DietPreferenceId)
                .HasConstraintName("FK__users__diet_pref__2BFE89A6");
        });

        modelBuilder.Entity<UserAllergy>(entity =>
        {
            entity.HasKey(e => e.UserAllergyId).HasName("PK__user_all__0A15CE10476328C7");

            entity.ToTable("user_allergies");

            entity.Property(e => e.UserAllergyId).HasColumnName("user_allergy_id");
            entity.Property(e => e.AllergenId).HasColumnName("allergen_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Allergen).WithMany(p => p.UserAllergies)
                .HasForeignKey(d => d.AllergenId)
                .HasConstraintName("FK__user_alle__aller__18EBB532");

            entity.HasOne(d => d.User).WithMany(p => p.UserAllergies)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__user_alle__user___208CD6FA");
        });

        modelBuilder.Entity<UserInventory>(entity =>
        {
            entity.HasKey(e => e.InventoryId).HasName("PK__user_inv__B59ACC4914E99E73");

            entity.ToTable("user_inventory");

            entity.Property(e => e.InventoryId).HasColumnName("inventory_id");
            entity.Property(e => e.ExpiryDate).HasColumnName("expiry_date");
            entity.Property(e => e.IngredientId).HasColumnName("ingredient_id");
            entity.Property(e => e.Quantity)
                .HasMaxLength(50)
                .HasColumnName("quantity");
            entity.Property(e => e.QuantityTypeId).HasColumnName("quantity_type_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Ingredient).WithMany(p => p.UserInventories)
                .HasForeignKey(d => d.IngredientId)
                .HasConstraintName("FK__user_inve__ingre__10566F31");

            entity.HasOne(d => d.User).WithMany(p => p.UserInventories)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__user_inve__user___2DE6D218");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
