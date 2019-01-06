using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Identity.SimpleStorage
{
    public class UserRepository<TUser>
        where TUser: IdentityUser
    {
        private const string UserRepositoryFilename = "users.json";

        public static ICollection<TUser> GetUsers()
        {
            return GetUsers(UserRepositoryFilename);
        }

        public static ICollection<TUser> GetUsers(string filename)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<TUser>>(File.ReadAllText(filename));
            }
            catch (FileNotFoundException)
            {
                return new List<TUser>();
            }
        }

        public static void SaveUsers(ICollection<TUser> users)
        {
            SaveUsers(users, UserRepositoryFilename);
        }

        public static void SaveUsers(ICollection<TUser> users, string filename)
        {
            File.WriteAllText(filename, JsonConvert.SerializeObject(users));
        }
    }
}
