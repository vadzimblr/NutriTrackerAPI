using Shared.Dto.UpdateResourcesDto;

namespace Contracts.ServiceContracts;

public interface ILimitService
{
    Task<ManipulationLimitDto> GetLimit(string userId, bool trackChanges);
    Task CreateLimit(ManipulationLimitDto limitDto);
}