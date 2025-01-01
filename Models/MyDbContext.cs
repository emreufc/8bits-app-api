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

    public virtual DbSet<Allergen> Allergens { get; set; }

    public virtual DbSet<DietPreference> DietPreferences { get; set; }

    public virtual DbSet<DietType> DietTypes { get; set; }

    public virtual DbSet<FavoriteRecipe> FavoriteRecipes { get; set; }

    public virtual DbSet<Ingredient> Ingredients { get; set; }

    public virtual DbSet<IngredientConversion> IngredientConversions { get; set; }

    public virtual DbSet<OldRecipe> OldRecipes { get; set; }

    public virtual DbSet<QuantityType> QuantityTypes { get; set; }

    public virtual DbSet<Recipe> Recipes { get; set; }

    public virtual DbSet<RecipeImage> RecipeImages { get; set; }

    public virtual DbSet<RecipeIngredient> RecipeIngredients { get; set; }

    public virtual DbSet<RecipeStep> RecipeSteps { get; set; }

    public virtual DbSet<ShoppingList> ShoppingLists { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAllergy> UserAllergies { get; set; }

    public virtual DbSet<UserInventory> UserInventories { get; set; }

    public virtual DbSet<UserRefreshToken> UserRefreshTokens { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=185.240.104.86\\SQLEXPRESS;Initial Catalog=8bitsdev;Persist Security Info=False;User ID=sa;Password=LWJLY23ikONQr4j;Encrypt=False;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Allergen>(entity =>
        {
            entity.ToTable("allergen");

            entity.Property(e => e.AllergenId)
                .ValueGeneratedNever()
                .HasColumnName("allergen_id");
            entity.Property(e => e.AllergenName)
                .HasMaxLength(50)
                .HasColumnName("allergen_name");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
        });

        modelBuilder.Entity<DietPreference>(entity =>
        {
            entity.HasKey(e => e.DietPreferenceId).HasName("PK__diet_pre__37906503BDC69427");

            entity.ToTable("diet_preferences");

            entity.Property(e => e.DietPreferenceId).HasColumnName("diet_preference_id");
            entity.Property(e => e.DietTypeId).HasColumnName("diet_type_id");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.DietType).WithMany(p => p.DietPreferences)
                .HasForeignKey(d => d.DietTypeId)
                .HasConstraintName("FK_diet_preferences_diet_type_id");

            entity.HasOne(d => d.User).WithMany(p => p.DietPreferences)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_diet_preferences_user_id");
        });

        modelBuilder.Entity<DietType>(entity =>
        {
            entity.HasKey(e => e.DietTypeId).HasName("diet_types_pk");

            entity.ToTable("diet_types");

            entity.Property(e => e.DietTypeId)
                .ValueGeneratedNever()
                .HasColumnName("diet_type_id");
            entity.Property(e => e.DietTypeExplanation)
                .HasColumnType("text")
                .HasColumnName("diet_type_explanation");
            entity.Property(e => e.DietTypeName)
                .HasColumnType("text")
                .HasColumnName("diet_type_name");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
        });

        modelBuilder.Entity<FavoriteRecipe>(entity =>
        {
            entity.HasKey(e => e.FavId).HasName("PK__favorite__37AAF6FE9C4253CE");

            entity.ToTable("favorite_recipes");

            entity.Property(e => e.FavId).HasColumnName("fav_id");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.RecipeId).HasColumnName("recipe_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Recipe).WithMany(p => p.FavoriteRecipes)
                .HasForeignKey(d => d.RecipeId)
                .HasConstraintName("FK_favorite_recipes_recipe_id");

            entity.HasOne(d => d.User).WithMany(p => p.FavoriteRecipes)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_favorite_recipes_user_id");
        });

        modelBuilder.Entity<Ingredient>(entity =>
        {
            entity.ToTable("ingredients");

            entity.Property(e => e.IngredientId)
                .ValueGeneratedNever()
                .HasColumnName("ingredient_id");
            entity.Property(e => e.AllergenId).HasColumnName("allergen_id");
            entity.Property(e => e.IngImgUrl)
                .HasMaxLength(250)
                .HasColumnName("ing_img_url");
            entity.Property(e => e.IngredientCategory)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ingredient_category");
            entity.Property(e => e.IngredientName)
                .HasMaxLength(70)
                .HasColumnName("ingredient_name");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");

            entity.HasOne(d => d.Allergen).WithMany(p => p.Ingredients)
                .HasForeignKey(d => d.AllergenId)
                .HasConstraintName("FK_ingredients_allergen_id");
        });

        modelBuilder.Entity<IngredientConversion>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ingredient_conversion");

            entity.Property(e => e.ConversionToGrams).HasColumnName("conversion_to_grams");
            entity.Property(e => e.ConversionToMl).HasColumnName("conversion_to_ml");
            entity.Property(e => e.IngredientId).HasColumnName("ingredient_id");
            entity.Property(e => e.IngredientName)
                .HasMaxLength(70)
                .HasColumnName("ingredient_name");
            entity.Property(e => e.QuantityTypeDesc)
                .HasMaxLength(70)
                .HasColumnName("quantity_type_desc");
            entity.Property(e => e.QuantityTypeId).HasColumnName("quantity_type_id");
        });

        modelBuilder.Entity<OldRecipe>(entity =>
        {
            entity.HasKey(e => e.OldRecipeId).HasName("PK__old_reci__09FCF62CCBDB2BD9");

            entity.ToTable("old_recipes");

            entity.Property(e => e.AddedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
        });

        modelBuilder.Entity<QuantityType>(entity =>
        {
            entity.ToTable("quantity_types");

            entity.Property(e => e.QuantityTypeId)
                .ValueGeneratedNever()
                .HasColumnName("quantity_type_id");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.QuantityTypeDesc)
                .HasMaxLength(50)
                .HasColumnName("quantity_type_desc");
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.ToTable("recipes");

            entity.Property(e => e.RecipeId)
                .ValueGeneratedNever()
                .HasColumnName("recipe_id");
            entity.Property(e => e.Aksam).HasColumnName("aksam");
            entity.Property(e => e.Carbohydrate).HasColumnName("carbohydrate");
            entity.Property(e => e.CookingTime).HasColumnName("cooking_time");
            entity.Property(e => e.DairyFree)
                .HasDefaultValue(false)
                .HasColumnName("Dairy_Free");
            entity.Property(e => e.Fat).HasColumnName("fat");
            entity.Property(e => e.Flexitarian).HasDefaultValue(false);
            entity.Property(e => e.GlutenFree)
                .HasDefaultValue(false)
                .HasColumnName("Gluten_Free");
            entity.Property(e => e.GramPerServing).HasColumnName("gram_per_serving");
            entity.Property(e => e.Icecek).HasColumnName("icecek");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(250)
                .HasColumnName("image_url");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.Kahvalti).HasColumnName("kahvalti");
            entity.Property(e => e.KcalPerServing).HasColumnName("kcal_per_serving");
            entity.Property(e => e.Keto).HasDefaultValue(false);
            entity.Property(e => e.LowCarb)
                .HasDefaultValue(false)
                .HasColumnName("Low_Carb");
            entity.Property(e => e.Mediterranean).HasDefaultValue(false);
            entity.Property(e => e.Normal).HasDefaultValue(false);
            entity.Property(e => e.Oglen).HasColumnName("oglen");
            entity.Property(e => e.Paleo).HasDefaultValue(false);
            entity.Property(e => e.PersonCount).HasColumnName("person_count");
            entity.Property(e => e.Pescatarian).HasDefaultValue(false);
            entity.Property(e => e.PreparationTime).HasColumnName("preparation_time");
            entity.Property(e => e.Protein).HasColumnName("protein");
            entity.Property(e => e.RecipeName)
                .HasMaxLength(50)
                .HasColumnName("recipe_name");
            entity.Property(e => e.RecipeRate).HasColumnName("recipe_rate");
            entity.Property(e => e.Tatli).HasColumnName("tatli");
            entity.Property(e => e.Vegan)
                .HasDefaultValue(false)
                .HasColumnName("vegan");
            entity.Property(e => e.Vegetarian).HasDefaultValue(false);
        });

        modelBuilder.Entity<RecipeImage>(entity =>
        {
            entity.HasKey(e => e.RecipeImageId).HasName("PK__recipe_i__23E65C237E8F3E8E");

            entity.ToTable("recipe_images");
        });

        modelBuilder.Entity<RecipeIngredient>(entity =>
        {
            entity.HasKey(e => e.RecipeIngredientId).HasName("recipe_ingredients_pk");

            entity.ToTable("recipe_ingredients");

            entity.Property(e => e.RecipeIngredientId).HasColumnName("recipe_ingredient_id");
            entity.Property(e => e.IngredientId).HasColumnName("ingredient_id");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.QuantityTypeId).HasColumnName("quantity_type_id");
            entity.Property(e => e.RecipeId).HasColumnName("recipe_id");

            entity.HasOne(d => d.Ingredient).WithMany(p => p.RecipeIngredients)
                .HasForeignKey(d => d.IngredientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_recipe_ingredients_ingredient_id");

            entity.HasOne(d => d.QuantityType).WithMany(p => p.RecipeIngredients)
                .HasForeignKey(d => d.QuantityTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_recipe_ingredients_quantity_type_id");

            entity.HasOne(d => d.Recipe).WithMany(p => p.RecipeIngredients)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_recipe_ingredients_recipe_id");
        });

        modelBuilder.Entity<RecipeStep>(entity =>
        {
            entity.HasKey(e => e.RecipeStepsId).HasName("recipe_steps_pk");

            entity.ToTable("recipe_steps");

            entity.Property(e => e.RecipeStepsId)
                .ValueGeneratedNever()
                .HasColumnName("recipe_steps_id");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.RecipeId).HasColumnName("recipe_id");
            entity.Property(e => e.RecipeName)
                .HasMaxLength(70)
                .HasColumnName("recipe_name");
            entity.Property(e => e.Step).HasColumnName("step");
            entity.Property(e => e.StepNum).HasColumnName("step_num");

            entity.HasOne(d => d.Recipe).WithMany(p => p.RecipeSteps)
                .HasForeignKey(d => d.RecipeId)
                .HasConstraintName("FK_recipe_steps_recipe_id");
        });

        modelBuilder.Entity<ShoppingList>(entity =>
        {
            entity.HasKey(e => e.ShoppingListId).HasName("PK__shopping__0659AC3A78049965");

            entity.ToTable("shopping_list");

            entity.Property(e => e.ShoppingListId).HasColumnName("shopping_list_id");
            entity.Property(e => e.IngredientId).HasColumnName("ingredient_id");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.Quantity)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("quantity");
            entity.Property(e => e.QuantityTypeId).HasColumnName("quantity_type_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Ingredient).WithMany(p => p.ShoppingLists)
                .HasForeignKey(d => d.IngredientId)
                .HasConstraintName("FK_shopping_list_ingredient_id");

            entity.HasOne(d => d.QuantityType).WithMany(p => p.ShoppingLists)
                .HasForeignKey(d => d.QuantityTypeId)
                .HasConstraintName("FK_shopping_list_quantity_type_id");

            entity.HasOne(d => d.User).WithMany(p => p.ShoppingLists)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_shopping_list_user_id");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__users__B9BE370FC57A27D3");

            entity.ToTable("users");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Gender)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("gender");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password_hash");
            entity.Property(e => e.PasswordSalt)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password_salt");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(50)
                .HasColumnName("phone_number");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .HasColumnName("role");
            entity.Property(e => e.Surname)
                .HasMaxLength(100)
                .HasColumnName("surname");
        });

        modelBuilder.Entity<UserAllergy>(entity =>
        {
            entity.HasKey(e => e.UserAllergyId).HasName("PK__user_all__0A15CE1036155AFD");

            entity.ToTable("user_allergies");

            entity.Property(e => e.UserAllergyId).HasColumnName("user_allergy_id");
            entity.Property(e => e.AllergyId).HasColumnName("allergy_id");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Allergy).WithMany(p => p.UserAllergies)
                .HasForeignKey(d => d.AllergyId)
                .HasConstraintName("FK_user_allergies_allergy_id");

            entity.HasOne(d => d.User).WithMany(p => p.UserAllergies)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_user_allergies_user_id");
        });

        modelBuilder.Entity<UserInventory>(entity =>
        {
            entity.HasKey(e => e.InventoryId).HasName("PK__user_inv__B59ACC49B6090E47");

            entity.ToTable("user_inventory");

            entity.Property(e => e.InventoryId).HasColumnName("inventory_id");
            entity.Property(e => e.IngredientId).HasColumnName("ingredient_id");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.QuantityTypeId).HasColumnName("quantity_type_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Ingredient).WithMany(p => p.UserInventories)
                .HasForeignKey(d => d.IngredientId)
                .HasConstraintName("FK_user_inventory_ingredient_id");

            entity.HasOne(d => d.QuantityType).WithMany(p => p.UserInventories)
                .HasForeignKey(d => d.QuantityTypeId)
                .HasConstraintName("FK_quantity_type_id");

            entity.HasOne(d => d.User).WithMany(p => p.UserInventories)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_user_inventory_user_id");
        });

        modelBuilder.Entity<UserRefreshToken>(entity =>
        {
            entity.HasKey(e => e.UserRefreshTokenId).HasName("PK__user_ref__341ED6BEF5CE9ABB");

            entity.ToTable("user_refresh_tokens");

            entity.Property(e => e.UserRefreshTokenId).HasColumnName("user_refresh_token_id");
            entity.Property(e => e.ExpirationDate)
                .HasColumnType("datetime")
                .HasColumnName("expiration_date");
            entity.Property(e => e.RefreshToken).HasColumnName("refresh_token");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.UserRefreshTokens)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_user_refresh_tokens_users");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
