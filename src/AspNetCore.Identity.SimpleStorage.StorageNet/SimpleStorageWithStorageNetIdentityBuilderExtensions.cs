using AspNetCore.Identity.SimpleStorage;
using AspNetCore.Identity.SimpleStorage.StorageNet;
using Microsoft.AspNetCore.Identity;
using Storage.Net;
using Storage.Net.Blob;
using System;
using IdentityRole = AspNetCore.Identity.SimpleStorage.IdentityRole;
using IdentityUser = AspNetCore.Identity.SimpleStorage.IdentityUser;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SimpleStorageWithStorageNetIdentityBuilderExtensions
    {
        /// <summary>
        ///     If you want control over creating the users and roles collections, use this overload.
        ///     This method only registers the storage stores, you also need to call AddIdentity.
        /// </summary>
        /// <typeparam name="TUser"></typeparam>
        /// <typeparam name="TRole"></typeparam>
        /// <param name="builder"></param>
        public static IdentityBuilder AddSimpleStorageStoresWithStorageNet<TUser, TRole>(this IdentityBuilder builder,
            string storageNetConnectionString, string usersStorageItemId, string rolesStorageItemId)
            where TRole : IdentityRole
            where TUser : IdentityUser
        {
            var blobStorage = StorageFactory.Blobs.FromConnectionString(storageNetConnectionString);
            builder.AddSimpleStorageUserStoreWithStorageNet<TUser>(blobStorage, usersStorageItemId);
            builder.AddSimpleStorageRoleStoreWithStorageNet<TRole>(blobStorage, rolesStorageItemId);

            return builder;
        }

        public static IdentityBuilder AddSimpleStorageUserStoreWithStorageNet<TUser>(this IdentityBuilder builder,
            IBlobStorage blobStorage, string storageItemId)
            where TUser : IdentityUser
        {
            builder.Services.AddSingleton<IStorageProvider<TUser>>(p =>
                new StorageNetStorageProvider<TUser>(blobStorage, storageItemId));

            builder.AddSimpleStorageUserStore<TUser>();

            return builder;
        }

        public static IdentityBuilder AddSimpleStorageRoleStoreWithStorageNet<TRole>(this IdentityBuilder builder,
            IBlobStorage blobStorage, string storageItemId)
            where TRole : IdentityRole
        {
            builder.Services.AddSingleton<IStorageProvider<TRole>>(p =>
                new StorageNetStorageProvider<TRole>(blobStorage, storageItemId));

            builder.AddSimpleStorageRoleStore<TRole>();

            return builder;
        }

        /// <summary>
        ///     This method registers identity services and storage stores using the IdentityUser and IdentityRole types.
        /// </summary>
        /// <param name="service">The <see cref="IdentityBuilder"/> to build upon.</param>
        /// <param name="identityOptions">The identity options used when calling AddIdentity.</param>
        ///     In addition, you need to register an IStorageProvider for your respective User and Role entity classes.
        /// <returns>The <see cref="IdentityBuilder"/> with the storage settings applied.</returns>
        public static IdentityBuilder AddIdentityWithSimpleStorageStoresWithStorageNet(this IServiceCollection service,
            string storageNetConnectionString, string usersStorageItemId, string rolesStorageIdemId,
            Action<IdentityOptions> identityOptions = null)
        {
            return service.AddIdentityWithSimpleStorageStoresWithStorageNet<IdentityUser, IdentityRole>(storageNetConnectionString, usersStorageItemId, rolesStorageIdemId, identityOptions);
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
        public static IdentityBuilder AddIdentityWithSimpleStorageStoresWithStorageNet<TUser, TRole>(this IServiceCollection service,
            string storageNetConnectionString, string usersStorageItemId, string rolesStorageIdemId, Action<IdentityOptions> identityOptions = null)
            where TUser : IdentityUser
            where TRole : IdentityRole
        {
            return service.AddIdentity<TUser, TRole>(identityOptions)
                .AddSimpleStorageStoresWithStorageNet<TUser, TRole>(storageNetConnectionString, usersStorageItemId, rolesStorageIdemId);
        }
    }
}