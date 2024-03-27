using System.Dynamic;
using AutoMapper;
using Contracts;
using Contracts.RepositoryContracts;
using Contracts.ServiceContracts;
using Entities.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;
using Services;
using Shared.Dto.CreationResourcesDto;
using Shared.Dto.ResponseDto;
using Shared.Dto.UpdateResourcesDto;
using Shared.RequestFeatures;

namespace Tests;

public class ProductServiceTests
{
    private readonly Mock<IRepositoryManager> _repositoryManagerMock = new();
    private readonly Mock<IMapper> _mapperMock = new();
    private readonly Mock<UserManager<User>> _userManagerMock = new(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
    private readonly Mock<IDataShaper<ProductDto>> _dataShaperMock = new();

    private readonly IProductService _productService;

    public ProductServiceTests()
    {
        _productService = new ProductService(_repositoryManagerMock.Object,
            _mapperMock.Object,
            _userManagerMock.Object, _dataShaperMock.Object);
    }

    [Fact]
    public async Task GetAllProductsAsync_ShouldReturnProductsAsIEnumerableOfExpandoObjectAndMetaData()
    {
        // Arrange
        var parameters = new ProductParameters();
        var products = new List<Product> { new Product { Id = Guid.NewGuid() }, new Product { Id = Guid.NewGuid() } };
        var expectedResult = (products:products.Select(p => new ExpandoObject()),metaData: new MetaData());

        _repositoryManagerMock.Setup(repo => repo.Product.GetAllProductsAsync(parameters, false))
            .ReturnsAsync(products);
        _mapperMock.Setup(mapper => mapper.Map<IEnumerable<ProductDto>>(products))
            .Returns(products.Select(p => new ProductDto { Id = p.Id }));
        _dataShaperMock.Setup(shaper => shaper.ShapeData(It.IsAny<PagedList<ProductDto>>(), It.IsAny<string>()))
            .Returns(new List<ExpandoObject> { new ExpandoObject(),new ExpandoObject()});

        // Act
        var result = await _productService.GetAllProductsAsync(parameters, false);

        // Assert
        result.products.Should().HaveCount(expectedResult.products.Count());
        result.products.First().Should().BeOfType<ExpandoObject>();
        result.metaData.Should().BeOfType<MetaData>();
    }

    [Fact]
    public async Task GetProductByIdAsync_WithProductIdAndParameters_ShouldReturnProductAsExpandoObject()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var parameters = new ProductParameters();
        var product = new Product { Id = productId };
        var expectedResult = new ExpandoObject();

        _repositoryManagerMock.Setup(repo => repo.Product.GetProductByIdAsync(productId, false))
            .ReturnsAsync(product);
        _mapperMock.Setup(mapper => mapper.Map<ProductDto>(product))
            .Returns(new ProductDto { Id = productId });
        _dataShaperMock.Setup(shaper => shaper.ShapeData(It.IsAny<ProductDto>(), It.IsAny<string>()))
            .Returns(expectedResult);

        // Act
        var result = await _productService.GetProductByIdAsync(productId, parameters, false);

        // Assert
        result.Should().BeSameAs(expectedResult);
    }
    [Fact]
    public async Task CreateProductAsync_WithProductDtoAndUserId_ShouldReturnCreatedProductAsProductDto()
    {
        // Arrange
        
        var productDto = new CProductDto { ProductName = "Test Product" };
        var userId = "testUserId";
        var createdProduct = new Product { Id = Guid.NewGuid(), ProductName = productDto.ProductName };
        var resultProduuct = new ProductDto { Id = Guid.NewGuid(), ProductName = productDto.ProductName};
        _mapperMock.Setup(mapper => mapper.Map<Product>(productDto)).Returns(createdProduct);
        _mapperMock.Setup(mapper => mapper.Map<ProductDto>(createdProduct)).Returns(resultProduuct);
        _repositoryManagerMock.Setup(r => r.Product.CreateProduct(It.IsAny<Product>()));
        // Act
        var result = await _productService.CreateProductAsync(productDto, userId);

        // Assert
        result.Should().BeSameAs(resultProduuct);
        result.Should().NotBeNull();
        result.ProductName.Should().BeSameAs(productDto.ProductName);

    }

    
    [Fact]
    public async Task DeleteProductAsync_WithProductId_ShouldInvokeDeleteProductMethodOfRepoOnce()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var existingProduct = new Product { Id = productId };

        _repositoryManagerMock.Setup(repo => repo.Product.GetProductByIdAsync(productId, false))
            .ReturnsAsync(existingProduct);
        _repositoryManagerMock.Setup(repo => repo.Product.DeleteProduct(It.IsAny<Product>()));

        // Act
        await _productService.DeleteProductAsync(productId);

        // Assert
        _repositoryManagerMock.Verify(repo => repo.Product.DeleteProduct(existingProduct), Times.Once);
    }

    [Fact]
    public async Task GetProductForPatchAsync_WithProductId_ShouldNotBeNull()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var existingProduct = new Product { Id = productId };

        _repositoryManagerMock.Setup(repo => repo.Product.GetProductByIdAsync(productId, true))
            .ReturnsAsync(existingProduct);
        _mapperMock.Setup(mapper => mapper.Map<UProductDto>(existingProduct)).Returns(new UProductDto());

        // Act
        var result = await _productService.GetProductForPatchAsync(productId);

        // Assert
        result.productDto.Should().NotBeNull();
        result.productEntity.Should().NotBeNull();
    }

    
    [Fact]
    public async Task IsProductCreator_WithProductIdAndUserId_ShouldReturnTrue()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var userId = "testUserId";
        var existingProduct = new Product { Id = productId, UserId = userId };

        _repositoryManagerMock.Setup(repo => repo.Product.GetProductByIdAsync(productId, false))
            .ReturnsAsync(existingProduct);

        // Act
        var result = await _productService.IsProductCreator(productId, userId);

        // Assert
        result.Should().BeTrue();
    }
    
}