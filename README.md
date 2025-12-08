# 🏢 HR Management System

A comprehensive, full-stack Human Resource Management System built with modern technologies. This enterprise-grade application provides complete HR functionality including employee management, payroll processing, leave management, recruitment, performance reviews, and more.

![Angular](https://img.shields.io/badge/Angular-20-red?logo=angular)
![.NET](https://img.shields.io/badge/.NET-8.0-purple?logo=dotnet)
![SQL Server](https://img.shields.io/badge/SQL%20Server-2022-blue?logo=microsoftsqlserver)
![Bootstrap](https://img.shields.io/badge/Bootstrap-5.3-purple?logo=bootstrap)

## 📋 Project Overview

This HR Management System is a full-stack web application that provides a complete solution for managing human resources, including employee management, attendance tracking, leave management, payroll processing, performance management, recruitment, and training. The system follows industry best practices with a clean architecture approach on the backend and a modern, responsive Angular frontend.

## ✨ Features

### 👥 Employee Management

- Complete employee lifecycle management
- Employee profiles with detailed information
- Department and designation assignments
- Face recognition-based attendance tracking
- Employee self-service portal

### 💰 Payroll Management

- Automated payslip generation
- Allowances and deductions configuration
- Monthly/yearly payroll processing
- PDF/Excel export capabilities
- Salary comparison reports

### 📅 Leave Management

- Multiple leave types (Annual, Sick, Maternity, etc.)
- Leave request workflow with approvals
- Leave balance tracking
- Manager approval dashboard
- Monthly leave trend analytics

### 🎯 Recruitment

- Job posting management
- Candidate tracking system
- Application pipeline management
- Interview scheduling
- Application status workflow

### 📊 Performance Management

- Review cycles configuration
- KPI tracking and management
- Competency assessments
- 360-degree feedback system
- Goal setting and tracking

### 🎓 Training & Development

- Training course management
- Employee enrollment tracking
- Course completion tracking
- Skills development monitoring

### 📈 Reports & Analytics

- Interactive dashboard with real-time KPIs
- Employee distribution charts
- Payroll trend analysis
- Leave usage reports
- Recruitment pipeline analytics

### 🤖 AI-Powered Features

- RAG-based HR chatbot
- Natural language employee queries
- Intelligent data synchronization

### Additional Features

- **Notifications**: Real-time notification system
- **Chat/Messaging**: Internal communication system
- **Resignation Management**: Employee exit process management
- **Authentication & Authorization**: Secure login with role-based access control

## 🏗️ Architecture

This project follows a **Clean Architecture** pattern with clear separation of concerns:

### Backend Architecture (ASP.NET Core)

The backend is structured in layers following Clean Architecture principles:

```
backend/HRManagementSystem/
├── src/
│   ├── Core/
│   │   ├── Domain/                    # Enterprise business rules
│   │   │   ├── Entities/              # Core business entities
│   │   │   ├── ValueObjects/          # Domain value objects
│   │   │   └── Interfaces/            # Domain contracts
│   │   └── Application/               # Application business rules
│   │       ├── DTOs/                  # Data Transfer Objects
│   │       ├── Services/              # Business logic services
│   │       ├── Interfaces/            # Application contracts
│   │       └── Validators/            # FluentValidation rules
│   ├── Infrastructure/
│   │   └── Infrastructure/            # External concerns & data access
│   │       ├── Data/                  # EF Core DbContext
│   │       ├── Repositories/          # Data access implementations
│   │       └── Services/              # External service implementations
│   └── Presentation/
│       └── WebApi/                    # REST API layer
│           ├── Controllers/           # API endpoints
│           └── Middleware/            # HTTP pipeline components
```

**Architecture Principles:**

- **Dependency Rule**: Dependencies point inward (Domain has no dependencies)
- **Separation of Concerns**: Each layer has a specific responsibility
- **Dependency Inversion**: High-level modules don't depend on low-level modules
- **Clean Code**: Testable, maintainable, and scalable codebase

### Frontend Architecture (Angular)

The frontend follows Angular best practices with a modular, component-based architecture:

```
frontend/src/
├── app/
│   ├── Components/              # Feature components
│   │   ├── auth/               # Authentication components
│   │   ├── employees/          # Employee management
│   │   ├── Attendance/         # Attendance tracking
│   │   ├── Leaves/             # Leave management
│   │   ├── payroll/            # Payroll components
│   │   ├── performance/        # Performance management
│   │   ├── recruitment/        # Recruitment components
│   │   ├── training/           # Training components
│   │   ├── organization/       # Org structure components
│   │   ├── ess/                # Employee self-service
│   │   └── landing/            # Landing pages
│   ├── Services/               # Business logic services
│   ├── models/                 # TypeScript interfaces/models
│   ├── interceptors/           # HTTP interceptors
│   ├── layouts/                # Application layouts
│   ├── pages/                  # Page components
│   └── pipes/                  # Custom Angular pipes
```

**Frontend Patterns:**

- **Component-Based Architecture**: Reusable, modular components
- **Service Layer**: Centralized business logic and API communication
- **Reactive Programming**: RxJS for asynchronous operations
- **Route Guards**: Protected routes with authentication
- **HTTP Interceptors**: Centralized HTTP request/response handling

## 🛠️ Technology Stack

| Layer              | Technology             | Version |
| ------------------ | ---------------------- | ------- |
| **Frontend**       | Angular                | 20.3    |
| **UI Framework**   | Bootstrap              | 5.3     |
| **Icons**          | Eva Icons              | 1.1     |
| **Charts**         | Chart.js               | 4.x     |
| **Backend**        | ASP.NET Core           | 8.0     |
| **ORM**            | Entity Framework Core  | 8.0     |
| **Database**       | SQL Server             | 2022    |
| **Authentication** | JWT + ASP.NET Identity | -       |
| **Mapping**        | AutoMapper             | 15.1    |
| **Validation**     | FluentValidation       | 11.9    |
| **AI/RAG**         | LangChain + MongoDB    | -       |

## 🚀 Getting Started

### Prerequisites

Before you begin, ensure you have the following installed:

#### For Backend:

- **.NET SDK 8.0** or later - [Download here](https://dotnet.microsoft.com/download)
- **SQL Server** (2019 or later) or **SQL Server LocalDB** - [Download here](https://www.microsoft.com/sql-server/sql-server-downloads)
- **MongoDB** (for RAG features) - [Download here](https://www.mongodb.com/try/download/community)
- **Git** - [Download here](https://git-scm.com/downloads)

#### For Frontend:

- **Node.js** (v18 or later) and **npm** - [Download here](https://nodejs.org/)
- **Angular CLI** - Install globally: `npm install -g @angular/cli`

### Installation & Setup

#### 1. Clone the Repository

```bash
git clone https://github.com/Moztafaa/hr-management-system.git
cd hr-management-system
```

#### 2. Backend Setup

```bash
# Navigate to the backend directory
cd backend/HRManagementSystem

# Restore NuGet packages
dotnet restore

# Update the connection string in appsettings.json
# Edit: src/Presentation/HRManagementSystem.WebApi/appsettings.json
# Update the "DefaultConnection" string to match your SQL Server instance

# Apply database migrations
dotnet ef database update --project src/Infrastructure/HRManagementSystem.Infrastructure --startup-project src/Presentation/HRManagementSystem.WebApi

# Run the backend API
dotnet run --project src/Presentation/HRManagementSystem.WebApi
```

The API will be available at `http://localhost:5093`

#### 3. Frontend Setup

```bash
# Open a new terminal and navigate to the frontend directory
cd frontend

# Install npm dependencies
npm install

# Start the development server
npm start
# or
ng serve
```

The application will be available at: `http://localhost:4200`

### Default Access

After setup, you can access the application at `http://localhost:4200`. You can either:

- Register a new user account through the registration page
- Use default admin credentials (if configured in the backend's `appsettings.json`)
- Check `/backend/HRManagementSystem/README.md` for additional authentication setup details

## 🔐 Authentication

The system uses JWT-based authentication with role-based access control.

### Default Roles

- **HR** - Full system access
- **Manager** - Team management, approvals
- **Employee** - Self-service features

### Login Flow

1. POST to `/api/Account/login` with credentials
2. Receive JWT token in response
3. Include token in Authorization header: `Bearer {token}`

## 📡 API Overview

Base URL: `http://localhost:5093/api`

All responses follow the format:

```json
{
  "data": <T>,
  "errorMessage": "string",
  "hasError": boolean
}
```

### Core Endpoints

| Module             | Endpoint              | Description                        |
| ------------------ | --------------------- | ---------------------------------- |
| **Account**        | `/api/Account`        | Authentication & registration      |
| **Employee**       | `/api/Employee`       | Employee CRUD operations           |
| **Department**     | `/api/Department`     | Department management              |
| **Designation**    | `/api/Designation`    | Job titles/positions               |
| **Attendance**     | `/api/Attendance`     | Check-in/out with face recognition |
| **Leave**          | `/api/LeaveRequest`   | Leave request management           |
| **LeaveType**      | `/api/LeaveType`      | Leave type configuration           |
| **Payroll**        | `/api/payslips`       | Payslip generation & management    |
| **Allowance**      | `/api/Allowance`      | Salary allowances                  |
| **Deduction**      | `/api/Deduction`      | Salary deductions                  |
| **JobPosting**     | `/api/JobPosting`     | Recruitment postings               |
| **Candidate**      | `/api/Candidate`      | Candidate management               |
| **JobApplication** | `/api/JobApplication` | Application tracking               |
| **Performance**    | `/api/Performance`    | Reviews, KPIs, goals               |
| **Training**       | `/api/TrainingCourse` | Training courses                   |
| **Reporting**      | `/api/Reporting`      | Analytics & dashboards             |
| **Notification**   | `/api/Notification`   | User notifications                 |
| **ESS**            | `/api/ESS`            | Employee self-service              |

## 🎨 UI Theme

The application uses a modern dark theme with the following color palette:

| Variable          | Color   | Usage            |
| ----------------- | ------- | ---------------- |
| `--color-primary` | #3366ff | Primary actions  |
| `--color-success` | #00d68f | Success states   |
| `--color-warning` | #ffaa00 | Warnings         |
| `--color-danger`  | #ff3d71 | Errors/deletions |
| `--color-info`    | #0095ff | Information      |
| `--card-bg`       | #222b45 | Card backgrounds |
| `--body-bg`       | #151a30 | Page background  |

## 📊 Dashboard

The dashboard provides real-time insights:

- **Stats Cards**: Total employees, departments, leave requests, job postings
- **Charts**:
  - Employees by Department (Bar)
  - Designation Distribution (Doughnut)
  - Leave Status Breakdown (Pie)
  - Monthly Leave Trend (Line)
- **Quick Actions**: Fast navigation to common tasks
- **Recent Activities**: Latest system events
- **System Status**: API and database health

## 📝 Development Workflow

### Backend Development

#### Adding a New Feature

1. Create entity in `Domain/Entities/`
2. Add repository interface in `Domain/Interfaces/`
3. Create DTOs in `Application/DTOs/`
4. Implement service in `Application/Services/`
5. Add repository implementation in `Infrastructure/Repositories/`
6. Create controller in `WebApi/Controllers/`

#### Database Migrations

```bash
# Create a new migration
dotnet ef migrations add MigrationName --project src/Infrastructure/HRManagementSystem.Infrastructure --startup-project src/Presentation/HRManagementSystem.WebApi

# Update database
dotnet ef database update --project src/Infrastructure/HRManagementSystem.Infrastructure --startup-project src/Presentation/HRManagementSystem.WebApi
```

### Frontend Development

#### Generate New Components

```bash
ng generate component components/feature-name
ng generate service services/feature-name
ng generate interface models/feature-name
```

#### Build for Production

```bash
# Backend
cd backend/HRManagementSystem
dotnet build --configuration Release

# Frontend
cd frontend
ng build --configuration production
```

## 🔧 Configuration

### Environment Variables

| Variable                               | Description         | Default              |
| -------------------------------------- | ------------------- | -------------------- |
| `ASPNETCORE_ENVIRONMENT`               | Environment mode    | Development          |
| `JWT_SECRET`                           | JWT signing key     | (set in appsettings) |
| `ConnectionStrings__DefaultConnection` | Database connection | LocalDB              |

### appsettings.json

```json
{
  "JWT": {
    "SecretKey": "your-secret-key",
    "Issuer": "HRManagementSystem",
    "Audience": "HRManagementSystemUsers",
    "ExpiryInDays": 7
  },
  "MongoDBSettings": {
    "ConnectionString": "mongodb://localhost:27017/",
    "DatabaseName": "HRManagementSystemRAG"
  }
}
```

## 📚 Project Structure

- **/backend** - ASP.NET Core Web API following Clean Architecture
- **/frontend** - Angular application with Bootstrap UI
- **/backend/HRManagementSystem/README.md** - Detailed backend documentation
- **/frontend/README.md** - Detailed frontend documentation

## 🤝 Contributing

We welcome contributions to the HR Management System! Please follow these guidelines:

### Code Standards

1. Follow Clean Architecture principles
2. Keep dependencies pointing inward (Domain has no dependencies)
3. Use interfaces for external dependencies
4. Maintain code consistency with existing patterns
5. Update documentation when adding new features

### Contribution Process

1. Fork the repository
2. Create a feature branch (`feature/your-feature-name` or `bugfix/issue-description`)
3. Make your changes following the code standards
4. Write or update tests as needed
5. Commit your changes with clear, descriptive messages
6. Push to your fork and submit a Pull Request
7. Wait for code review and address any feedback

### Pull Request Guidelines

- Provide a clear description of the changes
- Reference any related issues
- Ensure all tests pass
- Update documentation if needed
- Keep PRs focused on a single feature or fix

## 📄 License

This project is licensed under the MIT License.


## 📧 Contact

For questions or support, please open an issue in the GitHub repository.