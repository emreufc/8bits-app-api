using _8bits_app_api.Dtos;
using _8bits_app_api.Interfaces;
using _8bits_app_api.Models;
using _8bits_app_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace _8bits_app_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingListController : BaseController
    {
        private readonly IShoppingListService _shoppingListService;
        private readonly IConversionService _conversionservice;
        public ShoppingListController(IShoppingListService shoppingListService, IConversionService conversionservice)
        {
            _shoppingListService = shoppingListService;
            _conversionservice = conversionservice;
        }
        [HttpDelete("{shoppingListId}")]
        public async Task<IActionResult> DeleteFromShoppingList(int shoppingListId)
        {
            if (shoppingListId <= 0)
            {
                return BadRequest(new
                {
                    code = 400,
                    message = "Invalid shopping list ID. Please provide a valid ID.",
                    data = (object)null
                });
            }

            var result = await _shoppingListService.DeleteFromShoppingListAsync(shoppingListId);

            if (!result)
            {
                return NotFound(new
                {
                    code = 404,
                    message = $"No item found in the shopping list with ID {shoppingListId}.",
                    data = (object)null
                });
            }

            return Ok(new
            {
                code = 200,
                message = $"Item with ID {shoppingListId} successfully deleted from the shopping list.",
                data = (object)null
            });
        }

        // POST: api/ShoppingList/add
        [HttpPost("add")]
        public async Task<IActionResult> AddToShoppingList([FromBody] ShoppingListRequestDto shoppingList)
        {
            if (shoppingList == null)
            {
                return BadRequest(new
                {
                    code = 400,
                    message = "Shopping list data is required.",
                    data = (object)null
                });
            }

            var userId = GetCurrentUserId();
            shoppingList.UserId = userId;

            // Conversion işlemi
            var conversionResult = await _conversionservice.ConvertToStandardUnitAsync(
                shoppingList.IngredientId,
                shoppingList.QuantityTypeId,
                shoppingList.Quantity
            );

            if (conversionResult == null)
            {
                return BadRequest(new
                {
                    code = 400,
                    message = $"Conversion failed for ingredient ID {shoppingList.IngredientId}.",
                    data = (object)null
                });
            }

            // Conversion sonrası değerleri güncelle
            shoppingList.QuantityTypeId = conversionResult.Unit == "ml" ? 45 : 5;
            shoppingList.Quantity = conversionResult.ConvertedQuantity;

            // Shopping list'e ekleme işlemi
            var result = await _shoppingListService.AddToShoppingListAsync(shoppingList);

            if (result == null)
            {
                return BadRequest(new
                {
                    code = 400,
                    message = "Failed to add item to the shopping list. Please check the input data.",
                    data = (object)null
                });
            }

            return Ok(new
            {
                code = 200,
                message = "Item successfully added to the shopping list.",
                data = result
            });
        }

        // GET: api/ShoppingList/{userId}
        [HttpGet("mylist")]
        public async Task<IActionResult> GetMyShoppingList()

        {
            var userId = GetCurrentUserId();

            if (userId <= 0)
            {
                return BadRequest(new
                {
                    code = 400,
                    message = "Invalid user ID. Please provide a valid user ID.",
                    data = (object)null
                });
            }

            var result = await _shoppingListService.GetShoppingListByUserIdAsync(userId);

            if (result == null || !result.Any())
            {
                return NotFound(new
                {
                    code = 404,
                    message = $"No shopping list found for user ID {userId}.",
                    data = (object)null
                });
            }
            
            return Ok(new
            {
                code = 200,
                message = $"Successfully retrieved shopping list for user ID {userId}.",
                data = result
            });
        }
    }
}
