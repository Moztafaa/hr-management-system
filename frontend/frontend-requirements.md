# Frontend Requirements - HR Management System

This document outlines the main components and navigation structure for the frontend team to implement the sidebar navigation.

---

## Navigation Structure

### Sidebar Menu Hierarchy

```
📊 Dashboard
│
├── 👥 Employee Management
│   ├── Employee List
│   ├── Add Employee
│   └── Employee Details
│
├── 🏢 Organization
│   ├── Departments
│   └── Designations
│
├── 📅 Leave Management
│   ├── Leave Types
│   ├── Leave Requests
│   ├── Leave Approvals
│   └── Leave Balances
│
├── 💰 Payroll
│   ├── Payslips
│   ├── Allowances
│   ├── Deductions
│   ├── Employee Allowances
│   └── Employee Deductions
│
├── 📋 Recruitment
│   ├── Job Postings
│   ├── Candidates
│   └── Job Applications
│
├── 🔔 Notifications
│
├── 👤 Employee Self-Service (ESS)
│   ├── My Profile
│   ├── My Leave Requests
│   ├── My Payslips
│   └── My Dashboard
│
└── ⚙️ Settings
    ├── Account Settings
    └── Change Password
```

---

## Components to Create

### 1. Layout Components

| Component    | Path                             | Description                                 |
| ------------ | -------------------------------- | ------------------------------------------- |
| `MainLayout` | `Components/layout/main-layout/` | Main layout with sidebar and header         |
| `Sidebar`    | `Components/layout/sidebar/`     | Collapsible sidebar navigation              |
| `Header`     | `Components/layout/header/`      | Top header with user menu and notifications |
| `Footer`     | `Components/layout/footer/`      | Optional footer component                   |

### 2. Dashboard

| Component   | Path                    | Route        |
| ----------- | ----------------------- | ------------ |
| `Dashboard` | `Components/dashboard/` | `/dashboard` |

**Features:**

- Summary cards (Total Employees, Pending Leave Requests, Open Jobs, etc.)
- Recent activities
- Quick actions
- Charts/statistics (optional)

### 3. Employee Management

| Component         | Path                                     | Route                 |
| ----------------- | ---------------------------------------- | --------------------- |
| `EmployeeList`    | `Components/employees/employee-list/`    | `/employees`          |
| `EmployeeCreate`  | `Components/employees/employee-create/`  | `/employees/create`   |
| `EmployeeEdit`    | `Components/employees/employee-edit/`    | `/employees/:id/edit` |
| `EmployeeDetails` | `Components/employees/employee-details/` | `/employees/:id`      |

**API Endpoints:**

- `GET /api/Employee` - List all employees
- `GET /api/Employee/{id}` - Get employee by ID
- `POST /api/Employee` - Create employee
- `PUT /api/Employee/{id}` - Update employee
- `DELETE /api/Employee/{id}` - Delete employee

### 4. Organization Management

#### Departments

| Component          | Path                                                     | Route                   |
| ------------------ | -------------------------------------------------------- | ----------------------- |
| `DepartmentList`   | `Components/organization/departments/department-list/`   | `/departments`          |
| `DepartmentCreate` | `Components/organization/departments/department-create/` | `/departments/create`   |
| `DepartmentEdit`   | `Components/organization/departments/department-edit/`   | `/departments/:id/edit` |

**API Endpoints:**

- `GET /api/Department` - List all departments
- `GET /api/Department/{id}` - Get department by ID
- `POST /api/Department` - Create department
- `PUT /api/Department/{id}` - Update department
- `DELETE /api/Department/{id}` - Delete department

#### Designations

| Component           | Path                                                       | Route                    |
| ------------------- | ---------------------------------------------------------- | ------------------------ |
| `DesignationList`   | `Components/organization/designations/designation-list/`   | `/designations`          |
| `DesignationCreate` | `Components/organization/designations/designation-create/` | `/designations/create`   |
| `DesignationEdit`   | `Components/organization/designations/designation-edit/`   | `/designations/:id/edit` |

**API Endpoints:**

- `GET /api/Designation` - List all designations
- `GET /api/Designation/{id}` - Get designation by ID
- `POST /api/Designation` - Create designation
- `PUT /api/Designation/{id}` - Update designation
- `DELETE /api/Designation/{id}` - Delete designation

### 5. Leave Management

| Component            | Path                                     | Route                    |
| -------------------- | ---------------------------------------- | ------------------------ |
| `LeaveTypeList`      | `Components/leave/leave-types/`          | `/leave/types`           |
| `LeaveRequestList`   | `Components/leave/leave-requests/`       | `/leave/requests`        |
| `LeaveRequestCreate` | `Components/leave/leave-request-create/` | `/leave/requests/create` |
| `LeaveApprovalList`  | `Components/leave/leave-approvals/`      | `/leave/approvals`       |
| `LeaveBalanceList`   | `Components/leave/leave-balances/`       | `/leave/balances`        |

**API Endpoints:**

- `GET /api/LeaveType` - List leave types
- `GET /api/LeaveRequest` - List leave requests
- `POST /api/LeaveRequest` - Create leave request
- `GET /api/LeaveApproval` - List approvals
- `PUT /api/LeaveApproval/{id}` - Approve/Reject
- `GET /api/LeaveBalance` - Get leave balances

### 6. Payroll Management

| Component               | Path                                           | Route                          |
| ----------------------- | ---------------------------------------------- | ------------------------------ |
| `PayslipList`           | `Components/payroll/payslips/payslip-list/`    | `/payroll/payslips`            |
| `PayslipCreate`         | `Components/payroll/payslips/payslip-create/`  | `/payroll/payslips/create`     |
| `PayslipDetails`        | `Components/payroll/payslips/payslip-details/` | `/payroll/payslips/:id`        |
| `AllowanceList`         | `Components/payroll/allowances/`               | `/payroll/allowances`          |
| `DeductionList`         | `Components/payroll/deductions/`               | `/payroll/deductions`          |
| `EmployeeAllowanceList` | `Components/payroll/employee-allowances/`      | `/payroll/employee-allowances` |
| `EmployeeDeductionList` | `Components/payroll/employee-deductions/`      | `/payroll/employee-deductions` |

**API Endpoints:**

- `GET /api/Payslip` - List payslips
- `POST /api/Payslip` - Generate payslip
- `GET /api/Allowance` - List allowances
- `GET /api/Deduction` - List deductions
- `GET /api/EmployeeAllowance` - List employee allowances
- `GET /api/EmployeeDeduction` - List employee deductions

### 7. Recruitment

| Component               | Path                                                       | Route                           |
| ----------------------- | ---------------------------------------------------------- | ------------------------------- |
| `JobPostingList`        | `Components/recruitment/job-postings/job-posting-list/`    | `/recruitment/jobs`             |
| `JobPostingCreate`      | `Components/recruitment/job-postings/job-posting-create/`  | `/recruitment/jobs/create`      |
| `JobPostingDetails`     | `Components/recruitment/job-postings/job-posting-details/` | `/recruitment/jobs/:id`         |
| `CandidateList`         | `Components/recruitment/candidates/candidate-list/`        | `/recruitment/candidates`       |
| `CandidateDetails`      | `Components/recruitment/candidates/candidate-details/`     | `/recruitment/candidates/:id`   |
| `JobApplicationList`    | `Components/recruitment/applications/application-list/`    | `/recruitment/applications`     |
| `JobApplicationDetails` | `Components/recruitment/applications/application-details/` | `/recruitment/applications/:id` |

**API Endpoints:**

- `GET /api/JobPosting` - List job postings
- `POST /api/JobPosting` - Create job posting
- `GET /api/Candidate` - List candidates
- `GET /api/JobApplication` - List applications

### 8. Notifications

| Component              | Path                                              | Route                  |
| ---------------------- | ------------------------------------------------- | ---------------------- |
| `NotificationList`     | `Components/notifications/notification-list/`     | `/notifications`       |
| `NotificationDropdown` | `Components/notifications/notification-dropdown/` | N/A (Header component) |

**API Endpoints:**

- `GET /api/Notification` - List notifications
- `PUT /api/Notification/{id}/read` - Mark as read

### 9. Employee Self-Service (ESS)

| Component          | Path                                 | Route                 |
| ------------------ | ------------------------------------ | --------------------- |
| `ESSProfile`       | `Components/ess/ess-profile/`        | `/ess/profile`        |
| `ESSDashboard`     | `Components/ess/ess-dashboard/`      | `/ess/dashboard`      |
| `ESSLeaveRequests` | `Components/ess/ess-leave-requests/` | `/ess/leave-requests` |
| `ESSPayslips`      | `Components/ess/ess-payslips/`       | `/ess/payslips`       |

**API Endpoints:**

- `GET /api/ESS/profile` - Get current user profile
- `PUT /api/ESS/profile` - Update profile
- `GET /api/ESS/dashboard` - Get ESS dashboard data

### 10. Authentication (Already Exists)

| Component        | Path                                  | Route                      |
| ---------------- | ------------------------------------- | -------------------------- |
| `Login`          | `Components/login/`                   | `/login`                   |
| `Register`       | `Components/register/`                | `/register`                |
| `ChangePassword` | `Components/account/change-password/` | `/account/change-password` |
| `ResetPassword`  | `Components/account/reset-password/`  | `/account/reset-password`  |

---

## Shared Components

| Component        | Path                                 | Description                                     |
| ---------------- | ------------------------------------ | ----------------------------------------------- |
| `DataTable`      | `Components/shared/data-table/`      | Reusable table with sorting, pagination, search |
| `ConfirmDialog`  | `Components/shared/confirm-dialog/`  | Confirmation modal dialog                       |
| `LoadingSpinner` | `Components/shared/loading-spinner/` | Loading indicator                               |
| `AlertMessage`   | `Components/shared/alert-message/`   | Success/Error/Warning alerts                    |
| `FormField`      | `Components/shared/form-field/`      | Reusable form field wrapper                     |
| `PageHeader`     | `Components/shared/page-header/`     | Page title with breadcrumbs                     |
| `EmptyState`     | `Components/shared/empty-state/`     | Empty data state component                      |
| `SearchBox`      | `Components/shared/search-box/`      | Search input component                          |
| `Pagination`     | `Components/shared/pagination/`      | Pagination component                            |
| `StatusBadge`    | `Components/shared/status-badge/`    | Status indicator badges                         |

---

## Services to Create

| Service          | Path                          | Description                      |
| ---------------- | ----------------------------- | -------------------------------- |
| `Employee`       | `Services/employee.ts`        | Employee CRUD operations         |
| `Department`     | `Services/department.ts`      | Department CRUD operations       |
| `Designation`    | `Services/designation.ts`     | Designation CRUD operations      |
| `LeaveType`      | `Services/leave-type.ts`      | Leave type operations            |
| `LeaveRequest`   | `Services/leave-request.ts`   | Leave request operations         |
| `LeaveApproval`  | `Services/leave-approval.ts`  | Leave approval operations        |
| `LeaveBalance`   | `Services/leave-balance.ts`   | Leave balance operations         |
| `Payslip`        | `Services/payslip.ts`         | Payslip operations               |
| `Allowance`      | `Services/allowance.ts`       | Allowance operations             |
| `Deduction`      | `Services/deduction.ts`       | Deduction operations             |
| `JobPosting`     | `Services/job-posting.ts`     | Job posting operations           |
| `Candidate`      | `Services/candidate.ts`       | Candidate operations             |
| `JobApplication` | `Services/job-application.ts` | Job application operations       |
| `Notification`   | `Services/notification.ts`    | Notification operations          |
| `ESS`            | `Services/ess.ts`             | Employee self-service operations |

---

## Route Configuration

```typescript
export const routes: Routes = [
  // Public routes
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', loadComponent: () => import('./Components/login/login').then((m) => m.Login) },
  {
    path: 'register',
    loadComponent: () => import('./Components/register/register').then((m) => m.Register),
  },

  // Protected routes (with AuthGuard)
  {
    path: '',
    component: MainLayout,
    canActivate: [authGuard],
    children: [
      // Dashboard
      {
        path: 'dashboard',
        loadComponent: () => import('./Components/dashboard/dashboard').then((m) => m.Dashboard),
      },

      // Employees
      {
        path: 'employees',
        loadComponent: () =>
          import('./Components/employees/employee-list/employee-list').then((m) => m.EmployeeList),
      },
      {
        path: 'employees/create',
        loadComponent: () =>
          import('./Components/employees/employee-create/employee-create').then(
            (m) => m.EmployeeCreate
          ),
      },
      {
        path: 'employees/:id',
        loadComponent: () =>
          import('./Components/employees/employee-details/employee-details').then(
            (m) => m.EmployeeDetails
          ),
      },
      {
        path: 'employees/:id/edit',
        loadComponent: () =>
          import('./Components/employees/employee-edit/employee-edit').then((m) => m.EmployeeEdit),
      },

      // Departments
      {
        path: 'departments',
        loadComponent: () =>
          import('./Components/organization/departments/department-list/department-list').then(
            (m) => m.DepartmentList
          ),
      },

      // Designations
      {
        path: 'designations',
        loadComponent: () =>
          import('./Components/organization/designations/designation-list/designation-list').then(
            (m) => m.DesignationList
          ),
      },

      // Leave Management
      {
        path: 'leave/types',
        loadComponent: () =>
          import('./Components/leave/leave-types/leave-types').then((m) => m.LeaveTypeList),
      },
      {
        path: 'leave/requests',
        loadComponent: () =>
          import('./Components/leave/leave-requests/leave-requests').then(
            (m) => m.LeaveRequestList
          ),
      },
      {
        path: 'leave/approvals',
        loadComponent: () =>
          import('./Components/leave/leave-approvals/leave-approvals').then(
            (m) => m.LeaveApprovalList
          ),
      },
      {
        path: 'leave/balances',
        loadComponent: () =>
          import('./Components/leave/leave-balances/leave-balances').then(
            (m) => m.LeaveBalanceList
          ),
      },

      // Payroll
      {
        path: 'payroll/payslips',
        loadComponent: () =>
          import('./Components/payroll/payslips/payslip-list/payslip-list').then(
            (m) => m.PayslipList
          ),
      },
      {
        path: 'payroll/allowances',
        loadComponent: () =>
          import('./Components/payroll/allowances/allowances').then((m) => m.AllowanceList),
      },
      {
        path: 'payroll/deductions',
        loadComponent: () =>
          import('./Components/payroll/deductions/deductions').then((m) => m.DeductionList),
      },

      // Recruitment
      {
        path: 'recruitment/jobs',
        loadComponent: () =>
          import('./Components/recruitment/job-postings/job-posting-list/job-posting-list').then(
            (m) => m.JobPostingList
          ),
      },
      {
        path: 'recruitment/candidates',
        loadComponent: () =>
          import('./Components/recruitment/candidates/candidate-list/candidate-list').then(
            (m) => m.CandidateList
          ),
      },
      {
        path: 'recruitment/applications',
        loadComponent: () =>
          import('./Components/recruitment/applications/application-list/application-list').then(
            (m) => m.JobApplicationList
          ),
      },

      // Notifications
      {
        path: 'notifications',
        loadComponent: () =>
          import('./Components/notifications/notification-list/notification-list').then(
            (m) => m.NotificationList
          ),
      },

      // ESS
      {
        path: 'ess/profile',
        loadComponent: () =>
          import('./Components/ess/ess-profile/ess-profile').then((m) => m.ESSProfile),
      },
      {
        path: 'ess/dashboard',
        loadComponent: () =>
          import('./Components/ess/ess-dashboard/ess-dashboard').then((m) => m.ESSDashboard),
      },

      // Account
      {
        path: 'account/change-password',
        loadComponent: () =>
          import('./Components/account/change-password/change-password').then(
            (m) => m.ChangePassword
          ),
      },
    ],
  },

  // Wildcard
  { path: '**', redirectTo: 'login' },
];
```

---

## User Roles & Menu Visibility

| Menu Item           | Admin | HR  | Manager | Employee |
| ------------------- | :---: | :-: | :-----: | :------: |
| Dashboard           |  ✅   | ✅  |   ✅    |    ✅    |
| Employee Management |  ✅   | ✅  |   👁️    |    ❌    |
| Departments         |  ✅   | ✅  |   👁️    |    ❌    |
| Designations        |  ✅   | ✅  |   👁️    |    ❌    |
| Leave Types         |  ✅   | ✅  |   ❌    |    ❌    |
| Leave Requests      |  ✅   | ✅  |   ✅    |    ✅    |
| Leave Approvals     |  ✅   | ✅  |   ✅    |    ❌    |
| Leave Balances      |  ✅   | ✅  |   ✅    |    ✅    |
| Payroll             |  ✅   | ✅  |   ❌    |    ❌    |
| Recruitment         |  ✅   | ✅  |   ✅    |    ❌    |
| Notifications       |  ✅   | ✅  |   ✅    |    ✅    |
| ESS                 |  ✅   | ✅  |   ✅    |    ✅    |

Legend: ✅ Full Access | 👁️ View Only | ❌ No Access

---

## API Base URL

```typescript
// Development
const API_BASE_URL = 'https://localhost:7005/api';

// Production (update accordingly)
const API_BASE_URL = 'https://api.yourhrms.com/api';
```

---

## TypeScript Models

Create interfaces in `models/` folder matching backend DTOs:

```
models/
├── employee.ts
├── department.ts
├── designation.ts
├── leave-type.ts
├── leave-request.ts
├── leave-balance.ts
├── payslip.ts
├── allowance.ts
├── deduction.ts
├── job-posting.ts
├── candidate.ts
├── job-application.ts
├── notification.ts
├── user.ts
└── api-response.ts
```

### API Response Model

```typescript
export interface ApiResponse<T> {
  isSuccess: boolean;
  message: string;
  data: T | null;
  statusCode: number;
  errors: string[] | null;
}
```

---

## Priority Order

### Phase 1 - Core (Week 1-2)

1. Layout components (MainLayout, Sidebar, Header)
2. Dashboard
3. Employee Management (CRUD)
4. Authentication flow improvements

### Phase 2 - Organization (Week 3)

1. Department Management
2. Designation Management
3. Shared components (DataTable, ConfirmDialog, etc.)

### Phase 3 - Leave Management (Week 4)

1. Leave Types
2. Leave Requests
3. Leave Approvals
4. Leave Balances

### Phase 4 - Payroll (Week 5)

1. Payslips
2. Allowances & Deductions
3. Employee Allowances/Deductions

### Phase 5 - Recruitment (Week 6)

1. Job Postings
2. Candidates
3. Job Applications

### Phase 6 - ESS & Notifications (Week 7)

1. Notifications
2. ESS Dashboard
3. ESS Profile
4. ESS Leave/Payslips

---

## Notes for Development

1. **Standalone Components**: All components should be standalone (no NgModules)
2. **Lazy Loading**: Use lazy loading for all routes
3. **Service Naming**: Services should NOT have "Service" suffix (e.g., `Employee` not `EmployeeService`)
4. **Component Naming**: Components should NOT have "Component" suffix (e.g., `EmployeeList` not `EmployeeListComponent`)
5. **HTTP Interceptor**: Create an interceptor to add JWT token to all requests
6. **Error Handling**: Create a global error handler for API errors
7. **Loading States**: Show loading indicators during API calls
8. **Responsive Design**: Ensure sidebar is collapsible on mobile devices
