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
      },
      { path: 'checkIn', component: CheckIn, title: 'CheckIn' },
      { path: 'checkOut', component: CheckOut, title: 'CheckOut' },
      { path: 'updateEmployeeImage', component: UpdateEmployeeImage, title: 'UpdateEmployeeImage' },
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
      { path: 'leave/types', component: LeaveType, title: 'LeaveType' },
      { path: 'leave/balances', component: LeaveBalance, title: 'LeaveBalance' },
      { path: 'ess/leave-requests', component: EmployeeLeaveRequest, title: 'EmployeeLeaveRequest' },
      { path: 'leave/requests', component: LeaveRequests, title: 'LeaveRequests' },

    ],
  },
  { path: '', redirectTo: 'pages', pathMatch: 'full' },
];
