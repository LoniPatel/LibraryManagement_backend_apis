# Library Management System – Backend API

A Library Management Backend API built using **ASP.NET Core Web API, C#, SQL Server, Entity Framework Core, Dapper, CQRS, and MediatR**.

The application manages books, authors, categories, users, and notifications through RESTful APIs.

## 1. Project Overview

The Library Management System provides backend functionality to manage library resources.

The main objectives are:

- Manage books, authors, and categories.
- Perform Create, Read, Update, and Delete (CRUD) operations.
- Search records using user-provided search terms.
- Separate read and write operations using CQRS.
- Use Entity Framework Core for write operations.
- Use Dapper for read and search queries.
- Notify users when a new book is created.
- Support soft deletion and audit information.

## 2. Technologies Used

| Technology | Purpose |
|---|---|
| C# | Backend programming language |
| ASP.NET Core Web API | REST API development |
| .NET 8 | Application framework |
| SQL Server | Relational database |
| Entity Framework Core | Database operations for writes |
| Dapper | Database operations for reads and searches |
| MediatR | CQRS implementation |
| CQRS | Separation of read and write operations |
| Swagger | API documentation and testing |
| Postman | API testing |

## 3. Project Architecture

The project follows a layered architecture.

```text
LibraryManagement
│
├── LibraryManagement.Api
│   ├── Controllers
│   ├── ExceptionHandling
│   └── Program.cs
│
├── LibraryManagement.Application
│   ├── Commands
│   ├── Queries
│   ├── DTOs
│   ├── Interfaces
│   └── Services
│
├── LibraryManagement.Domain
│   └── Entities
│
└── LibraryManagement.Infrastructure
    ├── Data
    ├── Repositories
    └── Queries
        ├── Books
        ├── Authors
        ├── Categories
        └── Notifications
```

### Layer Responsibilities

**API Layer**
- Receives HTTP requests.
- Contains API controllers.
- Sends commands and queries through MediatR.
- Returns HTTP responses.

**Application Layer**
- Contains business use cases.
- Contains Commands and Queries.
- Contains handlers, DTOs, and interfaces.
- Coordinates application operations.

**Domain Layer**
- Contains core domain entities.
- Represents business concepts such as Book, Author, and Category.

**Infrastructure Layer**
- Implements repository interfaces.
- Handles database communication.
- Contains Entity Framework Core and Dapper implementations.

## 4. CQRS Implementation

CQRS stands for **Command Query Responsibility Segregation**.

It separates application operations into two categories.

### Commands – Write Operations

Commands modify application data.

Examples:

- Create Book
- Update Book
- Delete Book
- Create Author
- Update Author
- Delete Author
- Create Category
- Update Category
- Delete Category

### Queries – Read Operations

Queries retrieve application data without modifying it.

Examples:

- Get All Books
- Get Book By ID
- Search Books
- Get All Authors
- Get Author By ID
- Search Authors
- Get All Categories
- Get Category By ID
- Search Categories
- Get My Notifications

## 5. Entity Framework Core and Dapper

The project uses both Entity Framework Core and Dapper for different responsibilities.

### Entity Framework Core

Used for write operations such as:

- Creating records.
- Updating records.
- Deleting records.
- Saving changes to SQL Server.

### Dapper

Used for read and search operations.

Examples:

- Retrieving all books.
- Retrieving books by ID.
- Searching books.
- Retrieving authors and categories.
- Searching categories.
- Retrieving notifications for a user.

### Read Operation Flow

```text
Controller
    ↓
Query
    ↓
Query Handler
    ↓
Query Interface
    ↓
Dapper Query Implementation
    ↓
SQL Server
```

### Write Operation Flow

```text
Controller
    ↓
Command
    ↓
Command Handler
    ↓
Repository
    ↓
Entity Framework Core
    ↓
SQL Server
```

## 6. Dapper Search Functionality

The application supports searching categories using partial names.

Example API:

```http
GET /api/categories/search?searchTerm=Program
```

The search uses SQL `LIKE` to find matching category names.

Example SQL:

```sql
SELECT *
FROM Categories
WHERE IsDeleted = 0
AND Name LIKE @SearchTerm
ORDER BY Name;
```

The search term is passed as a parameter to Dapper.

This allows users to search using partial text instead of remembering database IDs.

## 7. Notification Functionality

The application includes a notification feature.

When a new book is created, users receive a notification containing information about the new book.

Example notification message:

```text
A new book is available on store,
please have a look at [Book Name]
written by [Author Name].
```

Users can retrieve their notifications through a user-specific query.

The notification query uses Dapper to retrieve notifications belonging to the requested user.

## 8. Soft Delete

The application uses soft deletion instead of permanently removing records.

Example:

```csharp
entity.IsDeleted = true;
entity.DeletedAt = DateTime.Now;
entity.DeletedBy = userId;
```

The record remains in the database.

Normal read queries filter out deleted records:

```sql
WHERE IsDeleted = 0
```

This helps preserve historical and audit information.

## 9. Audit Information

The entities maintain audit information such as:

- CreatedAt
- CreatedBy
- UpdatedAt
- UpdatedBy
- DeletedAt
- DeletedBy
- IsDeleted

These fields help track record creation, updates, and deletion.

## 10. Getting Started

### Prerequisites

Install the following:

- .NET 8 SDK
- SQL Server
- SQL Server Management Studio (SSMS)
- Visual Studio or Visual Studio Code
- Postman

### Clone the Repository

```bash
git clone YOUR_GITHUB_REPOSITORY_URL
```

Navigate to the project directory:

```bash
cd LibraryManagement
```

### Configure Database Connection

Open:

```text
LibraryManagement.Api/appsettings.json
```

Configure your SQL Server connection string:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=LibraryManagementDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

Replace `YOUR_SERVER_NAME` with your SQL Server instance name.

### Restore Dependencies

```bash
dotnet restore
```

### Build the Project

```bash
dotnet build
```

### Run the API

```bash
dotnet run --project LibraryManagement.Api
```

Open the Swagger URL displayed in the terminal to explore and test available endpoints.

## 11. API Testing

The APIs can be tested using:

- Swagger UI
- Postman

For protected endpoints, provide a valid authentication token where required.

## 12. Key Learning Outcomes

Through this project, I gained practical experience with:

- ASP.NET Core Web API development.
- C# and .NET application architecture.
- SQL Server database integration.
- Entity Framework Core.
- Dapper and parameterized SQL queries.
- CQRS architecture.
- MediatR and request handlers.
- Repository pattern.
- DTO mapping.
- RESTful API development.
- Soft delete and audit fields.
- Exception handling.
- API testing using Postman.

## 13. Future Improvements

Potential future enhancements include:

- Advanced search with multiple filters.
- Pagination and sorting.
- Improved validation.
- Unit and integration testing.
- Logging and monitoring.
- Performance optimization.
- Improved API documentation.

---

**Project:** Library Management System  
**Framework:** ASP.NET Core Web API (.NET 8)  
**Database:** Microsoft SQL Server  
**Architecture:** Layered Architecture with CQRS and MediatR  
**Data Access:** Entity Framework Core and Dapper
