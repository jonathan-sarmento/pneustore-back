using System.Collections.Generic;

namespace pneustoreAPI.Services
{
    public interface IService<T>
    {
        List<T> GetAll();
        
        T Get(int? id);

        bool Create(T objeto);
    }
}