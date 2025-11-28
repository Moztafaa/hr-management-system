import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../../services/auth.service';
import { LayoutService } from '../../../services/layout.service';

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
          if (res.hasError == false) {
            this.authService.saveToken(res.data.token);
            this.errorMessage = '';
            alert('Login successful! Token saved.');
            this.layoutService.closeLogin(); // Close overlay on success
            this.router.navigate(['/pages/dashboard']); // Navigate to dashboard
          }
        },
        error: (err) => {
          if (err.status === 401) {
            console.log(err.error.errorMessage);
            this.errorMessage = err.error.errorMessage;
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
    this.router.navigate(['/pages/dashboard']); // Navigate to dashboard on close
  }

  openRegister() {
    this.layoutService.openRegister();
    this.router.navigate(['/pages/register']); // Switch route
  }
}
