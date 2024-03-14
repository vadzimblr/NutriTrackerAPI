using System.Dynamic;
using System.Reflection;
using Contracts;

namespace Services;

public class DataShaper<T>: IDataShaper<T> where T: class
{
    public PropertyInfo [] Properties { get; set; }

    public DataShaper()
    {
        Properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
    }
    public IEnumerable<ExpandoObject> ShapeData(IEnumerable<T> entities, string fields)
    {
        var requiredProperties = GetRequiredProperties(fields);
        return FetchData(entities, requiredProperties);
    }

    public ExpandoObject ShapeData(T entity, string fields)
    {
        var requiredProperties = GetRequiredProperties(fields);
        return FetchDataForEntity(entity, requiredProperties);
    }

    public IEnumerable<PropertyInfo> GetRequiredProperties(string fields)
    {
        var requiredProperties = new List<PropertyInfo>();
       
        if (!string.IsNullOrWhiteSpace(fields))
        {
            var fieldsArray = fields.Split(',', StringSplitOptions.RemoveEmptyEntries);
            foreach (var field in fieldsArray)
            {
                var property = Properties.FirstOrDefault(p =>
                    p.Name.Equals(field, StringComparison.InvariantCultureIgnoreCase));
                if(property is null)
                    continue;
                requiredProperties.Add(property);
            }
        }
        else
        {
            return Properties.ToList();
        }

        return requiredProperties;
    }

    private ExpandoObject FetchDataForEntity(T entity, IEnumerable<PropertyInfo> requiredProperties)
    {
        var shapedObject = new ExpandoObject();
        foreach (var property in requiredProperties)
        {
            var objPropertyValue = property.GetValue(entity);
            shapedObject.TryAdd(property.Name, objPropertyValue);
        }

        return shapedObject;
    }
    private IEnumerable<ExpandoObject> FetchData(IEnumerable<T> entities, IEnumerable<PropertyInfo> requiredProperties)
    {
        var shapedObjects = new List<ExpandoObject>();
        foreach (var entity in entities)
        {
            var shapedObject = FetchDataForEntity(entity, requiredProperties);
            shapedObjects.Add(shapedObject);
        }
        return shapedObjects;
    }
}