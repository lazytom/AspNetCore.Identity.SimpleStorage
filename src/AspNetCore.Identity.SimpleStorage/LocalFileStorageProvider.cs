using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace AspNetCore.Identity.SimpleStorage
{
    public class LocalFileStorageProvider<T> : IStorageProvider<T>
    {
        private readonly string filename;

        public LocalFileStorageProvider(string filename)
        {
            this.filename = filename;
        }

        public ICollection<T> LoadFromStorage<T>()
        {
            try
            {
                return JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(filename));
            }
            catch (FileNotFoundException)
            {
                return new List<T>();
            }
        }

        public bool SaveToStorage<T>(ICollection<T> objects)
        {
            try
            {
                File.WriteAllText(filename, JsonConvert.SerializeObject(objects));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
