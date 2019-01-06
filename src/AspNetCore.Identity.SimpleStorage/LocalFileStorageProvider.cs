using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace AspNetCore.Identity.SimpleStorage
{
    public class LocalFileStorageProvider<T> : IStorageProvider<T>
    {
        private readonly string filename;

        public LocalFileStorageProvider(string filename)
        {
            this.filename = filename;
        }

        public async Task<ICollection<T>> LoadFromStorageAsync<T>()
        {
            try
            {
                using (var reader = new StreamReader(new FileStream(filename, FileMode.Open)))
                {
                    string fileContents = await reader.ReadToEndAsync();
                    return JsonConvert.DeserializeObject<List<T>>(fileContents);
                }
            }
            catch (FileNotFoundException)
            {
                return new List<T>();
            }
        }

        public async Task<bool> SaveToStorageAsync<T>(ICollection<T> objects)
        {
            try
            {
                string serializedObjects = JsonConvert.SerializeObject(objects);
                using (var writer = new StreamWriter(new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write)))
                {
                    await writer.WriteAsync(serializedObjects);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
