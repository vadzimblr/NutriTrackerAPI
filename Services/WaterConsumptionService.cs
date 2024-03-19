using AutoMapper;
using Contracts.RepositoryContracts;
using Contracts.ServiceContracts;
using Entities.Exceptions;
using Entities.Models;
using Shared.Dto.CreationResourcesDto;
using Shared.Dto.ResponseDto;
using Shared.Dto.UpdateResourcesDto;

namespace Services;

public class WaterConsumptionService:IWaterConsumptionService
{
    private readonly IMapper _mapper;
    private readonly IRepositoryManager _repositoryManager;
    
    public WaterConsumptionService(IMapper mapper,IRepositoryManager repositoryManager)
    {
        _mapper = mapper;
        _repositoryManager = repositoryManager;
    }

    private async Task<WaterConsumption> GetWaterConsumptionByIdIfExistsAsync(string userId, Guid waterConsumptionId, bool trackChanges)
    {
        var waterConsumptionEntity =
            await _repositoryManager.WaterConsumption.GetWaterConsumptionById(userId,waterConsumptionId, trackChanges);
        if (waterConsumptionEntity is null)
        {
            throw new WaterConsumptionNotFoundException(waterConsumptionId);
        }

        return waterConsumptionEntity;
    }
    public async Task<IEnumerable<WaterConsumptionDto>> GetAllWaterConsumptionsAsync(string userId,bool trackChanges)
    {
        var waterConsumptionsEntities = await _repositoryManager.WaterConsumption.GetAllWaterConsumptionsAsync(userId,trackChanges);
        var waterConsumptionsDtos = _mapper.Map<IEnumerable<WaterConsumptionDto>>(waterConsumptionsEntities);
        return waterConsumptionsDtos;
    }

    public async Task<IEnumerable<WaterConsumptionDto>> GetAllWaterConsumptionsByDateTimeAsync(string userId,DateTime time,bool trackChanges)
    {
        var waterConsumptionsEntities = await _repositoryManager.WaterConsumption.GetAllWaterConsumptionsByDateAsync(userId,time,trackChanges);
        var waterConsumptionsDtos = _mapper.Map<IEnumerable<WaterConsumptionDto>>(waterConsumptionsEntities);
        return waterConsumptionsDtos;
    }

    public async Task<WaterConsumptionDto> GetWaterConsumptionById(string userId,Guid waterConsumptionId, bool trackChanges)
    {
        var waterConsumptionEntity = await GetWaterConsumptionByIdIfExistsAsync(userId,waterConsumptionId, trackChanges);
        var waterConsumptionDto = _mapper.Map<WaterConsumptionDto>(waterConsumptionEntity);
        return waterConsumptionDto;
    }

    public async Task CreateWaterConsumption(string userId,CWaterConsumptionDto waterConsumptionDto)
    {
        var waterConsumptionEntity = _mapper.Map<WaterConsumption>(waterConsumptionDto);
        waterConsumptionEntity.UserId = userId;
        _repositoryManager.WaterConsumption.CreateWaterConsumption(waterConsumptionEntity);
        await _repositoryManager.SaveAsync();
    }

    public async Task UpdateWaterConsumption(string userId,Guid waterConsumptionId, UWaterConsumptionDto waterConsumptionDto)
    {
        var waterConsumptionEntity = await GetWaterConsumptionByIdIfExistsAsync(userId,waterConsumptionId, trackChanges: true);
        _mapper.Map(waterConsumptionDto, waterConsumptionEntity);
        await _repositoryManager.SaveAsync();
    }

    public async Task DeleteWaterConsumption(string userId,Guid waterConsumptionId)
    {
        var waterConsumptionEntity =
            await GetWaterConsumptionByIdIfExistsAsync(userId,waterConsumptionId, trackChanges: false);
        _repositoryManager.WaterConsumption.DeleteWaterConsumption(waterConsumptionEntity);
        await _repositoryManager.SaveAsync();
    }
}