using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace _8bits_app_api.Models;

public partial class mydbcontext : DbContext
{
    public mydbcontext()
    {
    }

    public mydbcontext(DbContextOptions<mydbcontext> options)
        : base(options)
    {
    }

    public virtual DbSet<Allergy> Allergies { get; set; }

    public virtual DbSet<DietPreference> DietPreferences { get; set; }

    public virtual DbSet<DietType> DietTypes { get; set; }

    public virtual DbSet<FavoriteRecipe> FavoriteRecipes { get; set; }

    public virtual DbSet<Ingredient> Ingredients { get; set; }

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
        => optionsBuilder.UseSqlServer("Server=tcp:34.32.36.210,1433;Initial Catalog=8bitsdevelopment;Persist Security Info=False;User ID=taylan;Password=SecurePasswordForTaylan123!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Allergy>(entity =>
        {
            entity.ToTable("allergies");

            entity.Property(e => e.AllergyId)
                .ValueGeneratedNever()
                .HasColumnName("allergy_id");
            entity.Property(e => e.AllergenInfo)
                .HasMaxLength(50)
                .HasColumnName("allergen_info");
        });

        modelBuilder.Entity<DietPreference>(entity =>
        {
            entity.HasKey(e => e.DietPreferenceId).HasName("PK__diet_pre__37906503D3A46A81");

            entity.ToTable("diet_preferences");

            entity.Property(e => e.DietPreferenceId).HasColumnName("diet_preference_id");
            entity.Property(e => e.DietTypeId).HasColumnName("diet_type_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.DietType).WithMany(p => p.DietPreferences)
                .HasForeignKey(d => d.DietTypeId)
                .HasConstraintName("FK__diet_pref__diet___4316F928");

            entity.HasOne(d => d.User).WithMany(p => p.DietPreferences)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__diet_pref__user___4222D4EF");
        });

        modelBuilder.Entity<DietType>(entity =>
        {
            entity.ToTable("diet_types");

            entity.Property(e => e.DietTypeId)
                .ValueGeneratedNever()
                .HasColumnName("diet_type_id");
            entity.Property(e => e.DietTypeExplanation)
                .HasMaxLength(100)
                .HasColumnName("diet_type_explanation");
            entity.Property(e => e.DietTypeName)
                .HasMaxLength(50)
                .HasColumnName("diet_type_name");
        });

        modelBuilder.Entity<FavoriteRecipe>(entity =>
        {
            entity.HasKey(e => e.FavId).HasName("PK__favorite__37AAF6FE3C700F7B");

            entity.ToTable("favorite_recipes");

            entity.Property(e => e.FavId).HasColumnName("fav_id");
            entity.Property(e => e.RecipeId).HasColumnName("recipe_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Recipe).WithMany(p => p.FavoriteRecipes)
                .HasForeignKey(d => d.RecipeId)
                .HasConstraintName("FK__favorite___recip__46E78A0C");

            entity.HasOne(d => d.User).WithMany(p => p.FavoriteRecipes)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__favorite___user___45F365D3");
        });

        modelBuilder.Entity<Ingredient>(entity =>
        {
            entity.HasKey(e => e.IngredientId).HasName("PK_Ingredients");

            entity.ToTable("ingredients");

            entity.Property(e => e.IngredientId)
                .ValueGeneratedNever()
                .HasColumnName("ingredient_id");
            entity.Property(e => e.DetailedAllergenInfoTr)
                .HasMaxLength(50)
                .HasColumnName("detailed_allergen_info_tr");
            entity.Property(e => e.IngredientName)
                .HasMaxLength(70)
                .HasColumnName("ingredient_name");
            entity.Property(e => e.PageUrl)
                .HasMaxLength(100)
                .HasColumnName("page_url");
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasKey(e => e.RecipeId).HasName("pk_recipeid");

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
                .HasMaxLength(70)
                .HasColumnName("recipe_name");
            entity.Property(e => e.Serving)
                .HasMaxLength(50)
                .HasColumnName("serving");
        });

        modelBuilder.Entity<RecipeImage>(entity =>
        {
            entity.HasKey(e => e.RecipeId).HasName("PK_Recipe_images");

            entity.ToTable("recipe_images");

            entity.Property(e => e.RecipeId)
                .ValueGeneratedNever()
                .HasColumnName("recipe_id");
            entity.Property(e => e.ImageLink)
                .HasMaxLength(150)
                .HasColumnName("image_link");
            entity.Property(e => e.RecipeName)
                .HasMaxLength(70)
                .HasColumnName("recipe_name");

            entity.HasOne(d => d.Recipe).WithOne(p => p.RecipeImage)
                .HasForeignKey<RecipeImage>(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_recipe_images_recipes");
        });

        modelBuilder.Entity<RecipeIngredient>(entity =>
        {
            entity.HasKey(e => e.RecipeingredientId).HasName("PK_Recipe_ingredients");

            entity.ToTable("recipe_ingredients");

            entity.Property(e => e.RecipeingredientId).HasColumnName("recipeingredient_id");
            entity.Property(e => e.IngredientId).HasColumnName("ingredient_id");
            entity.Property(e => e.IngredientName)
                .HasMaxLength(70)
                .HasColumnName("ingredient_name");
            entity.Property(e => e.Quantity)
                .HasMaxLength(50)
                .HasColumnName("quantity");
            entity.Property(e => e.QuantityType)
                .HasMaxLength(50)
                .HasColumnName("quantity_type");
            entity.Property(e => e.RecipeId).HasColumnName("recipe_id");
        });

        modelBuilder.Entity<RecipeRate>(entity =>
        {
            entity.HasKey(e => e.RecipeId);

            entity.ToTable("recipe_rates");

            entity.Property(e => e.RecipeId)
                .ValueGeneratedNever()
                .HasColumnName("recipe_id");
            entity.Property(e => e.RecipeName)
                .HasMaxLength(70)
                .HasColumnName("recipe_name");
            entity.Property(e => e.RecipeRate1).HasColumnName("recipe_rate");
        });

        modelBuilder.Entity<RecipeStep>(entity =>
        {
            entity.HasKey(e => e.RecipestepsId).HasName("PK_Recipe_steps");

            entity.ToTable("recipe_steps");

            entity.Property(e => e.RecipestepsId).HasColumnName("recipesteps_id");
            entity.Property(e => e.RecipeId).HasColumnName("recipe_id");
            entity.Property(e => e.RecipeName)
                .HasMaxLength(70)
                .HasColumnName("recipe_name");
            entity.Property(e => e.Step)
                .HasMaxLength(700)
                .HasColumnName("step");
            entity.Property(e => e.StepNum).HasColumnName("step_num");

            entity.HasOne(d => d.Recipe).WithMany(p => p.RecipeSteps)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_recipe_steps_recipes");
        });

        modelBuilder.Entity<ShoppingList>(entity =>
        {
            entity.HasKey(e => e.ShoppingListId).HasName("PK__shopping__0659AC3AE1DA9E3D");

            entity.ToTable("shopping_list");

            entity.Property(e => e.ShoppingListId).HasColumnName("shopping_list_id");
            entity.Property(e => e.IngredientId).HasColumnName("ingredient_id");
            entity.Property(e => e.Quantity)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("quantity");
            entity.Property(e => e.QuantityTypeId).HasColumnName("quantity_type_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Ingredient).WithMany(p => p.ShoppingLists)
                .HasForeignKey(d => d.IngredientId)
                .HasConstraintName("FK__shopping___ingre__4CA06362");

            entity.HasOne(d => d.User).WithMany(p => p.ShoppingLists)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__shopping___user___4BAC3F29");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__users__B9BE370F67628C46");

            entity.ToTable("users");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.AllergyId).HasColumnName("allergy_id");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.DietPreferenceId).HasColumnName("diet_preference_id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password_hash");

            entity.HasOne(d => d.Allergy).WithMany(p => p.Users)
                .HasForeignKey(d => d.AllergyId)
                .HasConstraintName("FK_users_user_allergies");

            entity.HasOne(d => d.DietPreference).WithMany(p => p.Users)
                .HasForeignKey(d => d.DietPreferenceId)
                .HasConstraintName("FK__users__diet_pref__5EBF139D");
        });

        modelBuilder.Entity<UserAllergy>(entity =>
        {
            entity.HasKey(e => e.UserAllergyId).HasName("PK__user_all__0A15CE105113DEA6");

            entity.ToTable("user_allergies");

            entity.Property(e => e.UserAllergyId).HasColumnName("user_allergy_id");
            entity.Property(e => e.AllergyId).HasColumnName("allergy_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Allergy).WithMany(p => p.UserAllergies)
                .HasForeignKey(d => d.AllergyId)
                .HasConstraintName("FK__user_alle__aller__59FA5E80");
        });

        modelBuilder.Entity<UserInventory>(entity =>
        {
            entity.HasKey(e => e.InventoryId).HasName("PK__user_inv__B59ACC499DB2C4F5");

            entity.ToTable("user_inventory");

            entity.Property(e => e.InventoryId).HasColumnName("inventory_id");
            entity.Property(e => e.ExpiryDate).HasColumnName("expiry_date");
            entity.Property(e => e.IngredientId).HasColumnName("ingredient_id");
            entity.Property(e => e.Quantity)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("quantity");
            entity.Property(e => e.QuantityTypeId).HasColumnName("quantity_type_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Ingredient).WithMany(p => p.UserInventories)
                .HasForeignKey(d => d.IngredientId)
                .HasConstraintName("FK__user_inve__ingre__5DCAEF64");

            entity.HasOne(d => d.User).WithMany(p => p.UserInventories)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__user_inve__user___5CD6CB2B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
