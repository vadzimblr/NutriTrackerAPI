using Entities.Models;
using Shared.Dto.CreationResourcesDto;
using Shared.Dto.ResponseDto;
using Shared.Dto.UpdateResourcesDto;

namespace Contracts.ServiceContracts;

public interface IWaterConsumptionService
{
    public Task<IEnumerable<WaterConsumptionDto>> GetAllWaterConsumptionsAsync(string userId, bool trackChanges);
    public Task<IEnumerable<WaterConsumptionDto>> GetAllWaterConsumptionsByDateTimeAsync(string userId,DateTime time, bool trackChanges);
    public Task<WaterConsumptionDto> GetWaterConsumptionById(string userId, Guid waterConsumptionId, bool trackChanges);
    public Task CreateWaterConsumption(string userId,CWaterConsumptionDto waterConsumptionDto);
    public Task UpdateWaterConsumption(string userId,Guid waterConsumptionId,UWaterConsumptionDto waterConsumptionDto);
    public Task DeleteWaterConsumption(string userId,Guid waterConsumptionId);
    
}