# HR Management System - Copilot Instructions

## Overview

This file provides guidance for AI coding assistants to generate consistent, convention-following features for the HR Management System. All patterns and conventions described here are based on actual observed patterns from the codebase.

The project is a **Full-Stack HR Management System** with:

- **Backend**: ASP.NET Core 8.0 Web API with Clean Architecture
- **Frontend**: Angular 20 with Standalone Components
- **Database**: SQL Server with Entity Framework Core
- **Authentication**: JWT Bearer tokens with ASP.NET Core Identity

---

## File Category Reference

### 1. API Controllers

**Purpose**: Handle HTTP requests and route to appropriate services.

**Examples**:

- `backend/HRManagementSystem/src/Presentation/HRManagementSystem.WebApi/Controllers/EmployeeController.cs`
- `backend/HRManagementSystem/src/Presentation/HRManagementSystem.WebApi/Controllers/BaseApiController.cs`

**Conventions**:

- Use C# 12 primary constructors for DI: `public class EmployeeController(IEmployeeService employeeService) : ControllerBase`
- Apply `[Route("api/[controller]")]` and `[ApiController]` attributes
- All async actions accept `CancellationToken` as last parameter
- Return `Response<T>` wrapper via `Ok(response)`
- Use `[FromBody]` for JSON, `[FromForm]` for file uploads

### 2. Application Services

**Purpose**: Contain business logic, orchestrate data access and mapping.

**Examples**:

- `backend/HRManagementSystem/src/Core/HRManagementSystem.Application/Services/EmployeeService.cs`

**Conventions**:

- Use primary constructors: `public class EmployeeService(IUnitOfWork _unitOfWork, IMapper _mapper) : IEmployeeService`
- All methods return `Response<T>` record
- Wrap operations in try-catch, returning `Response` with `HasError=true` on exception
- Use `_unitOfWork.Repository<T>()` for data access
- Use `_mapper.Map<>()` for DTO conversions
- Always call `_unitOfWork.SaveChangesAsync()` after writes

### 3. DTOs (Data Transfer Objects)

**Purpose**: Define API request/response contracts.

**Examples**:

- `backend/HRManagementSystem/src/Core/HRManagementSystem.Application/DTOs/Employee/EmployeeDto.cs`
- `backend/HRManagementSystem/src/Core/HRManagementSystem.Application/DTOs/Employee/CreateEmployeeDto.cs`

**Conventions**:

- Naming: `{Entity}Dto`, `Create{Entity}Dto`, `Update{Entity}Dto`
- Use `required` modifier for mandatory strings: `public required string FirstName { get; set; }`
- Use `?` for nullable properties: `public string? Gender { get; set; }`
- Organize in domain folders: `DTOs/Employee/`, `DTOs/Leaves/`
- No navigation properties, only foreign key IDs

### 4. Domain Entities

**Purpose**: Core business entities persisted to database.

**Examples**:

- `backend/HRManagementSystem/src/Core/HRManagementSystem.Domain/Entities/Employee.cs`
- `backend/HRManagementSystem/src/Core/HRManagementSystem.Domain/Entities/BaseEntity.cs`

**Conventions**:

- All entities inherit from `BaseEntity` (provides `Id`, `CreatedAt`, `UpdatedAt`)
- Use `required` for mandatory strings
- Include navigation properties with nullable FKs: `public Department? Department { get; set; }`
- Use `ICollection<T>` for one-to-many: `public ICollection<LeaveRequest> LeaveRequests { get; set; }`

### 5. Repositories

**Purpose**: Data access layer implementing Repository pattern.

**Examples**:

- `backend/HRManagementSystem/src/Infrastructure/HRManagementSystem.Infrastructure/Repositories/Repository.cs`
- `backend/HRManagementSystem/src/Infrastructure/HRManagementSystem.Infrastructure/Repositories/UnitOfWork.cs`

**Conventions**:

- Base `Repository<T>` provides CRUD operations
- Create specialized repositories for complex queries
- Access via `UnitOfWork`: `_unitOfWork.Repository<Employee>()`
- All methods accept `CancellationToken`

### 6. AutoMapper Profiles

**Purpose**: Configure entity-to-DTO mapping.

**Examples**:

- `backend/HRManagementSystem/src/Core/HRManagementSystem.Application/Mappings/EmployeeProfile.cs`

**Conventions**:

- One profile per domain area
- Inherit from `Profile`
- Create bidirectional maps
- Use `ForMember()` for property name differences

### 7. Angular Components

**Purpose**: UI components for the frontend.

**Examples**:

- `frontend/src/app/Components/employees/employee-list/employee-list.ts`
- `frontend/src/app/Components/auth/login/login.component.ts`

**Conventions**:

- Use `standalone: true` and explicitly import dependencies
- Use `inject()` for dependency injection
- Track `isLoading` and `error` states
- Handle API responses checking `response.hasError`
- Use Bootstrap 5 classes and Eva Icons

### 8. Angular Services

**Purpose**: API communication and state management.

**Examples**:

- `frontend/src/app/Services/employee.ts`
- `frontend/src/app/Services/auth.service.ts`

**Conventions**:

- Use `@Injectable({ providedIn: 'root' })`
- Use `inject(HttpClient)` for HTTP calls
- Return `Observable<ApiResponse<T>>`
- Define private `apiUrl` for backend endpoint

### 9. Angular Models

**Purpose**: TypeScript interfaces for data structures.

**Examples**:

- `frontend/src/app/models/employee.ts`
- `frontend/src/app/models/api-response.ts`

**Conventions**:

- Use `interface` keyword
- Match backend DTO structure
- Use `?` for optional properties

---

## Feature Scaffold Guide

When implementing a new feature, follow these steps:

### Step 1: Create Domain Entity (if new entity)

**Location**: `backend/HRManagementSystem/src/Core/HRManagementSystem.Domain/Entities/`

```csharp
namespace HRManagementSystem.Domain.Entities;

public class NewEntity : BaseEntity
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    // Navigation properties
}
```

### Step 2: Create DTOs

**Location**: `backend/HRManagementSystem/src/Core/HRManagementSystem.Application/DTOs/{EntityName}/`

Create:

- `{Entity}Dto.cs` - Response DTO
- `Create{Entity}Dto.cs` - Create request
- `Update{Entity}Dto.cs` - Update request

### Step 3: Create AutoMapper Profile

**Location**: `backend/HRManagementSystem/src/Core/HRManagementSystem.Application/Mappings/`

```csharp
public class NewEntityProfile : Profile
{
    public NewEntityProfile()
    {
        CreateMap<NewEntity, NewEntityDto>();
        CreateMap<CreateNewEntityDto, NewEntity>();
        CreateMap<UpdateNewEntityDto, NewEntity>();
    }
}
```

### Step 4: Create Service Interface and Implementation

**Locations**:

- Interface: `backend/HRManagementSystem/src/Core/HRManagementSystem.Application/Interfaces/I{Entity}Service.cs`
- Implementation: `backend/HRManagementSystem/src/Core/HRManagementSystem.Application/Services/{Entity}Service.cs`

### Step 5: Create Repository (if custom queries needed)

**Location**: `backend/HRManagementSystem/src/Infrastructure/HRManagementSystem.Infrastructure/Repositories/`

### Step 6: Register in DI

**Location**: `backend/HRManagementSystem/src/Core/HRManagementSystem.Application/DependencyInjection.cs`

```csharp
services.AddScoped<INewEntityService, NewEntityService>();
services.AddAutoMapper(x => x.AddProfile(new NewEntityProfile()));
```

### Step 7: Create Controller

**Location**: `backend/HRManagementSystem/src/Presentation/HRManagementSystem.WebApi/Controllers/`

```csharp
[Route("api/[controller]")]
[ApiController]
public class NewEntityController(INewEntityService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await service.GetAllAsync(cancellationToken);
        return Ok(result);
    }
}
```

### Step 8: Create Angular Model

**Location**: `frontend/src/app/models/`

```typescript
export interface NewEntity {
  id: number;
  name: string;
  description?: string;
}
```

### Step 9: Create Angular Service

**Location**: `frontend/src/app/Services/`

```typescript
@Injectable({ providedIn: "root" })
export class NewEntityService {
  private http = inject(HttpClient);
  private apiUrl = "http://localhost:5093/api/NewEntity";

  getAll(): Observable<ApiResponse<NewEntity[]>> {
    return this.http.get<ApiResponse<NewEntity[]>>(this.apiUrl);
  }
}
```

### Step 10: Create Angular Component

**Location**: `frontend/src/app/Components/{feature-name}/`

```typescript
@Component({
  selector: "app-new-entity-list",
  standalone: true,
  imports: [CommonModule],
  templateUrl: "./new-entity-list.html",
  styleUrls: ["./new-entity-list.css"],
})
export class NewEntityList implements OnInit {
  private service = inject(NewEntityService);
  items: NewEntity[] = [];
  isLoading = true;
  error: string | null = null;

  ngOnInit() {
    this.loadItems();
  }

  loadItems() {
    this.service.getAll().subscribe({
      next: (res) => {
        if (!res.hasError && res.data) {
          this.items = res.data;
        } else {
          this.error = res.errorMessage;
        }
        this.isLoading = false;
      },
      error: () => {
        this.error = "An error occurred";
        this.isLoading = false;
      },
    });
  }
}
```

### Step 11: Add Route

**Location**: `frontend/src/app/app.routes.ts`

```typescript
{
  path: 'new-entity',
  loadComponent: () =>
    import('./Components/new-entity/new-entity-list/new-entity-list')
      .then((m) => m.NewEntityList),
}
```

---

## Integration Rules

### Backend Constraints

1. **All services must use `IUnitOfWork`** for data access
2. **All API responses must use `Response<T>` wrapper**
3. **All entities must inherit from `BaseEntity`**
4. **Use `CancellationToken` in all async methods**
5. **Register all services in `DependencyInjection.cs`**

### Frontend Constraints

1. **All components must be standalone**
2. **Use `inject()` for dependency injection**
3. **Handle `ApiResponse<T>` checking `hasError` property**
4. **Protected routes must use `authGuard`**
5. **All authenticated pages nest under `MainLayoutComponent`**

### API Communication

1. **Backend URL**: `http://localhost:5093/api/{controller}`
2. **Response format**: `{ data: T, errorMessage: string, hasError: boolean }`
3. **Auth header**: `Authorization: Bearer {token}`

### Styling

1. **Use Bootstrap 5 utility classes**
2. **Use Eva Icons with `eva eva-{icon}-outline` pattern**
3. **Loading state**: Show Bootstrap spinner
4. **Errors**: Display in Bootstrap alert

---

## Example Prompt Usage

### Request:

> "Create a training certificate management feature where employees can view their training certificates"

### Expected Response Files:

**Backend:**

- `Domain/Entities/TrainingCertificate.cs`
- `Application/DTOs/Training/TrainingCertificateDto.cs`
- `Application/DTOs/Training/CreateTrainingCertificateDto.cs`
- `Application/Interfaces/ITrainingCertificateService.cs`
- `Application/Services/TrainingCertificateService.cs`
- `Application/Mappings/TrainingCertificateProfile.cs`
- `WebApi/Controllers/TrainingCertificateController.cs`

**Frontend:**

- `models/training-certificate.ts`
- `Services/training-certificate.ts`
- `Components/training/certificate-list/certificate-list.ts`
- `Components/training/certificate-list/certificate-list.html`
- `Components/training/certificate-list/certificate-list.css`

**Route Addition in `app.routes.ts`:**

```typescript
{ path: 'training/certificates', loadComponent: () =>
  import('./Components/training/certificate-list/certificate-list')
    .then(m => m.CertificateList)
}
```

---

## Technology Reference

| Layer              | Technology                  | Version |
| ------------------ | --------------------------- | ------- |
| Backend Framework  | ASP.NET Core                | 8.0     |
| ORM                | Entity Framework Core       | 8.0     |
| Database           | SQL Server                  | -       |
| Auth               | ASP.NET Core Identity + JWT | -       |
| Mapping            | AutoMapper                  | 15.1    |
| Validation         | FluentValidation            | 11.9    |
| Frontend Framework | Angular                     | 20.3    |
| UI Library         | Bootstrap                   | 5.3     |
| Icons              | Eva Icons                   | 1.1     |
| HTTP               | RxJS Observables            | 7.8     |

---

## API Documentation

Base URL: `http://localhost:5093/api`

All endpoints return responses in the format:

```json
{
  "data": T,
  "errorMessage": "string",
  "hasError": boolean
}
```

---

### Account Controller

**Base Route**: `/api/Account`

| Method | Endpoint             | Description                   | Request Body          | Response                      |
| ------ | -------------------- | ----------------------------- | --------------------- | ----------------------------- |
| POST   | `/login`             | Authenticate user             | `LoginDto`            | `AuthResponseDto`             |
| POST   | `/assign-role`       | Assign role to user           | `AssignRoleDto`       | `boolean`                     |
| POST   | `/register-employee` | Register new employee account | `RegisterEmployeeDto` | `RegisterEmployeeResponseDto` |

---

### Allowance Controller

**Base Route**: `/api/Allowance`

| Method | Endpoint | Description          | Request Body         | Response                    |
| ------ | -------- | -------------------- | -------------------- | --------------------------- |
| GET    | `/`      | Get all allowances   | -                    | `IEnumerable<AllowanceDto>` |
| GET    | `/{id}`  | Get allowance by ID  | -                    | `AllowanceDto`              |
| POST   | `/`      | Create new allowance | `CreateAllowanceDto` | `AllowanceDto`              |
| PUT    | `/{id}`  | Update allowance     | `UpdateAllowanceDto` | `204 No Content`            |
| DELETE | `/{id}`  | Delete allowance     | -                    | `204 No Content`            |

---

### Attendance Controller

**Base Route**: `/api/Attendance`

| Method | Endpoint                  | Description                     | Request Body                           | Response              |
| ------ | ------------------------- | ------------------------------- | -------------------------------------- | --------------------- |
| POST   | `/{employeeId}/check-in`  | Check in employee               | `CheckInRequestDto` (Form with Image)  | `int` (attendance ID) |
| POST   | `/{employeeId}/check-out` | Check out employee              | `CheckOutRequestDto` (Form with Image) | `int` (attendance ID) |
| GET    | `/employee/{employeeId}`  | Get employee attendance records | -                                      | Attendance data       |

---

### Candidate Controller

**Base Route**: `/api/Candidate`

| Method | Endpoint                    | Description                                 | Request Body         | Response                    |
| ------ | --------------------------- | ------------------------------------------- | -------------------- | --------------------------- |
| GET    | `/`                         | Get all candidates                          | -                    | `IEnumerable<CandidateDto>` |
| GET    | `/{id}`                     | Get candidate by ID                         | -                    | `CandidateDto`              |
| GET    | `/{id}/applications`        | Get candidate with application history      | -                    | `CandidateDto`              |
| GET    | `/email/{email}`            | Find candidate by email                     | -                    | `CandidateDto`              |
| GET    | `/search?searchTerm={term}` | Search candidates by name, email, or skills | -                    | `IEnumerable<CandidateDto>` |
| GET    | `/check-email/{email}`      | Check if email exists                       | -                    | `boolean`                   |
| POST   | `/`                         | Create new candidate                        | `CreateCandidateDto` | `CandidateDto`              |
| PUT    | `/{id}`                     | Update candidate                            | `UpdateCandidateDto` | `204 No Content`            |
| DELETE | `/{id}`                     | Delete candidate                            | -                    | `204 No Content`            |

---

### Deduction Controller

**Base Route**: `/api/Deduction`

| Method | Endpoint | Description          | Request Body         | Response                    |
| ------ | -------- | -------------------- | -------------------- | --------------------------- |
| GET    | `/`      | Get all deductions   | -                    | `IEnumerable<DeductionDto>` |
| GET    | `/{id}`  | Get deduction by ID  | -                    | `DeductionDto`              |
| POST   | `/`      | Create new deduction | `CreateDeductionDto` | `DeductionDto`              |
| PUT    | `/{id}`  | Update deduction     | `UpdateDeductionDto` | `204 No Content`            |
| DELETE | `/{id}`  | Delete deduction     | -                    | `204 No Content`            |

---

### Department Controller

**Base Route**: `/api/Department`

| Method | Endpoint          | Description                   | Request Body          | Response                     |
| ------ | ----------------- | ----------------------------- | --------------------- | ---------------------------- |
| GET    | `/`               | Get all departments           | -                     | `IEnumerable<DepartmentDto>` |
| GET    | `/{id}`           | Get department by ID          | -                     | `DepartmentDto`              |
| POST   | `/`               | Create new department         | `CreateDepartmentDto` | `DepartmentDto`              |
| PUT    | `/{id}`           | Update department             | `UpdateDepartmentDto` | `204 No Content`             |
| DELETE | `/{id}`           | Delete department             | -                     | `204 No Content`             |
| GET    | `/{id}/employees` | Get department with employees | -                     | `DepartmentWithEmployeesDto` |

---

### Designation Controller

**Base Route**: `/api/Designation`

| Method | Endpoint          | Description                    | Request Body           | Response                      |
| ------ | ----------------- | ------------------------------ | ---------------------- | ----------------------------- |
| GET    | `/`               | Get all designations           | -                      | `IEnumerable<DesignationDto>` |
| GET    | `/{id}`           | Get designation by ID          | -                      | `DesignationDto`              |
| POST   | `/`               | Create new designation         | `CreateDesignationDto` | `DesignationDto`              |
| PUT    | `/{id}`           | Update designation             | `UpdateDesignationDto` | `204 No Content`              |
| DELETE | `/{id}`           | Delete designation             | -                      | `204 No Content`              |
| GET    | `/{id}/employees` | Get designation with employees | -                      | `DesignationWithEmployeesDto` |

---

### Employee Controller

**Base Route**: `/api/Employee`

| Method | Endpoint                     | Description           | Request Body                              | Response                   |
| ------ | ---------------------------- | --------------------- | ----------------------------------------- | -------------------------- |
| GET    | `/`                          | Get all employees     | -                                         | `IEnumerable<EmployeeDto>` |
| GET    | `/{id}`                      | Get employee by ID    | -                                         | `EmployeeDto`              |
| POST   | `/`                          | Create new employee   | `CreateEmployeeDto`                       | `EmployeeDto`              |
| POST   | `/{employeeId}/update-image` | Update employee image | `UpdateImageRequestDto` (Form with Image) | `EmployeeDto`              |
| PUT    | `/{id}`                      | Update employee       | `UpdateEmployeeDto`                       | `boolean`                  |
| DELETE | `/{id}`                      | Delete employee       | -                                         | `boolean`                  |

---

### Employee Allowance Controller

**Base Route**: `/api/EmployeeAllowance`

| Method | Endpoint                      | Description                 | Request Body                 | Response                            |
| ------ | ----------------------------- | --------------------------- | ---------------------------- | ----------------------------------- |
| GET    | `/`                           | Get all employee allowances | -                            | `IEnumerable<EmployeeAllowanceDto>` |
| GET    | `/{employeeId}/{allowanceId}` | Get by composite key        | -                            | `EmployeeAllowanceDto`              |
| POST   | `/`                           | Create employee allowance   | `CreateEmployeeAllowanceDto` | `EmployeeAllowanceDto`              |
| PUT    | `/{employeeId}/{allowanceId}` | Update employee allowance   | `UpdateEmployeeAllowanceDto` | `204 No Content`                    |
| DELETE | `/{employeeId}/{allowanceId}` | Delete employee allowance   | -                            | `204 No Content`                    |
| GET    | `/employee/{employeeId}`      | Get allowances by employee  | -                            | `IEnumerable<EmployeeAllowanceDto>` |

---

### Employee Deduction Controller

**Base Route**: `/api/EmployeeDeduction`

| Method | Endpoint                      | Description                 | Request Body                 | Response                            |
| ------ | ----------------------------- | --------------------------- | ---------------------------- | ----------------------------------- |
| GET    | `/`                           | Get all employee deductions | -                            | `IEnumerable<EmployeeDeductionDto>` |
| GET    | `/{employeeId}/{deductionId}` | Get by composite key        | -                            | `EmployeeDeductionDto`              |
| POST   | `/`                           | Create employee deduction   | `CreateEmployeeDeductionDto` | `EmployeeDeductionDto`              |
| PUT    | `/{employeeId}/{deductionId}` | Update employee deduction   | `UpdateEmployeeDeductionDto` | `204 No Content`                    |
| DELETE | `/{employeeId}/{deductionId}` | Delete employee deduction   | -                            | `204 No Content`                    |
| GET    | `/employee/{employeeId}`      | Get deductions by employee  | -                            | `IEnumerable<EmployeeDeductionDto>` |

---

### Employee Training Controller

**Base Route**: `/api/EmployeeTraining`

| Method | Endpoint                            | Description                 | Request Body        | Response                           |
| ------ | ----------------------------------- | --------------------------- | ------------------- | ---------------------------------- |
| POST   | `/enroll`                           | Enroll employee in training | `EmployeeEnrollDto` | `EmployeeTrainingDto`              |
| POST   | `/{employeeId}/{courseId}/complete` | Mark training as complete   | -                   | `boolean`                          |
| POST   | `/{employeeId}/{courseId}/cancel`   | Cancel training enrollment  | -                   | `boolean`                          |
| GET    | `/by-employee/{employeeId}`         | Get enrollments by employee | -                   | `IEnumerable<EmployeeTrainingDto>` |
| GET    | `/by-course/{courseId}`             | Get enrollments by course   | -                   | `IEnumerable<EmployeeTrainingDto>` |

---

### ESS Controller (Employee Self-Service)

**Base Route**: `/api/ESS`
**Authentication**: Required (JWT Bearer Token)

| Method | Endpoint                     | Description                     | Request Body            | Response                |
| ------ | ---------------------------- | ------------------------------- | ----------------------- | ----------------------- |
| GET    | `/profile`                   | Get current employee profile    | -                       | `ESSProfileDto`         |
| PUT    | `/profile`                   | Update current employee profile | `UpdateESSProfileDto`   | `boolean`               |
| GET    | `/leave-requests`            | Get employee's leave history    | -                       | `List<LeaveRequestDto>` |
| POST   | `/leave-requests`            | Submit leave request            | `CreateLeaveRequestDto` | `int` (request ID)      |
| GET    | `/leave-balance?year={year}` | Get leave balance               | -                       | `List<LeaveBalanceDto>` |
| GET    | `/payslips`                  | Get employee's payslips         | -                       | `List<PayslipDto>`      |
| GET    | `/payslips/{payslipId}`      | Get specific payslip            | -                       | `PayslipDto`            |
| GET    | `/dashboard`                 | Get ESS dashboard data          | -                       | `ESSDashboardDto`       |

---

### Job Application Controller

**Base Route**: `/api/JobApplication`

| Method | Endpoint                                            | Description                           | Request Body                    | Response                         |
| ------ | --------------------------------------------------- | ------------------------------------- | ------------------------------- | -------------------------------- |
| GET    | `/`                                                 | Get all job applications              | -                               | `IEnumerable<JobApplicationDto>` |
| GET    | `/{id}`                                             | Get job application by ID             | -                               | `JobApplicationDto`              |
| GET    | `/{id}/detail`                                      | Get application with full details     | -                               | `JobApplicationDetailDto`        |
| GET    | `/jobposting/{jobPostingId}`                        | Get applications by job posting       | -                               | `IEnumerable<JobApplicationDto>` |
| GET    | `/candidate/{candidateId}`                          | Get applications by candidate         | -                               | `IEnumerable<JobApplicationDto>` |
| GET    | `/status/{status}`                                  | Get applications by status            | -                               | `IEnumerable<JobApplicationDto>` |
| GET    | `/count/{jobPostingId}`                             | Get application count for job posting | -                               | `int`                            |
| GET    | `/check-applied?candidateId={id}&jobPostingId={id}` | Check if candidate applied            | -                               | `boolean`                        |
| POST   | `/`                                                 | Submit job application                | `CreateJobApplicationDto`       | `JobApplicationDto`              |
| PUT    | `/{id}/status`                                      | Update application status             | `UpdateJobApplicationStatusDto` | `204 No Content`                 |
| DELETE | `/{id}`                                             | Delete job application                | -                               | `204 No Content`                 |

**Application Status Values**: `Applied`, `UnderReview`, `Shortlisted`, `Interviewed`, `Offered`, `Accepted`, `Rejected`, `Withdrawn`

---

### Job Posting Controller

**Base Route**: `/api/JobPosting`

| Method | Endpoint                       | Description                 | Request Body          | Response                     |
| ------ | ------------------------------ | --------------------------- | --------------------- | ---------------------------- |
| GET    | `/`                            | Get all job postings        | -                     | `IEnumerable<JobPostingDto>` |
| GET    | `/active`                      | Get active job postings     | -                     | `IEnumerable<JobPostingDto>` |
| GET    | `/{id}`                        | Get job posting by ID       | -                     | `JobPostingDto`              |
| GET    | `/department/{departmentId}`   | Get postings by department  | -                     | `IEnumerable<JobPostingDto>` |
| GET    | `/designation/{designationId}` | Get postings by designation | -                     | `IEnumerable<JobPostingDto>` |
| POST   | `/`                            | Create new job posting      | `CreateJobPostingDto` | `JobPostingDto`              |
| PUT    | `/{id}`                        | Update job posting          | `UpdateJobPostingDto` | `204 No Content`             |
| DELETE | `/{id}`                        | Delete job posting          | -                     | `204 No Content`             |

---

### Leave Approval Controller

**Base Route**: `/api/LeaveApproval`

| Method | Endpoint                    | Description                    | Request Body             | Response                        |
| ------ | --------------------------- | ------------------------------ | ------------------------ | ------------------------------- |
| POST   | `/approve`                  | Approve leave request          | `LeaveApprovalActionDto` | `boolean`                       |
| POST   | `/reject`                   | Reject leave request           | `LeaveApprovalActionDto` | `boolean`                       |
| GET    | `/approver/{approverId}`    | Get approvals by approver      | -                        | `IEnumerable<LeaveApprovalDto>` |
| GET    | `/request/{leaveRequestId}` | Get approval steps for request | -                        | `IEnumerable<LeaveApprovalDto>` |

---

### Leave Balance Controller

**Base Route**: `/api/LeaveBalance`

| Method | Endpoint                                          | Description                   | Request Body            | Response                       |
| ------ | ------------------------------------------------- | ----------------------------- | ----------------------- | ------------------------------ |
| GET    | `/employee-leavebalance/{employeeId}`             | Get all balances for employee | -                       | `IEnumerable<LeaveBalanceDto>` |
| GET    | `/employee-currentyear-leavebalance/{employeeId}` | Get current year balances     | -                       | `IEnumerable<LeaveBalanceDto>` |
| POST   | `/allocate/{employeeId}`                          | Allocate initial balances     | -                       | `boolean`                      |
| POST   | `/deduct`                                         | Deduct leave days             | `DeductLeaveRequestDto` | `boolean`                      |
| POST   | `/reset-annual`                                   | Reset balances for new year   | -                       | `boolean`                      |

---

### Leave Request Controller

**Base Route**: `/api/LeaveRequest`

| Method | Endpoint         | Description               | Request Body            | Response                       |
| ------ | ---------------- | ------------------------- | ----------------------- | ------------------------------ |
| GET    | `/`              | Get all leave requests    | -                       | `IEnumerable<LeaveRequestDto>` |
| GET    | `/{id}`          | Get leave request by ID   | -                       | `LeaveRequestDto`              |
| POST   | `/`              | Create leave request      | `CreateLeaveRequestDto` | `LeaveRequestDto`              |
| PUT    | `/{id}`          | Update leave request      | `UpdateLeaveRequestDto` | `204 No Content`               |
| DELETE | `/{id}`          | Delete leave request      | -                       | `204 No Content`               |
| GET    | `/employee/{id}` | Get requests by employee  | -                       | `IEnumerable<LeaveRequestDto>` |
| GET    | `/manager/{id}`  | Get requests for reviewer | -                       | `IEnumerable<LeaveRequestDto>` |

---

### Leave Type Controller

**Base Route**: `/api/LeaveType`

| Method | Endpoint | Description          | Request Body | Response                    |
| ------ | -------- | -------------------- | ------------ | --------------------------- |
| GET    | `/`      | Get all leave types  | -            | `IEnumerable<LeaveTypeDto>` |
| GET    | `/{id}`  | Get leave type by ID | -            | `LeaveTypeDto`              |

---

### Notification Controller

**Base Route**: `/api/Notification`

| Method | Endpoint           | Description                      | Request Body            | Response                       |
| ------ | ------------------ | -------------------------------- | ----------------------- | ------------------------------ |
| GET    | `/my`              | Get current user's notifications | -                       | `IEnumerable<NotificationDto>` |
| GET    | `/my/unread`       | Get unread notifications         | -                       | `IEnumerable<NotificationDto>` |
| GET    | `/my/unread-count` | Get unread count                 | -                       | `int`                          |
| GET    | `/{id}`            | Get notification by ID           | -                       | `NotificationDto`              |
| POST   | `/`                | Create notification              | `CreateNotificationDto` | `NotificationDto`              |
| PUT    | `/{id}/read`       | Mark as read                     | -                       | `204 No Content`               |
| PUT    | `/my/read-all`     | Mark all as read                 | -                       | `204 No Content`               |
| DELETE | `/{id}`            | Delete notification              | -                       | `204 No Content`               |

---

### Payslip Controller

**Base Route**: `/api/payslips`

| Method | Endpoint                                          | Description                         | Request Body | Response                  |
| ------ | ------------------------------------------------- | ----------------------------------- | ------------ | ------------------------- |
| POST   | `/{employeeId}/generate?month={m}&year={y}`       | Generate payslip for employee       | -            | `PayslipDto`              |
| POST   | `/generate-month?month={m}&year={y}`              | Generate payslips for all employees | -            | `IEnumerable<PayslipDto>` |
| POST   | `/{employeeId}/regenerate?month={m}&year={y}`     | Regenerate payslip                  | -            | `PayslipDto`              |
| GET    | `/{id}`                                           | Get payslip by ID                   | -            | `PayslipDto`              |
| GET    | `/employee/{employeeId}`                          | Get all payslips for employee       | -            | `IEnumerable<PayslipDto>` |
| GET    | `/employee/{employeeId}/month?month={m}&year={y}` | Get monthly payslip                 | -            | `PayslipDto`              |
| GET    | `/month?month={m}&year={y}`                       | Get all payslips for month          | -            | `IEnumerable<PayslipDto>` |
| GET    | `/exists?employeeId={id}&month={m}&year={y}`      | Check if payslip exists             | -            | `boolean`                 |
| GET    | `/self?month={m}&year={y}`                        | Get own payslip (self-service)      | -            | `PayslipDto`              |
| DELETE | `/{id}`                                           | Delete payslip                      | -            | `boolean`                 |
| GET    | `/{id}/export/pdf`                                | Export payslip to PDF               | -            | PDF file                  |
| GET    | `/export/month/excel?month={m}&year={y}`          | Export monthly payslips to Excel    | -            | Excel file                |

---

### Performance Controller

**Base Route**: `/api/Performance`

#### Review Cycles

| Method | Endpoint                       | Description         | Request Body     | Response                      |
| ------ | ------------------------------ | ------------------- | ---------------- | ----------------------------- |
| POST   | `/cycles`                      | Create review cycle | `CreateCycleDto` | `ReviewCycleDto`              |
| GET    | `/cycles`                      | Get all cycles      | -                | `IEnumerable<ReviewCycleDto>` |
| GET    | `/cycles/active?at={datetime}` | Get active cycles   | -                | `IEnumerable<ReviewCycleDto>` |

#### Reviews

| Method | Endpoint                         | Description          | Request Body           | Response                            |
| ------ | -------------------------------- | -------------------- | ---------------------- | ----------------------------------- |
| POST   | `/reviews`                       | Create review        | `CreateReviewDto`      | `PerformanceReviewDto`              |
| POST   | `/reviews/{reviewId}/close`      | Close review         | `decimal? finalRating` | `PerformanceReviewDto`              |
| GET    | `/reviews/employee/{employeeId}` | Get employee reviews | -                      | `IEnumerable<PerformanceReviewDto>` |

#### Goals

| Method | Endpoint          | Description          | Request Body            | Response  |
| ------ | ----------------- | -------------------- | ----------------------- | --------- |
| POST   | `/goals`          | Create goal          | `CreateGoalDto`         | `GoalDto` |
| PUT    | `/goals/progress` | Update goal progress | `UpdateGoalProgressDto` | `boolean` |

#### KPIs

| Method | Endpoint        | Description    | Request Body         | Response       |
| ------ | --------------- | -------------- | -------------------- | -------------- |
| POST   | `/kpis`         | Create KPI     | `CreateKpiDto`       | `KPIDto`       |
| POST   | `/kpis/results` | Add KPI result | `CreateKpiResultDto` | `KPIResultDto` |

#### Feedback

| Method | Endpoint                                           | Description          | Request Body        | Response                   |
| ------ | -------------------------------------------------- | -------------------- | ------------------- | -------------------------- |
| POST   | `/feedbacks`                                       | Add feedback         | `CreateFeedbackDto` | `FeedbackDto`              |
| GET    | `/reviews/{reviewId}/feedback-by-type?type={type}` | Get feedback by type | -                   | `IEnumerable<FeedbackDto>` |

**Feedback Types**: `SelfAssessment`, `ManagerReview`, `PeerFeedback`, `360Feedback`

#### Competencies

| Method | Endpoint             | Description       | Request Body          | Response                      |
| ------ | -------------------- | ----------------- | --------------------- | ----------------------------- |
| POST   | `/competencies`      | Create competency | `CreateCompetencyDto` | `CompetencyDto`               |
| POST   | `/competencies/rate` | Rate competency   | `RateCompetencyDto`   | `EmployeeCompetencyRatingDto` |

#### Reports

| Method | Endpoint                     | Description       | Request Body | Response               |
| ------ | ---------------------------- | ----------------- | ------------ | ---------------------- |
| GET    | `/reviews/{reviewId}/report` | Get review report | -            | `PerformanceReportDto` |

---

### Reporting Controller

**Base Route**: `/api/Reporting`

#### Employee Metrics

| Method | Endpoint                  | Description                          | Response              |
| ------ | ------------------------- | ------------------------------------ | --------------------- |
| GET    | `/TotalEmployees`         | Get total employee count             | `int`                 |
| GET    | `/EmployeesByDepartment`  | Get employees grouped by department  | Department breakdown  |
| GET    | `/EmployeesByDesignation` | Get employees grouped by designation | Designation breakdown |

#### Payroll Metrics

| Method | Endpoint                                 | Description                 | Response        |
| ------ | ---------------------------------------- | --------------------------- | --------------- |
| GET    | `/TotalPayroll/{year}/{month}`           | Get total payroll for month | `decimal`       |
| GET    | `/PayrollTrend/{year}`                   | Get yearly payroll trend    | Trend data      |
| GET    | `/AllowanceCostBreakdown/{year}/{month}` | Get allowance breakdown     | Cost breakdown  |
| GET    | `/DeductionCostBreakdown/{year}/{month}` | Get deduction breakdown     | Cost breakdown  |
| GET    | `/SalaryComparison`                      | Get salary comparison data  | Comparison data |

#### Recruitment Metrics

| Method | Endpoint                        | Description                   | Response       |
| ------ | ------------------------------- | ----------------------------- | -------------- |
| GET    | `/TotalJobPostings`             | Get total job postings        | `int`          |
| GET    | `/ActiveJobPostings`            | Get active job postings count | `int`          |
| GET    | `/ApplicationsPerJobPosting`    | Get applications per posting  | Breakdown data |
| GET    | `/RecruitmentPipeline`          | Get recruitment pipeline data | Pipeline data  |
| GET    | `/AverageApplicationReviewTime` | Get average review time       | `TimeSpan`     |

#### Leave Metrics

| Method | Endpoint                    | Description               | Response         |
| ------ | --------------------------- | ------------------------- | ---------------- |
| GET    | `/TotalLeaveRequests`       | Get total leave requests  | `int`            |
| GET    | `/LeaveRequestsByStatus`    | Get requests by status    | Status breakdown |
| GET    | `/LeaveUsageByType`         | Get usage by leave type   | Type breakdown   |
| GET    | `/MonthlyLeaveTrend/{year}` | Get monthly leave trend   | Trend data       |
| GET    | `/AverageLeaveApprovalTime` | Get average approval time | `TimeSpan`       |

#### Dashboard

| Method | Endpoint                        | Description        | Response           |
| ------ | ------------------------------- | ------------------ | ------------------ |
| GET    | `/DashboardKpis/{year}/{month}` | Get dashboard KPIs | `DashboardKpisDto` |

---

### Training Course Controller

**Base Route**: `/api/TrainingCourse`

| Method | Endpoint | Description                | Request Body              | Response                         |
| ------ | -------- | -------------------------- | ------------------------- | -------------------------------- |
| GET    | `/`      | Get all training courses   | -                         | `IEnumerable<TrainingCourseDto>` |
| GET    | `/{id}`  | Get training course by ID  | -                         | `TrainingCourseDto`              |
| POST   | `/`      | Create new training course | `TrainingCourseCreateDto` | `TrainingCourseDto`              |
| PUT    | `/{id}`  | Update training course     | `TrainingCourseUpdateDto` | `TrainingCourseDto`              |
| DELETE | `/{id}`  | Delete training course     | -                         | `boolean`                        |

---

## DTO Reference

This section documents all Data Transfer Objects (DTOs) used in API requests and responses.

---

### Account DTOs

#### LoginDto

```json
{
  "email": "string (required, email format)",
  "password": "string (required)",
  "rememberMe": "boolean"
}
```

#### AuthResponseDto

```json
{
  "userId": "guid",
  "email": "string",
  "fullName": "string",
  "employeeId": "int?",
  "roles": ["string"],
  "token": "string?",
  "tokenExpiration": "datetime?"
}
```

#### AssignRoleDto

```json
{
  "userId": "guid (required)",
  "roleName": "string (required)"
}
```

#### RegisterEmployeeDto

```json
{
  "email": "string (required, email format)",
  "password": "string (required, min 6 chars)",
  "confirmPassword": "string (required, must match password)",
  "phoneNumber": "string?",
  "firstName": "string (required, max 50)",
  "lastName": "string (required, max 50)",
  "dateOfBirth": "datetime (required)",
  "gender": "string? (max 10)",
  "hireDate": "datetime (required)",
  "eFF_Start": "datetime?",
  "eFF_End": "datetime?",
  "contactNumber": "string?",
  "address": "string? (max 200)",
  "basicSalary": "decimal (required, >= 0)",
  "deptId": "int",
  "designationId": "int",
  "roles": ["string"]?
}
```

#### RegisterEmployeeResponseDto

```json
{
  "userId": "guid",
  "employeeId": "int",
  "email": "string",
  "fullName": "string",
  "roles": ["string"],
  "token": "string?",
  "tokenExpiration": "datetime?"
}
```

---

### Employee DTOs

#### EmployeeDto

```json
{
  "id": "int",
  "deptId": "int",
  "designationId": "int",
  "firstName": "string",
  "lastName": "string",
  "dateOfBirth": "datetime",
  "gender": "string?",
  "hireDate": "datetime",
  "eFF_Start": "datetime?",
  "eFF_End": "datetime?",
  "email": "string",
  "contactNumber": "string?",
  "address": "string?",
  "basicSalary": "decimal",
  "faceEmbedding": "[float]?",
  "applicationUserId": "string?"
}
```

#### CreateEmployeeDto

```json
{
  "firstName": "string (required)",
  "lastName": "string (required)",
  "deptId": "int",
  "designationId": "int",
  "dateOfBirth": "datetime",
  "gender": "string?",
  "hireDate": "datetime",
  "eFF_Start": "datetime?",
  "eFF_End": "datetime?",
  "email": "string (required)",
  "contactNumber": "string?",
  "address": "string?",
  "basicSalary": "decimal",
  "applicationUserId": "string?"
}
```

#### UpdateEmployeeDto

```json
{
  "firstName": "string (required)",
  "lastName": "string (required)",
  "deptId": "int",
  "designationId": "int",
  "dateOfBirth": "datetime",
  "gender": "string?",
  "hireDate": "datetime",
  "eFF_Start": "datetime?",
  "eFF_End": "datetime?",
  "email": "string (required)",
  "contactNumber": "string?",
  "address": "string?",
  "basicSalary": "decimal",
  "applicationUserId": "string?"
}
```

#### EmployeeSummaryDto

```json
{
  "id": "int",
  "firstName": "string",
  "lastName": "string",
  "email": "string",
  "position": "string?"
}
```

---

### Department DTOs

#### DepartmentDto

```json
{
  "id": "int",
  "name": "string",
  "description": "string?",
  "managerId": "int?",
  "employeeCount": "int?",
  "createdAt": "datetime",
  "updatedAt": "datetime?"
}
```

#### CreateDepartmentDto

```json
{
  "name": "string (required, 2-100 chars)",
  "description": "string? (max 500)",
  "managerId": "int?"
}
```

#### UpdateDepartmentDto

```json
{
  "name": "string (required, 2-100 chars)",
  "description": "string? (max 500)",
  "managerId": "int?"
}
```

#### DepartmentWithEmployeesDto

```json
{
  "id": "int",
  "name": "string",
  "description": "string?",
  "managerId": "int?",
  "employees": [EmployeeSummaryDto],
  "createdAt": "datetime",
  "updatedAt": "datetime?"
}
```

---

### Designation DTOs

#### DesignationDto

```json
{
  "id": "int",
  "title": "string",
  "description": "string?",
  "createdAt": "datetime",
  "updatedAt": "datetime?"
}
```

#### CreateDesignationDto

```json
{
  "title": "string (required, 5-100 chars)",
  "description": "string? (max 500)"
}
```

#### UpdateDesignationDto

```json
{
  "title": "string (required, 5-100 chars)",
  "description": "string? (max 500)"
}
```

#### DesignationWithEmployeesDto

```json
{
  "id": "int",
  "title": "string",
  "description": "string?",
  "employees": [EmployeeSummaryDto],
  "createdAt": "datetime",
  "updatedAt": "datetime?"
}
```

---

### Allowance DTOs

#### AllowanceDto

```json
{
  "id": "int",
  "name": "string",
  "amount": "decimal",
  "isPercentage": "boolean"
}
```

#### CreateAllowanceDto

```json
{
  "name": "string (required)",
  "amount": "decimal",
  "isPercentage": "boolean"
}
```

#### UpdateAllowanceDto

```json
{
  "name": "string?",
  "amount": "decimal?",
  "isPercentage": "boolean?"
}
```

---

### Deduction DTOs

#### DeductionDto

```json
{
  "id": "int",
  "name": "string",
  "amount": "decimal",
  "isPercentage": "boolean"
}
```

#### CreateDeductionDto

```json
{
  "name": "string (required)",
  "amount": "decimal",
  "isPercentage": "boolean"
}
```

#### UpdateDeductionDto

```json
{
  "name": "string?",
  "amount": "decimal?",
  "isPercentage": "boolean?"
}
```

---

### Employee Allowance DTOs

#### EmployeeAllowanceDto

```json
{
  "employeeId": "int",
  "allowanceId": "int",
  "amount": "decimal",
  "isPercentage": "boolean",
  "recurrence": "RecurrenceType (enum)",
  "startDate": "datetime?",
  "endDate": "datetime?"
}
```

#### CreateEmployeeAllowanceDto

```json
{
  "employeeId": "int",
  "allowanceId": "int",
  "amount": "decimal?",
  "isPercentage": "boolean?",
  "recurrence": "RecurrenceType (default: OneTime)",
  "startDate": "datetime?",
  "endDate": "datetime?"
}
```

#### UpdateEmployeeAllowanceDto

```json
{
  "employeeId": "int",
  "allowanceId": "int?",
  "amount": "decimal?",
  "isPercentage": "boolean?",
  "recurrence": "RecurrenceType?",
  "startDate": "datetime?",
  "endDate": "datetime?"
}
```

---

### Employee Deduction DTOs

#### EmployeeDeductionDto

```json
{
  "employeeId": "int",
  "deductionId": "int",
  "amount": "decimal",
  "isPercentage": "boolean",
  "recurrence": "RecurrenceType (enum)",
  "startDate": "datetime?",
  "endDate": "datetime?"
}
```

#### CreateEmployeeDeductionDto

```json
{
  "employeeId": "int",
  "deductionId": "int",
  "amount": "decimal?",
  "isPercentage": "boolean?",
  "recurrence": "RecurrenceType (default: OneTime)",
  "startDate": "datetime?",
  "endDate": "datetime?"
}
```

#### UpdateEmployeeDeductionDto

```json
{
  "employeeId": "int",
  "deductionId": "int?",
  "amount": "decimal?",
  "isPercentage": "boolean?",
  "recurrence": "RecurrenceType?",
  "startDate": "datetime?",
  "endDate": "datetime?"
}
```

---

### Attendance DTOs

#### CheckInRequestDto / CheckOutRequestDto

```
Content-Type: multipart/form-data
{
  "image": "file (required, IFormFile)"
}
```

#### AttendanceEmployeeDto

```json
{
  "id": "int",
  "employeeId": "int",
  "date": "datetime",
  "checkInTime": "datetime?",
  "checkOutTime": "datetime?",
  "isLate": "boolean",
  "isAbsent": "boolean"
}
```

---

### Candidate DTOs

#### CandidateDto

```json
{
  "id": "int",
  "firstName": "string",
  "lastName": "string",
  "fullName": "string (computed)",
  "email": "string",
  "phone": "string?",
  "resumeUrl": "string?",
  "linkedInUrl": "string?",
  "portfolioUrl": "string?",
  "address": "string?",
  "city": "string?",
  "country": "string?",
  "postalCode": "string?",
  "dateOfBirth": "datetime?",
  "gender": "string?",
  "yearsOfExperience": "int?",
  "currentCompany": "string?",
  "currentJobTitle": "string?",
  "currentSalary": "decimal?",
  "expectedSalary": "decimal?",
  "skills": "string?",
  "education": "string?",
  "certifications": "string?",
  "noticePeriodDays": "int?",
  "availableFrom": "datetime?",
  "preferredWorkLocation": "string?",
  "willingToRelocate": "boolean",
  "notes": "string?",
  "convertedToEmployeeId": "int?",
  "createdAt": "datetime",
  "updatedAt": "datetime?"
}
```

#### CreateCandidateDto

```json
{
  "firstName": "string (required, 2-100 chars)",
  "lastName": "string (required, 2-100 chars)",
  "email": "string (required, email format, max 255)",
  "phone": "string? (max 20)",
  "resumeUrl": "string? (url, max 500)",
  "linkedInUrl": "string? (url, max 500)",
  "portfolioUrl": "string? (url, max 500)",
  "address": "string? (max 500)",
  "city": "string? (max 100)",
  "country": "string? (max 100)",
  "postalCode": "string? (max 20)",
  "dateOfBirth": "datetime?",
  "gender": "string? (max 20)",
  "yearsOfExperience": "int? (0-50)",
  "currentCompany": "string? (max 200)",
  "currentJobTitle": "string? (max 200)",
  "currentSalary": "decimal? (>= 0)",
  "expectedSalary": "decimal? (>= 0)",
  "skills": "string? (max 2000)",
  "education": "string? (max 1000)",
  "certifications": "string? (max 1000)",
  "noticePeriodDays": "int? (0-365)",
  "availableFrom": "datetime?",
  "preferredWorkLocation": "string? (max 200)",
  "willingToRelocate": "boolean",
  "notes": "string? (max 2000)"
}
```

#### UpdateCandidateDto

Same structure as `CreateCandidateDto`

---

### Job Posting DTOs

#### JobPostingDto

```json
{
  "title": "string",
  "description": "string?",
  "requirements": "string?",
  "postedDate": "datetime",
  "closingDate": "datetime?",
  "isActive": "boolean?",
  "departmentName": "string?",
  "designationName": "string?"
}
```

#### CreateJobPostingDto

```json
{
  "title": "string (required)",
  "description": "string?",
  "requirements": "string?",
  "postedDate": "datetime",
  "closingDate": "datetime?",
  "isActive": "boolean?",
  "departmentId": "int",
  "designationId": "int"
}
```

#### UpdateJobPostingDto

```json
{
  "title": "string (required)",
  "description": "string?",
  "requirements": "string?",
  "postedDate": "datetime",
  "closingDate": "datetime?",
  "isActive": "boolean?",
  "departmentId": "int",
  "designationId": "int"
}
```

---

### Job Application DTOs

#### JobApplicationDto

```json
{
  "id": "int",
  "candidateId": "int",
  "jobPostingId": "int",
  "applicationDate": "datetime",
  "status": "string",
  "source": "string",
  "coverLetter": "string?",
  "notes": "string?",
  "reviewedBy": "int?",
  "reviewedDate": "datetime?",
  "interviewDate": "datetime?",
  "interviewFeedback": "string?",
  "interviewRating": "int?",
  "expectedSalary": "decimal?",
  "offeredSalary": "decimal?",
  "rejectionReason": "string?",
  "assignedRecruiterId": "int?",
  "currentStage": "string",
  "createdAt": "datetime",
  "updatedAt": "datetime?"
}
```

#### JobApplicationDetailDto

Extends `JobApplicationDto` with:

```json
{
  ...JobApplicationDto,
  "candidate": CandidateDto,
  "jobPosting": JobPostingDto
}
```

#### CreateJobApplicationDto

```json
{
  "candidateId": "int? (for existing candidate)",
  "candidateInfo": CreateCandidateDto? (for new candidate),
  "jobPostingId": "int (required)",
  "source": "string (required, max 50)",
  "coverLetter": "string? (max 5000)",
  "expectedSalary": "decimal? (>= 0)",
  "notes": "string? (max 1000)",
  "assignedRecruiterId": "int?"
}
```

#### UpdateJobApplicationStatusDto

```json
{
  "jobApplicationId": "int (required)",
  "status": "string (required, max 50)",
  "currentStage": "string? (max 50)",
  "notes": "string? (max 2000)",
  "reviewedBy": "int?",
  "reviewedDate": "datetime?",
  "interviewDate": "datetime?",
  "interviewFeedback": "string? (max 2000)",
  "interviewRating": "int? (1-10)",
  "rejectionReason": "string? (max 1000)",
  "offeredSalary": "decimal? (>= 0)"
}
```

---

### Leave DTOs

#### LeaveTypeDto

```json
{
  "id": "int",
  "name": "string",
  "description": "string?",
  "maxDays": "int",
  "canCarryForward": "boolean",
  "carryForwardLimit": "int?",
  "isPaid": "boolean"
}
```

#### LeaveRequestDto

```json
{
  "id": "int",
  "employeeId": "int",
  "leaveTypeId": "int",
  "startDate": "datetime",
  "endDate": "datetime",
  "totalDays": "int",
  "status": "int (LeaveStatus enum: 0=Pending, 1=Approved, 2=Rejected)",
  "reviewedById": "int?",
  "reviewedAt": "datetime?"
}
```

#### CreateLeaveRequestDto

```json
{
  "employeeId": "int",
  "leaveTypeId": "int",
  "startDate": "datetime",
  "endDate": "datetime"
}
```

#### UpdateLeaveRequestDto

Same structure as `CreateLeaveRequestDto`

#### LeaveBalanceDto

```json
{
  "id": "int",
  "employeeId": "int",
  "leaveTypeId": "int",
  "totalAllocated": "int",
  "usedDays": "int",
  "remainingDays": "int",
  "year": "int"
}
```

#### DeductLeaveRequestDto

```json
{
  "employeeId": "int",
  "leaveTypeId": "int",
  "daysUsed": "int"
}
```

#### LeaveApprovalDto

```json
{
  "id": "int",
  "leaveRequestId": "int",
  "approverId": "int",
  "level": "LevelApproval (enum: 1=Manager, 2=HR)",
  "status": "LeaveStatus (enum: 0=Pending, 1=Approved, 2=Rejected)",
  "actionDate": "datetime?"
}
```

#### LeaveApprovalActionDto

```json
{
  "leaveRequestId": "int",
  "approverId": "int",
  "level": "LevelApproval (enum: 1=Manager, 2=HR)"
}
```

---

### Notification DTOs

#### NotificationDto

```json
{
  "id": "int",
  "recipientUserId": "string?",
  "title": "string",
  "message": "string",
  "type": "string (NotificationType)",
  "category": "string (NotificationCategory)",
  "isRead": "boolean",
  "readAt": "datetime?",
  "actionUrl": "string?",
  "relatedEntityId": "int?",
  "relatedEntityType": "string?",
  "priority": "string (NotificationPriority)",
  "createdAt": "datetime",
  "updatedAt": "datetime"
}
```

#### CreateNotificationDto

```json
{
  "recipientUserId": "string?",
  "title": "string (required)",
  "message": "string (required)",
  "type": "NotificationType (default: Info)",
  "category": "NotificationCategory",
  "actionUrl": "string?",
  "relatedEntityId": "int?",
  "relatedEntityType": "string?",
  "priority": "NotificationPriority (default: Normal)"
}
```

---

### Payslip DTOs

#### PayslipDto

```json
{
  "id": "int",
  "employeeId": "int",
  "employeeName": "string",
  "basicSalary": "decimal",
  "totalAllowances": "decimal",
  "totalDeductions": "decimal",
  "netSalary": "decimal",
  "month": "int",
  "year": "int",
  "generatedAt": "datetime",
  "allowances": [AllowanceDto],
  "deductions": [DeductionDto]
}
```

---

### ESS (Employee Self-Service) DTOs

#### ESSProfileDto

```json
{
  "id": "int",
  "firstName": "string",
  "lastName": "string",
  "fullName": "string (computed)",
  "dateOfBirth": "datetime",
  "gender": "string?",
  "hireDate": "datetime",
  "email": "string",
  "contactNumber": "string?",
  "address": "string?",
  "departmentName": "string?",
  "designationName": "string?",
  "deptId": "int",
  "designationId": "int"
}
```

#### UpdateESSProfileDto

```json
{
  "email": "string (required)",
  "contactNumber": "string?",
  "address": "string?"
}
```

#### ESSDashboardDto

```json
{
  "profile": ESSProfileDto,
  "leaveBalances": [LeaveBalanceDto],
  "recentPayslips": [PayslipDto],
  "pendingLeaveRequestsCount": "int",
  "approvedLeaveRequestsCount": "int"
}
```

---

### Training DTOs

#### TrainingCourseDto

```json
{
  "id": "int",
  "title": "string",
  "description": "string?",
  "durationHours": "int?"
}
```

#### TrainingCourseCreateDto

```json
{
  "title": "string (required)",
  "description": "string?",
  "durationHours": "int?"
}
```

#### TrainingCourseUpdateDto

```json
{
  "title": "string?",
  "description": "string?",
  "durationHours": "int?"
}
```

#### EmployeeTrainingDto

```json
{
  "employeeId": "int",
  "trainingCourseId": "int",
  "status": "string (TrainingStatus: Enrolled, Completed, Cancelled)",
  "enrollmentDate": "datetime",
  "completionDate": "datetime?",
  "rewardGiven": "boolean",
  "employeeName": "string?",
  "trainingCourseTitle": "string?"
}
```

#### EmployeeEnrollDto

```json
{
  "employeeId": "int",
  "trainingCourseId": "int"
}
```

---

### Performance DTOs

#### ReviewCycleDto

```json
{
  "id": "int",
  "name": "string",
  "frequency": "CycleFrequency (enum: 1=Annual, 2=Quarterly, 3=Monthly)",
  "startDate": "datetime",
  "endDate": "datetime",
  "ratingScale": "RatingScaleType (enum: 1=OneToFive, 2=Percentile)"
}
```

#### CreateCycleDto

```json
{
  "name": "string",
  "frequency": "CycleFrequency",
  "startDate": "datetime",
  "endDate": "datetime",
  "ratingScale": "RatingScaleType"
}
```

#### PerformanceReviewDto

```json
{
  "id": "int",
  "employeeId": "int",
  "reviewCycleId": "int",
  "status": "ReviewStatus (enum: 0=Draft, 1=InProgress, 2=Submitted, 3=Closed)",
  "finalRating": "decimal?"
}
```

#### CreateReviewDto

```json
{
  "employeeId": "int",
  "reviewCycleId": "int"
}
```

#### GoalDto

```json
{
  "id": "int",
  "performanceReviewId": "int",
  "title": "string",
  "description": "string?",
  "status": "GoalStatus (enum: 0=NotStarted, 1=OnTrack, 2=AtRisk, 3=Completed)",
  "progressPercent": "decimal",
  "dueDate": "datetime?"
}
```

#### CreateGoalDto

```json
{
  "performanceReviewId": "int",
  "title": "string",
  "description": "string?",
  "dueDate": "datetime?"
}
```

#### UpdateGoalProgressDto

```json
{
  "goalId": "int",
  "progressPercent": "decimal (0-100)",
  "status": "string? (NotStarted, OnTrack, AtRisk, Completed)"
}
```

#### KPIDto

```json
{
  "id": "int",
  "code": "string",
  "name": "string",
  "unit": "string?",
  "target": "decimal",
  "description": "string?"
}
```

#### CreateKpiDto

```json
{
  "code": "string",
  "name": "string",
  "unit": "string?",
  "target": "decimal",
  "description": "string?"
}
```

#### KPIResultDto

```json
{
  "id": "int",
  "performanceReviewId": "int",
  "kpiId": "int",
  "actual": "decimal",
  "weightedScore": "decimal?"
}
```

#### CreateKpiResultDto

```json
{
  "performanceReviewId": "int",
  "kpiId": "int",
  "actual": "decimal"
}
```

#### FeedbackDto

```json
{
  "id": "int",
  "performanceReviewId": "int",
  "fromEmployeeId": "int",
  "type": "FeedbackType (enum: 1=Self, 2=Manager, 3=Peer)",
  "comments": "string",
  "submittedAt": "datetime"
}
```

#### CreateFeedbackDto

```json
{
  "performanceReviewId": "int",
  "fromEmployeeId": "int",
  "type": "FeedbackType",
  "comments": "string"
}
```

#### CompetencyDto

```json
{
  "id": "int",
  "name": "string",
  "description": "string?"
}
```

#### CreateCompetencyDto

```json
{
  "name": "string",
  "description": "string?"
}
```

#### RateCompetencyDto

```json
{
  "performanceReviewId": "int",
  "competencyId": "int",
  "rating": "decimal",
  "notes": "string?"
}
```

#### EmployeeCompetencyRatingDto

```json
{
  "id": "int",
  "performanceReviewId": "int",
  "competencyId": "int",
  "rating": "decimal",
  "notes": "string?"
}
```

#### PerformanceReportDto

```json
{
  "reviewId": "int",
  "finalRating": "decimal?",
  "goals": [GoalDto],
  "kpiResults": [KPIResultDto],
  "competencyRatings": [EmployeeCompetencyRatingDto],
  "feedbacks": [FeedbackDto]
}
```

---

## Enum Reference

### RecurrenceType

| Value | Name      | Description           |
| ----- | --------- | --------------------- |
| 1     | OneTime   | Single occurrence     |
| 2     | Period    | For a specific period |
| 3     | Annual    | Yearly recurring      |
| 4     | Permanent | Always active         |

### LeaveStatus

| Value | Name     |
| ----- | -------- |
| 0     | Pending  |
| 1     | Approved |
| 2     | Rejected |

### LevelApproval

| Value | Name    |
| ----- | ------- |
| 1     | Manager |
| 2     | HR      |

### ApplicationStatus

| Value | Name               |
| ----- | ------------------ |
| 0     | Applied            |
| 1     | UnderReview        |
| 2     | InterviewScheduled |
| 3     | Offered            |
| 4     | Rejected           |
| 5     | Hired              |

### TrainingStatus

| Value | Name      |
| ----- | --------- |
| 0     | Enrolled  |
| 1     | Completed |
| 2     | Cancelled |

### NotificationType

| Value | Name    |
| ----- | ------- |
| 0     | Info    |
| 1     | Success |
| 2     | Warning |
| 3     | Error   |

### NotificationCategory

| Value | Name                 |
| ----- | -------------------- |
| 0     | LeaveRequest         |
| 1     | LeaveApproval        |
| 2     | JobApplication       |
| 3     | EmployeeRegistration |
| 4     | Interview            |
| 5     | Payroll              |
| 6     | System               |

### NotificationPriority

| Value | Name   |
| ----- | ------ |
| 0     | Low    |
| 1     | Normal |
| 2     | High   |
| 3     | Urgent |

### CycleFrequency (Performance)

| Value | Name      |
| ----- | --------- |
| 1     | Annual    |
| 2     | Quarterly |
| 3     | Monthly   |

### RatingScaleType (Performance)

| Value | Name       |
| ----- | ---------- |
| 1     | OneToFive  |
| 2     | Percentile |

### ReviewStatus (Performance)

| Value | Name       |
| ----- | ---------- |
| 0     | Draft      |
| 1     | InProgress |
| 2     | Submitted  |
| 3     | Closed     |

### GoalStatus (Performance)

| Value | Name       |
| ----- | ---------- |
| 0     | NotStarted |
| 1     | OnTrack    |
| 2     | AtRisk     |
| 3     | Completed  |

### FeedbackType (Performance)

| Value | Name    |
| ----- | ------- |
| 1     | Self    |
| 2     | Manager |
| 3     | Peer    |

---

_This file was generated based on actual codebase analysis. All patterns reflect observed conventions, not invented best practices._
