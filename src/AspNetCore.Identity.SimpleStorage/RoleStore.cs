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
    /// <typeparam name="TRole">Needs to extend the provided IdentityRole type.</typeparam>
    public class RoleStore<TRole> : IQueryableRoleStore<TRole>, IRoleClaimStore<TRole>
        where TRole : IdentityRole
    {
        private readonly IStorageProvider<TRole> storageProvider;

        public RoleStore(IStorageProvider<TRole> storageProvider)
        {
            this.storageProvider = storageProvider;
        }

        public virtual void Dispose()
        {
        }

        public virtual async Task<IdentityResult> CreateAsync(TRole role, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            if (string.IsNullOrEmpty(role.Id))
            {
                role.Id = Guid.NewGuid().ToString().ToLowerInvariant();
            }

            var roles = GetRoles();
            roles.Add(role);
            SaveRoles(roles);

            return IdentityResult.Success;
        }

        public virtual async Task<IdentityResult> UpdateAsync(TRole role, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            await DeleteAsync(role, token);
            await CreateAsync(role, token);

            return IdentityResult.Success;
        }

        public virtual async Task<IdentityResult> DeleteAsync(TRole role, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            var roles = GetRoles();
            var roleToDelete = roles.FirstOrDefault(r => r.Id == role.Id);
            if (roleToDelete != null)
            {
                roles.Remove(roleToDelete);
            }
            SaveRoles(roles);

            return IdentityResult.Success;
        }

        public virtual async Task<string> GetRoleIdAsync(TRole role, CancellationToken cancellationToken)
            => role.Id;

        public virtual async Task<string> GetRoleNameAsync(TRole role, CancellationToken cancellationToken)
            => role.Name;

        public virtual async Task SetRoleNameAsync(TRole role, string roleName, CancellationToken cancellationToken)
            => role.Name = roleName;

        public virtual async Task<string> GetNormalizedRoleNameAsync(TRole role, CancellationToken cancellationToken)
            => role.NormalizedName;

        public virtual async Task SetNormalizedRoleNameAsync(TRole role, string normalizedName, CancellationToken cancellationToken)
            => role.NormalizedName = normalizedName;

        public virtual async Task<TRole> FindByIdAsync(string roleId, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            return GetRoles().FirstOrDefault(r => r.Id.ToString().Equals(roleId));
        }

        public virtual async Task<TRole> FindByNameAsync(string normalizedName, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            return GetRoles().FirstOrDefault(r => r.NormalizedName.ToString().Equals(normalizedName));
        }

        /// <summary>
        /// Returns a list of all roles.
        /// </summary>
        public virtual IQueryable<TRole> Roles
        {
            get
            {
                return GetRoles().AsQueryable();
            }
        }

        public virtual async Task<IList<Claim>> GetClaimsAsync(TRole role, CancellationToken token)
            => role.Claims.Select(c => c.ToSecurityClaim()).ToList();

        public virtual Task AddClaimAsync(TRole role, Claim claim, CancellationToken token = default(CancellationToken))
        {
            role.AddClaim(claim);
            return Task.FromResult(0);
        }

        public virtual Task RemoveClaimAsync(TRole role, Claim claim, CancellationToken token = default(CancellationToken))
        {
            role.RemoveClaim(claim);
            return Task.FromResult(0);
        }

        #region "RoleRepository"
        private ICollection<TRole> GetRoles()
        {
            return storageProvider.LoadFromStorage<TRole>(); ;
        }

        private void SaveRoles(ICollection<TRole> roles)
        {
            storageProvider.SaveToStorage(roles);
        }
        #endregion

    }
}