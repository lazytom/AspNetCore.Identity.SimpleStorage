namespace AspNetCore.Identity.SimpleStorage
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    public class UserStore<TUser> :
            IUserPasswordStore<TUser>,
            IUserRoleStore<TUser>,
            IUserLoginStore<TUser>,
            IUserSecurityStampStore<TUser>,
            IUserEmailStore<TUser>,
            IUserClaimStore<TUser>,
            IUserPhoneNumberStore<TUser>,
            IUserTwoFactorStore<TUser>,
            IUserLockoutStore<TUser>,
            IQueryableUserStore<TUser>,
            IUserAuthenticationTokenStore<TUser>
        where TUser : IdentityUser
    {

        public UserStore() 
        {
        }

        public virtual void Dispose()
        {
        }

        public virtual async Task<IdentityResult> CreateAsync(TUser user, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            if(string.IsNullOrEmpty( user.Id))
            {
                user.Id = Guid.NewGuid().ToString().ToLowerInvariant();
            }

            var users = UserRepository<TUser>.GetUsers();
            users.Add(user);
            UserRepository<TUser>.SaveUsers(users);

            return IdentityResult.Success;
        }

        public virtual async Task<IdentityResult> UpdateAsync(TUser user, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            await DeleteAsync(user, token);
            await CreateAsync(user, token);

            return IdentityResult.Success;
        }

        public virtual async Task<IdentityResult> DeleteAsync(TUser user, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            var users = UserRepository<TUser>.GetUsers();
            var userToDelete = users.FirstOrDefault(u => u.Id == user.Id);
            if(userToDelete != null)
            {
                users.Remove(userToDelete);
            }
            UserRepository<TUser>.SaveUsers(users);

            return IdentityResult.Success;
        }

        public virtual async Task<string> GetUserIdAsync(TUser user, CancellationToken cancellationToken)
            => user.Id;

        public virtual async Task<string> GetUserNameAsync(TUser user, CancellationToken cancellationToken)
            => user.UserName;

        public virtual async Task SetUserNameAsync(TUser user, string userName, CancellationToken cancellationToken)
            => user.UserName = userName;

        public virtual async Task<string> GetNormalizedUserNameAsync(TUser user, CancellationToken cancellationToken)
            => user.NormalizedUserName;

        public virtual async Task SetNormalizedUserNameAsync(TUser user, string normalizedUserName, CancellationToken cancellationToken)
            => user.NormalizedUserName = normalizedUserName;

        public virtual async Task<TUser> FindByIdAsync(string userId, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            return UserRepository<TUser>.GetUsers().FirstOrDefault(u => u.Id.ToString().Equals(userId));
        }

        public virtual async Task<TUser> FindByNameAsync(string normalizedUserName, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            return UserRepository<TUser>.GetUsers().FirstOrDefault(u => u.NormalizedUserName.Equals(normalizedUserName));
        }

        public virtual async Task SetPasswordHashAsync(TUser user, string passwordHash, CancellationToken token)
            => user.PasswordHash = passwordHash;

        public virtual async Task<string> GetPasswordHashAsync(TUser user, CancellationToken token)
            => user.PasswordHash;

        public virtual async Task<bool> HasPasswordAsync(TUser user, CancellationToken token)
            => user.HasPassword();

        public virtual async Task AddToRoleAsync(TUser user, string normalizedRoleName, CancellationToken token)
            => user.AddRole(normalizedRoleName);

        public virtual async Task RemoveFromRoleAsync(TUser user, string normalizedRoleName, CancellationToken token)
            => user.RemoveRole(normalizedRoleName);

        public virtual async Task<IList<string>> GetRolesAsync(TUser user, CancellationToken token)
            => user.Roles;

        public virtual async Task<bool> IsInRoleAsync(TUser user, string normalizedRoleName, CancellationToken token)
            => user.Roles.Contains(normalizedRoleName);

        public virtual async Task<IList<TUser>> GetUsersInRoleAsync(string normalizedRoleName, CancellationToken token)
            => Users.Where(u => u.Roles.Contains(normalizedRoleName))
                .ToList();

        public virtual async Task AddLoginAsync(TUser user, UserLoginInfo login, CancellationToken token)
            => user.AddLogin(login);

        public virtual async Task RemoveLoginAsync(TUser user, string loginProvider, string providerKey, CancellationToken cancellationToken = default(CancellationToken))
            => user.RemoveLogin(loginProvider, providerKey);

        public virtual async Task<IList<UserLoginInfo>> GetLoginsAsync(TUser user, CancellationToken token)
            => user.Logins
                .Select(l => l.ToUserLoginInfo())
                .ToList();

        public virtual async Task<TUser> FindByLoginAsync(
            string loginProvider, string providerKey, CancellationToken cancellationToken = default(CancellationToken))
        {
            
            return Users
                .SelectMany(u => u.Logins.Where(l => l.LoginProvider == loginProvider && l.ProviderKey == providerKey).Select(u2 => u))
                .ToList().FirstOrDefault();
        }
        public virtual async Task SetSecurityStampAsync(TUser user, string stamp, CancellationToken token)
            => user.SecurityStamp = stamp;

        public virtual async Task<string> GetSecurityStampAsync(TUser user, CancellationToken token)
            => user.SecurityStamp;

        public virtual async Task<bool> GetEmailConfirmedAsync(TUser user, CancellationToken token)
            => user.EmailConfirmed;

        public virtual async Task SetEmailConfirmedAsync(TUser user, bool confirmed, CancellationToken token)
            => user.EmailConfirmed = confirmed;

        public virtual async Task SetEmailAsync(TUser user, string email, CancellationToken token)
            => user.Email = email;

        public virtual async Task<string> GetEmailAsync(TUser user, CancellationToken token)
            => user.Email;

        public virtual async Task<string> GetNormalizedEmailAsync(TUser user, CancellationToken cancellationToken)
            => user.NormalizedEmail;

        public virtual async Task SetNormalizedEmailAsync(TUser user, string normalizedEmail, CancellationToken cancellationToken)
            => user.NormalizedEmail = normalizedEmail;

        public virtual async Task<TUser> FindByEmailAsync(string normalizedEmail, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            return UserRepository<TUser>.GetUsers().FirstOrDefault(u => u.NormalizedEmail.Equals(normalizedEmail));
        }

        public virtual async Task<IList<Claim>> GetClaimsAsync(TUser user, CancellationToken token)
            => user.Claims.Select(c => c.ToSecurityClaim()).ToList();

        public virtual Task AddClaimsAsync(TUser user, IEnumerable<Claim> claims, CancellationToken token)
        {
            foreach (var claim in claims)
            {
                user.AddClaim(claim);
            }
            return Task.FromResult(0);
        }

        public virtual Task RemoveClaimsAsync(TUser user, IEnumerable<Claim> claims, CancellationToken token)
        {
            foreach (var claim in claims)
            {
                user.RemoveClaim(claim);
            }
            return Task.FromResult(0);
        }

        public virtual async Task ReplaceClaimAsync(TUser user, Claim claim, Claim newClaim, CancellationToken cancellationToken = default(CancellationToken))
        {
            user.ReplaceClaim(claim, newClaim);
        }

        public virtual Task SetPhoneNumberAsync(TUser user, string phoneNumber, CancellationToken token)
        {
            user.PhoneNumber = phoneNumber;
            return Task.FromResult(0);
        }

        public virtual Task<string> GetPhoneNumberAsync(TUser user, CancellationToken token)
        {
            return Task.FromResult(user.PhoneNumber);
        }

        public virtual Task<bool> GetPhoneNumberConfirmedAsync(TUser user, CancellationToken token)
        {
            return Task.FromResult(user.PhoneNumberConfirmed);
        }

        public virtual Task SetPhoneNumberConfirmedAsync(TUser user, bool confirmed, CancellationToken token)
        {
            user.PhoneNumberConfirmed = confirmed;
            return Task.FromResult(0);
        }

        public virtual Task SetTwoFactorEnabledAsync(TUser user, bool enabled, CancellationToken token)
        {
            user.TwoFactorEnabled = enabled;
            return Task.FromResult(0);
        }

        public virtual Task<bool> GetTwoFactorEnabledAsync(TUser user, CancellationToken token)
        {
            return Task.FromResult(user.TwoFactorEnabled);
        }

        public virtual async Task<IList<TUser>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Users
                .SelectMany(u => u.Claims.Where(c => c.Type == claim.Type && c.Value == claim.Value).Select(u2 => u))
                .ToList();
        }

        public virtual Task<DateTimeOffset?> GetLockoutEndDateAsync(TUser user, CancellationToken token)
        {
            DateTimeOffset? dateTimeOffset = user.LockoutEndDateUtc;
            return Task.FromResult(dateTimeOffset);
        }

        public virtual Task SetLockoutEndDateAsync(TUser user, DateTimeOffset? lockoutEnd, CancellationToken token)
        {
            user.LockoutEndDateUtc = lockoutEnd?.UtcDateTime;
            return Task.FromResult(0);
        }

        public virtual Task<int> IncrementAccessFailedCountAsync(TUser user, CancellationToken token)
        {
            user.AccessFailedCount++;
            return Task.FromResult(user.AccessFailedCount);
        }

        public virtual Task ResetAccessFailedCountAsync(TUser user, CancellationToken token)
        {
            user.AccessFailedCount = 0;
            return Task.FromResult(0);
        }

        public virtual async Task<int> GetAccessFailedCountAsync(TUser user, CancellationToken token)
            => user.AccessFailedCount;

        public virtual async Task<bool> GetLockoutEnabledAsync(TUser user, CancellationToken token)
            => user.LockoutEnabled;

        public virtual async Task SetLockoutEnabledAsync(TUser user, bool enabled, CancellationToken token)
            => user.LockoutEnabled = enabled;

        /// <summary>
        /// Returns a list of all users.
        /// </summary>
        public virtual IQueryable<TUser> Users
        {
            get
            {
                return UserRepository<TUser>.GetUsers().AsQueryable();
            }
        }
            

        public virtual async Task SetTokenAsync(TUser user, string loginProvider, string name, string value, CancellationToken cancellationToken)
            => user.SetToken(loginProvider, name, value);

        public virtual async Task RemoveTokenAsync(TUser user, string loginProvider, string name, CancellationToken cancellationToken)
            => user.RemoveToken(loginProvider, name);

        public virtual async Task<string> GetTokenAsync(TUser user, string loginProvider, string name, CancellationToken cancellationToken)
            => user.GetTokenValue(loginProvider, name);

        
    }
}