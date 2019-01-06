using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Identity.SimpleStorage
{
    public class RoleRepository<TRole>
        where TRole : IdentityRole
    {
        private const string RoleRepositoryFilename = "roles.json";

        public static ICollection<TRole> GetRoles()
        {
            return GetRoles(RoleRepositoryFilename);
        }

        public static ICollection<TRole> GetRoles(string filename)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<TRole>>(File.ReadAllText(filename));
            }
            catch (FileNotFoundException)
            {
                return new List<TRole>();
            }
        }

        public static void SaveRoles(ICollection<TRole> roles)
        {
            SaveRoles(roles, RoleRepositoryFilename);
        }

        public static void SaveRoles(ICollection<TRole> roles, string filename)
        {
            File.WriteAllText(filename, JsonConvert.SerializeObject(roles));
        }
    }
}
