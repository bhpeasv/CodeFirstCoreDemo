using System;
using System.Collections.Generic;
using System.Text;

namespace DataModel.Interfaces
{
    public interface IRepository<K, T>
    {
        T Add(T item);
        void Update(T item);
        void Remove(T item);
        IList<T> GetAll();
        T GetById(K id);
    }
}
