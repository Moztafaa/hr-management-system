import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { PerformanceReviewService } from '../../../../Services/performance/performance-review.service';
import { ReviewCycleService } from '../../../../Services/performance/review-cycle.service';
import { EmployeeService } from '../../../../Services/employee';
import { ToastService } from '../../../../Services/toast.service';
import {
  PerformanceReview,
  CreateReviewDto,
  PerformanceReport,
  ReviewCycle,
  ReviewStatus,
  getReviewStatusLabel,
  getReviewStatusClass,
  getGoalStatusLabel,
  getGoalStatusClass,
  getFeedbackTypeLabel,
  getFeedbackTypeClass,
} from '../../../../models/performance';
import { Employee } from '../../../../models/employee';

@Component({
  selector: 'app-review-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './review-list.component.html',
  styleUrls: ['./review-list.component.css'],
})
export class ReviewListComponent implements OnInit {
  private reviewService = inject(PerformanceReviewService);
  private cycleService = inject(ReviewCycleService);
  private employeeService = inject(EmployeeService);
  private toastService = inject(ToastService);

  reviews: PerformanceReview[] = [];
  employees: Employee[] = [];
  cycles: ReviewCycle[] = [];
  isLoading = true;
  error: string | null = null;

  // Modal state
  showModal = false;
  showReportModal = false;
  showCloseModal = false;
  isSaving = false;
  isLoadingReport = false;

  // Form data
  selectedEmployeeId: number | null = null;
  formData: CreateReviewDto = {
    employeeId: 0,
    reviewCycleId: 0,
  };

  // Report data
  selectedReport: PerformanceReport | null = null;
  selectedReview: PerformanceReview | null = null;
  finalRating: number | null = null;

  // Enum references for template
  ReviewStatus = ReviewStatus;

  ngOnInit(): void {
    this.loadEmployees();
    this.loadCycles();
  }

  loadEmployees(): void {
    this.employeeService.getEmployees().subscribe({
      next: (response) => {
        if (!response.hasError && response.data) {
          this.employees = response.data;
          if (this.employees.length > 0) {
            this.selectedEmployeeId = this.employees[0].id;
            this.loadReviews();
          } else {
            this.isLoading = false;
          }
        }
      },
      error: () => {
        this.toastService.error('Failed to load employees');
        this.isLoading = false;
      },
    });
  }

  loadCycles(): void {
    this.cycleService.getAll().subscribe({
      next: (response) => {
        if (!response.hasError && response.data) {
          this.cycles = response.data;
        }
      },
      error: () => {
        this.toastService.error('Failed to load review cycles');
      },
    });
  }

  loadReviews(): void {
    if (!this.selectedEmployeeId) {
      this.reviews = [];
      this.isLoading = false;
      return;
    }

    this.isLoading = true;
    this.error = null;

    this.reviewService.getByEmployee(this.selectedEmployeeId).subscribe({
      next: (response) => {
        if (!response.hasError && response.data) {
          this.reviews = response.data;
        } else {
          this.error = response.errorMessage || 'Failed to load reviews';
          this.toastService.error(this.error);
        }
        this.isLoading = false;
      },
      error: () => {
        this.error = 'An error occurred while loading reviews';
        this.toastService.error(this.error);
        this.isLoading = false;
      },
    });
  }

  onEmployeeChange(): void {
    this.loadReviews();
  }

  openCreateModal(): void {
    this.formData = {
      employeeId: this.selectedEmployeeId || 0,
      reviewCycleId: this.cycles.length > 0 ? this.cycles[0].id : 0,
    };
    this.showModal = true;
  }

  closeModal(): void {
    this.showModal = false;
  }

  saveReview(): void {
    if (!this.formData.employeeId || !this.formData.reviewCycleId) {
      this.toastService.error('Please select an employee and a review cycle');
      return;
    }

    this.isSaving = true;

    this.reviewService.create(this.formData).subscribe({
      next: (response) => {
        if (!response.hasError) {
          this.toastService.success('Performance review created successfully');
          this.closeModal();
          this.loadReviews();
        } else {
          this.toastService.error(response.errorMessage || 'Failed to create review');
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

  openReportModal(review: PerformanceReview): void {
    this.selectedReview = review;
    this.isLoadingReport = true;
    this.showReportModal = true;

    this.reviewService.getReport(review.id).subscribe({
      next: (response) => {
        if (!response.hasError && response.data) {
          this.selectedReport = response.data;
        } else {
          this.toastService.error(response.errorMessage || 'Failed to load report');
        }
        this.isLoadingReport = false;
      },
      error: () => {
        this.toastService.error('Failed to load report');
        this.isLoadingReport = false;
      },
    });
  }

  closeReportModal(): void {
    this.showReportModal = false;
    this.selectedReport = null;
    this.selectedReview = null;
  }

  openCloseModal(review: PerformanceReview): void {
    this.selectedReview = review;
    this.finalRating = null;
    this.showCloseModal = true;
  }

  closeCloseModal(): void {
    this.showCloseModal = false;
    this.selectedReview = null;
    this.finalRating = null;
  }

  confirmCloseReview(): void {
    if (!this.selectedReview) return;

    this.isSaving = true;

    this.reviewService.close(this.selectedReview.id, this.finalRating || undefined).subscribe({
      next: (response) => {
        if (!response.hasError) {
          this.toastService.success('Review closed successfully');
          this.closeCloseModal();
          this.loadReviews();
        } else {
          this.toastService.error(response.errorMessage || 'Failed to close review');
        }
        this.isSaving = false;
      },
      error: () => {
        this.toastService.error('Failed to close review');
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

  getEmployeeName(employeeId: number): string {
    const emp = this.employees.find((e) => e.id === employeeId);
    return emp ? `${emp.firstName} ${emp.lastName}` : `Employee #${employeeId}`;
  }

  getCycleName(cycleId: number): string {
    const cycle = this.cycles.find((c) => c.id === cycleId);
    return cycle ? cycle.name : `Cycle #${cycleId}`;
  }

  getStatusLabel(status: ReviewStatus): string {
    return getReviewStatusLabel(status);
  }

  getStatusClass(status: ReviewStatus): string {
    return getReviewStatusClass(status);
  }

  getGoalStatusLabel = getGoalStatusLabel;
  getGoalStatusClass = getGoalStatusClass;
  getFeedbackTypeLabel = getFeedbackTypeLabel;
  getFeedbackTypeClass = getFeedbackTypeClass;

  formatDate(dateStr: string): string {
    if (!dateStr) return 'N/A';
    const date = new Date(dateStr);
    return date.toLocaleDateString('en-US', {
      year: 'numeric',
      month: 'short',
      day: 'numeric',
    });
  }

  canClose(review: PerformanceReview): boolean {
    return review.status !== ReviewStatus.Closed;
  }
}
