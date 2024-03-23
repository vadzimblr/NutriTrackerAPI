using System.Dynamic;
using Entities.Models;
using Shared.Dto.CreationResourcesDto;
using Shared.Dto.ResponseDto;
using Shared.Dto.UpdateResourcesDto;
using Shared.RequestFeatures;

namespace Contracts.ServiceContracts;

public interface IWaterConsumptionService
{
    Task<(IEnumerable<ExpandoObject> data, MetaData metaData)> GetAllWaterConsumptionsAsync(WaterConsumptionParameters parameters, string userId, bool trackChanges);
    Task<(IEnumerable<ExpandoObject> data, MetaData metaData)> GetAllWaterConsumptionsByDateTimeAsync(WaterConsumptionParameters parameters,string userId,DateTime time, bool trackChanges);
    Task<ExpandoObject> GetWaterConsumptionByIdAsync(WaterConsumptionParameters parameters,string userId, Guid waterConsumptionId, bool trackChanges);
    Task<WaterConsumptionDto> CreateWaterConsumption(string userId,CWaterConsumptionDto waterConsumptionDto);
    Task UpdateWaterConsumption(string userId,Guid waterConsumptionId,UWaterConsumptionDto waterConsumptionDto);
    Task DeleteWaterConsumption(string userId,Guid waterConsumptionId);
    
}