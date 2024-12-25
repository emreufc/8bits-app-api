﻿namespace _8bits_app_api.Dtos
{
    public class ShoppingListRequestDto
    {
        public int? UserId { get; set; }

        public int? IngredientId { get; set; }

        public int? QuantityTypeId { get; set; }

        public string? Quantity { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
