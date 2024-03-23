using System.Dynamic;
using AutoMapper;
using Contracts;
using Contracts.RepositoryContracts;
using Contracts.ServiceContracts;
using Entities.Exceptions;
using Entities.Models;
using Shared.Dto.CreationResourcesDto;
using Shared.Dto.ResponseDto;
using Shared.Dto.UpdateResourcesDto;
using Shared.RequestFeatures;

namespace Services;

public class WaterConsumptionService:IWaterConsumptionService
{
    private readonly IMapper _mapper;
    private readonly IRepositoryManager _repositoryManager;
    private readonly IDataShaper<WaterConsumptionDto> _dataShaper;
    
    public WaterConsumptionService(IMapper mapper,IRepositoryManager repositoryManager, IDataShaper<WaterConsumptionDto> dataShaper)
    {
        _mapper = mapper;
        _repositoryManager = repositoryManager;
        _dataShaper = dataShaper;
    }

    private async Task<WaterConsumption> GetWaterConsumptionByIdIfExistsAsync( string userId, Guid waterConsumptionId, bool trackChanges)
    {
        var waterConsumptionEntity =
            await _repositoryManager.WaterConsumption.GetWaterConsumptionById(userId,waterConsumptionId, trackChanges);
        if (waterConsumptionEntity is null)
        {
            throw new WaterConsumptionNotFoundException(waterConsumptionId);
        }

        return waterConsumptionEntity;
    }
    public async Task<(IEnumerable<ExpandoObject> data, MetaData metaData)> GetAllWaterConsumptionsAsync(WaterConsumptionParameters parameters,string userId,bool trackChanges)
    {
        var waterConsumptionsEntities = await _repositoryManager.WaterConsumption.GetAllWaterConsumptionsAsync(parameters,userId,trackChanges);
        var waterConsumptionsDtos = _mapper.Map<IEnumerable<WaterConsumptionDto>>(waterConsumptionsEntities);
        var pagedResult =
            PagedList<WaterConsumptionDto>.ToPagedList(waterConsumptionsDtos, parameters.PageNumber, parameters.PageSize);
        var shapedResult = _dataShaper.ShapeData(pagedResult,parameters.Fields);
        return (shapedResult,pagedResult.MetaData);
    }

    public async Task<(IEnumerable<ExpandoObject> data, MetaData metaData)> GetAllWaterConsumptionsByDateTimeAsync(WaterConsumptionParameters parameters, string userId,DateTime time,bool trackChanges)
    {
        var waterConsumptionsEntities = await _repositoryManager.WaterConsumption.GetAllWaterConsumptionsByDateAsync(parameters,userId,time,trackChanges);
        var waterConsumptionsDtos = _mapper.Map<IEnumerable<WaterConsumptionDto>>(waterConsumptionsEntities);
        var pagedResult =
            PagedList<WaterConsumptionDto>.ToPagedList(waterConsumptionsDtos, parameters.PageNumber, parameters.PageSize);
        var shapedResult = _dataShaper.ShapeData(pagedResult,parameters.Fields);
        return (shapedResult,pagedResult.MetaData);
    }

    public async Task<ExpandoObject> GetWaterConsumptionByIdAsync(WaterConsumptionParameters parameters,string userId,Guid waterConsumptionId, bool trackChanges)
    {
        var waterConsumptionEntity = await GetWaterConsumptionByIdIfExistsAsync(userId,waterConsumptionId, trackChanges);
        var waterConsumptionDto = _mapper.Map<WaterConsumptionDto>(waterConsumptionEntity);
        var shapedResult = _dataShaper.ShapeData(waterConsumptionDto,parameters.Fields);
        return shapedResult;
    }

    public async Task<WaterConsumptionDto> CreateWaterConsumption(string userId,CWaterConsumptionDto waterConsumptionDto)
    {
        var waterConsumptionEntity = _mapper.Map<WaterConsumption>(waterConsumptionDto);
        waterConsumptionEntity.UserId = userId;
        _repositoryManager.WaterConsumption.CreateWaterConsumption(waterConsumptionEntity);
        await _repositoryManager.SaveAsync();
        var waterConsumptionDtoForResult = _mapper.Map<WaterConsumptionDto>(waterConsumptionEntity);
        return waterConsumptionDtoForResult;
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