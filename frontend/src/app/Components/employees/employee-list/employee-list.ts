import { Component, OnInit, inject } from '@angular/core';
import { EmployeeService } from '../../../Services/employee';
import {
  Employee as EmployeeModel,
  UpdateEmployeeDto,
  RegisterEmployeeDto,
} from '../../../models/employee';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { DepartmentService } from '../../../Services/department.service';
import { DesignationService } from '../../../Services/designation.service';
import { Department } from '../../../models/department';
import { Designation } from '../../../models/designation';
import { ToastService } from '../../../Services/toast.service';
import { ChatService } from '../../../Services/chat.service';

@Component({
  selector: 'app-employee-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './employee-list.html',
  styleUrls: ['./employee-list.css'],
})
export class EmployeeList implements OnInit {
  private employeeService = inject(EmployeeService);
  private departmentService = inject(DepartmentService);
  private designationService = inject(DesignationService);
  private toastService = inject(ToastService);
  private chatService = inject(ChatService);

  employees: EmployeeModel[] = [];
  departments: Department[] = [];
  designations: Designation[] = [];
  isLoading = true;
  error: string | null = null;

  // Search and filter
  searchTerm = '';
  filterDepartment: number | null = null;
  filterDesignation: number | null = null;

  // Modal state
  showModal = false;
  showDetailModal = false;
  showDeleteModal = false;
  showCredentialsModal = false;
  isEditing = false;
  isSaving = false;
  isSyncing = false; // For sync button state

  // Form data for creating new employee
  createFormData: RegisterEmployeeDto = this.getEmptyCreateFormData();
  // Form data for editing employee
  editFormData: UpdateEmployeeDto = this.getEmptyEditFormData();

  selectedEmployee: EmployeeModel | null = null;
  employeeToDelete: EmployeeModel | null = null;

  // Store created employee credentials to show to HR
  createdEmployeeCredentials: { email: string; password: string; fullName: string } | null = null;

  // Selected role for new employee
  selectedRole: string = 'Employee';

  ngOnInit() {
    this.loadEmployees();
    this.loadDepartments();
    this.loadDesignations();
  }

  private getEmptyCreateFormData(): RegisterEmployeeDto {
    return {
      firstName: '',
      lastName: '',
      email: '',
      password: '',
      confirmPassword: '',
      deptId: 0,
      designationId: 0,
      dateOfBirth: '',
      gender: '',
      hireDate: new Date().toISOString().split('T')[0],
      contactNumber: '',
      address: '',
      basicSalary: 0,
      roles: ['Employee'],
    };
  }

  private getEmptyEditFormData(): UpdateEmployeeDto {
    return {
      firstName: '',
      lastName: '',
      email: '',
      deptId: 0,
      designationId: 0,
      dateOfBirth: '',
      gender: '',
      hireDate: new Date().toISOString().split('T')[0],
      contactNumber: '',
      address: '',
      basicSalary: 0,
    };
  }

  loadEmployees() {
    this.isLoading = true;
    this.error = null;
    this.employeeService.getEmployees().subscribe({
      next: (response) => {
        if (!response.hasError && response.data) {
          this.employees = response.data;
        } else {
          this.error = response.errorMessage || 'Failed to load employees';
        }
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Error loading employees', err);
        this.error = 'An error occurred while loading employees';
        this.isLoading = false;
      },
    });
  }

  loadDepartments() {
    this.departmentService.getAll().subscribe({
      next: (response) => {
        if (!response.hasError && response.data) {
          this.departments = response.data;
        }
      },
      error: () => { },
    });
  }

  loadDesignations() {
    this.designationService.getAll().subscribe({
      next: (response) => {
        if (!response.hasError && response.data) {
          this.designations = response.data;
        }
      },
      error: () => { },
    });
  }

  // Get filtered employees based on search and filters
  get filteredEmployees(): EmployeeModel[] {
    return this.employees.filter((emp) => {
      const matchesSearch =
        !this.searchTerm ||
        `${emp.firstName} ${emp.lastName}`.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
        emp.email.toLowerCase().includes(this.searchTerm.toLowerCase());

      const matchesDepartment = !this.filterDepartment || emp.deptId === this.filterDepartment;
      const matchesDesignation =
        !this.filterDesignation || emp.designationId === this.filterDesignation;

      return matchesSearch && matchesDepartment && matchesDesignation;
    });
  }

  // Modal methods
  openCreateModal(): void {
    this.isEditing = false;
    this.createFormData = this.getEmptyCreateFormData();
    this.selectedRole = 'Employee'; // Reset role to default
    this.selectedEmployee = null;
    this.showModal = true;
  }

  openEditModal(employee: EmployeeModel): void {
    this.isEditing = true;
    this.selectedEmployee = employee;
    this.editFormData = {
      firstName: employee.firstName,
      lastName: employee.lastName,
      email: employee.email,
      deptId: employee.deptId,
      designationId: employee.designationId,
      dateOfBirth: employee.dateOfBirth ? employee.dateOfBirth.split('T')[0] : '',
      gender: employee.gender || '',
      hireDate: employee.hireDate ? employee.hireDate.split('T')[0] : '',
      eFF_Start: employee.eFF_Start ? employee.eFF_Start.split('T')[0] : '',
      eFF_End: employee.eFF_End ? employee.eFF_End.split('T')[0] : '',
      contactNumber: employee.contactNumber || '',
      address: employee.address || '',
      basicSalary: employee.basicSalary,
      applicationUserId: employee.applicationUserId,
    };
    this.showModal = true;
  }

  closeModal(): void {
    this.showModal = false;
    this.selectedEmployee = null;
  }

  // Generate a random password
  generatePassword(): string {
    const length = 12;
    const uppercase = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ';
    const lowercase = 'abcdefghijklmnopqrstuvwxyz';
    const numbers = '0123456789';
    const special = '!@#$%^&*';
    const all = uppercase + lowercase + numbers + special;

    let password = '';
    // Ensure at least one of each type
    password += uppercase[Math.floor(Math.random() * uppercase.length)];
    password += lowercase[Math.floor(Math.random() * lowercase.length)];
    password += numbers[Math.floor(Math.random() * numbers.length)];
    password += special[Math.floor(Math.random() * special.length)];

    // Fill the rest randomly
    for (let i = password.length; i < length; i++) {
      password += all[Math.floor(Math.random() * all.length)];
    }

    // Shuffle the password
    return password
      .split('')
      .sort(() => Math.random() - 0.5)
      .join('');
  }

  onGeneratePassword(): void {
    const password = this.generatePassword();
    this.createFormData.password = password;
    this.createFormData.confirmPassword = password;
  }

  saveEmployee(): void {
    if (this.isEditing) {
      this.updateEmployee();
    } else {
      this.createEmployee();
    }
  }

  private createEmployee(): void {
    if (!this.createFormData.firstName?.trim() || !this.createFormData.lastName?.trim()) {
      this.toastService.error('First name and last name are required');
      return;
    }

    if (!this.createFormData.email?.trim()) {
      this.toastService.error('Email is required');
      return;
    }

    if (!this.createFormData.password?.trim()) {
      this.toastService.error('Password is required');
      return;
    }

    if (this.createFormData.password !== this.createFormData.confirmPassword) {
      this.toastService.error('Passwords do not match');
      return;
    }

    if (this.createFormData.password.length < 6) {
      this.toastService.error('Password must be at least 6 characters');
      return;
    }

    if (!this.createFormData.deptId) {
      this.toastService.error('Department is required');
      return;
    }

    if (!this.createFormData.designationId) {
      this.toastService.error('Designation is required');
      return;
    }

    this.isSaving = true;

    // Store password before sending (we'll show it to HR after successful creation)
    const passwordToShow = this.createFormData.password;

    // Set the selected role
    this.createFormData.roles = [this.selectedRole];

    this.employeeService.registerEmployee(this.createFormData).subscribe({
      next: (response) => {
        if (!response.hasError && response.data) {
          this.toastService.success('Employee account created successfully');
          this.closeModal();
          this.loadEmployees();

          // Show credentials modal to HR
          this.createdEmployeeCredentials = {
            email: this.createFormData.email,
            password: passwordToShow,
            fullName: `${this.createFormData.firstName} ${this.createFormData.lastName}`,
          };
          this.showCredentialsModal = true;
        } else {
          this.toastService.error(response.errorMessage || 'Failed to create employee account');
        }
        this.isSaving = false;
      },
      error: (err) => {
        const errorMessage = this.extractValidationErrors(err);
        this.toastService.error(errorMessage);
        this.isSaving = false;
      },
    });
  }

  private updateEmployee(): void {
    if (!this.selectedEmployee) return;

    if (!this.editFormData.firstName?.trim() || !this.editFormData.lastName?.trim()) {
      this.toastService.error('First name and last name are required');
      return;
    }

    if (!this.editFormData.email?.trim()) {
      this.toastService.error('Email is required');
      return;
    }

    if (!this.editFormData.deptId) {
      this.toastService.error('Department is required');
      return;
    }

    if (!this.editFormData.designationId) {
      this.toastService.error('Designation is required');
      return;
    }

    this.isSaving = true;

    this.employeeService.updateEmployee(this.selectedEmployee.id, this.editFormData).subscribe({
      next: (response) => {
        if (!response.hasError) {
          this.toastService.success('Employee updated successfully');
          this.closeModal();
          this.loadEmployees();
        } else {
          this.toastService.error(response.errorMessage || 'Failed to update employee');
        }
        this.isSaving = false;
      },
      error: (err) => {
        const errorMessage = this.extractValidationErrors(err);
        this.toastService.error(errorMessage);
        this.isSaving = false;
      },
    });
  }

  closeCredentialsModal(): void {
    this.showCredentialsModal = false;
    this.createdEmployeeCredentials = null;
  }

  copyToClipboard(text: string, label?: string): void {
    navigator.clipboard
      .writeText(text)
      .then(() => {
        this.toastService.success(label ? `${label} copied to clipboard` : 'Copied to clipboard');
      })
      .catch(() => {
        this.toastService.error('Failed to copy to clipboard');
      });
  }

  private extractValidationErrors(err: any): string {
    if (err?.error?.errors) {
      const errors = err.error.errors;
      const messages: string[] = [];
      for (const field in errors) {
        if (Array.isArray(errors[field])) {
          messages.push(...errors[field]);
        }
      }
      return messages.join('. ') || 'Validation error occurred';
    }
    if (err?.error?.errorMessage) {
      return err.error.errorMessage;
    }
    if (err?.error?.title) {
      return err.error.title;
    }
    if (err?.message) {
      return err.message;
    }
    return 'An error occurred while processing the request';
  }

  // Detail modal
  viewDetail(employee: EmployeeModel): void {
    this.selectedEmployee = employee;
    this.showDetailModal = true;
  }

  closeDetailModal(): void {
    this.showDetailModal = false;
    this.selectedEmployee = null;
  }

  // Delete modal
  openDeleteModal(employee: EmployeeModel): void {
    this.employeeToDelete = employee;
    this.showDeleteModal = true;
  }

  closeDeleteModal(): void {
    this.showDeleteModal = false;
    this.employeeToDelete = null;
  }

  confirmDelete(): void {
    if (!this.employeeToDelete) return;

    this.isSaving = true;
    this.employeeService.deleteEmployee(this.employeeToDelete.id).subscribe({
      next: (response) => {
        if (!response.hasError) {
          this.toastService.success('Employee deleted successfully');
          this.closeDeleteModal();
          this.loadEmployees();
        } else {
          this.toastService.error(response.errorMessage || 'Failed to delete employee');
        }
        this.isSaving = false;
      },
      error: () => {
        this.toastService.error('An error occurred while deleting the employee');
        this.isSaving = false;
      },
    });
  }

  // Utility methods
  getDepartmentName(deptId: number): string {
    const dept = this.departments.find((d) => d.id === deptId);
    return dept ? dept.name : '-';
  }

  getDesignationName(designationId: number): string {
    const designation = this.designations.find((d) => d.id === designationId);
    return designation ? designation.title : '-';
  }

  formatDate(dateString?: string): string {
    if (!dateString) return 'N/A';
    return new Date(dateString).toLocaleDateString('en-US', {
      year: 'numeric',
      month: 'short',
      day: 'numeric',
    });
  }

  getInitials(firstName: string, lastName: string): string {
    return `${firstName.charAt(0)}${lastName.charAt(0)}`.toUpperCase();
  }

  formatCurrency(amount?: number): string {
    if (amount === undefined || amount === null) return 'N/A';
    return new Intl.NumberFormat('en-US', {
      style: 'currency',
      currency: 'USD',
    }).format(amount);
  }

  clearFilters(): void {
    this.searchTerm = '';
    this.filterDepartment = null;
    this.filterDesignation = null;
  }

  // Summary stats
  getTotalSalary(): number {
    return this.employees.reduce((sum, emp) => sum + (emp.basicSalary || 0), 0);
  }

  getAverageSalary(): string {
    if (this.employees.length === 0) return '$0.00';
    const avg = this.getTotalSalary() / this.employees.length;
    return this.formatCurrency(avg);
  }

  getEmployeesThisMonth(): number {
    const now = new Date();
    const firstDayOfMonth = new Date(now.getFullYear(), now.getMonth(), 1);
    return this.employees.filter((emp) => {
      if (!emp.hireDate) return false;
      const hireDate = new Date(emp.hireDate);
      return hireDate >= firstDayOfMonth;
    }).length;
  }

  // Sync employees to MongoDB for chatbot
  syncEmployeesToChatbot(): void {
    this.isSyncing = true;
    this.chatService.syncEmployeesToMongoDB().subscribe({
      next: (response) => {
        this.isSyncing = false;
        if (!response.hasError) {
          this.toastService.success('Employee data synced to chatbot successfully!');
        } else {
          this.toastService.error(response.errorMessage || 'Failed to sync employee data');
        }
      },
      error: (err) => {
        this.isSyncing = false;
        console.error('Sync error:', err);
        this.toastService.error('An error occurred while syncing employee data');
      },
    });
  }
}
