# .NET 8 + PostgreSQL REST API Starter

REST API template with .NET 8, C#, and PostgreSQL. Designed for quick customization.

## Quick Start

### 1. Set Database Connection
```bash
# PostgreSQL URI (auto-converts to Npgsql format)
export ConnectionStrings__DefaultConnection="postgresql://user:password@host:5432/database"

# OR key-value format
export ConnectionStrings__DefaultConnection="Host=localhost;Port=5432;Database=mydb;Username=postgres;Password=password"
```

### 2. Customize (Optional)
```bash
# Rename entity in WebAPI/Data/Entities/User.cs
# Update IUserRepository, UserRepository, UsersController accordingly
```

### 3. Setup & Run Migrations
```bash
# Install packages and tools
dotnet restore
dotnet tool restore

# Create and apply migrations
cd WebAPI
dotnet ef migrations add InitialCreate
dotnet ef database update
cd ..
```

### 4. Run
```bash
dotnet run --project WebAPI --urls "http://0.0.0.0:5000"
```

API: `http://localhost:5000/api/users`

## API Endpoints

| Method | Endpoint | Body |
|--------|----------|------|
| GET | `/api/users` | - |
| POST | `/api/users` | `{ name, email, phoneNumber }` |
| PUT | `/api/users/{id}` | `{ name, email, phoneNumber }` |
| DELETE | `/api/users/{id}` | - |

## Features

- ✅ CRUD Operations
- 📧 Email & Phone Validation
- 🐘 PostgreSQL + EF Core
- 🔄 Dynamic DB Configuration
- 🎯 Repository Pattern
- 🌐 CORS Enabled (All origins allowed)

## Customize Entity

1. Rename `User.cs` to your entity (e.g., `Customer.cs`, `Product.cs`)
2. Update entity properties as needed
3. Update `IUserRepository` → `ICustomerRepository`
4. Update `UserRepository` → `CustomerRepository`
5. Update `UsersController` → `CustomersController`
6. Update `AppDbContext.cs`: `DbSet<User> Users` → `DbSet<Customer> Customers`
7. Create migration: `cd WebAPI && dotnet ef migrations add InitialCreate`
8. Apply migration: `dotnet ef database update`

## Common Commands

```bash
dotnet restore                  # Install packages
dotnet tool restore             # Install dotnet-ef tool
dotnet run --project WebAPI     # Run
dotnet build                    # Build

cd WebAPI
dotnet ef migrations add <Name> # Add migration
dotnet ef database update       # Apply migration
```

## Requirements

- .NET 8 SDK
- PostgreSQL

## Troubleshooting

**Error: "dotnet-ef does not exist"**
```bash
# Restore local tools
dotnet tool restore
```

**Error: "Database connection string not found"**
```bash
export ConnectionStrings__DefaultConnection="postgresql://user:pass@host:5432/db"
```

**Error: "relation Users does not exist"**
```bash
cd WebAPI
dotnet ef database update
```

## Production Notes

**CORS Configuration:**  
Currently allows all origins for maximum flexibility. When you know your frontend URL, update `Program.cs`:
```csharp
policy.WithOrigins("https://yourfrontend.com")
      .AllowAnyMethod()
      .AllowAnyHeader();
```
