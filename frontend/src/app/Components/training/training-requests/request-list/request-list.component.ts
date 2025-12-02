import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TrainingRequestService } from '../../../../Services/training/training-request.service';
import { TrainingCourseService } from '../../../../Services/training/training-course.service';
import { ToastService } from '../../../../Services/toast.service';
import { Employee as EmployeeService } from '../../../../Services/employee';
import { Employee as EmployeeModel } from '../../../../models/employee';
import {
  TrainingRequest,
  CreateTrainingRequestDto,
  TrainingRequestStatus,
  TrainingCourse,
} from '../../../../models/training';

@Component({
  selector: 'app-request-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './request-list.component.html',
  styleUrls: ['./request-list.component.css'],
})
export class RequestListComponent implements OnInit {
  private requestService = inject(TrainingRequestService);
  private courseService = inject(TrainingCourseService);
  private employeeService = inject(EmployeeService);
  private toastService = inject(ToastService);

  requests: TrainingRequest[] = [];
  courses: TrainingCourse[] = [];
  employees: EmployeeModel[] = [];
  isLoading = true;
  isLoadingCourses = false;
  isLoadingEmployees = false;
  error: string | null = null;

  // Filter
  statusFilter: TrainingRequestStatus | null = null;

  // Modal state
  showCreateModal = false;
  showReviewModal = false;
  showDeleteModal = false;
  isSaving = false;

  // Form data for new request
  createFormData: CreateTrainingRequestDto = {
    employeeId: 0,
    trainingCourseId: 0,
    employeeNote: '',
  };

  // Review modal state
  reviewRequest: TrainingRequest | null = null;
  reviewApproved = true;
  reviewNote = '';

  // Delete modal state
  requestToDelete: TrainingRequest | null = null;

  // Current user info
  currentEmployeeId: number | null = null;

  // Expose enum to template
  TrainingRequestStatus = TrainingRequestStatus;

  ngOnInit(): void {
    this.loadCurrentUser();
    this.loadCourses();
    this.loadEmployees();
    this.loadRequests();
  }

  loadCurrentUser(): void {
    // Get employee ID from localStorage (stored during login)
    const userId = localStorage.getItem('userId');
    if (userId) {
      this.currentEmployeeId = parseInt(userId, 10);
    }
  }

  loadRequests(): void {
    this.isLoading = true;
    this.error = null;

    const request$ =
      this.statusFilter !== null
        ? this.requestService.getByStatus(this.statusFilter)
        : this.requestService.getAll();

    request$.subscribe({
      next: (response) => {
        if (!response.hasError && response.data) {
          this.requests = response.data;
        } else {
          this.error = response.errorMessage || 'Failed to load training requests';
          this.toastService.error(this.error);
        }
        this.isLoading = false;
      },
      error: () => {
        this.error = 'An error occurred while loading training requests';
        this.toastService.error(this.error);
        this.isLoading = false;
      },
    });
  }

  loadCourses(): void {
    this.isLoadingCourses = true;
    this.courseService.getAll().subscribe({
      next: (response) => {
        if (!response.hasError && response.data) {
          this.courses = response.data;
        }
        this.isLoadingCourses = false;
      },
      error: () => {
        this.isLoadingCourses = false;
      },
    });
  }

  loadEmployees(): void {
    this.isLoadingEmployees = true;
    this.employeeService.getEmployees().subscribe({
      next: (response) => {
        if (!response.hasError && response.data) {
          this.employees = response.data;
        }
        this.isLoadingEmployees = false;
      },
      error: () => {
        this.isLoadingEmployees = false;
      },
    });
  }

  onStatusFilterChange(): void {
    this.loadRequests();
  }

  clearFilter(): void {
    this.statusFilter = null;
    this.loadRequests();
  }

  openCreateModal(): void {
    this.createFormData = {
      employeeId: this.currentEmployeeId || 0,
      trainingCourseId: 0,
      employeeNote: '',
    };
    this.showCreateModal = true;
  }

  closeCreateModal(): void {
    this.showCreateModal = false;
  }

  submitRequest(): void {
    if (!this.createFormData.employeeId || !this.createFormData.trainingCourseId) {
      this.toastService.error('Please select both employee and course');
      return;
    }

    this.isSaving = true;
    this.requestService.create(this.createFormData).subscribe({
      next: (response) => {
        if (!response.hasError) {
          this.toastService.success('Training request submitted successfully');
          this.closeCreateModal();
          this.loadRequests();
        } else {
          this.toastService.error(response.errorMessage || 'Failed to submit request');
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

  openReviewModal(request: TrainingRequest): void {
    this.reviewRequest = request;
    this.reviewApproved = true;
    this.reviewNote = '';
    this.showReviewModal = true;
  }

  closeReviewModal(): void {
    this.showReviewModal = false;
    this.reviewRequest = null;
    this.reviewNote = '';
  }

  submitReview(): void {
    if (!this.reviewRequest || !this.currentEmployeeId) {
      this.toastService.error('Cannot process review');
      return;
    }

    this.isSaving = true;
    this.requestService
      .review(
        this.reviewRequest.id,
        this.currentEmployeeId,
        this.reviewApproved,
        this.reviewNote || undefined
      )
      .subscribe({
        next: (response) => {
          if (!response.hasError) {
            const action = this.reviewApproved ? 'approved' : 'rejected';
            this.toastService.success(`Training request ${action} successfully`);
            this.closeReviewModal();
            this.loadRequests();
          } else {
            this.toastService.error(response.errorMessage || 'Failed to process review');
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

  openDeleteModal(request: TrainingRequest): void {
    this.requestToDelete = request;
    this.showDeleteModal = true;
  }

  closeDeleteModal(): void {
    this.showDeleteModal = false;
    this.requestToDelete = null;
  }

  confirmDelete(): void {
    if (!this.requestToDelete) return;

    this.isSaving = true;
    this.requestService.delete(this.requestToDelete.id).subscribe({
      next: (response) => {
        if (!response.hasError) {
          this.toastService.success('Training request deleted successfully');
          this.closeDeleteModal();
          this.loadRequests();
        } else {
          this.toastService.error(response.errorMessage || 'Failed to delete request');
        }
        this.isSaving = false;
      },
      error: () => {
        this.toastService.error('An error occurred while deleting the request');
        this.isSaving = false;
      },
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

  getStatusBadgeClass(status: TrainingRequestStatus): string {
    switch (status) {
      case TrainingRequestStatus.Pending:
        return 'bg-warning bg-opacity-10 text-warning';
      case TrainingRequestStatus.Approved:
        return 'bg-success bg-opacity-10 text-success';
      case TrainingRequestStatus.Rejected:
        return 'bg-danger bg-opacity-10 text-danger';
      default:
        return 'bg-light text-dark';
    }
  }

  getStatusIcon(status: TrainingRequestStatus): string {
    switch (status) {
      case TrainingRequestStatus.Pending:
        return 'eva-clock-outline';
      case TrainingRequestStatus.Approved:
        return 'eva-checkmark-circle-2-outline';
      case TrainingRequestStatus.Rejected:
        return 'eva-close-circle-outline';
      default:
        return 'eva-question-mark-circle-outline';
    }
  }

  getStatusText(status: TrainingRequestStatus): string {
    switch (status) {
      case TrainingRequestStatus.Pending:
        return 'Pending';
      case TrainingRequestStatus.Approved:
        return 'Approved';
      case TrainingRequestStatus.Rejected:
        return 'Rejected';
      default:
        return 'Unknown';
    }
  }

  formatDate(dateString?: string): string {
    if (!dateString) return 'N/A';
    return new Date(dateString).toLocaleDateString('en-US', {
      year: 'numeric',
      month: 'short',
      day: 'numeric',
    });
  }

  getEmployeeFullName(employee: EmployeeModel): string {
    return `${employee.firstName} ${employee.lastName}`;
  }

  getPendingCount(): number {
    return this.requests.filter((r) => r.status === TrainingRequestStatus.Pending).length;
  }

  getApprovedCount(): number {
    return this.requests.filter((r) => r.status === TrainingRequestStatus.Approved).length;
  }

  getRejectedCount(): number {
    return this.requests.filter((r) => r.status === TrainingRequestStatus.Rejected).length;
  }
}
