import { Component, EventEmitter, Output, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../../Services/auth.service';

@Component({
  selector: 'app-login-modal',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './login-modal.component.html',
  styleUrl: './login-modal.component.css',
})
export class LoginModalComponent {
  @Output() close = new EventEmitter<void>();
  @Output() loginSuccess = new EventEmitter<void>();

  private authService = inject(AuthService);
  private router = inject(Router);

  email = '';
  password = '';
  rememberMe = false;
  isLoading = signal(false);
  error = signal<string | null>(null);
  showPassword = signal(false);

  onSubmit(): void {
    if (!this.email || !this.password) {
      this.error.set('Please enter both email and password');
      return;
    }

    this.isLoading.set(true);
    this.error.set(null);

    const loginData = {
      email: this.email,
      password: this.password,
      rememberMe: this.rememberMe,
    };

    this.authService.login(loginData).subscribe({
      next: (response) => {
        this.isLoading.set(false);
        if (response.hasError) {
          this.error.set(response.errorMessage || 'Login failed. Please try again.');
        } else if (response.data) {
          // Store token and user info
          if (response.data.token) {
            this.authService.saveToken(response.data.token);
          }
          // employeeId is a number; save it with the dedicated method and
          // guard against null/undefined (don't treat 0 as absence)
          if (response.data.employeeId != null) {
            this.authService.saveEmployeeId(response.data.employeeId);
          }

          this.loginSuccess.emit();

          // Redirect based on role
          const roles = response.data.roles || [];
          if (roles.includes('HR') || roles.includes('Manager')) {
            this.router.navigate(['/pages/dashboard']);
          } else {
            // Employee or other roles go to ESS dashboard
            this.router.navigate(['/pages/ess/dashboard']);
          }
        }
      },
      error: (err) => {
        this.isLoading.set(false);
        this.error.set(err.error?.errorMessage || 'An error occurred. Please try again.');
      },
    });
  }

  togglePasswordVisibility(): void {
    this.showPassword.update((v) => !v);
  }

  onBackdropClick(event: MouseEvent): void {
    if ((event.target as HTMLElement).classList.contains('modal-overlay')) {
      this.close.emit();
    }
  }

  onCloseClick(): void {
    this.close.emit();
  }
}
