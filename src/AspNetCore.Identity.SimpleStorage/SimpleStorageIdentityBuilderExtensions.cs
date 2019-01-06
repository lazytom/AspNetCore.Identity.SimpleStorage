using System.Diagnostics.CodeAnalysis;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using AspNetCore.Identity.SimpleStorage;
using IdentityRole = AspNetCore.Identity.SimpleStorage.IdentityRole;
using IdentityUser = AspNetCore.Identity.SimpleStorage.IdentityUser;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SimpleStorageIdentityBuilderExtensions
    {
        /// <summary>
        ///     If you want control over creating the users and roles collections, use this overload.
        ///     This method only registers the storage stores, you also need to call AddIdentity.
        /// </summary>
        /// <typeparam name="TUser"></typeparam>
        /// <typeparam name="TRole"></typeparam>
        /// <param name="builder"></param>
        public static IdentityBuilder AddSimpleStorageStores<TUser, TRole>(
            this IdentityBuilder builder)
            where TRole : IdentityRole
            where TUser : IdentityUser
        {
            if (typeof(TUser) != builder.UserType)
            {
                var message = "User type passed to AddSimpleStorageStores must match user type passed to AddIdentity. "
                              + $"You passed {builder.UserType} to AddIdentity and {typeof(TUser)} to AddSimpleStorageStores, "
                              + "these do not match.";
                throw new ArgumentException(message);
            }
            if (typeof(TRole) != builder.RoleType)
            {
                var message = "Role type passed to AddSimpleStorageStores must match role type passed to AddIdentity. "
                              + $"You passed {builder.RoleType} to AddIdentity and {typeof(TRole)} to AddSimpleStorageStores, "
                              + "these do not match.";
                throw new ArgumentException(message);
            }
            builder.Services.AddSingleton<IUserStore<TUser>>(p => new UserStore<TUser>());
            builder.Services.AddSingleton<IRoleStore<TRole>>(p => new RoleStore<TRole>());

            return builder;
        }

        /// <summary>
        ///     If you want control over creating the users and roles collections, use this overload.
        ///     This method only registers the storage stores, you also need to call AddIdentity.
        /// </summary>
        /// <typeparam name="TUser"></typeparam>
        /// <typeparam name="TRole"></typeparam>
        /// <param name="builder"></param>
        public static IdentityBuilder AddSimpleStorageStores<TUser>(
            this IdentityBuilder builder)
            where TUser : IdentityUser
        {
            if (typeof(TUser) != builder.UserType)
            {
                var message = "User type passed to AddSimpleStorageStores must match user type passed to AddIdentity. "
                              + $"You passed {builder.UserType} to AddIdentity and {typeof(TUser)} to AddSimpleStorageStores, "
                              + "these do not match.";
                throw new ArgumentException(message);
            }

            builder.Services.AddSingleton<IUserStore<TUser>>(p => new UserStore<TUser>());

            return builder;
        }


        /// <summary>
        ///     This method registers identity services and storage stores using the IdentityUser and IdentityRole types.
        /// </summary>
        /// <param name="service">The <see cref="IdentityBuilder"/> to build upon.</param>
        /// <param name="identityOptions">The identity options used when calling AddIdentity.</param>
        /// <returns>The <see cref="IdentityBuilder"/> with the storage settings applied.</returns>
        public static IdentityBuilder AddIdentityWithSimpleStorageStores(
            this IServiceCollection service,
            Action<IdentityOptions> identityOptions = null)
        {
            return service.AddIdentityWithSimpleStorageStores<IdentityUser, IdentityRole>(
                    identityOptions);
        }

        /// <summary>
        ///     This method allows you to customize the user and role type when registering identity services
        ///     and storage stores.`
        /// </summary>
        /// <typeparam name="TUser">The type associated with user identity information.</typeparam>
        /// <typeparam name="TRole">The type associated with role identity information.</typeparam>
        /// <param name="service">The <see cref="IdentityBuilder"/> to build upon.</param>
        /// <param name="identityOptions">The identity options used when calling AddIdentity.</param>
        /// <returns>The <see cref="IdentityBuilder"/> with the storage settings applied.</returns>
        public static IdentityBuilder AddIdentityWithSimpleStorageStores<TUser, TRole>(
            this IServiceCollection service,
            Action<IdentityOptions> identityOptions = null)
            where TUser : IdentityUser
            where TRole : IdentityRole
        {
            return service.AddIdentity<TUser, TRole>(identityOptions)
                .AddSimpleStorageStores<TUser, TRole>();
        }
    }
}