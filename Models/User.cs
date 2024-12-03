using System;
using System.Collections.Generic;

namespace _8bits_app_api.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? PasswordHash { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public int? AllergyId { get; set; }

    public int? DietPreferenceId { get; set; }

    public virtual UserAllergy? Allergy { get; set; }

    public virtual DietPreference? DietPreference { get; set; }

    public virtual ICollection<DietPreference> DietPreferences { get; set; } = new List<DietPreference>();

    public virtual ICollection<FavoriteRecipe> FavoriteRecipes { get; set; } = new List<FavoriteRecipe>();

    public virtual ICollection<ShoppingList> ShoppingLists { get; set; } = new List<ShoppingList>();

    public virtual ICollection<UserInventory> UserInventories { get; set; } = new List<UserInventory>();
}
