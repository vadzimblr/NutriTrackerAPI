using System.Dynamic;
using Shared.Dto.CreationResourcesDto;
using Shared.Dto.ResponseDto;
using Shared.Dto.UpdateResourcesDto;
using Shared.RequestFeatures;

namespace Contracts.ServiceContracts;

public interface IProductConsumptionService
{
    Task<(IEnumerable<ExpandoObject> data, MetaData metaData)> GetAllProductConsumptionAsync(ProductConsumptionParameters parameters, string userId, bool trackChanges);
    Task<(IEnumerable<ExpandoObject> data, MetaData metaData)> GetAllProductConsumptionByDateAsync(ProductConsumptionParameters parameters, string userId, DateTime time, bool trackChanges);

    Task<ExpandoObject> GetProductConsumptionByIdAsync(ProductConsumptionParameters parameters, string userId, Guid productConsumptionId,
        bool trackChanges);

    Task<ProductConsumptionDto> CreateProductConsumption(string userId, CProductConsumptionDto productConsumptionDto);
    Task UpdateProductConsumption(string userId, UProductConsumptionDto productConsumptionDto, Guid productConsumptionId);
    Task DeleteProductConsumption(string userId, Guid productConsumptionId);

} 