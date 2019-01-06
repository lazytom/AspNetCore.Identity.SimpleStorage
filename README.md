# AspNetCore.Identity.SimpleStorage

An AspNetCore.Identity storage provider for those who don't want to use a database.

Usage:
In `Startup.cs`:
```c#
services.AddDefaultIdentity<SimpleStorageCore.IdentityUser>()
	.AddSimpleStorageStores<SimpleStorageCore.IdentityUser>();
```

Loosely based on https://github.com/FelschR/AspNetCore.Identity.DocumentDB