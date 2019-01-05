namespace AspNetCore.Identity.SimpleStorage.Core
{
    using Microsoft.AspNetCore.Identity;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class IdentityUser : IdentityClaimStore
    {
        public IdentityUser()
        {
            Roles = new List<string>();
            Logins = new List<IdentityUserLogin>();
            Tokens = new List<IdentityUserToken>();
        }


        [JsonProperty("id")]
        public virtual string Id { get; set; }

        [JsonProperty(PropertyName = "userName")]
        public virtual string UserName { get; set; }

        [JsonProperty(PropertyName = "normalizedUserName")]
        public virtual string NormalizedUserName { get; set; }

        /// <summary>
        ///     A random value that must change whenever a users credentials change
        ///     (password changed, login removed)
        /// </summary>
        [JsonProperty(PropertyName = "securityStamp")]
        public virtual string SecurityStamp { get; set; }

        [JsonProperty(PropertyName = "email")]
        public virtual string Email { get; set; }

        [JsonProperty(PropertyName = "normalizedEmail")]
        public virtual string NormalizedEmail { get; set; }

        [JsonProperty(PropertyName = "emailConfirmed")]
        public virtual bool EmailConfirmed { get; set; }

        [JsonProperty(PropertyName = "phoneNumber")]
        public virtual string PhoneNumber { get; set; }

        [JsonProperty(PropertyName = "phoneNumberConfirmed")]
        public virtual bool PhoneNumberConfirmed { get; set; }

        [JsonProperty(PropertyName = "twoFactorEnabled")]
        public virtual bool TwoFactorEnabled { get; set; }

        [JsonProperty(PropertyName = "lockoutEndDateUtc")]
        public virtual DateTime? LockoutEndDateUtc { get; set; }

        [JsonProperty(PropertyName = "lockoutEnabled")]
        public virtual bool LockoutEnabled { get; set; }

        [JsonProperty(PropertyName = "accessFailedCount")]
        public virtual int AccessFailedCount { get; set; }

        [JsonProperty(PropertyName = "roles", NullValueHandling = NullValueHandling.Ignore)]
        public virtual List<string> Roles { get; set; }

        public virtual void AddRole(string role)
        {
            Roles.Add(role);
        }

        public virtual void RemoveRole(string role)
        {
            Roles.Remove(role);
        }

        [JsonProperty(PropertyName = "passwordHash", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string PasswordHash { get; set; }

        [JsonProperty(PropertyName = "logins", NullValueHandling = NullValueHandling.Ignore)]
        public virtual List<IdentityUserLogin> Logins { get; set; }

        public virtual void AddLogin(UserLoginInfo login)
        {
            Logins.Add(new IdentityUserLogin(login));
        }

        public virtual void RemoveLogin(string loginProvider, string providerKey)
        {
            Logins.RemoveAll(l => l.LoginProvider == loginProvider && l.ProviderKey == providerKey);
        }

        public virtual bool HasPassword()
        {
            return false;
        }
        
        [JsonProperty(PropertyName = "tokens", NullValueHandling = NullValueHandling.Ignore)]
        public virtual List<IdentityUserToken> Tokens { get; set; }

        private IdentityUserToken GetToken(string loginProider, string name)
            => Tokens
                .FirstOrDefault(t => t.LoginProvider == loginProider && t.Name == name);

        public virtual void SetToken(string loginProider, string name, string value)
        {
            var existingToken = GetToken(loginProider, name);
            if (existingToken != null)
            {
                existingToken.Value = value;
                return;
            }

            Tokens.Add(new IdentityUserToken
            {
                LoginProvider = loginProider,
                Name = name,
                Value = value
            });
        }

        public virtual string GetTokenValue(string loginProider, string name)
        {
            return GetToken(loginProider, name)?.Value;
        }

        public virtual void RemoveToken(string loginProvider, string name)
        {
            Tokens.RemoveAll(t => t.LoginProvider == loginProvider && t.Name == name);
        }

        public override string ToString() => UserName;
    }
}