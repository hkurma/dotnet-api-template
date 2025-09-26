# .NET API Starter Template

A comprehensive .NET 9 Web API starter template implementing Clean Architecture principles.

## 🏗️ Architecture

This project follows Clean Architecture principles with clear separation of concerns:

```bash
src/
├── DotNet.Template.Domain/          # Business entities, enums, and interfaces
├── DotNet.Template.Application/     # Use cases, DTOs, and application logic
├── DotNet.Template.Infrastructure/  # Data access, repositories, and external services
└── DotNet.Template.Api/            # Controllers, middleware, and API configuration

tests/
├── DotNet.Template.Tests.Unit/      # Unit tests
└── DotNet.Template.Tests.Integration/ # Integration tests
```

## 🚀 Features

- **Clean Architecture** - Separation of concerns with clear dependencies
- **CQRS Pattern** - Command Query Responsibility Segregation using MediatR
- **Repository Pattern** - Data access abstraction
- **Unit of Work** - Transaction management
- **Domain-Driven Design** - Rich domain models with business logic
- **FluentValidation** - Input validation
- **AutoMapper** - Object-to-object mapping
- **Global Exception Handling** - Centralized error handling
- **OpenAPI** - Built-in API documentation with Scalar UI
- **Entity Framework Core** - ORM with In-Memory database
- **CORS Support** - Cross-origin resource sharing
- **Structured Logging** - Built-in logging configuration

## 🛠️ Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)

## ⚡ Quick Start

1. **Clone the repository**

   ```bash
   git clone <repository-url>
   cd dotnet-api-starter-template
   ```

2. **Restore dependencies**

   ```bash
   dotnet restore
   ```

3. **Run the application**

   ```bash
   dotnet run --project src/DotNet.Template.Api
   ```

4. **Debug with VS Code** (Recommended)
   - Open the project in VS Code
   - Press `F5` or go to Run and Debug
   - Select "Launch API" configuration
   - Scalar API documentation will automatically open at `https://localhost:5001`

5. **Access the API**
   - **F5 Debug**: Scalar at `https://localhost:5001/scalar/v1` (with debugging)
   - **Run Task**: Scalar at `http://localhost:5000/scalar/v1` (without debugging)
   - **API Base URL**: `http://localhost:5000/api` or `https://localhost:5001/api`

## 🗄️ Database Configuration

### In-Memory Database

The application uses Entity Framework Core In-Memory database for simplicity and immediate setup. No additional database installation or configuration required - just run and go!

## 🏃‍♂️ Running Tests

```bash
# Run all tests
dotnet test

# Run unit tests only
dotnet test tests/DotNet.Template.Tests.Unit

# Run integration tests only
dotnet test tests/DotNet.Template.Tests.Integration
```

## 🐛 VS Code Development Setup

The project includes complete VS Code configuration for optimal development experience:

### 🚀 Debugging Configuration

- **Launch API**: Press `F5` to start debugging in development mode
- **Auto-open Scalar**: Scalar API documentation opens automatically at `https://localhost:5001`
- **Breakpoint Support**: Set breakpoints anywhere in the codebase

### 🛠️ Editor Settings

- **EditorConfig**: Consistent formatting rules across all editors (`.editorconfig`)
- **Auto-format**: Code formatting on save
- **Organize Imports**: Automatically organize using statements
- **Hide Build Artifacts**: `bin/` and `obj/` folders hidden from explorer
- **Default Solution**: Automatically uses `DotNet.Template.sln`

### 📦 Recommended Extensions

VS Code will automatically suggest installing:

- **C# Dev Kit**: Complete C# development experience
- **JSON**: Enhanced JSON editing
- **Auto Rename Tag**: Automatically rename paired tags (useful for XML docs)

### 🎯 Quick Start

1. Open project in VS Code: `code .`
2. Install recommended extensions (VS Code will prompt)
3. Press `F5` to start debugging
4. Scalar API documentation opens automatically - start testing your API!

### 🎨 Code Formatting

Format the entire project using EditorConfig rules:

```bash
# Format entire solution
dotnet format

# Format and verify (check only, no changes)
dotnet format --verify-no-changes

# VS Code: Ctrl+Shift+P → "Tasks: Run Task" → "format"
```

### 🧪 Running Tests

Run tests easily from VS Code or command line:

```bash
# Run all tests
dotnet test

# VS Code: Ctrl+Shift+P → "Tasks: Run Task" → "test"
```

### 🚀 Running the Application

Start the API from VS Code or command line:

```bash
# Run the API
dotnet run --project src/DotNet.Template.Api

# Run on specific port
dotnet run --project src/DotNet.Template.Api --urls "http://localhost:5000"

# VS Code: Ctrl+Shift+P → "Tasks: Run Task" → "run" (runs on localhost:5000)
```

## 🔧 Configuration

### Code Formatting

The project includes `.editorconfig` for consistent code formatting across all editors:

- **C# files**: 4-space indentation
- **JSON/XML files**: 2-space indentation  
- **Consistent line endings**: CRLF (Windows)
- **UTF-8 encoding**: Universal character support
- **.NET code style rules**: Modern C# conventions

### Environment Variables

- `ASPNETCORE_ENVIRONMENT` - Set to `Development`, `Staging`, or `Production`

### Application Settings

Key configuration options in `appsettings.json`:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "Microsoft.EntityFrameworkCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

## 🏗️ Project Structure Details

### Domain Layer (`DotNet.Template.Domain`)

- **Entities**: Core business entities
- **Enums**: Domain enumerations
- **Interfaces**: Repository and service contracts
- **Common**: Base classes and shared domain logic

### Application Layer (`DotNet.Template.Application`)

- **Features**: CQRS commands and queries organized by feature
- **DTOs**: Data transfer objects
- **Mappings**: AutoMapper profiles
- **Behaviors**: Cross-cutting concerns (validation, logging)
- **Common**: Shared application logic and result types

### Infrastructure Layer (`DotNet.Template.Infrastructure`)

- **Data**: Entity Framework DbContext and Unit of Work
- **Repositories**: Data access implementations
- **DependencyInjection**: Service registration

### API Layer (`DotNet.Template.Api`)

- **Controllers**: REST API endpoints
- **Middleware**: Custom middleware components
- **Configuration**: Startup and service registration

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🙏 Acknowledgments

- [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html) by Robert C. Martin
- [MediatR](https://github.com/jbogard/MediatR) for CQRS implementation
- [FluentValidation](https://fluentvalidation.net/) for validation
- [AutoMapper](https://automapper.org/) for object mapping

## 🚀 Next Steps

This starter template provides a solid foundation. Consider adding:

- **Database**: SQL Server, PostgreSQL, or other persistent databases
- **Authentication and Authorization**: JWT, OAuth, Identity
- **Caching**: Redis, In-Memory caching
- **Rate Limiting**: API throttling
- **Health Checks**: Application monitoring
- **Docker**: Containerization
- **CI/CD**: Automated pipelines
- **API Versioning**: Version management
- **Background Jobs**: Hangfire, Quartz
- **Event Sourcing**: Event-driven architecture
- **Message Queuing**: RabbitMQ, Azure Service Bus
