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

        public UserInventoryController(IUserInventoryService userInventoryService, IConversionService conversionservice)
        {
            _userInventoryService = userInventoryService;
            _conversionservice = conversionservice;
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

            if (result == null)
            {
                return NotFound(new
                {
                    code = 404,
                    message = $"No inventory found for user ID {userId}.",
                    data = (object)null
                });
            }

            return Ok(new
            {
                code = 200,
                message = $"Successfully retrieved inventory for user ID {userId}.",
                data = result
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
            
            var conversionResult = await _conversionservice.ConvertToStandardUnitAsync(inventory.IngredientId,inventory.QuantityTypeId,inventory.Quantity);
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

        // PUT: api/Users/update
        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser([FromBody] ShoppingListRequestDto inventory)
        {
            var userId = GetCurrentUserId();
            var result = await _userInventoryService.UpdateInventoryAsync(userId, inventory);
            if (!result)
            {
                return BadRequest(new
                {
                    code = 400,
                    message = "Failed to update user. Please check your input and try again.",
                    data = (object)null
                });
            }

            return Ok(new
            {
                code = 200,
                message = $"User with ID {userId} updated successfully.",
                data = inventory
            });
        }
    }
}
