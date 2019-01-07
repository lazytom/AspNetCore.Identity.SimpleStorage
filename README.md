# AspNetCore.Identity.SimpleStorage

An AspNetCore.Identity storage provider for those who don't want to use a database.

### NuGet Package
https://www.nuget.org/packages/AspNetCore.Identity.SimpleStorage/

### Usage:
In `Startup.cs`:
```c#
services.AddDefaultIdentity<SimpleStorageCore.IdentityUser>()
	.AddSimpleStorageStores<SimpleStorageCore.IdentityUser>("users.json");
```


---
Loosely based on https://github.com/FelschR/AspNetCore.Identity.DocumentDB