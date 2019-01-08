# AspNetCore.Identity.SimpleStorage

An AspNetCore.Identity storage provider for those who don't want to use a database. Users and roles will be saved as JSON in a file. There are various options for storing the file(s) - local disk, via a Storage.Net `IBlogStorage` object or you can implement your own `IStorageProvider`

### NuGet Package
- https://www.nuget.org/packages/AspNetCore.Identity.SimpleStorage/
  The basic package

- https://www.nuget.org/packages/AspNetCore.Identity.SimpleStorage.StorageNet
  Extension to use an Storage.Net `IBlogStorage` implementation for storage.

### Usage:
In `Startup.cs`:
```c#
services.AddDefaultIdentity<TUser, TRole>()
	.AddSimpleStorageUserStore<TUser>("users.json")
	.AddSimpleStorageRoleStore<TRole>("roles.json");
```

**Note:**
- `TUser` must derive from `AspNetCore.Identity.SimpleStorage.IdentityUser`.
- `TRole` must derive from `AspNetCore.Identity.SimpleStorage.IdentityRole`.

---
Loosely based on https://github.com/FelschR/AspNetCore.Identity.DocumentDB
