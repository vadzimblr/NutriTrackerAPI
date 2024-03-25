using Contracts.RepositoryContracts;

namespace Repository;

public class RepositoryManager:IRepositoryManager
{
    
    private readonly RepositoryContext _context;
    private readonly Lazy<ProductRepository> _productRepository;
    private readonly Lazy<WaterConsumptionRepository> _waterConsumptionRepository;
    private readonly Lazy<ProductConsumptionRepository> _productConsumptionRepository;
    private readonly Lazy<LimitRepository> _limitRepository;
    public RepositoryManager(RepositoryContext context)
    {
        _context = context;
        _productRepository = new Lazy<ProductRepository>(() => new ProductRepository(context));
        _waterConsumptionRepository = new Lazy<WaterConsumptionRepository>(() => new WaterConsumptionRepository(context));
        _productConsumptionRepository =
            new Lazy<ProductConsumptionRepository>(() => new ProductConsumptionRepository(context));
        _limitRepository = new Lazy<LimitRepository>(() => new LimitRepository(context));
    }

    public IProductRepository Product => _productRepository.Value;
    public IWaterConsumptionRepository WaterConsumption => _waterConsumptionRepository.Value;
    public IProductConsumptionRepository ProductConsumption => _productConsumptionRepository.Value;
    public ILimitRepository Limit => _limitRepository.Value;
    public async Task SaveAsync() => await _context.SaveChangesAsync();
    
}