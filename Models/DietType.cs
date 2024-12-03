using System;
using System.Collections.Generic;

namespace _8bits_app_api.Models;

public partial class DietType
{
    public int DietTypeId { get; set; }

    public string DietTypeName { get; set; } = null!;

    public string DietTypeExplanation { get; set; } = null!;

    public virtual ICollection<DietPreference> DietPreferences { get; set; } = new List<DietPreference>();
}
