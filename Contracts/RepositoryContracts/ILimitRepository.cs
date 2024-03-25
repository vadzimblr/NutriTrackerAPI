using Entities.Models;

namespace Contracts.RepositoryContracts;

public interface ILimitRepository
{
    Task<Limit> GetLimit(string userId, bool trackChanges);
    void CreateLimit(Limit limit);
}