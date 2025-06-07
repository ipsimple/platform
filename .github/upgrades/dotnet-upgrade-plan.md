# .NET 9.0 Upgrade Plan

## Execution Steps

1. Validate that an .NET 9.0 SDK required for this upgrade is installed on the machine and if not, help to get it installed.
2. Ensure that the SDK version specified in global.json files is compatible with the .NET 9.0 upgrade.
3. Upgrade src/IpSimple.Domain/IpSimple.Domain.csproj
4. Upgrade src/IpSimple.Extensions/IpSimple.Extensions.csproj
5. Upgrade src/IpSimple.PublicIp.Api/IpSimple.PublicIp.Api.csproj
6. Upgrade src/IpSimple.Domain.Tests/IpSimple.Domain.Tests.csproj
7. Upgrade src/IpSimple.Extensions.Tests/IpSimple.Extensions.Tests.csproj
8. Upgrade src/IpSimple.PublicIp.Api.Tests/IpSimple.PublicIp.Api.Tests.csproj
9. Run unit tests to validate upgrade in the projects listed below:
  - src/IpSimple.Domain.Tests/IpSimple.Domain.Tests.csproj
  - src/IpSimple.Extensions.Tests/IpSimple.Extensions.Tests.csproj
  - src/IpSimple.PublicIp.Api.Tests/IpSimple.PublicIp.Api.Tests.csproj

## Settings

This section contains settings and data used by execution steps.

### Excluded projects

| Project name                                   | Description                 |
|:-----------------------------------------------|:---------------------------:|

### Aggregate NuGet packages modifications across all projects

| Package Name                        | Current Version | New Version | Description                         |
|:------------------------------------|:---------------:|:-----------:|:------------------------------------|
| Microsoft.AspNetCore.Mvc.Testing    |   7.0.0         |  9.0.5      | Recommended for .NET 9.0            |
| Microsoft.AspNetCore.OpenApi        |   8.0.6         |  9.0.5      | Recommended for .NET 9.0            |
| Microsoft.AspNetCore.TestHost       |   7.0.0         |  9.0.5      | Recommended for .NET 9.0            |
| Microsoft.VisualStudio.Azure.Containers.Tools.Targets |   1.20.1         |  null      | Incompatible with .NET 9.0          |

### Project upgrade details

#### IpSimple.Domain modifications

Project properties changes:
  - Target framework should be changed from `net8.0` to `net9.0`

#### IpSimple.Extensions modifications

Project properties changes:
  - Target framework should be changed from `net8.0` to `net9.0`
NuGet packages changes:
  - Microsoft.AspNetCore.OpenApi should be updated from `8.0.6` to `9.0.5` (*recommended for .NET 9.0*)

#### IpSimple.PublicIp.Api modifications

Project properties changes:
  - Target framework should be changed from `net8.0` to `net9.0`
NuGet packages changes:
  - Microsoft.AspNetCore.OpenApi should be updated from `8.0.6` to `9.0.5` (*recommended for .NET 9.0*)
  - Microsoft.VisualStudio.Azure.Containers.Tools.Targets should be removed (incompatible with .NET 9.0)

#### IpSimple.Domain.Tests modifications

Project properties changes:
  - Target framework should be changed from `net8.0` to `net9.0`

#### IpSimple.Extensions.Tests modifications

Project properties changes:
  - Target framework should be changed from `net8.0` to `net9.0`
NuGet packages changes:
  - Microsoft.AspNetCore.OpenApi should be updated from `8.0.6` to `9.0.5` (*recommended for .NET 9.0*)
  - Microsoft.AspNetCore.Mvc.Testing should be updated from `7.0.0` to `9.0.5` (*recommended for .NET 9.0*)
  - Microsoft.AspNetCore.TestHost should be updated from `7.0.0` to `9.0.5` (*recommended for .NET 9.0*)

#### IpSimple.PublicIp.Api.Tests modifications

Project properties changes:
  - Target framework should be changed from `net8.0` to `net9.0`
NuGet packages changes:
  - Microsoft.AspNetCore.OpenApi should be updated from `8.0.6` to `9.0.5` (*recommended for .NET 9.0*)
  - Microsoft.AspNetCore.Mvc.Testing should be updated from `7.0.0` to `9.0.5` (*recommended for .NET 9.0*)
  - Microsoft.AspNetCore.TestHost should be updated from `7.0.0` to `9.0.5` (*recommended for .NET 9.0*)
