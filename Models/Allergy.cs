using System;
using System.Collections.Generic;

namespace _8bits_app_api.Models;

public partial class Allergy
{
    public int AllergyId { get; set; }

    public string AllergenInfo { get; set; } = null!;

    public virtual ICollection<UserAllergy> UserAllergies { get; set; } = new List<UserAllergy>();
}
