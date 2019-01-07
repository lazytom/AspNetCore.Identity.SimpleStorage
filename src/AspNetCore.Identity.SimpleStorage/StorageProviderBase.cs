using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.Identity.SimpleStorage
{
    public class StorageProviderBase
    {
        protected ICollection<T> Deserialize<T>(string fileContents)
        {
            return JsonConvert.DeserializeObject<List<T>>(fileContents);
        }

        protected string Serialize<T>(ICollection<T> objects)
        {
            return JsonConvert.SerializeObject(objects);
        }
    }
}
