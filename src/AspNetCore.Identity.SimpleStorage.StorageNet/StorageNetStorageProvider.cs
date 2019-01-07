using Storage.Net.Blob;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.Identity.SimpleStorage.StorageNet
{
    public class StorageNetStorageProvider<T> : StorageProviderBase, IStorageProvider<T>
    {
        private readonly IBlobStorage storage;
        private readonly string storageItemId;

        public StorageNetStorageProvider(IBlobStorage storage, string storageItemId)
        {
            this.storage = storage;
            this.storageItemId = storageItemId;
        }

        public async Task<ICollection<T>> LoadFromStorageAsync<T>()
        {
            string fileContents = await storage.ReadTextAsync(storageItemId);
            if (!string.IsNullOrEmpty(fileContents))
            {
                return Deserialize<T>(fileContents);
            }
            return new List<T>();
        }

        public async Task<bool> SaveToStorageAsync<T>(ICollection<T> objects)
        {
            try
            {
                string serializedObjects = Serialize<T>(objects);
                await storage.WriteTextAsync(storageItemId, serializedObjects);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
