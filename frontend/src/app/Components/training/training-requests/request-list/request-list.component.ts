import { Component, OnInit, AfterViewInit, OnDestroy, inject } from '@angular/core';
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

declare const bootstrap: any;

@Component({
  selector: 'app-request-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './request-list.component.html',
  styleUrls: ['./request-list.component.css'],
})
export class RequestListComponent implements OnInit, AfterViewInit, OnDestroy {
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

  // Filter - use string or number
  statusFilter: string | TrainingRequestStatus | null = null;

  // Modal state
  showReviewModal = false;
  isSaving = false;

  // Review modal state
  reviewRequest: TrainingRequest | null = null;
  reviewApproved = true;
  reviewAction: string = 'Review';
  reviewNote = '';

  // Current user info
  currentEmployeeId: number | null = null;

  // Tooltip instances
  private tooltipInstances: any[] = [];

  // Expose enum to template
  TrainingRequestStatus = TrainingRequestStatus;

  ngOnInit(): void {
    this.loadCurrentUser();
    this.loadCourses();
    this.loadEmployees();
    this.loadRequests();
  }

  ngAfterViewInit(): void {
    // Initialize Bootstrap tooltips after view is ready
    this.initializeTooltips();
  }

  ngOnDestroy(): void {
    // Clean up tooltips to prevent memory leaks
    this.destroyTooltips();
  }

  private initializeTooltips(): void {
    // Clean up existing tooltips first
    this.destroyTooltips();

    // Initialize tooltips for all elements with data-bs-toggle="tooltip"
    const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]');

    // Store tooltip instances for later cleanup
    this.tooltipInstances = Array.from(tooltipTriggerList).map(
      tooltipTriggerEl => {
        // Check if tooltip already exists
        if ((tooltipTriggerEl as any)._tooltip) {
          return (tooltipTriggerEl as any)._tooltip;
        }
        return new bootstrap.Tooltip(tooltipTriggerEl);
      }
    );
  }

  private destroyTooltips(): void {
    // Dispose of all tooltip instances
    this.tooltipInstances.forEach(tooltip => {
      if (tooltip && typeof tooltip.dispose === 'function') {
        tooltip.dispose();
      }
    });
    this.tooltipInstances = [];
  }

  loadCurrentUser(): void {
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
        ? this.requestService.getByStatus(this.statusFilter as TrainingRequestStatus)
        : this.requestService.getAll();

    request$.subscribe({
      next: (response) => {
        if (!response.hasError && response.data) {
          this.requests = response.data;
          // Reinitialize tooltips after data loads
          setTimeout(() => this.initializeTooltips(), 100);
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

  openReviewModal(request: TrainingRequest, approved: boolean): void {
    this.reviewRequest = request;
    this.reviewApproved = approved;
    this.reviewAction = approved ? 'Approve' : 'Reject';
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

  // Helper method to convert string status to enum
  private getStatusEnum(status: string | TrainingRequestStatus): TrainingRequestStatus {
    if (typeof status === 'number') {
      return status;
    }

    switch (status?.toLowerCase()) {
      case 'pending':
      case '0':
        return TrainingRequestStatus.Pending;
      case 'approved':
      case '1':
        return TrainingRequestStatus.Approved;
      case 'rejected':
      case '2':
        return TrainingRequestStatus.Rejected;
      default:
        return TrainingRequestStatus.Pending;
    }
  }

  getStatusBadgeClass(status: string | TrainingRequestStatus): string {
    const statusEnum = this.getStatusEnum(status);

    switch (statusEnum) {
      case TrainingRequestStatus.Pending:
        return 'status-pending'; // Updated
      case TrainingRequestStatus.Approved:
        return 'status-approved'; // Updated
      case TrainingRequestStatus.Rejected:
        return 'status-rejected'; // Updated
      default:
        return 'status-pending'; // Updated
    }
  }

  getStatusIcon(status: string | TrainingRequestStatus): string {
    const statusEnum = this.getStatusEnum(status);

    switch (statusEnum) {
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

  getStatusText(status: string | TrainingRequestStatus): string {
    const statusEnum = this.getStatusEnum(status);

    switch (statusEnum) {
      case TrainingRequestStatus.Pending:
        return 'Pending';
      case TrainingRequestStatus.Approved:
        return 'Approved';
      case TrainingRequestStatus.Rejected:
        return 'Rejected';
      default:
        return 'Pending';
    }
  }

  // Check if status is pending (for template comparisons)
  isPending(status: string | TrainingRequestStatus): boolean {
    return this.getStatusEnum(status) === TrainingRequestStatus.Pending;
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
    return this.requests.filter((r) =>
      this.getStatusEnum(r.status) === TrainingRequestStatus.Pending
    ).length;
  }

  getApprovedCount(): number {
    return this.requests.filter((r) =>
      this.getStatusEnum(r.status) === TrainingRequestStatus.Approved
    ).length;
  }

  getRejectedCount(): number {
    return this.requests.filter((r) =>
      this.getStatusEnum(r.status) === TrainingRequestStatus.Rejected
    ).length;
  }
}