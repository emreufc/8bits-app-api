using _8bits_app_api.Dtos;
using _8bits_app_api.Interfaces;
using _8bits_app_api.Models;
using _8bits_app_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace _8bits_app_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInventoryController : BaseController
    {
        private readonly IConversionService _conversionservice;
        private readonly IUserInventoryService _userInventoryService;
        private readonly IShoppingListService _shoppingListService;

        public UserInventoryController(IUserInventoryService userInventoryService, IConversionService conversionservice, IShoppingListService shoppingListService)
        {
            _userInventoryService = userInventoryService;
            _conversionservice = conversionservice;
            _shoppingListService = shoppingListService;
        }


        // GET: api/Inventory/{userId}
        [HttpGet("myInventory")]
        public async Task<IActionResult> GetmyInventoryList()

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

            var result = await _userInventoryService.GetInventoryByUserIdAsync(userId);

            return Ok(new
            {
                    code = 200,
                    message = result == null || !result.Any()
               ? $"No items found in the inventory for user ID {userId}."
               : $"Successfully retrieved inventory for user ID {userId}.",
                    data = result ?? new List<InventoryDto>() // Eğer liste null ise boş bir liste döndür
            });
        }
        [HttpGet("/categories")]
        public async Task<IActionResult> GetInventoryByCategory([FromQuery] List<string> selectedCategories)
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
            
            try
            {
                // Call the service layer to get inventory
                var inventoryDtos = await _userInventoryService.GetInventoryByCategoryAsync(userId, selectedCategories);

                if (!inventoryDtos.Any())
                {
                    return NotFound("No inventory items found.");
                }

                return Ok(new
                    {
                        code = 200,
                        message = inventoryDtos == null || !inventoryDtos.Any()
                            ? $"No items found in the inventory for user ID {userId}."
                            : $"Successfully retrieved inventory for user ID {userId}.",
                        data = inventoryDtos ?? new List<InventoryDto>() });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }
       
        [HttpGet("check-inventory/{recipeId}")]
        public async Task<IActionResult> CheckInventory(int recipeId)
        {
            var userId = GetCurrentUserId();

            var (isSufficient, missingIngredients) = await _userInventoryService.IsInventorySufficientAsync(recipeId, userId);

            return Ok(new
            {
                code = 200,
                message = isSufficient
                    ? "Envanterinizde tarif için yeterli malzeme var."
                    : "Envanterinizde tarif için yeterli malzeme yok.",
                isSufficient = isSufficient,
                data = isSufficient ? null : missingIngredients
            });
        }
        [HttpPost("deduct-ingredient")]
        public async Task<IActionResult> ProcessRecipe(int recipeId)
        {
            var userId = GetCurrentUserId();

            if (userId <= 0)
            {
                return BadRequest(new
                {
                    code = 400,
                    message = "Geçersiz kullanıcı ID.",
                    data = (object)null
                });
            }

            // Elinde yeterli malzeme var mı kontrol et
            var isSufficient = await _userInventoryService.IsInventorySufficientAsync(recipeId, userId);

            if (isSufficient.IsSufficient == false)
            {
                var missingIngredients = isSufficient.MissingIngredients;

                return Ok(new
                {
                    code = 200,
                    message = "Elinde yeterli malzeme yok.",
                    data = missingIngredients
                });
            }

            // Envanterden düş
            await _userInventoryService.DeductIngredientsFromInventoryAsync(recipeId, userId);

            return Ok(new
            {
                code = 200,
                message = "Tarif başarıyla tamamlandı ve envanterden düşüldü."
            });
        }

        // POST: api/Inventory/add
        [HttpPost("add")]
        public async Task<IActionResult> AddtoInventory([FromBody] ShoppingListRequestDto inventory)
        {
            if (inventory == null)
            {
                return BadRequest(new
                {
                    code = 400,
                    message = "inventory data is required.",
                    data = (object)null
                });
            }
            var userId = GetCurrentUserId();

            var conversionResult = await _conversionservice.ConvertToStandardUnitAsync(inventory.IngredientId, inventory.QuantityTypeId, inventory.Quantity);
            if (conversionResult == null)
            {
                return NotFound(new
                {
                    code = 404,
                    message = $"conversion found in the inventory with ID {inventory.IngredientId}.",
                    data = (object)null
                });
            }
            inventory.UserId = userId;
            inventory.QuantityTypeId = conversionResult.Unit == "ml" ? 45 : 5;
            inventory.Quantity = conversionResult.ConvertedQuantity;
            var result = await _userInventoryService.AddToInventoryAsync(inventory);

            if (result == null)
            {
                return BadRequest(new
                {
                    code = 400,
                    message = "Failed to add item to the inventory. Please check the input data.",
                    data = (object)null
                });
            }

            return Ok(new
            {
                code = 200,
                message = "Item successfully added to the inventory.",
                data = result
            });
        }

        // delete api
        [HttpDelete("{inventory_id}")]
        public async Task<IActionResult> DeleteFromInventory(int inventory_id)
        {
            if (inventory_id <= 0)
            {
                return BadRequest(new
                {
                    code = 400,
                    message = "Invalid inventory ID. Please provide a valid ID.",
                    data = (object)null
                });
            }

            var result = await _userInventoryService.DeleteFromInventoryAsync(inventory_id);

            if (!result)
            {
                return NotFound(new
                {
                    code = 404,
                    message = $"No item found in the inventory with ID {inventory_id}.",
                    data = (object)null
                });
            }

            return Ok(new
            {
                code = 200,
                message = $"Item with ID {inventory_id} successfully deleted from the inventory.",
                data = (object)null
            });
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser([FromBody] ShoppingListRequestDto inventory)
        {
            if (inventory == null)
            {
                return BadRequest(new
                {
                    code = 400,
                    message = "Inventory data is required.",
                    data = (object)null
                });
            }

            var userId = GetCurrentUserId();

            try
            {
                var result = await _userInventoryService.UpdateInventoryAsync(userId, inventory);

                if (!result)
                {
                    return NotFound(new
                    {
                        code = 404,
                        message = "The inventory item could not be updated. It may not exist.",
                        data = (object)null
                    });
                }

                return Ok(new
                {
                    code = 200,
                    message = $"Inventory updated successfully for user ID {userId}.",
                    data = inventory
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    code = 400,
                    message = ex.Message,
                    data = (object)null
                });
            }
        }
        [HttpPost("shoppinglist-to-inventory")]
        public async Task<IActionResult> MoveItemToInventory([FromBody] ShoppingListRequestDto request)
        {
            var userId = GetCurrentUserId();

            if (userId <= 0)
            {
                return BadRequest(new
                {
                    code = 400,
                    message = "Invalid user ID.",
                    data = (object)null
                });
            }

            if (request == null || request.IngredientId <= 0 || request.Quantity <= 0)
            {
                return BadRequest(new
                {
                    code = 400,
                    message = "Invalid item data. Please provide valid ingredientId, quantityTypeId, and quantity.",
                    data = (object)null
                });
            }

            // Alışveriş listesindeki ürünü kontrol et
            var shoppingListItem = await _shoppingListService.GetShoppingListItemByIdAndIngredientIdAsync(userId, request.IngredientId);

            if (shoppingListItem == null)
            {
                return NotFound(new
                {
                    code = 404,
                    message = "Item not found in the shopping list.",
                    data = (object)null
                });
            }
            var inventoryItem = await _userInventoryService.AddOrUpdateInventoryItemAsync(new ShoppingListRequestDto
            {
                UserId = userId,
                IngredientId = request.IngredientId,
                Quantity = request.Quantity,
                QuantityTypeId = request.QuantityTypeId
            });

            // Alışveriş listesinden sil
            await _shoppingListService.DeleteFromShoppingListAsync(shoppingListItem.ShoppingListId);

            return Ok(new
            {
                code = 200,
                message = "Item successfully moved to inventory and removed from shopping list.",
                data =  inventoryItem
            });
        }

    }
}
