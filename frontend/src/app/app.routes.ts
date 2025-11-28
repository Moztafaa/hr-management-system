import { Routes } from '@angular/router';
import { MainLayoutComponent } from './layouts/main-layout/main-layout.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { LoginComponent } from './components/auth/login/login.component';
import { RegisterComponent } from './components/auth/register/register.component';
import { authGuard } from './auth.guard';

export const routes: Routes = [
  {
    path: 'pages',
    component: MainLayoutComponent,
    children: [
      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegisterComponent },
      { path: 'dashboard', component: DashboardComponent, canActivate: [authGuard] },
      {
        path: 'employees',
        loadComponent: () =>
          import('./components/employees/employee-list/employee-list').then((m) => m.EmployeeList),
      },
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
    ],
  },
  { path: '', redirectTo: 'pages', pathMatch: 'full' },
];
