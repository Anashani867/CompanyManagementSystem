# 🏗️ CompanyManagement API – Clean Architecture

## ✅ Overview
This is a backend system designed using Clean Architecture principles. It manages employees, projects, and their assignments with hours, along with secure authentication and role-based authorization.

---

## 📁 Project Structure

- API – ASP.NET Core Web API (Controllers, Program.cs)
- Core – Contains Entities, Interfaces
- Infrastructure – EF Core DbContext, Repositories
- Application – Business logic, Services, DTOs

---

## 🧩 Features

### ✅ Part 1: CRUD & Reporting

| Feature                                    | Status |
|-------------------------------------------|--------|
| Employee CRUD                             | ✅     |
| Project CRUD                              | ✅     |
| Many-to-Many: Employees ↔ Projects        | ✅     |
| Assignment with Working Hours             | ✅     |
| Reporting Endpoints (6 types)             | ✅     |
| Soft Delete for Employee & Project        | ✅     |

### 🔐 Part 2: Authentication & Authorization

| Feature                                    | Status |
|-------------------------------------------|--------|
| JWT Login                                 | ✅     |
| Role-Based Access (Admin, User)           | ✅     |
| Assign Only by Admin                      | ✅     |
| User sees only their own Projects         | ✅     |
| Token Validation & Expiration             | ✅     |
| Swagger 🔐 Integration                    | ✅     |
| Seeder for Admin/User & Roles             | ✅     |

---

## 🔐 Default Users

| Username | Password | Role  |
|----------|----------|-------|
| admin    | 123456   | Admin |
| user     | 123456   | User  |

---

## ▶️ How to Run

dotnet build
dotnet ef database update --project Infrastructure --startup-project API
dotnet run --project API

Swagger UI: https://localhost:{PORT}/swagger

---

## 🚀 Sample Endpoints

### Auth
POST /api/auth/login

### Employees
GET /api/employee
POST /api/employee
DELETE /api/employee/{id}

### Projects
GET /api/project
POST /api/project

### Assignments
POST /api/employeeproject/assign (Admin only)
GET /api/employeeproject/my-projects (User only)

### Reporting
GET /api/employeeproject/project-total-hours (Admin)
GET /api/employeeproject/employee-total-hours (Admin)

---

## 📌 Technologies

- ASP.NET Core 7 Web API
- Entity Framework Core (SQL Server)
- JWT Authentication
- Clean Architecture
- Swagger / Swashbuckle


