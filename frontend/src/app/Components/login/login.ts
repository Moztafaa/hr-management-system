import { Component } from '@angular/core';
import { Auth } from '../../Services/auth';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

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
          if (res.succeeded) {
            this.authService.saveToken(res.response.token);
            this.errorMessage = '';
            alert('Login successful! Token saved.');
          }
        },
        error: err => {
          if (err.status === 401) {
            this.errorMessage = 'Invalid email or password';
            alert('Invalid email or password');
          } else {
            this.errorMessage = 'Error connecting to server';
            alert('Error connecting to server');
          }
        }
      });
  }
}
