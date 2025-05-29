# 💸 ExpenseTrackerAPI

A secure and scalable RESTful API built with ASP.NET Core and Entity Framework Core for managing personal expenses. Includes robust user authentication, role-based access (admin/user), and advanced expense and category management with pagination and filtering.

---

## 🚀 Features

- 🔐 JWT-based authentication with role support (Admin/User)
- 👥 User registration and login
- 💼 Admin-only management of expense categories (create, update, delete)
- 🧾 CRUD operations for personal expenses
- 🔍 Filtering and pagination support for expense queries
- 🧼 DTO-based request/response models with data annotations for automatic validation
- 🧠 Clean separation of concerns using service, repository, and Unit of Work patterns
- 🔁 Unit of Work to manage transactional integrity across multiple repositories
- 📊 Role-based authorization for secure access control
- 🐬 MySQL database integration via Pomelo provider

---

## 🛠️ Technologies Used

- **ASP.NET Core 8**
- **Entity Framework Core**
- **Pomelo.EntityFrameworkCore.MySql**
- **MySQL**
- **C#**

---

## 📁 Project Structure

```
ExpenseTrackerAPI/
├── Controllers/           # API controllers for Auth, Categories, and Expenses
├── Data/                  # EF DbContext and configurations
├── Dtos/                  # Data Transfer Objects with validation annotations
│   ├── CategoriesDtos/
│   ├── ExpensesDtos/
│   └── UsersDtos/
├── Entities/              # Domain models (User, Expense, Category)
├── Mapping/               # AutoMapper profiles
├── Migrations/            # EF Core migrations
├── Repositories/          # Repository interfaces and implementations
├── Responses/             # Standardized API response models
├── Services/              # Business logic, Unit of Work, and validation rules
├── appsettings.json       # App configuration
├── Program.cs             # Application entry point
└── README.md              # Project documentation
```

---

## 🧪 Getting Started

1. Clone the repository:
   ```bash
   git clone https://github.com/your-username/ExpenseTrackerAPI.git
   cd ExpenseTrackerAPI
   ```

2. Configure the database connection string in `appsettings.json` or use user secrets.

3. Apply migrations and update the database:
   ```bash
   dotnet ef database update
   ```

4. Run the application:
   ```bash
   dotnet run
   ```

5. Explore the API using Swagger at `https://localhost:{port}/swagger`.

---

## 🔁 Unit of Work

The project uses the **Unit of Work** pattern to coordinate changes across multiple repositories (`Expenses`, `Categories`, `Users`). All data mutations are committed via a single call to:

```csharp
await _unitOfWork.CompleteAsync();
```

This ensures consistency, simplifies testing, and avoids scattered calls to `SaveChangesAsync()`.

---

## 🧼 Automatic Validation via DTO Annotations

All input models (DTOs) include validation annotations like `[Required]`, `[StringLength]`, and `[Range]`. Combined with ASP.NET Core's `[ApiController]` attribute, invalid requests are **automatically rejected** with `400 Bad Request` responses.

Example:
```csharp
public class ExpenseCreateDto
{
    [Required]
    [StringLength(50)]
    public string Title { get; set; }

    [Range(0.01, 10000)]
    public decimal Amount { get; set; }

    [Required]
    public int CategoryId { get; set; }
}
```

---

## 🔐 Authentication

- `POST /api/auth/register` – Register a new user
- `POST /api/auth/login` – Authenticate and receive a JWT

---

## 📬 API Endpoints

### Categories (Admin Only)

- `GET /api/categories` – Get all categories (with pagination)
- `POST /api/categories` – Create a new category
- `PUT /api/categories/{id}` – Update a category
- `DELETE /api/categories/{id}` – Delete a category

### Expenses (Authenticated Users)

- `GET /api/expenses` – List user expenses (supports filtering by date or keyword + pagination)
- `POST /api/expenses` – Add an expense
- `PUT /api/expenses/{id}` – Update an expense
- `DELETE /api/expenses/{id}` – Delete an expense

> 🔐 All endpoints (except `/auth`) require `Authorization: Bearer <token>`

---

## 🧪 Testing

Use [Postman](https://www.postman.com/) or Swagger UI (`/swagger`) to test endpoints. Invalid inputs will return validation errors thanks to DTO annotations.

---

## 📄 License

Licensed under the [MIT License](LICENSE).

---

## 📫 Contact

Created by [Omar Khaled](https://github.com/OmarKhaled1504).

**Inspired by [roadmap.sh](https://roadmap.sh/projects/expense-tracker-api).**
