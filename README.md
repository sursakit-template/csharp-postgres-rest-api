# .NET 8 + C# + PostgreSQL REST API

User Management REST API with .NET 8, C#, and PostgreSQL.

## Quick Start

```bash
# 1. Create database
createdb editor

# 2. Install dependencies
dotnet restore

# 3. Run migrations (first time only)
cd WebAPI
dotnet ef database update
cd ..

# 4. Start the API
dotnet run --project WebAPI
```

API: http://localhost:5000/api/users

## Requirements

- .NET 8 SDK
- PostgreSQL

## Features

- ✅ CRUD Operations
- 📧 Email & Phone Validation
- 🐘 PostgreSQL with EF Core
- 🔌 RESTful API

## API Endpoints

| Method | Endpoint | Body |
|--------|----------|------|
| `GET` | `/api/users` | - |
| `POST` | `/api/users` | `{ name, email, phoneNumber }` |
| `PUT` | `/api/users/{id}` | `{ name, email, phoneNumber }` |
| `DELETE` | `/api/users/{id}` | - |

## Configuration

Edit `WebAPI/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=editor;Username=postgres;Password=password"
  }
}
```

## Testing

Import `postman/starter-pack.postman_collection.json` to Postman for complete test suite.

## Common Commands

```bash
dotnet restore                  # Install dependencies
dotnet run --project WebAPI     # Start dev server
dotnet build                    # Build project

# EF Core Migrations
cd WebAPI
dotnet ef migrations add <Name>
dotnet ef database update
```

## Troubleshooting

**Error: "relation Users does not exist"**
```bash
cd WebAPI
dotnet ef database update
cd ..
```

---

**Happy Coding!** 🎉
