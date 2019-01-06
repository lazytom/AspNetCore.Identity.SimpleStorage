using Newtonsoft.Json;

namespace AspNetCore.Identity.SimpleStorage
{
    /// <summary>
    ///     Authentication token associated with a user
    /// </summary>
    public class IdentityUserToken
    {
        public IdentityUserToken()
        {
        }

        /// <summary>
        /// The provider that the token came from.
        /// </summary>
        [JsonProperty(PropertyName = "loginProvider")]
        public string LoginProvider { get; set; }

        /// <summary>
        /// The name of the token.
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// The value of the token.
        /// </summary>
        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }
    }
}