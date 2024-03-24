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

public class ProductConsumptionService:IProductConsumptionService
{
    private readonly IMapper _mapper;
    private readonly IRepositoryManager _repositoryManager;
    private readonly IDataShaper<ProductConsumptionDto> _dataShaper;

    public ProductConsumptionService(IMapper mapper, IRepositoryManager repositoryManager, IDataShaper<ProductConsumptionDto> dataShaper)
    {
        _mapper = mapper;
        _repositoryManager = repositoryManager;
        _dataShaper = dataShaper;
    }

    private async Task<ProductConsumption> GetProductConsumptionIfExists(string userId,Guid productConsumptionId,bool trackChanges)
    {
        var productConsumptionEntity =
            await _repositoryManager.ProductConsumption.GetProductConsumptionByIdAsync(userId, productConsumptionId,
                trackChanges);
        if (productConsumptionEntity is null)
        {
            throw new ProductConsumptionNotFoundException(productConsumptionId);
        }

        return productConsumptionEntity;
    }
    
    public async Task<(IEnumerable<ExpandoObject> data, MetaData metaData)> GetAllProductConsumptionAsync(ProductConsumptionParameters parameters, string userId, bool trackChanges)
    {
        var productConsumptionEntites =
            await _repositoryManager.ProductConsumption.GetAllProductConsumptionAsync(parameters, userId, trackChanges);
        var productConsumptionDtos = _mapper.Map<IEnumerable<ProductConsumptionDto>>(productConsumptionEntites);
        var pagedResult =
            PagedList<ProductConsumptionDto>.ToPagedList(productConsumptionDtos, parameters.PageNumber,
                parameters.PageSize);
        var shapedResult = _dataShaper.ShapeData(pagedResult, parameters.Fields);
        return (shapedResult, pagedResult.MetaData);
    }

    public async Task<(IEnumerable<ExpandoObject> data, MetaData metaData)> GetAllProductConsumptionByDateAsync(ProductConsumptionParameters parameters, string userId, DateTime time,
        bool trackChanges)
    {
        var productConsumptionEntites =
            await _repositoryManager.ProductConsumption.GetAllProductConsumptionByDateAsync(parameters, userId,time,trackChanges);
        var productConsumptionDtos = _mapper.Map<IEnumerable<ProductConsumptionDto>>(productConsumptionEntites);
        var pagedResult =
            PagedList<ProductConsumptionDto>.ToPagedList(productConsumptionDtos, parameters.PageNumber,
                parameters.PageSize);
        var shapedResult = _dataShaper.ShapeData(pagedResult, parameters.Fields);
        return (shapedResult, pagedResult.MetaData);
    }

    public async Task<ExpandoObject> GetProductConsumptionByIdAsync(ProductConsumptionParameters parameters, string userId, Guid productConsumptionId,
        bool trackChanges)
    {
        var productConsumptionEntity = await GetProductConsumptionIfExists(userId, productConsumptionId, trackChanges);
        var productConsumptionDto = _mapper.Map<ProductConsumptionDto>(productConsumptionEntity);
        var shapedResult = _dataShaper.ShapeData(productConsumptionDto, parameters.Fields);
        return shapedResult;
    }

    public async Task<ProductConsumptionDto> CreateProductConsumption(string userId, CProductConsumptionDto productConsumptionDto)
    {
        var productConsumptionEntity = _mapper.Map<ProductConsumption>(productConsumptionDto);
        productConsumptionEntity.UserId = userId;
        _repositoryManager.ProductConsumption.CreateProductConsumption(productConsumptionEntity);
        await _repositoryManager.SaveAsync();
        var productConsumptionDtoResult = _mapper.Map<ProductConsumptionDto>(productConsumptionEntity);
        return productConsumptionDtoResult;
    }

    public async Task UpdateProductConsumption(string userId, UProductConsumptionDto productConsumptionDto, Guid productConsumptionId)
    {
        var productConsumptionEntity =
            await GetProductConsumptionIfExists(userId, productConsumptionId, trackChanges: true);
        _mapper.Map(productConsumptionDto, productConsumptionEntity);
        await _repositoryManager.SaveAsync();
    }

    public async Task DeleteProductConsumption(string userId, Guid productConsumptionId)
    {
        var productConsumptionEntity =
            await GetProductConsumptionIfExists(userId, productConsumptionId, trackChanges: false);
        _repositoryManager.ProductConsumption.DeleteProductConsumption(productConsumptionEntity);
        await _repositoryManager.SaveAsync();
    }
}