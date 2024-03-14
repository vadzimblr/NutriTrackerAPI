using System.Dynamic;

namespace Contracts;

public interface IDataShaper<T>
{
    IEnumerable<ExpandoObject> ShapeData(IEnumerable<T> entities, string fields);
    ExpandoObject ShapeData(T entity, string fields);
}