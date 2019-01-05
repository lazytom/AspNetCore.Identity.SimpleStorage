namespace AspNetCore.Identity.SimpleStorage.Core
{
    using Newtonsoft.Json;

    public class IdentityRole : IdentityClaimStore
    {
        public IdentityRole()
        {
        }

        public IdentityRole(string roleName) : this()
        {
            Name = roleName;
        }

        [JsonProperty("id")]
        public virtual string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "normalizedName")]
        public string NormalizedName { get; set; }

        public override string ToString() => Name;
    }
}