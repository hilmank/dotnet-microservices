# Craete WEB API with Dapper, and PostgreSQL
## 1. Create projects webapi (API), classlib (Application, Core, Infrastructure)

```
dotnet new webapi -o Services/Student/Student.API
dotnet new classlib -o Services/Student/Student.Application
dotnet new classlib -o Services/Student/Student.Core
dotnet new classlib -o Services/Student/Student.Infrastructure
```
## 2. Add projects to solution
```
dotnet sln school.sln add Services/Student/Student.API/Student.API.csproj Services/Student/Student.Application/Student.Application.csproj Services/Student/Student.Core/Student.Core.csproj Services/Student/Student.Infrastructure/Student.Infrastructure.csproj
```
## 3. Add projects reference
- add Core project to Application
- add Application to Infrastructure
- add Application and Infrastructure to API
- add Common.Logging to API
```
dotnet add Services/Student/Student.Application/Student.Application.csproj reference Services/Student/Student.Core/Student.Core.csproj
dotnet add Services/Student/Student.Infrastructure/Student.Infrastructure.csproj reference Services/Student/Student.Application/Student.Application.csproj
dotnet add Services/Student/Student.API/Student.API.csproj reference Services/Student/Student.Application/Student.Application.csproj
dotnet add Services/Student/Student.API/Student.API.csproj reference Services/Student/Student.Infrastructure/Student.Infrastructure.csproj
dotnet add Services/Student/Student.API/Student.API.csproj reference Infrastructure/Common.Logging/Common.Logging.csproj
```
## 4. Add nuget package as needed
- add Dapper, Dapper.Contrib, Dapper.SimpleCRUD on Core project
```
dotnet add Services/Student/Student.Core/Student.Core.csproj package Dapper
dotnet add Services/Student/Student.Core/Student.Core.csproj package Dapper.Contrib
dotnet add Services/Student/Student.Core/Student.Core.csproj package Dapper.SimpleCRUD
```
- add Dapper, Npgsql on Infrastructure project
```
dotnet add Services/Student/Student.Infrastructure/Student.Infrastructure.csproj package Dapper
dotnet add Services/Student/Student.Infrastructure/Student.Infrastructure.csproj package Npgsql
```
- add AutoMapper, MediatR on Application project
```
dotnet add Services/Student/Student.Application/Student.Application.csproj package AutoMapper
dotnet add Services/Student/Student.Application/Student.Application.csproj package MediatR
```
## 5. build the solution to ensure there are no errors
```
dotnet build school.sln
```
## 6. add folders on projects
- API (Controllers)
- Application (Commands, Handlers, Mapper)
- Core (Entities, Repositories)
- Infrastructure (Extensions, Repositories)
```
mkdir Services/Student/Student.API/Controllers
mkdir Services/Student/Student.Application/Commands
mkdir Services/Student/Student.Application/Handlers
mkdir Services/Student/Student.Application/Mapper
mkdir Services/Student/Student.Application/Responses
mkdir Services/Student/Student.Application/GrpcService
mkdir Services/Student/Student.Core/Entities
mkdir Services/Student/Student.Core/Repositories
mkdir Services/Student/Student.Infrastructure/Extensions
mkdir Services/Student/Student.Infrastructure/Repositories
mkdir Services/Student/Student.Infrastructure/DbSql
```
## 8. do coding
- create class for entities on Services/Student/Student.Core
```
dotnet new class -n Student -o Services/Student/Student.Core/Entities
dotnet new class -n Enrollment -o Services/Student/Student.Core/Entities
dotnet new class -n Course -o Services/Student/Student.Core/Entities
```
don't forget to rename namespace on all class created
- create intercae for repositories Services/Student/Student.Core
```
dotnet new interface -n IStudentRepository -o Services/Student/Student.Core/Repositories
dotnet new interface -n IRegistrationRepository -o Services/Student/Student.Core/Repositories
```
don't forget to rename namespace on all class created
- create dbconnection on Services/Student/Student.Infrastructure/Extensions
```
dotnet new class -n DapperConnectionProvider -o Services/Student/Student.Infrastructure/Extensions
```

