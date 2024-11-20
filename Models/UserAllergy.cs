using System;
using System.Collections.Generic;

namespace _8bits_app_api.Models;

public partial class UserAllergy
{
    public int UserAllergyId { get; set; }

    public int? UserId { get; set; }

    public int? AllergenId { get; set; }

    public virtual Allergy? Allergen { get; set; }

    public virtual User? User { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
