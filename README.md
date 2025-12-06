# Full-Stack HR Management System

A comprehensive, modern HR Management System built with Angular and ASP.NET Core, designed to streamline and automate human resource management processes for organizations of all sizes.

## 📋 Project Overview

This HR Management System is a full-stack web application that provides a complete solution for managing human resources, including employee management, attendance tracking, leave management, payroll processing, performance management, recruitment, and training. The system follows industry best practices with a clean architecture approach on the backend and a modern, responsive Angular frontend.

## ✨ Key Features

### Core HR Management
- **Employee Management**: Complete employee lifecycle management including onboarding, profile management, and offboarding
- **Department & Designation**: Organizational structure management with departments and designations
- **Employee Self-Service (ESS)**: Portal for employees to manage their own information and requests

### Time & Attendance
- **Attendance Tracking**: Real-time attendance monitoring and management
- **Leave Management**: Comprehensive leave request, approval, and balance tracking system
- **Leave Types**: Configurable leave types to match organizational policies

### Payroll & Compensation
- **Payroll Processing**: Automated payroll calculation and management
- **Payslip Generation**: Digital payslip creation and distribution
- **Allowances**: Manage various employee allowances
- **Deductions**: Track and manage employee deductions

### Performance Management
- **Performance Reviews**: Structured employee performance evaluation system
- **Review Cycles**: Configurable review periods and cycles
- **Goals & KPIs**: Goal setting and Key Performance Indicator tracking
- **Feedback System**: 360-degree feedback collection
- **Competency Management**: Skills and competency assessment

### Recruitment
- **Job Postings**: Create and manage job openings
- **Candidate Management**: Track and manage job applicants
- **Job Applications**: Application submission and processing workflow

### Training & Development
- **Training Courses**: Manage training programs and courses
- **Employee Training**: Track employee training participation and completion
- **Training Requests**: Request and approval workflow for training

### Additional Features
- **Notifications**: Real-time notification system
- **Reporting**: Comprehensive reporting capabilities
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

### Backend
- **Framework**: ASP.NET Core 8.0
- **Database**: SQL Server with Entity Framework Core
- **API Documentation**: Swagger/OpenAPI
- **Validation**: FluentValidation
- **Architecture**: Clean Architecture

### Frontend
- **Framework**: Angular 20.3.x
- **UI Library**: Bootstrap 5.3.x
- **Icons**: Eva Icons
- **Markdown**: Marked library for rendering
- **Build Tool**: Angular CLI
- **Testing**: Jasmine & Karma

### Development Tools
- **Version Control**: Git
- **Package Management**: npm (frontend), NuGet (backend)
- **Editor Config**: Consistent coding styles

## 🚀 Getting Started

### Prerequisites

Before you begin, ensure you have the following installed:

#### For Backend:
- **.NET SDK 8.0** or later - [Download here](https://dotnet.microsoft.com/download)
- **SQL Server** (2019 or later) or **SQL Server LocalDB** - [Download here](https://www.microsoft.com/sql-server/sql-server-downloads)
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

The API will be available at:
- HTTPS: `https://localhost:5001`
- HTTP: `http://localhost:5000`
- Swagger UI: `https://localhost:5001/swagger`

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

#### Running Tests
```bash
# Backend (if tests are available)
dotnet test

# Frontend
ng test
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