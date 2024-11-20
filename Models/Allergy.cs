using System;
using System.Collections.Generic;

namespace _8bits_app_api.Models;

public partial class Allergy
{
    public int AllerjiId { get; set; }

    public string AllerjiBilgisi { get; set; } = null!;

    public virtual ICollection<UserAllergy> UserAllergies { get; set; } = new List<UserAllergy>();
}
