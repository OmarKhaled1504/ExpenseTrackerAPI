# 💸 ExpenseTrackerAPI

A secure and scalable RESTful API built with ASP.NET Core and Entity Framework Core for managing personal expenses. Includes features for user authentication, role-based access (admin/user), and CRUD operations for categories and expenses.

---

## 🚀 Features

- 🔐 JWT-based authentication with role support (Admin/User)
- 👥 User registration and login
- 💼 Admin-only management of expense categories
- 🧾 CRUD operations for personal expenses
- 🧼 DTO-based request/response models
- 📊 Role-based authorization for secure access control
- 🧠 Clean separation of concerns using service and repository patterns
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
├── Data/                  # EF DbContext and database initializer
├── Dtos/                  # Data Transfer Objects for requests/responses
├── Entities/              # Domain models (User, Expense, Category)
├── Mappings/              # Mapping extensions between entities and DTOs
├── Middleware/            # Global exception handling middleware
├── Repositories/          # Interfaces and implementations for data access
├── Services/              # Business logic for Auth, Expense, and Category
├── appsettings.json       # Main configuration file
├── Program.cs             # Entry point and application setup
└── ExpenseTrackerAPI.csproj # Project configuration file
```

---

## ⚙️ Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [MySQL Server](https://dev.mysql.com/downloads/mysql/)
- Visual Studio or VS Code

### Installation Steps

1. **Clone the repository**

```bash
git clone https://github.com/your-username/ExpenseTrackerAPI.git
cd ExpenseTrackerAPI
```

2. **Set your database connection string**

In `appsettings.json`:

```json
"ConnectionStrings": {{
  "DefaultConnection": "server=localhost;port=3306;database=ExpenseDb;user=root;password=yourpassword;"
}}
```

> 💡 For security, use [User Secrets](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets) or environment variables.

3. **Apply EF Core migrations**

```bash
dotnet ef database update
```

4. **Run the application**

```bash
dotnet run
```

Visit `https://localhost:5001` or `http://localhost:5000`

---

## 🔐 Authentication

- `POST /api/auth/register` – Register a new user
- `POST /api/auth/login` – Authenticate and receive a JWT

---

## 📬 API Endpoints

### Categories (Admin Only)

- `GET /api/categories` – Get all categories
- `POST /api/categories` – Create a new category
- `PUT /api/categories/{id}` – Update a category
- `DELETE /api/categories/{id}` – Delete a category

### Expenses (Authenticated Users)

- `GET /api/expenses` – List user expenses
- `POST /api/expenses` – Add an expense
- `PUT /api/expenses/{id}` – Update an expense
- `DELETE /api/expenses/{id}` – Delete an expense

> 🔐 All endpoints (except `/auth`) require `Authorization: Bearer <token>`

---

## 🧪 Testing

Use [Postman](https://www.postman.com/) or Swagger UI (`/swagger`) to test endpoints.

---

## 📄 License

Licensed under the [MIT License](LICENSE).

---

## 📫 Contact

Created by [Omar Khaled](https://github.com/OmarKhaled1504)**Inspired by [roadmap.sh](https://roadmap.sh/projects/expense-tracker-api).**
