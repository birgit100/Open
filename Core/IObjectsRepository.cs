using System.Collections.Generic;
using System.Threading.Tasks;


namespace Open.Core
{
    public interface IObjectsRepository<T>
    {
        Task<T> GetObject(string id);
        Task<IEnumerable<T>> GetObjectsList();
        Task<T> AddObject(T o);
        void UpdateObject(T o);
        void DeleteObject(T o);
    }
}
