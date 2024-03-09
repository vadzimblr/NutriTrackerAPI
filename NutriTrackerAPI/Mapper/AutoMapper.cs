using AutoMapper;
using Entities.Models;
using Shared.Dto;

namespace NutriTrackerAPI.Mapper;

public class AutoMapper:Profile
{
    public AutoMapper()
    {
        CreateMap<UserRegistrationDto, User>();
    }
}