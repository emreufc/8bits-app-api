using System;
using System.Collections.Generic;

namespace _8bits_app_api.Models;

public partial class DietPreference
{
    public int DietPreferenceId { get; set; }

    public int? UserId { get; set; }

    public int? DietTypeId { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual DietType? DietType { get; set; }

    public virtual User? User { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
