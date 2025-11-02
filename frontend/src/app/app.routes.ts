import { Routes } from '@angular/router';
import { Login } from './Components/login/login';

export const routes: Routes = [
    // { path: '', redirectTo: 'home', pathMatch: 'full', title: 'Home' },
    { path: 'login', component: Login, title: 'Login' }
];
