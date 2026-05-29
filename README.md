# peppers-bpmn

BPMN process management platform — Camunda 7 + ASP.NET Core 9 + React.

---

## How to Start

### 1. Start Infrastructure (Camunda + PostgreSQL)

```powershell
docker-compose up -d
```

| Service | URL | Credentials |
|---------|-----|-------------|
| Camunda Cockpit | http://localhost:8080/camunda | demo / demo |
| PostgreSQL | localhost:5432 | postgres / postgres |

### 2. Run the Backend API

```powershell
dotnet run --project src/PeppersBpmn.Api
```

- API base: http://localhost:5000
- Swagger UI: http://localhost:5000/swagger

### 3. Run the Frontend

```powershell
cd ..\peppers-bpmn-web
npm install
npm run dev
```

- App: http://localhost:5173

---

## DB Migration

EF Core migrations live in `src/PeppersBpmn.Infrastructure/Migrations/`.
The `AppDbContextFactory` in Infrastructure provides the design-time connection,
so no startup-project switch is needed.

### Apply migrations (create tables)

```powershell
dotnet ef database update --project src/PeppersBpmn.Infrastructure --startup-project src/PeppersBpmn.Infrastructure
```

### Add a new migration

```powershell
dotnet ef migrations add <MigrationName> --project src/PeppersBpmn.Infrastructure --startup-project src/PeppersBpmn.Infrastructure
```

### Remove the last migration (before applying)

```powershell
dotnet ef migrations remove --project src/PeppersBpmn.Infrastructure --startup-project src/PeppersBpmn.Infrastructure
```

> **Note:** The connection string used at design time is hardcoded in
> `AppDbContextFactory.cs` (`Host=localhost;Database=peppers_bpmn;...`).
> For runtime the connection string comes from `appsettings.json`.

---

## NuGet Sources

A project-level `NuGet.Config` restricts package resolution to `nuget.org` only,
overriding any globally configured private feeds.
