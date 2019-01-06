namespace AspNetCore.Identity.SimpleStorage
{
    using Newtonsoft.Json;
    using System.Security.Claims;

    /// <summary>
    /// A claim that a user possesses.
    /// </summary>
    public class IdentityClaim
    {
        public IdentityClaim()
        {
        }

        public IdentityClaim(Claim claim)
        {
            Type = claim.Type;
            Value = claim.Value;
        }

        /// <summary>
        /// Claim type
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        /// <summary>
        /// Claim value
        /// </summary>
        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }

        public Claim ToSecurityClaim()
        {
            return new Claim(Type, Value);
        }
    }
}