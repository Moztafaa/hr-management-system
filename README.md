# 🏢 HR Management System

A comprehensive, full-stack Human Resource Management System built with modern technologies. This enterprise-grade application provides complete HR functionality including employee management, payroll processing, leave management, recruitment, performance reviews, and more.

![Angular](https://img.shields.io/badge/Angular-20-red?logo=angular)
![.NET](https://img.shields.io/badge/.NET-8.0-purple?logo=dotnet)
![SQL Server](https://img.shields.io/badge/SQL%20Server-2022-blue?logo=microsoftsqlserver)
![Bootstrap](https://img.shields.io/badge/Bootstrap-5.3-purple?logo=bootstrap)

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

## 🛠️ Technology Stack

| Layer | Technology | Version |
|-------|-----------|---------|
| **Frontend** | Angular | 20.3 |
| **UI Framework** | Bootstrap | 5.3 |
| **Icons** | Eva Icons | 1.1 |
| **Charts** | Chart.js | 4.x |
| **Backend** | ASP.NET Core | 8.0 |
| **ORM** | Entity Framework Core | 8.0 |
| **Database** | SQL Server | 2022 |
| **Authentication** | JWT + ASP.NET Identity | - |
| **Mapping** | AutoMapper | 15.1 |
| **Validation** | FluentValidation | 11.9 |
| **AI/RAG** | LangChain + MongoDB | - |

## 📁 Project Structure

```
hr-management-system/
├── backend/
│   └── HRManagementSystem/
│       └── src/
│           ├── Core/
│           │   ├── HRManagementSystem.Application/    # Business logic, DTOs, Services
│           │   └── HRManagementSystem.Domain/         # Entities, Interfaces
│           ├── Infrastructure/
│           │   └── HRManagementSystem.Infrastructure/ # EF Core, Repositories
│           └── Presentation/
│               └── HRManagementSystem.WebApi/         # Controllers, Program.cs
├── frontend/
│   └── src/
│       ├── app/
│       │   ├── Components/     # Reusable UI components
│       │   ├── pages/          # Page components
│       │   ├── Services/       # API services
│       │   ├── models/         # TypeScript interfaces
│       │   └── guards/         # Route guards
│       ├── assets/             # Static assets
│       └── styles.css          # Global styles
└── README.md
```

## 🚀 Getting Started

### Prerequisites

- **Node.js** 18+ and npm
- **.NET SDK** 8.0
- **SQL Server** 2019+ (or LocalDB)
- **MongoDB** (for RAG features)

### Backend Setup

1. Navigate to the backend directory:
   ```bash
   cd backend/HRManagementSystem/src/Presentation/HRManagementSystem.WebApi
   ```

2. Update connection string in `appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=.;Database=HRManagementSystem;Trusted_Connection=True;TrustServerCertificate=True;"
     }
   }
   ```

3. Run database migrations:
   ```bash
   dotnet ef database update
   ```

4. Start the API server:
   ```bash
   dotnet run
   ```
   
   API will be available at `http://localhost:5093`

### Frontend Setup

1. Navigate to the frontend directory:
   ```bash
   cd frontend
   ```

2. Install dependencies:
   ```bash
   npm install
   ```

3. Start the development server:
   ```bash
   ng serve
   ```
   
   Application will be available at `http://localhost:4200`

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

| Module | Endpoint | Description |
|--------|----------|-------------|
| **Account** | `/api/Account` | Authentication & registration |
| **Employee** | `/api/Employee` | Employee CRUD operations |
| **Department** | `/api/Department` | Department management |
| **Designation** | `/api/Designation` | Job titles/positions |
| **Attendance** | `/api/Attendance` | Check-in/out with face recognition |
| **Leave** | `/api/LeaveRequest` | Leave request management |
| **LeaveType** | `/api/LeaveType` | Leave type configuration |
| **Payroll** | `/api/payslips` | Payslip generation & management |
| **Allowance** | `/api/Allowance` | Salary allowances |
| **Deduction** | `/api/Deduction` | Salary deductions |
| **JobPosting** | `/api/JobPosting` | Recruitment postings |
| **Candidate** | `/api/Candidate` | Candidate management |
| **JobApplication** | `/api/JobApplication` | Application tracking |
| **Performance** | `/api/Performance` | Reviews, KPIs, goals |
| **Training** | `/api/TrainingCourse` | Training courses |
| **Reporting** | `/api/Reporting` | Analytics & dashboards |
| **Notification** | `/api/Notification` | User notifications |
| **ESS** | `/api/ESS` | Employee self-service |

## 🎨 UI Theme

The application uses a modern dark theme with the following color palette:

| Variable | Color | Usage |
|----------|-------|-------|
| `--color-primary` | #3366ff | Primary actions |
| `--color-success` | #00d68f | Success states |
| `--color-warning` | #ffaa00 | Warnings |
| `--color-danger` | #ff3d71 | Errors/deletions |
| `--color-info` | #0095ff | Information |
| `--card-bg` | #222b45 | Card backgrounds |
| `--body-bg` | #151a30 | Page background |

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

## 🔧 Configuration

### Environment Variables

| Variable | Description | Default |
|----------|-------------|---------|
| `ASPNETCORE_ENVIRONMENT` | Environment mode | Development |
| `JWT_SECRET` | JWT signing key | (set in appsettings) |
| `ConnectionStrings__DefaultConnection` | Database connection | LocalDB |

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

## 🧪 Testing

### Backend Tests
```bash
cd backend/HRManagementSystem
dotnet test
```

### Frontend Tests
```bash
cd frontend
ng test
```

## 📦 Deployment

### Docker (Recommended)

```bash
# Build and run with Docker Compose
docker-compose up -d
```

### Manual Deployment

1. Build frontend:
   ```bash
   cd frontend
   ng build --configuration production
   ```

2. Publish backend:
   ```bash
   cd backend/HRManagementSystem
   dotnet publish -c Release
   ```

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch: `git checkout -b feature/amazing-feature`
3. Commit changes: `git commit -m 'Add amazing feature'`
4. Push to branch: `git push origin feature/amazing-feature`
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License.

## 👥 Authors

- **Mohamed Wazery** - *Initial work*

## 🙏 Acknowledgments

- Angular Team for the amazing framework
- Microsoft for .NET Core and Entity Framework
- Bootstrap team for the UI components
- Eva Icons for beautiful iconography

---

**Made with ❤️ for modern HR teams**