import { Routes } from '@angular/router';
import { Login } from './Components/login/login';
import { Register } from './Components/register/register';
import { CheckIn } from './Components/Attendance/check-in/check-in';
import { CheckOut } from './Components/Attendance/check-out/check-out';
import { UpdateEmployeeImage } from './Components/Employee/update-employee-image/update-employee-image';
import { MainLayoutComponent } from './layouts/main-layout/main-layout.component';
import { LoginComponent } from './Components/auth/login/login.component';
import { RegisterComponent } from './Components/auth/register/register.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { authGuard } from './auth.guard';
import { LeaveType } from './Components/Leaves/leave-type/leave-type';
import { LeaveBalance } from './Components/Leaves/leave-balance/leave-balance';
import { EmployeeLeaveRequest } from './Components/Leaves/Employee-leave-request/Employee-leave-request';
import { LeaveRequests } from './Components/Leaves/Leave-requests/leave-requests';
import { LandingPageComponent } from './Components/landing/landing-page/landing-page.component';

/* export const routes: Routes = [
    // { path: '', redirectTo: 'home', pathMatch: 'full', title: 'Home' },
    { path: 'login', component: Login, title: 'Login' },
    { path: 'register', component: Register, title: 'Register' },
    { path: 'checkIn', component: CheckIn, title: 'CheckIn' },
    { path: 'checkOut', component: CheckOut, title: 'CheckOut' },
    { path: 'updateEmployeeImage', component: UpdateEmployeeImage, title: 'UpdateEmployeeImage' }
];
 */

export const routes: Routes = [
  {
    path: 'pages',
    component: MainLayoutComponent,
    children: [
      { path: 'login', component: LoginComponent },

      { path: 'login2', component: Login }, // nada a7med login
      { path: 'register', component: RegisterComponent },
      { path: 'register2', component: Register }, // nada a7med register
      { path: 'dashboard', component: DashboardComponent, canActivate: [authGuard] },
      {
        path: 'employees',
        loadComponent: () =>
          import('./Components/employees/employee-list/employee-list').then((m) => m.EmployeeList),
        canActivate: [authGuard],
        title: 'Employee Management',
      },
      {
        path: 'attendance',
        canActivate: [authGuard],
        children: [
          { path: 'check', component: CheckIn, title: 'Check In/Out' },
          { path: 'checkout', component: CheckOut, title: 'Check Out' },
          {
            path: 'records',
            loadComponent: () => import('./Components/Attendance/attendance-records/attendance-records.component').then(m => m.AttendanceRecordsComponent),
            title: 'Attendance Records'
          },
          { path: '', redirectTo: 'check', pathMatch: 'full' }
        ]
      },
      {
        path: 'employees/resignations',
        loadComponent: () =>
          import('./Components/employees/resignation-list/resignation-list.component').then(
            (m) => m.ResignationListComponent
          ),
        canActivate: [authGuard],
        title: 'Resignation Requests',
      },
      { path: 'checkIn', component: CheckIn, title: 'CheckIn' },
      { path: 'checkOut', component: CheckOut, title: 'CheckOut' },
      { path: 'updateEmployeeImage', component: UpdateEmployeeImage, title: 'UpdateEmployeeImage' },

      // ESS (Employee Self-Service) Routes
      {
        path: 'ess',
        canActivate: [authGuard],
        children: [
          {
            path: 'dashboard',
            loadComponent: () =>
              import('./Components/ess/ess-dashboard/ess-dashboard.component').then(
                (m) => m.EssDashboardComponent
              ),
            title: 'ESS Dashboard',
          },
          {
            path: 'profile',
            loadComponent: () =>
              import('./Components/ess/ess-profile/ess-profile.component').then(
                (m) => m.EssProfileComponent
              ),
            title: 'My Profile',
          },
          {
            path: 'leave-requests',
            loadComponent: () =>
              import('./Components/ess/ess-leave-requests/ess-leave-requests.component').then(
                (m) => m.EssLeaveRequestsComponent
              ),
            title: 'My Leave Requests',
          },
          {
            path: 'payslips',
            loadComponent: () =>
              import('./Components/ess/ess-payslips/ess-payslips.component').then(
                (m) => m.EssPayslipsComponent
              ),
            title: 'My Payslips',
          },
          {
            path: 'training',
            loadComponent: () =>
              import('./Components/ess/ess-training/ess-training.component').then(
                (m) => m.EssTrainingComponent
              ),
            title: 'My Training',
          },
          {
            path: 'resignations',
            loadComponent: () =>
              import('./Components/ess/ess-resignations/ess-resignations.component').then(
                (m) => m.EssResignationsComponent
              ),
            title: 'My Resignations',
          },
          { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
        ],
      },

      // Recruitment Routes
      {
        path: 'recruitment',
        canActivate: [authGuard],
        children: [
          {
            path: 'jobs',
            loadComponent: () =>
              import(
                './Components/recruitment/job-postings/job-posting-list/job-posting-list.component'
              ).then((m) => m.JobPostingListComponent),
            title: 'Job Postings',
          },
          {
            path: 'candidates',
            loadComponent: () =>
              import(
                './Components/recruitment/candidates/candidate-list/candidate-list.component'
              ).then((m) => m.CandidateListComponent),
            title: 'Candidates',
          },
          {
            path: 'applications',
            loadComponent: () =>
              import(
                './Components/recruitment/applications/application-list/application-list.component'
              ).then((m) => m.ApplicationListComponent),
            title: 'Job Applications',
          },
          { path: '', redirectTo: 'jobs', pathMatch: 'full' },
        ],
      },

      // Training Routes
      {
        path: 'training',
        canActivate: [authGuard],
        children: [
          {
            path: 'courses',
            loadComponent: () =>
              import(
                './Components/training/training-courses/training-course-list/training-course-list.component'
              ).then((m) => m.TrainingCourseListComponent),
            title: 'Training Courses',
          },
          {
            path: 'enrollments',
            loadComponent: () =>
              import(
                './Components/training/employee-enrollments/enrollment-list/enrollment-list.component'
              ).then((m) => m.EnrollmentListComponent),
            title: 'Employee Enrollments',
          },
          {
            path: 'requests',
            loadComponent: () =>
              import(
                './Components/training/training-requests/request-list/request-list.component'
              ).then((m) => m.RequestListComponent),
            title: 'Training Requests',
          },
          { path: '', redirectTo: 'courses', pathMatch: 'full' },
        ],
      },

      // Organization Routes (Departments & Designations)
      {
        path: 'departments',
        loadComponent: () =>
          import(
            './Components/organization/departments/department-list/department-list.component'
          ).then((m) => m.DepartmentListComponent),
        canActivate: [authGuard],
        title: 'Departments',
      },
      {
        path: 'designations',
        loadComponent: () =>
          import(
            './Components/organization/designations/designation-list/designation-list.component'
          ).then((m) => m.DesignationListComponent),
        canActivate: [authGuard],
        title: 'Designations',
      },

      // Payroll Routes
      {
        path: 'payroll',
        canActivate: [authGuard],
        children: [
          {
            path: 'payslips',
            loadComponent: () =>
              import('./Components/payroll/payslip-list/payslip-list.component').then(
                (m) => m.PayslipListComponent
              ),
            title: 'Payslips',
          },
          {
            path: 'allowances',
            loadComponent: () =>
              import('./Components/payroll/allowance-list/allowance-list.component').then(
                (m) => m.AllowanceListComponent
              ),
            title: 'Allowances',
          },
          {
            path: 'deductions',
            loadComponent: () =>
              import('./Components/payroll/deduction-list/deduction-list.component').then(
                (m) => m.DeductionListComponent
              ),
            title: 'Deductions',
          },
          {
            path: 'employee-allowances',
            loadComponent: () =>
              import(
                './Components/payroll/employee-allowance-list/employee-allowance-list.component'
              ).then((m) => m.EmployeeAllowanceListComponent),
            title: 'Employee Allowances',
          },
          {
            path: 'employee-deductions',
            loadComponent: () =>
              import(
                './Components/payroll/employee-deduction-list/employee-deduction-list.component'
              ).then((m) => m.EmployeeDeductionListComponent),
            title: 'Employee Deductions',
          },
          { path: '', redirectTo: 'allowances', pathMatch: 'full' },
        ],
      },

      // Performance Routes
      {
        path: 'performance',
        canActivate: [authGuard],
        children: [
          {
            path: 'cycles',
            loadComponent: () =>
              import(
                './Components/performance/review-cycles/review-cycle-list/review-cycle-list.component'
              ).then((m) => m.ReviewCycleListComponent),
            title: 'Review Cycles',
          },
          {
            path: 'reviews',
            loadComponent: () =>
              import('./Components/performance/reviews/review-list/review-list.component').then(
                (m) => m.ReviewListComponent
              ),
            title: 'Performance Reviews',
          },
          {
            path: 'goals',
            loadComponent: () =>
              import('./Components/performance/goals/goal-list/goal-list.component').then(
                (m) => m.GoalListComponent
              ),
            title: 'Goals',
          },
          {
            path: 'kpis',
            loadComponent: () =>
              import('./Components/performance/kpis/kpi-list/kpi-list.component').then(
                (m) => m.KpiListComponent
              ),
            title: 'KPIs',
          },
          {
            path: 'competencies',
            loadComponent: () =>
              import(
                './Components/performance/competencies/competency-list/competency-list.component'
              ).then((m) => m.CompetencyListComponent),
            title: 'Competencies',
          },
          {
            path: 'feedback',
            loadComponent: () =>
              import(
                './Components/performance/feedback/feedback-list/feedback-list.component'
              ).then((m) => m.FeedbackListComponent),
            title: 'Performance Feedback',
          },
          { path: '', redirectTo: 'cycles', pathMatch: 'full' },
        ],
      },

      // Reports Routes
      {
        path: 'reports',
        canActivate: [authGuard],
        children: [
          {
            path: 'dashboard',
            loadComponent: () =>
              import('./pages/reports/dashboard/reporting-dashboard.component').then(
                (m) => m.ReportingDashboardComponent
              ),
            title: 'Reports Dashboard',
          },
          {
            path: 'employees',
            loadComponent: () =>
              import('./pages/reports/employees/employees-reports.component').then(
                (m) => m.EmployeesReportsComponent
              ),
            title: 'Employee Reports',
          },
          {
            path: 'payroll',
            loadComponent: () =>
              import('./pages/reports/payroll/payroll-reports.component').then(
                (m) => m.PayrollReportsComponent
              ),
            title: 'Payroll Reports',
          },
          {
            path: 'leave',
            loadComponent: () =>
              import('./pages/reports/leave/leave-reports.component').then(
                (m) => m.LeaveReportsComponent
              ),
            title: 'Leave Reports',
          },
          {
            path: 'recruitment',
            loadComponent: () =>
              import('./pages/reports/recruitment/recruitment-reports.component').then(
                (m) => m.RecruitmentReportsComponent
              ),
            title: 'Recruitment Reports',
          },
          { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
        ],
      },

      // Notifications Route
      {
        path: 'notifications',
        loadComponent: () =>
          import('./Components/notifications/notification-list.component').then(
            (m) => m.NotificationListComponent
          ),
        canActivate: [authGuard],
        title: 'Notifications',
      },

      // Leave Management Routes
      { path: 'leave/types', component: LeaveType, title: 'Leave Types' },
      { path: 'leave/balances', component: LeaveBalance, title: 'Leave Balances' },
      { path: 'leave/requests', component: LeaveRequests, title: 'Leave Requests' },
      {
        path: 'leave/manage-balances',
        loadComponent: () => import('./Components/Leaves/leave-balance-management/leave-balance-management.component').then(m => m.LeaveBalanceManagementComponent),
        title: 'Manage Leave Balances'
      },
      {
        path: 'leave/approvals',
        loadComponent: () => import('./Components/Leaves/my-approvals/my-approvals.component').then(m => m.MyApprovalsComponent),
        title: 'My Approvals'
      },
      { path: 'leave/employee-requests', component: EmployeeLeaveRequest, title: 'My Leave Requests' },
    ],

  },
  {
    path: '',
    component: LandingPageComponent,
    title: 'HRM System - Human Resource Management',
  },
];
