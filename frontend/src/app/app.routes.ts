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

      { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
    ],
  },
  {
    path: '',
    component: LandingPageComponent,
    title: 'HRM System - Human Resource Management',
  },
];
