using System.Dynamic;
using AutoMapper;
using Contracts;
using Contracts.RepositoryContracts;
using Contracts.ServiceContracts;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Shared.Dto.CreationResourcesDto;
using Shared.Dto.ResponseDto;
using Shared.Dto.UpdateResourcesDto;
using Shared.RequestFeatures;

namespace Services;

public class ProductService:IProductService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly IDataShaper<ProductDto> _dataShaper;
    private async Task<Product> GetProductIfExists(Guid productId, bool trackChanges)
    {
        var result = await _repositoryManager.Product.GetProductByIdAsync(productId, trackChanges);
        if (result is null)
        {
            throw new ProductNotFoundException(productId);
        }

        return result;
    }
    
    public ProductService(IRepositoryManager repositoryManager, IMapper mapper, UserManager<User> userManager, IDataShaper<ProductDto> dataShaper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
        _userManager = userManager;
        _dataShaper = dataShaper;
    }
    public async Task<(IEnumerable<ExpandoObject> products,MetaData metaData)> GetAllProductsAsync(ProductParameters parameters,bool trackChanges)
    {
        var result = await _repositoryManager.Product.GetAllProductsAsync(parameters,trackChanges);
        var resultDtos = _mapper.Map<IEnumerable<ProductDto>>(result);
        var resultPagedList = PagedList<ProductDto>.ToPagedList(resultDtos,parameters.PageNumber,parameters.PageSize);
        var shapedData = _dataShaper.ShapeData(resultPagedList, parameters.Fields);
        return (shapedData,resultPagedList.MetaData);
    }

    public async Task<ExpandoObject> GetProductByIdAsync(Guid productId, ProductParameters parameters, bool trackChanges)
    {
        var result = await GetProductIfExists(productId, trackChanges);
        var resultDto = _mapper.Map<ProductDto>(result);
        var shapedData = _dataShaper.ShapeData(resultDto,parameters.Fields);
        return shapedData;
    }

    public async Task<ProductDto> CreateProductAsync(CProductDto productDto, string userId)
    {
        var productEntity = _mapper.Map<Product>(productDto);
        productEntity.UserId = userId;
        _repositoryManager.Product.CreateProduct(productEntity);
        await _repositoryManager.SaveAsync();
        var productResponseDto = _mapper.Map<ProductDto>(productEntity);
        return productResponseDto;
    }

    public async Task UpdateProductAsync(Guid productId, UProductDto productDto)
    {
        var result = await GetProductIfExists(productId, trackChanges: true);
        _mapper.Map(productDto, result);
        await _repositoryManager.SaveAsync();
    }

    public async Task DeleteProductAsync(Guid productId)
    {
        var result = await _repositoryManager.Product.GetProductByIdAsync(productId, false);
        _repositoryManager.Product.DeleteProduct(result);
        await _repositoryManager.SaveAsync();
    }

    public async Task<(Product productEntity, UProductDto productDto)> GetProductForPatchAsync(Guid productId)
    {
        var productEntity = await GetProductIfExists(productId, trackChanges: true);
        var productDto = _mapper.Map<UProductDto>(productEntity);
        return (productEntity, productDto);
    }

    public async Task SavePatchedProductAsync(Product productEntity, UProductDto productDto)
    {
        _mapper.Map(productDto, productEntity);
        await _repositoryManager.SaveAsync();
    }

    public async Task<bool> IsProductCreator(Guid productId, string userId)
    {
        var productEntity = await GetProductIfExists(productId, trackChanges: false);
        return (productEntity.UserId.Equals(userId));
    }
}