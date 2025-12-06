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
import { AuthGuard } from './auth.guard';
import { LeaveType } from './Components/Leaves/leave-type/leave-type';
import { LeaveBalance } from './Components/Leaves/leave-balance/leave-balance';
import { EmployeeLeaveRequest } from './Components/Leaves/Employee-leave-request/Employee-leave-request';
import { LeaveRequests } from './Components/Leaves/Leave-requests/leave-requests';
import { LandingPageComponent } from './Components/landing/landing-page/landing-page.component';

export const routes: Routes = [
  {
    path: 'pages',
    component: MainLayoutComponent,
    children: [

      { path: 'login', component: LoginComponent },
      { path: 'login2', component: Login }, // nada a7med login
      { path: 'register', component: RegisterComponent },
      { path: 'register2', component: Register }, // nada a7med register

      // Unauthorized page
      {
        path: 'unauthorized',
        loadComponent: () =>
          import('./pages/unauthorized/unauthorized.component').then(
            (m) => m.UnauthorizedComponent
          ),
        title: 'Access Denied',
      },


      {
        path: 'dashboard',
        component: DashboardComponent,
        canActivate: [AuthGuard],
        data: { roles: ['HR', 'Manager'] },
      },

      // ================= EMPLOYEES =================
      {
        path: 'employees',
        loadComponent: () =>
          import('./Components/employees/employee-list/employee-list').then(
            (m) => m.EmployeeList
          ),
        canActivate: [AuthGuard],
        data: { roles: ['HR'] },
        title: 'Employee Management',
      },

      {
        path: 'employees/resignations',
        loadComponent: () =>
          import('./Components/employees/resignation-list/resignation-list.component').then(
            (m) => m.ResignationListComponent
          ),
        canActivate: [AuthGuard],
        data: { roles: ['HR', 'Manager'] },
        title: 'Resignation Requests',
      },

      // ================= ATTENDANCE =================
      {
        path: 'attendance',
        canActivate: [AuthGuard],
        data: { roles: ['HR', 'Manager', 'Employee'] },
        children: [
          { path: 'check', component: CheckIn, title: 'Check In/Out' },
          { path: 'checkout', component: CheckOut, title: 'Check Out' },
          {
            path: 'records',
            loadComponent: () =>
              import(
                './Components/Attendance/attendance-records/attendance-records.component'
              ).then((m) => m.AttendanceRecordsComponent),
            title: 'Attendance Records',
          },
          { path: '', redirectTo: 'check', pathMatch: 'full' },
        ],
      },


      {
        path: 'checkIn',
        component: CheckIn,
        canActivate: [AuthGuard],
        data: { roles: ['HR', 'Manager', 'Employee'] },
        title: 'CheckIn',
      },
      {
        path: 'checkOut',
        component: CheckOut,
        canActivate: [AuthGuard],
        data: { roles: ['HR', 'Manager', 'Employee'] },
        title: 'CheckOut',
      },
      {
        path: 'updateEmployeeImage',
        component: UpdateEmployeeImage,
        canActivate: [AuthGuard],
        data: { roles: ['HR', 'Employee', 'Manager'] },
        title: 'UpdateEmployeeImage',
      },

      // ================= ESS – Employee Self-Service =================
      {
        path: 'ess',
        canActivate: [AuthGuard],
        data: { roles: ['Employee', 'Manager', 'HR'] },
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

      // ================= RECRUITMENT =================
      {
        path: 'recruitment',
        canActivate: [AuthGuard],
        data: { roles: ['HR'] },
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

      // ================= TRAINING =================
      {
        path: 'training',
        canActivate: [AuthGuard],
        data: { roles: ['HR', 'Manager'] },
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

      // ================= ORGANIZATION =================
      {
        path: 'departments',
        loadComponent: () =>
          import(
            './Components/organization/departments/department-list/department-list.component'
          ).then((m) => m.DepartmentListComponent),
        canActivate: [AuthGuard],
        data: { roles: ['HR'] },
        title: 'Departments',
      },
      {
        path: 'designations',
        loadComponent: () =>
          import(
            './Components/organization/designations/designation-list/designation-list.component'
          ).then((m) => m.DesignationListComponent),
        canActivate: [AuthGuard],
        data: { roles: ['HR'] },
        title: 'Designations',
      },

      // ================= PAYROLL =================
      {
        path: 'payroll',
        canActivate: [AuthGuard],
        data: { roles: ['HR'] },
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

      // ================= PERFORMANCE =================
      {
        path: 'performance',
        canActivate: [AuthGuard],
        children: [
          {
            path: 'cycles',
            canActivate: [AuthGuard],
            data: { roles: ['HR', 'Manager'] },
            loadComponent: () =>
              import(
                './Components/performance/review-cycles/review-cycle-list/review-cycle-list.component'
              ).then((m) => m.ReviewCycleListComponent),
            title: 'Review Cycles',
          },
          {
            path: 'reviews',
            canActivate: [AuthGuard],
            data: { roles: ['HR', 'Manager'] },
            loadComponent: () =>
              import('./Components/performance/reviews/review-list/review-list.component').then(
                (m) => m.ReviewListComponent
              ),
            title: 'Performance Reviews',
          },
          {
            path: 'goals',
            canActivate: [AuthGuard],

            data: { roles: ['Employee', 'HR', 'Manager'] },
            loadComponent: () =>
              import('./Components/performance/goals/goal-list/goal-list.component').then(
                (m) => m.GoalListComponent
              ),
            title: 'Goals',
          },
          {
            path: 'kpis',
            canActivate: [AuthGuard],
            data: { roles: ['HR', 'Manager'] },
            loadComponent: () =>
              import('./Components/performance/kpis/kpi-list/kpi-list.component').then(
                (m) => m.KpiListComponent
              ),
            title: 'KPIs',
          },
          {
            path: 'competencies',
            canActivate: [AuthGuard],
            data: { roles: ['HR', 'Manager'] },
            loadComponent: () =>
              import(
                './Components/performance/competencies/competency-list/competency-list.component'
              ).then((m) => m.CompetencyListComponent),
            title: 'Competencies',
          },
          {
            path: 'feedback',
            canActivate: [AuthGuard],

            data: { roles: ['Employee', 'HR', 'Manager'] },
            loadComponent: () =>
              import(
                './Components/performance/feedback/feedback-list/feedback-list.component'
              ).then((m) => m.FeedbackListComponent),
            title: 'Performance Feedback',
          },
          { path: '', redirectTo: 'cycles', pathMatch: 'full' },
        ],
      },


      // ================= REPORTS =================
      {
        path: 'reports',
        canActivate: [AuthGuard],
        data: { roles: ['HR', 'Manager'] },
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

      // ================= NOTIFICATIONS =================
      {
        path: 'notifications',
        loadComponent: () =>
          import('./Components/notifications/notification-list.component').then(
            (m) => m.NotificationListComponent
          ),
        canActivate: [AuthGuard],
        data: { roles: ['HR', 'Manager', 'Employee'] },
        title: 'Notifications',
      },
      {
        path: 'change-password',
        loadComponent: () =>
          import('./pages/change-password/change-password.component').then(
            (m) => m.ChangePasswordComponent
          ),
        canActivate: [AuthGuard],
        title: 'Change Password',
      },

      // ================= LEAVE MANAGEMENT =================
      {
        path: 'leave/types',
        component: LeaveType,
        canActivate: [AuthGuard],
        data: { roles: ['HR'] },
        title: 'Leave Types',
      },
      {
        path: 'leave/balances',
        component: LeaveBalance,
        canActivate: [AuthGuard],
        data: { roles: ['HR'] },
        title: 'Leave Balances',
      },
      {
        path: 'leave/requests',
        component: LeaveRequests,
        canActivate: [AuthGuard],
        data: { roles: ['HR'] },
        title: 'Leave Requests',
      },
      {
        path: 'leave/manage-balances',
        loadComponent: () =>
          import(
            './Components/Leaves/leave-balance-management/leave-balance-management.component'
          ).then((m) => m.LeaveBalanceManagementComponent),
        canActivate: [AuthGuard],
        data: { roles: ['HR'] },
        title: 'Manage Leave Balances',
      },
      {
        path: 'leave/approvals',
        loadComponent: () =>
          import('./Components/Leaves/my-approvals/my-approvals.component').then(
            (m) => m.MyApprovalsComponent
          ),
        canActivate: [AuthGuard],
        data: { roles: ['Manager', 'HR'] },
        title: 'My Approvals',
      },
      {
        path: 'leave/employee-requests',
        component: EmployeeLeaveRequest,
        canActivate: [AuthGuard],
        data: { roles: ['Employee', 'Manager', 'HR'] },
        title: 'My Leave Requests',
      },
    ],
  },
  {
    path: '',
    component: LandingPageComponent,
    title: 'HRM System - Human Resource Management',
  },
];
