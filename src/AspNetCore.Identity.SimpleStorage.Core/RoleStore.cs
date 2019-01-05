
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
// I'm using async methods to leverage implicit Task wrapping of results from expression bodied functions.

namespace AspNetCore.Identity.SimpleStorage.Core
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    ///     Note: Deleting and updating do not modify the roles stored on a user document. If you desire this dynamic
    ///     capability, override the appropriate operations on RoleStore as desired for your application. For example you could
    ///     perform a document modification on the users collection before a delete or a rename.
    ///     When passing a cancellation token, it will only be used if the operation requires a database interaction.
    /// </summary>
    /// <typeparam name="TRole">Needs to extend the provided IdentityRole type.</typeparam>
    public class RoleStore<TRole> : IQueryableRoleStore<TRole>, IRoleClaimStore<TRole>
        where TRole : IdentityRole
    {
        public RoleStore()
        {
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

            var roles = RoleRepository<TRole>.GetRoles();
            roles.Add(role);
            RoleRepository<TRole>.SaveRoles(roles);

            return IdentityResult.Success;
        }

        public virtual async Task<IdentityResult> UpdateAsync(TRole role, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            await DeleteAsync(role, token);
            await CreateAsync(role, token);

            // todo low priority result based on replace result
            return IdentityResult.Success;
        }

        public virtual async Task<IdentityResult> DeleteAsync(TRole role, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            var roles = RoleRepository<TRole>.GetRoles();
            var roleToDelete = roles.FirstOrDefault(r => r.Id == role.Id);
            if (roleToDelete != null)
            {
                roles.Remove(roleToDelete);
            }
            RoleRepository<TRole>.SaveRoles(roles);

            return IdentityResult.Success;
        }

        public virtual async Task<string> GetRoleIdAsync(TRole role, CancellationToken cancellationToken)
            => role.Id;

        public virtual async Task<string> GetRoleNameAsync(TRole role, CancellationToken cancellationToken)
            => role.Name;

        public virtual async Task SetRoleNameAsync(TRole role, string roleName, CancellationToken cancellationToken)
            => role.Name = roleName;

        // note: can't test as of yet through integration testing because the Identity framework doesn't use this method internally anywhere
        public virtual async Task<string> GetNormalizedRoleNameAsync(TRole role, CancellationToken cancellationToken)
            => role.NormalizedName;

        public virtual async Task SetNormalizedRoleNameAsync(TRole role, string normalizedName, CancellationToken cancellationToken)
            => role.NormalizedName = normalizedName;

        public virtual async Task<TRole> FindByIdAsync(string roleId, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            return RoleRepository<TRole>.GetRoles().FirstOrDefault(r => r.Id.ToString().Equals(roleId));
        }

        public virtual async Task<TRole> FindByNameAsync(string normalizedName, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            return RoleRepository<TRole>.GetRoles().FirstOrDefault(r => r.NormalizedName.ToString().Equals(normalizedName));
        }

        /// <summary>
        /// Returns a list of all roles.
        /// Avoid using this property whenever possible.
        /// The cross-partition database request resulting from this will be very expensive.
        /// </summary>
        public virtual IQueryable<TRole> Roles
        {
            get
            {
                return RoleRepository<TRole>.GetRoles().AsQueryable();
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
    }
}