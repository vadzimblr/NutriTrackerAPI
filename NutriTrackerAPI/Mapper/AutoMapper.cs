using AutoMapper;
using Entities.Models;
using Shared.Dto;
using Shared.Dto.CreationResourcesDto;
using Shared.Dto.ResponseDto;
using Shared.Dto.UpdateResourcesDto;

namespace NutriTrackerAPI.Mapper;

public class AutoMapper:Profile
{
    public AutoMapper()
    {
        CreateMap<UserRegistrationDto, User>();
        CreateMap<Product, ProductDto>();
        CreateMap<CProductDto, Product>();
        CreateMap<UProductDto, Product>().ReverseMap();
        CreateMap<WaterConsumption, WaterConsumptionDto>();
        CreateMap<CWaterConsumptionDto, WaterConsumption>();
        CreateMap<UWaterConsumptionDto, WaterConsumption>().ReverseMap();
    }
}