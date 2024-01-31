# How to Run:
## Update Configuration
<details>
  <summary><b>Database</b></summary>
  
- Run Migration:
  + Option 1: Using dotnet cli:
    + Install **dotnet-ef** cli:
      ```
      dotnet ef migrations add Init --context ApplicationDbContext -o Migrations/ApplicationDbContext
      dotnet ef database update --context ApplicationDbContext

      dotnet ef migrations add Init --context ConfigurationDbContext -o Migrations/ConfigurationDb
      dotnet ef migrations add Init --context PersistedGrantDbContext -o Migrations/PersistedGrantDb
      dotnet ef database update --context AdsDbContext
      dotnet ef database update --context ConfigurationDbContext
      dotnet ef database update --context PersistedGrantDbContext
      ```
  + Option 2: Using Package Manager Console:
    + Run these commands:
      ```
      Add-Migration -Context ApplicationDbContext Init
      Update-Database -Context ApplicationDbContext

      Add-Migration -Context ProductDbContext Init
      Update-Database -Context ProductDbContext

      Add-Migration -Context ConfigurationDbContext Init -OutputDir Migrations/ConfigurationDb
      Add-Migration -Context PersistedGrantDbContext Init -OutputDir Migrations/PersistedGrantDb
      Update-Database -Context AdsDbContext
      Update-Database -Context ConfigurationDbContext
      Update-Database -Context PersistedGrantDbContext
      ```
</details>

docker run -p 6379:6379 redis