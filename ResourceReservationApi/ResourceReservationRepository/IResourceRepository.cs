using System;
using System.Collections.Generic;

namespace ResourceRepository
{
    public interface IResourceRepository<T>: IDisposable where T:class
    {
        void Add(T resource);
        void Update(T resource);
        void Delete(T resource);
        T GetById(long? id);
        IEnumerable<T> GetAll();
    }
}
