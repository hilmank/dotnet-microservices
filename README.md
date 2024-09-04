# dotnet-microservices
Microservices using the .Net * platform along with Docker, Kubernetes, PostgreSQL

## 1. Initial Project
### a. **Create solution**
   ```bash
   dotnet new sln -n school
   ```
### b. Create folder Infrastructure
```
mkdir Infrastructure
```
### c. Create folder for services
- **Student Service**: Mengelola data siswa, pendaftaran, dan pelaporan nilai.
- **Teacher Service**: Mengelola data guru dan jadwal.
- **Classroom Service**: Mengelola penjadwalan kelas dan alokasi ruang.
- **Finance Service**: Mengelola pembayaran dan pengeluaran keuangan.
- **Inventory Service**: Mengelola barang dan fasilitas sekolah.

```bash
mkdir Services/Userapp
mkdir Services/Student
mkdir Services/Teacher
mkdir Services/Classroom
mkdir Services/Finance
mkdir Services/Inventory
```
### d. Create classlib project for logging
- create new classlib project
```
dotnet new classlib -n Common.Logging -o Infrastructure
```
- add project to solution
```
dotnet sln school.sln add Infrastructure/Common.Logging/Common.Logging.csproj
```
- add nuget packages
```
dotnet add Infrastructure/Common.Logging/Common.Logging.csproj package Serilog.AspNetCore
dotnet add Infrastructure/Common.Logging/Common.Logging.csproj package Serilog.Enrichers.Environment
dotnet add Infrastructure/Common.Logging/Common.Logging.csproj package Serilog.Exceptions
dotnet add Infrastructure/Common.Logging/Common.Logging.csproj package Serilog.Sinks.Console
dotnet add Infrastructure/Common.Logging/Common.Logging.csproj package Serilog.Sinks.Elasticsearch
```
- create class Logging
```
dotnet new class -n Logging.cs -o Infrastructure/Common.Logging
```

## 2. Create API with Grpc
Create API with Grpc, Dapper, and PostgreSQL
## 3. Student Service
API untuk pengelolaan data siswa.
### a. Create projects webapi (API), classlib (Application, Core, Infrastructure)

