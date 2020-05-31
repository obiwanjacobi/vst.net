# Jacobi.Vst.Interop

# Nuget Packages

The Visual Studio Nuget Package Manager does not work for C++/CLI projects (like interop). 
These packages are referenced by hand and documented here.

These assemblies must be part of any vst.net plugin deployment.

`$(USERPROFILE)` is the root of your user under Windows: `C:\Users\Me`

### `Microsoft.Extensions.Configuration` 3.1.2
$(USERPROFILE)\.nuget\packages\microsoft.extensions.configuration\3.1.2\lib\netcoreapp3.1

### `Microsoft.Extensions.Configuration.FileExtensions` 3.1.1
$(USERPROFILE)\.nuget\packages\microsoft.extensions.configuration.fileextensions\3.1.1\lib\netcoreapp3.1

### `Microsoft.Extensions.Configuration.Abstractions` 3.1.2
$(USERPROFILE)\.nuget\packages\microsoft.extensions.configuration.abstractions\3.1.2\lib\netcoreapp3.1

### `Microsoft.Extensions.Configuration.Json` 2.1.0
$(USERPROFILE)\.nuget\packages\microsoft.extensions.configuration.json\2.1.0\lib\netstandard2.0
