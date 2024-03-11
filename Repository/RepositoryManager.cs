using Contracts.RepositoryContracts;

namespace Repository;

public class RepositoryManager:IRepositoryManager
{
    
    private readonly RepositoryContext _context;
    private readonly Lazy<ProductRepository> _productRepository;
    public RepositoryManager(RepositoryContext context)
    {
        _context = context;
        _productRepository = new Lazy<ProductRepository>(() => new ProductRepository(context));
    }

    public IProductRepository Product => _productRepository.Value;
    public async Task SaveAsync() => await _context.SaveChangesAsync();
    
}