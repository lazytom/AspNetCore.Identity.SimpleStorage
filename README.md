# AspNetCore.Identity.SimpleStorage

An AspNetCore.Identity storage provider for those who don't want to use a database. Users and roles will be saved as JSON in a file. There are various options for storing the file(s) - local disk, via a Storage.Net `IBlogStorage` object (see https://github.com/aloneguid/storage for details) or you can implement your own `IStorageProvider`

### NuGet Package
#### The basic package
`AspNetCore.Identity.SimpleStorage` 

[![NuGet version](https://badge.fury.io/nu/AspNetCore.Identity.SimpleStorage.svg)](https://badge.fury.io/nu/AspNetCore.Identity.SimpleStorage)

#### Extension to use an Storage.Net `IBlogStorage` implementation for storage.

`AspNetCore.Identity.SimpleStorage.StorageNet` 

[![NuGet version](https://badge.fury.io/nu/AspNetCore.Identity.SimpleStorage.StorageNet.svg)](https://badge.fury.io/nu/AspNetCore.Identity.SimpleStorage.StorageNet)


### Usage:
*Note:*
- `MyUser` must derive from `AspNetCore.Identity.SimpleStorage.IdentityUser`.
- `MyRole` must derive from `AspNetCore.Identity.SimpleStorage.IdentityRole`.

#### Basic
In `Startup.cs`:
```c#
services.AddDefaultIdentity<MyUser>()
	.AddSimpleStorageUserStore<MyUser>("users.json");
```

#### With Storage.Net
In `Startup.cs`:
```c#
var blobStorage = StorageFactory.Blobs.FromConnectionString("disk://path=.");

services.AddDefaultIdentity<MyUser>()
	.AddSimpleStorageUserStoreWithStorageNet<MyUser>(blobStorage, "users.json")
	.AddSimpleStorageRoleStoreWithStorageNet<MyRole>(blobStorage, "roles.json");
```

#### With Custom Storage
In `Startup.cs`:
```c#
services.AddDefaultIdentity<MyUser>()
	.AddSimpleStorageStores<MyUser, MyRole>();
	
services.AddSingleton<IStorageProvider<MyUser>>(p => <your custom IStorageProvider implementation>)
services.AddSingleton<IStorageProvider<MyRole>>(p => <your custom IStorageProvider implementation>)
```


---
Loosely based on https://github.com/FelschR/AspNetCore.Identity.DocumentDB
