import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { FeedbackService } from '../../../../Services/performance/feedback.service';
import { PerformanceReviewService } from '../../../../Services/performance/performance-review.service';
import { EmployeeService } from '../../../../Services/employee';
import { ToastService } from '../../../../Services/toast.service';
import {
  Feedback,
  CreateFeedbackDto,
  FeedbackType,
  getFeedbackTypeLabel,
  PerformanceReview,
} from '../../../../models/performance';
import { Employee } from '../../../../models/employee';

@Component({
  selector: 'app-feedback-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './feedback-list.component.html',
  styleUrls: ['./feedback-list.component.css'],
})
export class FeedbackListComponent implements OnInit {
  private feedbackService = inject(FeedbackService);
  private reviewService = inject(PerformanceReviewService);
  private employeeService = inject(EmployeeService);
  private toastService = inject(ToastService);

  feedbacks: Feedback[] = [];
  reviews: PerformanceReview[] = [];
  employees: Employee[] = [];
  isLoading = true;
  error: string | null = null;

  // Modal state
  showCreateModal = false;
  isSaving = false;

  // Filter state
  selectedEmployeeId: number | null = null;
  selectedReviewId: number | null = null;
  selectedType: FeedbackType | null = null;

  // Form data
  formData: CreateFeedbackDto = {
    performanceReviewId: 0,
    fromEmployeeId: 0,
    type: FeedbackType.Self,
    comments: '',
  };

  // Feedback types for dropdown
  feedbackTypes = [
    { value: FeedbackType.Self, name: 'Self Assessment' },
    { value: FeedbackType.Manager, name: 'Manager Review' },
    { value: FeedbackType.Peer, name: 'Peer Feedback' },
  ];

  ngOnInit(): void {
    this.loadEmployees();
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

  loadReviews(): void {
    if (!this.selectedEmployeeId) {
      this.reviews = [];
      this.feedbacks = [];
      this.isLoading = false;
      return;
    }

    this.reviewService.getByEmployee(this.selectedEmployeeId).subscribe({
      next: (response) => {
        if (!response.hasError && response.data) {
          this.reviews = response.data;
          if (this.reviews.length > 0) {
            this.selectedReviewId = this.reviews[0].id;
            this.loadFeedbacks();
          } else {
            this.feedbacks = [];
            this.isLoading = false;
          }
        }
      },
      error: () => {
        this.toastService.error('Failed to load reviews');
        this.isLoading = false;
      },
    });
  }

  loadFeedbacks(): void {
    if (!this.selectedReviewId) {
      this.feedbacks = [];
      this.isLoading = false;
      return;
    }

    this.isLoading = true;
    this.error = null;

    // Get feedbacks from the review report
    this.reviewService.getReport(this.selectedReviewId).subscribe({
      next: (response) => {
        if (!response.hasError && response.data) {
          let feedbacks = response.data.feedbacks;
          // Filter by type if selected
          if (this.selectedType !== null) {
            feedbacks = feedbacks.filter((f) => f.type === this.selectedType);
          }
          this.feedbacks = feedbacks;
        } else {
          this.error = response.errorMessage || 'Failed to load feedbacks';
          this.toastService.error(this.error);
        }
        this.isLoading = false;
      },
      error: () => {
        this.error = 'An error occurred while loading feedbacks';
        this.toastService.error(this.error);
        this.isLoading = false;
      },
    });
  }

  onEmployeeChange(): void {
    this.selectedReviewId = null;
    this.feedbacks = [];
    this.loadReviews();
  }

  onReviewChange(): void {
    this.loadFeedbacks();
  }

  onTypeChange(): void {
    this.loadFeedbacks();
  }

  openCreateModal(): void {
    if (!this.selectedReviewId) {
      this.toastService.error('Please select a review first');
      return;
    }
    this.formData = {
      performanceReviewId: this.selectedReviewId,
      fromEmployeeId: this.employees[0]?.id || 0,
      type: FeedbackType.Self,
      comments: '',
    };
    this.showCreateModal = true;
  }

  closeCreateModal(): void {
    this.showCreateModal = false;
  }

  saveFeedback(): void {
    if (!this.formData.comments?.trim()) {
      this.toastService.error('Comments are required');
      return;
    }
    if (!this.formData.fromEmployeeId) {
      this.toastService.error('Please select the feedback provider');
      return;
    }

    this.isSaving = true;

    this.feedbackService.create(this.formData).subscribe({
      next: (response) => {
        if (!response.hasError) {
          this.toastService.success('Feedback submitted successfully');
          this.closeCreateModal();
          this.loadFeedbacks();
        } else {
          this.toastService.error(response.errorMessage || 'Failed to submit feedback');
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

  getFeedbackTypeName(type: FeedbackType): string {
    return getFeedbackTypeLabel(type);
  }

  getSelfCount(): number {
    return this.feedbacks.filter((f) => f.type === FeedbackType.Self).length;
  }

  getManagerCount(): number {
    return this.feedbacks.filter((f) => f.type === FeedbackType.Manager).length;
  }

  getPeerCount(): number {
    return this.feedbacks.filter((f) => f.type === FeedbackType.Peer).length;
  }

  getTypeIcon(type: FeedbackType): string {
    switch (type) {
      case FeedbackType.Self:
        return 'eva-person-outline';
      case FeedbackType.Manager:
        return 'eva-briefcase-outline';
      case FeedbackType.Peer:
        return 'eva-people-outline';
      default:
        return 'eva-message-circle-outline';
    }
  }

  getTypeBadgeClass(type: FeedbackType): string {
    switch (type) {
      case FeedbackType.Self:
        return 'bg-info';
      case FeedbackType.Manager:
        return 'bg-primary';
      case FeedbackType.Peer:
        return 'bg-success';
      default:
        return 'bg-secondary';
    }
  }

  formatDate(date: Date | string | undefined): string {
    if (!date) return 'N/A';
    return new Date(date).toLocaleDateString('en-US', {
      year: 'numeric',
      month: 'short',
      day: 'numeric',
      hour: '2-digit',
      minute: '2-digit',
    });
  }

  getEmployeeName(employeeId: number): string {
    const employee = this.employees.find((e) => e.id === employeeId);
    return employee ? `${employee.firstName} ${employee.lastName}` : `Employee #${employeeId}`;
  }
}
