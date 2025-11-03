import { Routes } from '@angular/router';
import { Login } from './Components/login/login';
import { Register } from './Components/register/register';

export const routes: Routes = [
    // { path: '', redirectTo: 'home', pathMatch: 'full', title: 'Home' },
    { path: 'login', component: Login, title: 'Login' },
    { path: 'register', component: Register, title: 'Register' }
];
