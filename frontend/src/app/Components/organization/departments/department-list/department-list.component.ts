import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { DepartmentService } from '../../../../Services/department.service';
import { ToastService } from '../../../../Services/toast.service';
import {
  Department,
  DepartmentWithEmployees,
  CreateDepartmentDto,
  UpdateDepartmentDto,
  EmployeeSummary,
} from '../../../../models/department';

@Component({
  selector: 'app-department-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './department-list.component.html',
  styleUrls: ['./department-list.component.css'],
})
export class DepartmentListComponent implements OnInit {
  private departmentService = inject(DepartmentService);
  private toastService = inject(ToastService);

  departments: Department[] = [];
  isLoading = true;
  error: string | null = null;

  // Modal state
  showModal = false;
  showDetailModal = false;
  showDeleteModal = false;
  isEditing = false;
  isSaving = false;
  isLoadingDetail = false;

  // Form data
  formData: CreateDepartmentDto | UpdateDepartmentDto = {
    name: '',
    description: '',
    managerId: undefined,
  };
  selectedDepartment: Department | null = null;
  departmentDetail: DepartmentWithEmployees | null = null;
  departmentToDelete: Department | null = null;

  ngOnInit(): void {
    this.loadDepartments();
  }

  loadDepartments(): void {
    this.isLoading = true;
    this.error = null;

    this.departmentService.getAll().subscribe({
      next: (response) => {
        if (!response.hasError && response.data) {
          this.departments = response.data;
        } else {
          this.error = response.errorMessage || 'Failed to load departments';
          this.toastService.error(this.error);
        }
        this.isLoading = false;
      },
      error: (err) => {
        this.error = 'An error occurred while loading departments';
        this.toastService.error(this.error);
        this.isLoading = false;
      },
    });
  }

  openCreateModal(): void {
    this.isEditing = false;
    this.formData = {
      name: '',
      description: '',
      managerId: undefined,
    };
    this.selectedDepartment = null;
    this.showModal = true;
  }

  openEditModal(department: Department): void {
    this.isEditing = true;
    this.selectedDepartment = department;
    this.formData = {
      name: department.name,
      description: department.description || '',
      managerId: department.managerId,
    };
    this.showModal = true;
  }

  closeModal(): void {
    this.showModal = false;
    this.selectedDepartment = null;
  }

  saveDepartment(): void {
    if (!this.formData.name?.trim()) {
      this.toastService.error('Department name is required');
      return;
    }

    this.isSaving = true;

    if (this.isEditing && this.selectedDepartment) {
      this.departmentService
        .update(this.selectedDepartment.id, this.formData as UpdateDepartmentDto)
        .subscribe({
          next: (response) => {
            if (!response.hasError) {
              this.toastService.success('Department updated successfully');
              this.closeModal();
              this.loadDepartments();
            } else {
              this.toastService.error(response.errorMessage || 'Failed to update department');
            }
            this.isSaving = false;
          },
          error: (err) => {
            const errorMessage = this.extractValidationErrors(err);
            this.toastService.error(errorMessage);
            this.isSaving = false;
          },
        });
    } else {
      this.departmentService.create(this.formData as CreateDepartmentDto).subscribe({
        next: (response) => {
          if (!response.hasError) {
            this.toastService.success('Department created successfully');
            this.closeModal();
            this.loadDepartments();
          } else {
            this.toastService.error(response.errorMessage || 'Failed to create department');
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
  }

  private extractValidationErrors(err: any): string {
    // Handle ASP.NET Core validation error format
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
    // Handle other error formats
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

  viewDetail(department: Department): void {
    this.selectedDepartment = department;
    this.isLoadingDetail = true;
    this.showDetailModal = true;
    this.departmentDetail = null;

    this.departmentService.getWithEmployees(department.id).subscribe({
      next: (response) => {
        if (!response.hasError && response.data) {
          this.departmentDetail = response.data;
        } else {
          this.toastService.error(response.errorMessage || 'Failed to load department details');
        }
        this.isLoadingDetail = false;
      },
      error: () => {
        this.toastService.error('An error occurred while loading department details');
        this.isLoadingDetail = false;
      },
    });
  }

  closeDetailModal(): void {
    this.showDetailModal = false;
    this.departmentDetail = null;
    this.selectedDepartment = null;
  }

  openDeleteModal(department: Department): void {
    this.departmentToDelete = department;
    this.showDeleteModal = true;
  }

  closeDeleteModal(): void {
    this.showDeleteModal = false;
    this.departmentToDelete = null;
  }

  confirmDelete(): void {
    if (!this.departmentToDelete) return;

    this.isSaving = true;
    this.departmentService.delete(this.departmentToDelete.id).subscribe({
      next: (response) => {
        if (!response.hasError) {
          this.toastService.success('Department deleted successfully');
          this.closeDeleteModal();
          this.loadDepartments();
        } else {
          this.toastService.error(response.errorMessage || 'Failed to delete department');
        }
        this.isSaving = false;
      },
      error: () => {
        this.toastService.error('An error occurred while deleting the department');
        this.isSaving = false;
      },
    });
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

  getTotalEmployees(): number {
    return this.departments.reduce((sum, d) => sum + (d.employeeCount || 0), 0);
  }

  getAvgEmployees(): string {
    if (this.departments.length === 0) return '0';
    return (this.getTotalEmployees() / this.departments.length).toFixed(1);
  }
}
