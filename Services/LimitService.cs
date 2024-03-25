using AutoMapper;
using Contracts.RepositoryContracts;
using Contracts.ServiceContracts;
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
    public Task<ManipulationLimitDto> GetLimit(string userId, bool trackChanges)
    {
        throw new NotImplementedException();
    }

    public Task CreateLimit(ManipulationLimitDto limitDto)
    {
        throw new NotImplementedException();
    }
}