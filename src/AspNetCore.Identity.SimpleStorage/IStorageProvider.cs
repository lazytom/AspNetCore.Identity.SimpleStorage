using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore.Identity.SimpleStorage
{
    public interface IStorageProvider<T>
    {
        ICollection<T> LoadFromStorage<T>();
        bool SaveToStorage<T>(ICollection<T> objects);
    }
}
