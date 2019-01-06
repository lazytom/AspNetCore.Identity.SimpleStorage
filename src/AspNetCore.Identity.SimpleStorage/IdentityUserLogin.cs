using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace AspNetCore.Identity.SimpleStorage
{
    public class IdentityUserLogin
    {
        private IdentityUserLogin()
        {
        }

        public IdentityUserLogin(string loginProvider, string providerKey, string providerDisplayName)
        {
            LoginProvider = loginProvider;
            ProviderDisplayName = providerDisplayName;
            ProviderKey = providerKey;
        }

        public IdentityUserLogin(UserLoginInfo login)
        {
            LoginProvider = login.LoginProvider;
            ProviderDisplayName = login.ProviderDisplayName;
            ProviderKey = login.ProviderKey;
        }

        [JsonProperty(PropertyName = "loginProvider")]
        public string LoginProvider { get; set; }
        [JsonProperty(PropertyName = "providerDisplayName")]
        public string ProviderDisplayName { get; set; }
        [JsonProperty(PropertyName = "providerKey")]
        public string ProviderKey { get; set; }

        public UserLoginInfo ToUserLoginInfo()
        {
            return new UserLoginInfo(LoginProvider, ProviderKey, ProviderDisplayName);
        }
    }
}