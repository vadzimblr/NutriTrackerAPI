using AutoMapper;
using Contracts.RepositoryContracts;
using Contracts.ServiceContracts;
using Entities.Exceptions;
using Entities.Models;
using Shared.Dto.UpdateResourcesDto;

namespace Services;

public class LimitService : ILimitService
{
    private readonly IMapper _mapper;
    private readonly IRepositoryManager _repositoryManager;

    public LimitService(IMapper mapper, IRepositoryManager repositoryManager)
    {
        _mapper = mapper;
        _repositoryManager = repositoryManager;
    }
    public async Task<ManipulationLimitDto> GetLimit(string userId, bool trackChanges)
    {
        var limitEntity = await _repositoryManager.Limit.GetLimit(userId, trackChanges);
        if (limitEntity is null)
        {
            throw new LimitNotFoundException();
        }

        var limitDto = _mapper.Map<ManipulationLimitDto>(limitEntity);
        return limitDto;
    }

    public async Task CreateLimit(string userId,ManipulationLimitDto limitDto)
    {
        var limitEntity = _mapper.Map<Limit>(limitDto);
        limitEntity.UserId = userId;
         _repositoryManager.Limit.CreateLimit(limitEntity);
         await _repositoryManager.SaveAsync();
    }
}