using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.Identity.SimpleStorage
{
    public interface IStorageProvider<T>
    {
        Task<ICollection<T>> LoadFromStorageAsync<T>();
        Task<bool> SaveToStorageAsync<T>(ICollection<T> objects);
    }
}
