namespace Contracts.RepositoryContracts;

public interface IRepositoryManager
{
    IProductRepository Product { get; }
    Task SaveAsync();
}