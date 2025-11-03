import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Auth } from '../../Services/auth';
import { Router } from '@angular/router';
import { RegisterEmployee } from '../../models/RegisterEmployee ';

@Component({
  selector: 'app-register',
  imports: [FormsModule],
  templateUrl: './register.html',
  styleUrl: './register.css',
})

export class Register {
  payload: RegisterEmployee = {
    email: '',
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

  constructor(private authService: Auth, private router: Router) { }

  register() {
  this.errorMessage = '';
  this.successMessage = '';

  // تحقق من required fields
  const requiredFields = ['email', 'password', 'confirmPassword', 'firstName', 'lastName', 'dateOfBirth', 'hireDate', 'basicSalary'];
  for (const field of requiredFields) {
    const value = this.payload[field as keyof RegisterEmployee];
    if (value === null || value === undefined || value === '') {
      this.errorMessage = `${field} is required`;
      return;
    }
  }

  // تحقق من تطابق الباسورد
  if (this.payload.password !== this.payload.confirmPassword) {
    this.errorMessage = 'Passwords do not match';
    return;
  }

  // تحويل النص إلى array
  if (this.stringRoles) {
    this.payload.roles = this.stringRoles
      .split(',')
      .map(r => r.trim())
      .filter(r => r.length > 0);
  }

  // إعداد payload للإرسال
  const payloadToSend = {
    ...this.payload,
    dateOfBirth: new Date(this.payload.dateOfBirth!).toISOString(),
    hireDate: new Date(this.payload.hireDate!).toISOString(),
    efF_Start: this.payload.efF_Start ? new Date(this.payload.efF_Start).toISOString() : undefined,
    efF_End: this.payload.efF_End ? new Date(this.payload.efF_End).toISOString() : undefined,
  };

  // إرسال البيانات
  this.authService.register(payloadToSend).subscribe({
    next: (res: any) => {
      if (res.succeeded) {
        this.successMessage = 'Registration successful! You can now login.';
        this.router.navigate(['/login']);
      } else {
        this.errorMessage = res.errors
          ? Array.isArray(res.errors)
            ? res.errors.join(', ')
            : JSON.stringify(res.errors)
          : 'Registration failed';
      }
    },
    error: (err: any) => {
      const serverErrors: string[] = [];
      if (err.error && err.error.errors) {
        const errorsObj = err.error.errors;
        Object.keys(errorsObj).forEach(key => {
          if (Array.isArray(errorsObj[key])) {
            serverErrors.push(...errorsObj[key]);
          } else if (typeof errorsObj[key] === 'string') {
            serverErrors.push(errorsObj[key]);
          }
        });
      }
      this.errorMessage = serverErrors.join(', ') || 'Error connecting to server';
    },
  });
}

}