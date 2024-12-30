using _8bits_app_api.Dtos;
using _8bits_app_api.Interfaces;
using _8bits_app_api.Models;

namespace _8bits_app_api.Services
{
    public class ConversionService : IConversionService
    {
        private readonly IConversionRepository _conversionRepository;
        public ConversionService(IConversionRepository conversionRepository)
        {
            _conversionRepository = conversionRepository;
        }
        public async Task<IEnumerable<IngredientConversion>> GetConversionsByIngredientIdAsync(int ingredientId)
        {
            return await _conversionRepository.GetConversionsByIngredientIdAsync(ingredientId);
        }
        public async Task<ConversionResult> ConvertToStandardUnitAsync(int ingredientId, int quantityTypeId, double quantity)
        {
            var conversion = await _conversionRepository.GetConversionbyQuantitytypeIdAsync(ingredientId, quantityTypeId);

            if (conversion == null)
            {
                throw new Exception("Conversion information not found for the given ingredient and quantity type.");
            }

            if (conversion.ConversionToMl.HasValue)
            {
                return new ConversionResult
                {
                    ConvertedQuantity = quantity * conversion.ConversionToMl.Value,
                    Unit = "ml"
                };
            }
            else if (conversion.ConversionToGrams.HasValue)
            {
                return new ConversionResult
                {
                    ConvertedQuantity = quantity * conversion.ConversionToGrams.Value,
                    Unit = "gram"
                };
            }
            else
            {
                throw new Exception("No valid conversion available for the given ingredient and quantity type.");
            }
        }
    }
}
