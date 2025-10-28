# .NET 9 + PostgreSQL REST API Starter

REST API template with .NET 9, C#, and PostgreSQL. Designed for quick customization.

## Quick Start

### 1. Set Database Connection
```bash
# PostgreSQL URI (auto-converts to Npgsql format)
export ConnectionStrings__DefaultConnection="postgresql://user:password@host:5432/database"

# OR key-value format
export ConnectionStrings__DefaultConnection="Host=localhost;Port=5432;Database=mydb;Username=postgres;Password=password"
```

### 2. Setup & Run Migrations
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

### 3. Run
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
 