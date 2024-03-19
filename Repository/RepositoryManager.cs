using Contracts.RepositoryContracts;

namespace Repository;

public class RepositoryManager:IRepositoryManager
{
    
    private readonly RepositoryContext _context;
    private readonly Lazy<ProductRepository> _productRepository;
    private readonly Lazy<WaterConsumptionRepository> _waterConsumptionRepository;
    public RepositoryManager(RepositoryContext context)
    {
        _context = context;
        _productRepository = new Lazy<ProductRepository>(() => new ProductRepository(context));
        _waterConsumptionRepository = new Lazy<WaterConsumptionRepository>(() => new WaterConsumptionRepository(context));
    }

    public IProductRepository Product => _productRepository.Value;
    public IWaterConsumptionRepository WaterConsumption => _waterConsumptionRepository.Value;
    public async Task SaveAsync() => await _context.SaveChangesAsync();
    
}