using Contracts.RepositoryContracts;

namespace Repository;

public class RepositoryManager:IRepositoryManager
{
    
    private readonly RepositoryContext _context;
    public RepositoryManager(RepositoryContext context)
    {
        _context = context;
    }

    public async Task SaveAsync() => await _context.SaveChangesAsync();
    
}