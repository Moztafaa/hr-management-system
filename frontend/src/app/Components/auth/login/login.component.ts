import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../../Services/auth.service';
import { LayoutService } from '../../../Services/layout.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  email = '';
  password = '';
  errorMessage = '';

  private authService = inject(AuthService);
  private router = inject(Router);
  private layoutService = inject(LayoutService);

  login() {
    this.authService
      .login({ email: this.email, password: this.password, rememberMe: true })
      .subscribe({
        next: (res) => {
          // The saveAuthData in the service already handles saving all data via tap operator
          if (!res.hasError && res.data) {
            this.errorMessage = '';
            this.layoutService.closeLogin();

            // Redirect based on role
            const roles = res.data.roles || [];
            if (roles.includes('HR') || roles.includes('Manager')) {
              this.router.navigate(['/pages/dashboard']);
            } else {
              // Employee or other roles go to ESS dashboard
              this.router.navigate(['/pages/ess/dashboard']);
            }
          } else {
            this.errorMessage = res.errorMessage || 'Login failed';
            alert(this.errorMessage);
          }
        },
        error: (err) => {
          console.error('FULL ERROR: ', err);
          if (err.status === 401) {
            this.errorMessage = err.error?.errorMessage || 'Invalid credentials';
            alert(this.errorMessage);
          } else {
            this.errorMessage = 'Error connecting to server';
            alert('Error connecting to server');
          }
        },
      });
  }

  close() {
    this.layoutService.closeLogin();
    this.router.navigate(['/']);
  }

  openRegister() {
    this.layoutService.openRegister();
    this.router.navigate(['/pages/register']);
  }
}
