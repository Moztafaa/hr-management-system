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
          if (res.hasError == false) {
            this.authService.saveToken(res.data.token);
            this.authService.saveUserId(res.data.employeeId);
            this.errorMessage = '';
            alert('Login successful! Token saved.');
            this.layoutService.closeLogin(); // Close overlay on success
            this.router.navigate(['/pages/dashboard']); // Navigate to dashboard
          }
        },
        error: (err) => {
          console.error("FULL ERROR: ", err);
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
