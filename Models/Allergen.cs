using System;
using System.Collections.Generic;

namespace _8bits_app_api.Models;

public partial class Allergen
{
    public int AllergenId { get; set; }

    public string AllergenName { get; set; } = null!;

    public bool? IsDeleted { get; set; }

    public virtual ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

    public virtual ICollection<UserAllergy> UserAllergies { get; set; } = new List<UserAllergy>();
}
