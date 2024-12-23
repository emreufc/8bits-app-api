using System;
using System.Collections.Generic;

namespace _8bits_app_api.Models;

public partial class UserInventory
{
    public int InventoryId { get; set; }

    public int? UserId { get; set; }

    public int? IngredientId { get; set; }

    public DateOnly? ExpiryDate { get; set; }

    public bool? IsDeleted { get; set; }

    public double? Quantity { get; set; }

    public int? QuantityTypeId { get; set; }

    public virtual Ingredient? Ingredient { get; set; }

    public virtual QuantityType? QuantityType { get; set; }

    public virtual User? User { get; set; }
}
