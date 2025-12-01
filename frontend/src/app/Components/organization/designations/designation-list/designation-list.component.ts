import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { DesignationService } from '../../../../Services/designation.service';
import { ToastService } from '../../../../Services/toast.service';
import {
  Designation,
  DesignationWithEmployees,
  CreateDesignationDto,
  UpdateDesignationDto,
} from '../../../../models/designation';
import { EmployeeSummary } from '../../../../models/department';

@Component({
  selector: 'app-designation-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './designation-list.component.html',
  styleUrls: ['./designation-list.component.css'],
})
export class DesignationListComponent implements OnInit {
  private designationService = inject(DesignationService);
  private toastService = inject(ToastService);

  designations: Designation[] = [];
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
  formData: CreateDesignationDto | UpdateDesignationDto = {
    title: '',
    description: '',
  };
  selectedDesignation: Designation | null = null;
  designationDetail: DesignationWithEmployees | null = null;
  designationToDelete: Designation | null = null;

  ngOnInit(): void {
    this.loadDesignations();
  }

  loadDesignations(): void {
    this.isLoading = true;
    this.error = null;

    this.designationService.getAll().subscribe({
      next: (response) => {
        if (!response.hasError && response.data) {
          this.designations = response.data;
        } else {
          this.error = response.errorMessage || 'Failed to load designations';
          this.toastService.error(this.error);
        }
        this.isLoading = false;
      },
      error: (err) => {
        this.error = 'An error occurred while loading designations';
        this.toastService.error(this.error);
        this.isLoading = false;
      },
    });
  }

  openCreateModal(): void {
    this.isEditing = false;
    this.formData = {
      title: '',
      description: '',
    };
    this.selectedDesignation = null;
    this.showModal = true;
  }

  openEditModal(designation: Designation): void {
    this.isEditing = true;
    this.selectedDesignation = designation;
    this.formData = {
      title: designation.title,
      description: designation.description || '',
    };
    this.showModal = true;
  }

  closeModal(): void {
    this.showModal = false;
    this.selectedDesignation = null;
  }

  saveDesignation(): void {
    if (!this.formData.title?.trim()) {
      this.toastService.error('Designation title is required');
      return;
    }

    this.isSaving = true;

    if (this.isEditing && this.selectedDesignation) {
      this.designationService
        .update(this.selectedDesignation.id, this.formData as UpdateDesignationDto)
        .subscribe({
          next: (response) => {
            if (!response.hasError) {
              this.toastService.success('Designation updated successfully');
              this.closeModal();
              this.loadDesignations();
            } else {
              this.toastService.error(response.errorMessage || 'Failed to update designation');
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
      this.designationService.create(this.formData as CreateDesignationDto).subscribe({
        next: (response) => {
          if (!response.hasError) {
            this.toastService.success('Designation created successfully');
            this.closeModal();
            this.loadDesignations();
          } else {
            this.toastService.error(response.errorMessage || 'Failed to create designation');
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

  viewDetail(designation: Designation): void {
    this.selectedDesignation = designation;
    this.isLoadingDetail = true;
    this.showDetailModal = true;
    this.designationDetail = null;

    this.designationService.getWithEmployees(designation.id).subscribe({
      next: (response) => {
        if (!response.hasError && response.data) {
          this.designationDetail = response.data;
        } else {
          this.toastService.error(response.errorMessage || 'Failed to load designation details');
        }
        this.isLoadingDetail = false;
      },
      error: () => {
        this.toastService.error('An error occurred while loading designation details');
        this.isLoadingDetail = false;
      },
    });
  }

  closeDetailModal(): void {
    this.showDetailModal = false;
    this.designationDetail = null;
    this.selectedDesignation = null;
  }

  openDeleteModal(designation: Designation): void {
    this.designationToDelete = designation;
    this.showDeleteModal = true;
  }

  closeDeleteModal(): void {
    this.showDeleteModal = false;
    this.designationToDelete = null;
  }

  confirmDelete(): void {
    if (!this.designationToDelete) return;

    this.isSaving = true;
    this.designationService.delete(this.designationToDelete.id).subscribe({
      next: (response) => {
        if (!response.hasError) {
          this.toastService.success('Designation deleted successfully');
          this.closeDeleteModal();
          this.loadDesignations();
        } else {
          this.toastService.error(response.errorMessage || 'Failed to delete designation');
        }
        this.isSaving = false;
      },
      error: () => {
        this.toastService.error('An error occurred while deleting the designation');
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

  getWithDescCount(): number {
    return this.designations.filter((d) => d.description).length;
  }

  getNoDescCount(): number {
    return this.designations.filter((d) => !d.description).length;
  }
}
