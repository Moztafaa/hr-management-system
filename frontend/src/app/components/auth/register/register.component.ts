import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../../services/auth.service';
import { LayoutService } from '../../../services/layout.service';
import { RegisterEmployee } from '../../../models/RegisterEmployee';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent {
  payload: RegisterEmployee = {
    email: '',
    deptId: 0,
    designationId: 0,
    password: '',
    confirmPassword: '',
    phoneNumber: '',
    firstName: '',
    lastName: '',
    dateOfBirth: '',
    gender: '',
    hireDate: '',
    efF_Start: '',
    efF_End: '',
    contactNumber: '',
    address: '',
    basicSalary: 0,
    roles: [],
  };

  stringRoles: string = '';
  errorMessage = '';
  successMessage = '';

  private authService = inject(AuthService);
  private router = inject(Router);
  private layoutService = inject(LayoutService);

  register() {
    this.errorMessage = '';
    this.successMessage = '';

    // Required fields check
    const requiredFields = [
      'email',
      'password',
      'confirmPassword',
      'firstName',
      'lastName',
      'dateOfBirth',
      'hireDate',
      'basicSalary',
    ];
    for (const field of requiredFields) {
      const value = this.payload[field as keyof RegisterEmployee];
      if (value === null || value === undefined || value === '') {
        this.errorMessage = `${field} is required`;
        return;
      }
    }

    // Password match check
    if (this.payload.password !== this.payload.confirmPassword) {
      this.errorMessage = 'Passwords do not match';
      return;
    }

    // Roles processing
    if (this.stringRoles) {
      this.payload.roles = this.stringRoles
        .split(',')
        .map((r) => r.trim())
        .filter((r) => r.length > 0);
    }

    // Prepare payload
    const payloadToSend = {
      ...this.payload,
      dateOfBirth: new Date(this.payload.dateOfBirth!).toISOString(),
      hireDate: new Date(this.payload.hireDate!).toISOString(),
      efF_Start: this.payload.efF_Start
        ? new Date(this.payload.efF_Start).toISOString()
        : undefined,
      efF_End: this.payload.efF_End ? new Date(this.payload.efF_End).toISOString() : undefined,
    };

    // Send data
    this.authService.register(payloadToSend).subscribe({
      next: (res: any) => {
        if (res.hasError == false) {
          this.successMessage = 'Registration successful! You can now login.';
          // Switch to login overlay after short delay or immediately
          setTimeout(() => {
            this.layoutService.openLogin();
            this.router.navigate(['/pages/login']); // Switch route
          }, 1500);
        }
      },
      error: (err) => {
        if (err.status === 400) {
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
    this.layoutService.closeRegister();
    this.router.navigate(['/pages/dashboard']); // Navigate to dashboard on close
  }

  openLogin() {
    this.layoutService.openLogin();
    this.router.navigate(['/pages/login']); // Switch route
  }
}
