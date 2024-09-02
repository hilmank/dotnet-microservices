# Craete API with Grpc, Dapper, and PostgreSQL
## 1. Create folder for Service under Services folder
```
mkdir Services/Userapp
```
## 2. Create projects webapi (API), classlib (Application, Core, Infrastructure)

```
dotnet new webapi -o Services/Userapp/Userapp.API
dotnet new classlib -o Services/Userapp/Userapp.Application
dotnet new classlib -o Services/Userapp/Userapp.Core
dotnet new classlib -o Services/Userapp/Userapp.Infrastructure
```
## 3. Add projects to solution
```
dotnet sln school.sln add Services/Userapp/Userapp.API/Userapp.API.csproj Services/Userapp/Userapp.Application/Userapp.Application.csproj Services/Userapp/Userapp.Core/Userapp.Core.csproj Services/Userapp/Userapp.Infrastructure/Userapp.Infrastructure.csproj
```
## 4. Add projects reference
- add Core project to Application
- add Application to Infrastructure
- add Application and Infrastructure toAPI
- add Common.Logging to API
```
dotnet add Services/Userapp/Userapp.Application/Userapp.Application.csproj reference Services/Userapp/Userapp.Core/Userapp.Core.csproj
dotnet add Services/Userapp/Userapp.Infrastructure/Userapp.Infrastructure.csproj reference Services/Userapp/Userapp.Application/Userapp.Application.csproj
dotnet add Services/Userapp/Userapp.API/Userapp.API.csproj reference Services/Userapp/Userapp.Application/Userapp.Application.csproj
dotnet add Services/Userapp/Userapp.API/Userapp.API.csproj reference Services/Userapp/Userapp.Infrastructure/Userapp.Infrastructure.csproj
dotnet add Services/Userapp/Userapp.API/Userapp.API.csproj reference Infrastructure/Common.Logging/Common.Logging.csproj
```
## 5. Add nuget package as needed
- add Dapper, Dapper.Contrib, Dapper.SimpleCRUD on Core project
```
dotnet add Services/Userapp/Userapp.Core/Userapp.Core.csproj package Dapper
dotnet add Services/Userapp/Userapp.Core/Userapp.Core.csproj package Dapper.Contrib
dotnet add Services/Userapp/Userapp.Core/Userapp.Core.csproj package Dapper.SimpleCRUD
```

- add Dapper, Grpc.AspNetCore, Npgsql on Infrastructure project
```
dotnet add Services/Userapp/Userapp.Infrastructure/Userapp.Infrastructure.csproj package Dapper
dotnet add Services/Userapp/Userapp.Infrastructure/Userapp.Infrastructure.csproj package Grpc.AspNetCore
dotnet add Services/Userapp/Userapp.Infrastructure/Userapp.Infrastructure.csproj package Npgsql
```
- add AutoMapper, Grpc.AspNetCore, MediatR on Application project
```
dotnet add Services/Userapp/Userapp.Application/Userapp.Application.csproj package AutoMapper
dotnet add Services/Userapp/Userapp.Application/Userapp.Application.csproj package Grpc.AspNetCore
dotnet add Services/Userapp/Userapp.Application/Userapp.Application.csproj package MediatR
dotnet add Services/Userapp/Userapp.Application/Userapp.Application.csproj package Google.Protobuf
```
## 6. build the solution to ensure there are no errors
```
dotnet build school.sln
```
## 7. add folders on projects
- API (Services)
- Application (Commands, Handlers, Mapper, Protos)
- Core (Entities, Repositories)
- Infrastructure (Extensions, Repositories)
```
mkdir Services/Userapp/Userapp.API/Services
mkdir Services/Userapp/Userapp.Application/Commands
mkdir Services/Userapp/Userapp.Application/Handlers
mkdir Services/Userapp/Userapp.Application/Mapper
mkdir Services/Userapp/Userapp.Application/Protos
mkdir Services/Userapp/Userapp.Core/Entities
mkdir Services/Userapp/Userapp.Core/Repositories
mkdir Services/Userapp/Userapp.Infrastructure/Extensions
mkdir Services/Userapp/Userapp.Infrastructure/Repositories
mkdir Services/Userapp/Userapp.Infrastructure/DbSql
```
## 8. do coding
- create class for entities on Services/Userapp/Userapp.Core
```
dotnet new class -n User -o Services/Userapp/Userapp.Core/Entities
```
don't forget to rename namespace on all class created
- create intercae for repositories Services/Userapp/Userapp.Core
```
dotnet new interface -n IUserRepository -o Services/Userapp/Userapp.Core/Repositories
```
don't forget to rename namespace on all class created
- create dbconnection on Services/Userapp/Userapp.Infrastructure/Extensions
```
dotnet new class -n DapperConnectionProvider -o Services/Userapp/Userapp.Infrastructure/Extensions
```
- create db migration on Services/Userapp/Userapp.Infrastructure/Extensions
```
dotnet new class -n DbExtension -o Services/Userapp/Userapp.Infrastructure/Extensions
```
- implement repositories on Services/Userapp/Userapp.Infrastructure
```
dotnet new class -n UserRepository -o Services/Userapp/Userapp.Infrastructure/Repositories
```
don't forget to rename namespace on all class created
- create proto files on Services/Userapp/Userapp.Application/Protos
    - create manualy user.proto
    - add this to Services/Userapp/Userapp.Application/Userapp.Application.csproj
  ```
  <ItemGroup>
    <Protobuf Include="Protos\discount.proto" GrpcServices="Server" />
  </ItemGroup>   
  ```
- Creating Profiles on  Services/Userapp/Userapp.Application/Mapper
```
dotnet new class -n UserProfile -o Services/Userapp/Userapp.Application/Mapper
```
- Creating Query on Services/Userapp/Userapp.Application/Queries
```
dotnet new class -n GetUsersQuery -o Services/Userapp/Userapp.Application/Queries
```
- Creating Query Handler on Services/Userapp/Userapp.Application/Handlers
```
dotnet new class -n GetUsersQueryHandler -o Services/Userapp/Userapp.Application/Handlers
```
- Creating Command on Services/Userapp/Userapp.Application/Commands
```
dotnet new class -n CreateUserCommand -o Services/Userapp/Userapp.Application/Commands
```
- Creating Command Handler on Services/Userapp/Userapp.Application/Handlers
```
dotnet new class -n CreateUserCommandHandler -o Services/Userapp/Userapp.Application/Handlers
```
- Creating API Services, on  Userapp/Userapp.API/Services
```
dotnet new class -n UserService -o Services/Userapp/Userapp.API/Services
```
- Modify/Create Program file on Services/Userapp/Userapp.API
- Modify/Create appsetting.json file on Services/Userapp/Userapp.API
- Create Dockerfile
- run docker compose all container
```
docker-compose -f docker-compose-userapp.yml -f docker-compose-userapp.override up -d
```

