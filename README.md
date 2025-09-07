# NLayered Clean Architecture with .NET 8

A concise, production‑oriented **N‑Layered + Clean Architecture** template on **.NET 8**. The goal is clean boundaries, easy maintenance, and a clear starting point for enterprise Web API work.

---

## Project Structure
```
NLayeredCleanArchitectureWithDotNet8.sln
├─ App.API/          # ASP.NET Core Web API (composition root, DI, endpoints)
├─ CleanApp.API/     # Alternative/minimal API host (sample)
├─ App.Domain/       # Entities, value objects, domain logic (no external deps)
├─ App.Application/  # Use cases, DTOs, contracts (ports), validation
├─ App.Persistence/  # Infrastructure for data access (e.g., EF Core DbContext)
├─ App.Caching/      # Cache abstractions/implementations (optional)
├─ App.Bus/          # Messaging abstractions/integrations (optional)
├─ Repositories/     # Concrete repository implementations (if any)
└─ Services/         # Cross‑cutting services (if any)
```
> **Dependency rule:** Outer layers depend on inner layers only. `App.API → App.Application → App.Domain`. Infrastructure projects are referenced from the API for wiring.

---

## Architecture at a Glance
- **Domain (Core):** Pure business model (entities/value objects/domain services). No external dependencies.
- **Application:** Use‑cases orchestrating domain logic. Contracts (ports) for persistence/cache/bus; DTOs and validation.
- **Infrastructure (Persistence/Caching/Bus):** Concrete adapters that implement application contracts (e.g., EF Core, Redis, RabbitMQ). Can be swapped without touching core.
- **API (Presentation):** HTTP endpoints, DI configuration, middleware.

---

## Getting Started
### Prerequisites
- **.NET SDK 8.0+**
- **SQL Server** (or your configured database) if `App.Persistence` uses EF Core

### Restore & Run
```bash
# Clone
 git clone https://github.com/murcell/NLayeredCleanArchitectureWithDotNet8.git
 cd NLayeredCleanArchitectureWithDotNet8

# Restore & run API
 dotnet restore
 dotnet run --project App.API
```
The API will start on the port printed in the console (Kestrel). Configure environment via `ASPNETCORE_ENVIRONMENT` as needed.

### Configuration (minimal)
Update **`App.API/appsettings.Development.json`** (or environment variables):
```json
{
  "ConnectionStrings": {
    "Default": "Server=localhost;Database=CleanDb;User Id=sa;Password=Your_password123;TrustServerCertificate=True;"
  },
  "Cache": {
    "UseDistributed": false,
    "Redis": { "Configuration": "localhost:6379" }
  },
  "Messaging": {
    "Enabled": false
  }
}
```
> Toggle caching/messaging based on your needs. If you don’t use them, keep them disabled.

### Database (if using EF Core)
If `App.Persistence` includes EF Core and migrations:
```bash
 dotnet tool install --global dotnet-ef
 dotnet ef database update --project App.Persistence --startup-project App.API
```

---

## What’s Included / Not Included
- ✅ Clean layering with clear boundaries
- ✅ Placeholders for **Caching** and **Messaging** (optional)
- ✅ Persistence project ready for EF Core (if you choose to use it)
- ❌ **No test projects** included at the moment
- ❌ No CI/CD pipeline by default

---

## Guidelines
- Keep **Domain** pure and independent.
- Keep **Application** thin; use contracts (ports) to talk to infrastructure.
- Register infrastructure implementations in **App.API** (composition root).
- Prefer **DTOs** at the API boundary; avoid leaking entities.

---

## License
No license file is currently included. Add one (e.g., MIT) if you plan to reuse/share code in other projects.

---

## Questions / Feedback
Open an issue on GitHub to discuss improvements or ask questions.

