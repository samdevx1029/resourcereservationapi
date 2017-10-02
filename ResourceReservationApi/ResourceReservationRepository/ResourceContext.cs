using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace ResourceRepository
{
    public class ResourceContext<T> : IResourceRepository<T> where T : class
    {
        public void Add(T resource)
        {
            throw new NotImplementedException();
        }

        public void Delete(T resource)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public T GetById(long? id)
        {
            throw new NotImplementedException();
        }

        public void Update(T resource)
        {
            throw new NotImplementedException();
        }
    }
}
