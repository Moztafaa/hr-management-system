import { Component } from '@angular/core';
import { Auth } from '../../Services/auth';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-login',
  imports: [FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
})

export class Login {
  email = '';
  password = '';
  errorMessage = '';

  constructor(private authService: Auth, private router: Router) { }

  login() {
    this.authService.login({ email: this.email, password: this.password, rememberMe: true })
      .subscribe({
        next: res => {
          if (res.hasError == false && res.data) {
            this.authService.saveToken(res.data.token);
            this.authService.saveUserId(res.data.employeeId);
            this.errorMessage = '';

            // Redirect based on role
            const roles = res.data.roles || [];
            if (roles.includes('HR') || roles.includes('Manager')) {
              this.router.navigate(['/pages/dashboard']);
            } else {
              // Employee or other roles go to ESS dashboard
              this.router.navigate(['/pages/ess/dashboard']);
            }
          }
        },
        error: err => {
          if (err.status === 401) {
            console.log(err.error.errorMessage);
            this.errorMessage = err.error.errorMessage;
            alert(this.errorMessage);
          } else {
            this.errorMessage = 'Error connecting to server';
            alert('Error connecting to server');
          }
        }
      });
  }
}
