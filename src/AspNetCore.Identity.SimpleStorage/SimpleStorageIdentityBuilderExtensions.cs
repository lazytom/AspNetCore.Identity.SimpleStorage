using AspNetCore.Identity.SimpleStorage;
using Microsoft.AspNetCore.Identity;
using System;
using IdentityRole = AspNetCore.Identity.SimpleStorage.IdentityRole;
using IdentityUser = AspNetCore.Identity.SimpleStorage.IdentityUser;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SimpleStorageIdentityBuilderExtensions
    {
        /// <summary>
        ///     If you want control over creating the users and roles collections, use this overload.
        ///     This method only registers the storage stores, you also need to call AddIdentity.
        ///     In addition, you need to register an IStorageProvider for your respective User and Role entity classes.
        /// </summary>
        /// <typeparam name="TUser"></typeparam>
        /// <typeparam name="TRole"></typeparam>
        /// <param name="builder"></param>
        public static IdentityBuilder AddSimpleStorageStores<TUser, TRole>(this IdentityBuilder builder)
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

            builder.Services.AddSingleton<IUserStore<TUser>, UserStore<TUser>>();
            builder.Services.AddSingleton<IRoleStore<TRole>, RoleStore<TRole>>();

            return builder;
        }

        /// <summary>
        ///     If you want control over creating the users and roles collections, use this overload.
        ///     This method only registers the storage stores, you also need to call AddIdentity.
        /// </summary>
        /// <typeparam name="TUser"></typeparam>
        /// <typeparam name="TRole"></typeparam>
        /// <param name="builder"></param>
        public static IdentityBuilder AddSimpleStorageStores<TUser, TRole>(this IdentityBuilder builder,
            string userStoreFilename, string roleStoreFilename)
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

            builder.Services.AddSingleton<IStorageProvider<TUser>>(p => new LocalFileStorageProvider<TUser>(userStoreFilename));
            builder.Services.AddSingleton<IStorageProvider<TRole>>(p => new LocalFileStorageProvider<TRole>(roleStoreFilename));

            builder.Services.AddSingleton<IUserStore<TUser>, UserStore<TUser>>();
            builder.Services.AddSingleton<IRoleStore<TRole>, RoleStore<TRole>>();

            return builder;
        }

        /// <summary>
        ///     If you want control over creating the users collection, use this overload.
        ///     This method only registers the storage store for users, you also need to call AddIdentity.
        ///     In addition, you need to register an IStorageProvider for your respective user entity class.
        /// </summary>
        /// <typeparam name="TUser"></typeparam>
        /// <param name="builder"></param>
        public static IdentityBuilder AddSimpleStorageUserStore<TUser>(this IdentityBuilder builder)
            where TUser : IdentityUser
        {
            if (typeof(TUser) != builder.UserType)
            {
                var message = "User type passed to AddSimpleStorageStores must match user type passed to AddIdentity. "
                              + $"You passed {builder.UserType} to AddIdentity and {typeof(TUser)} to AddSimpleStorageUserStore, "
                              + "these do not match.";
                throw new ArgumentException(message);
            }

            builder.Services.AddSingleton<IUserStore<TUser>, UserStore<TUser>>();

            return builder;
        }

        /// <summary>
        ///     If you want control over creating the users collection, use this overload.
        ///     This method only registers the storage store for roles, you also need to call AddIdentity.
        ///     In addition, you need to register an IStorageProvider for your respective role entity class.
        /// </summary>
        /// <typeparam name="TRole"></typeparam>
        /// <param name="builder"></param>
        public static IdentityBuilder AddSimpleStorageRoleStore<TRole>(this IdentityBuilder builder)
            where TRole : IdentityRole
        {
            if (typeof(TRole) != builder.UserType)
            {
                var message = "Role type passed to AddSimpleStorageStores must match role type passed to AddIdentity. "
                              + $"You passed {builder.RoleType} to AddIdentity and {typeof(TRole)} to AddSimpleStorageRoleStore, "
                              + "these do not match.";
                throw new ArgumentException(message);
            }

            builder.Services.AddSingleton<IRoleStore<TRole>, RoleStore<TRole>>();

            return builder;
        }

        /// <summary>
        ///     If you want control over creating the users and roles collections, use this overload.
        ///     This method only registers the storage store for users, you also need to call AddIdentity.
        /// </summary>
        /// <typeparam name="TUser"></typeparam>
        /// <param name="builder"></param>
        public static IdentityBuilder AddSimpleStorageUserStore<TUser>(this IdentityBuilder builder, string userStoreFilename)
            where TUser : IdentityUser
        {
            if (typeof(TUser) != builder.UserType)
            {
                var message = "User type passed to AddSimpleStorageStores must match user type passed to AddIdentity. "
                              + $"You passed {builder.UserType} to AddIdentity and {typeof(TUser)} to AddSimpleStorageUserStore, "
                              + "these do not match.";
                throw new ArgumentException(message);
            }

            builder.Services.AddSingleton<IStorageProvider<TUser>>(p => new LocalFileStorageProvider<TUser>(userStoreFilename));

            builder.Services.AddSingleton<IUserStore<TUser>, UserStore<TUser>>();

            return builder;
        }

        /// <summary>
        ///     If you want control over creating the users and roles collections, use this overload.
        ///     This method only registers the storage store for roles, you also need to call AddIdentity.
        /// </summary>
        /// <typeparam name="TRole"></typeparam>
        /// <param name="builder"></param>
        public static IdentityBuilder AddSimpleStorageRoleStore<TRole>(this IdentityBuilder builder, string roleStoreFilename)
            where TRole : IdentityRole
        {
            if (typeof(TRole) != builder.UserType)
            {
                var message = "User type passed to AddSimpleStorageStores must match user type passed to AddIdentity. "
                              + $"You passed {builder.UserType} to AddIdentity and {typeof(TRole)} to AddSimpleStorageUserStore, "
                              + "these do not match.";
                throw new ArgumentException(message);
            }

            builder.Services.AddSingleton<IStorageProvider<TRole>>(p => new LocalFileStorageProvider<TRole>(roleStoreFilename));

            builder.Services.AddSingleton<IRoleStore<TRole>, RoleStore<TRole>>();

            return builder;
        }

        /// <summary>
        ///     This method registers identity services and storage stores using the IdentityUser and IdentityRole types.
        /// </summary>
        /// <param name="service">The <see cref="IdentityBuilder"/> to build upon.</param>
        /// <param name="identityOptions">The identity options used when calling AddIdentity.</param>
        /// <returns>The <see cref="IdentityBuilder"/> with the storage settings applied.</returns>
        public static IdentityBuilder AddIdentityWithSimpleStorageStores(this IServiceCollection service,
            string userStoreFilename, string roleStoreFilename,
            Action<IdentityOptions> identityOptions = null)
        {
            return service.AddIdentityWithSimpleStorageStores<IdentityUser, IdentityRole>(
                    userStoreFilename, roleStoreFilename, identityOptions);
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
        public static IdentityBuilder AddIdentityWithSimpleStorageStores<TUser, TRole>(this IServiceCollection service,
            string userStoreFilename, string roleStoreFilename,
            Action<IdentityOptions> identityOptions = null)
            where TUser : IdentityUser
            where TRole : IdentityRole
        {
            return service.AddIdentity<TUser, TRole>(identityOptions)
                .AddSimpleStorageStores<TUser, TRole>(userStoreFilename, roleStoreFilename);
        }

        /// <summary>
        ///     This method registers identity services and storage stores using the IdentityUser and IdentityRole types.
        /// </summary>
        /// <param name="service">The <see cref="IdentityBuilder"/> to build upon.</param>
        /// <param name="identityOptions">The identity options used when calling AddIdentity.</param>
        ///     In addition, you need to register an IStorageProvider for your respective User and Role entity classes.
        /// <returns>The <see cref="IdentityBuilder"/> with the storage settings applied.</returns>
        public static IdentityBuilder AddIdentityWithSimpleStorageStores(this IServiceCollection service,
            Action<IdentityOptions> identityOptions = null)
        {
            return service.AddIdentityWithSimpleStorageStores<IdentityUser, IdentityRole>(identityOptions);
        }

        /// <summary>
        ///     This method allows you to customize the user and role type when registering identity services
        ///     and storage stores.`
        /// </summary>
        /// <typeparam name="TUser">The type associated with user identity information.</typeparam>
        /// <typeparam name="TRole">The type associated with role identity information.</typeparam>
        ///     In addition, you need to register an IStorageProvider for your respective User and Role entity classes.
        /// <param name="service">The <see cref="IdentityBuilder"/> to build upon.</param>
        /// <param name="identityOptions">The identity options used when calling AddIdentity.</param>
        /// <returns>The <see cref="IdentityBuilder"/> with the storage settings applied.</returns>
        public static IdentityBuilder AddIdentityWithSimpleStorageStores<TUser, TRole>(this IServiceCollection service,
            Action<IdentityOptions> identityOptions = null)
            where TUser : IdentityUser
            where TRole : IdentityRole
        {
            return service.AddIdentity<TUser, TRole>(identityOptions)
                .AddSimpleStorageStores<TUser, TRole>();
        }
    }
}