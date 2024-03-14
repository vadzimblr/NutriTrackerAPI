using System.Reflection;
using System.Text;

namespace Repository.Utils;

public static class OrderQueryBuilder
{
    public static string CreateOrderQuery<T>(string orderByQueryString)
    {
        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var parameters = orderByQueryString.Trim().Split(',');
        var queryBuilder = new StringBuilder();
        foreach (var parameter in parameters)
        {
            if(string.IsNullOrWhiteSpace(parameter))
                continue;
            var propertyName = parameter.Split(" ")[0];
            var objectProperty = properties.FirstOrDefault(p =>
                p.Name.Equals(propertyName, StringComparison.InvariantCultureIgnoreCase));
            if(objectProperty is null)
                continue;
            var direction = parameter.EndsWith(" desc") ? "descending" : "ascending";
            queryBuilder.Append($"{objectProperty.Name.ToString()} {direction}, ");
        }

        var orderQuery = queryBuilder.ToString().Trim(',', ' ');
        return orderQuery;
    }
}