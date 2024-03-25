using Contracts.RepositoryContracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore.DynamicLinq;

namespace Repository;

public class LimitRepository:RepositoryBase<Limit>, ILimitRepository
{
    public LimitRepository(RepositoryContext context) : base(context)
    {
    }

    public async Task<Limit> GetLimit(string userId, bool trackChanges)
    {
        return await FindByCondition(l => l.UserId.Equals(userId),trackChanges).SingleOrDefaultAsync();
    }

    public void CreateLimit(Limit limit)
    {
        Create(limit);
    }
}