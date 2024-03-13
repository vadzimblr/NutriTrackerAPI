using Entities.Models;

namespace Repository.Extensions;

public static class ProductRepositoryExtension
{
    public static IQueryable<Product> FilterProductsByProtein(this IQueryable<Product> products, double minProtein, double maxProtein)
    {
        return products.Where(p => (p.Protein >= minProtein && p.Protein <= maxProtein));
    }
    public static IQueryable<Product> FilterProductsByCarbs(this IQueryable<Product> products, double minCarbs, double maxCarbs)
    {
        return products.Where(p => (p.Carbs >= minCarbs && p.Carbs <= maxCarbs));
    }
    public static IQueryable<Product> FilterProductsByFat(this IQueryable<Product> products, double minFat, double maxFat)
    {
        return products.Where(p => (p.Fat >= minFat && p.Fat <= maxFat));
    }
    public static IQueryable<Product> FilterProductsByCalories(this IQueryable<Product> products, double minCalories, double maxCalories)
    {
        return products.Where(p => (p.Fat >= minCalories && p.Fat <= maxCalories));
    }
    public static IQueryable<Product> FilterProductsByServingSize(this IQueryable<Product> products, double minServingSize, double maxServingSize)
    {
        return products.Where(p => (p.Fat >= minServingSize && p.Fat <= maxServingSize));
    }

    public static IQueryable<Product> Search(this IQueryable<Product> products, string? searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return products;
        }
        var searchTermToLowerCase = searchTerm.TrimStart().TrimEnd().ToLower();
        return products.Where(p => p.ProductName.TrimStart().TrimEnd().ToLower().Contains(searchTermToLowerCase));
    }
}