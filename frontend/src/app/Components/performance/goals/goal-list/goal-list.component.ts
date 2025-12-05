import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { GoalService } from '../../../../Services/performance/goal.service';
import { PerformanceReviewService } from '../../../../Services/performance/performance-review.service';
import { EmployeeService } from '../../../../Services/employee';
import { ToastService } from '../../../../Services/toast.service';
import {
  Goal,
  CreateGoalDto,
  UpdateGoalProgressDto,
  PerformanceReview,
  GoalStatus,
  getGoalStatusLabel,
  getGoalStatusClass,
} from '../../../../models/performance';
import { Employee } from '../../../../models/employee';

@Component({
  selector: 'app-goal-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './goal-list.component.html',
  styleUrls: ['./goal-list.component.css'],
})
export class GoalListComponent implements OnInit {
  private goalService = inject(GoalService);
  private reviewService = inject(PerformanceReviewService);
  private employeeService = inject(EmployeeService);
  private toastService = inject(ToastService);

  goals: Goal[] = [];
  reviews: PerformanceReview[] = [];
  employees: Employee[] = [];
  isLoading = true;
  error: string | null = null;

  // Modal state
  showModal = false;
  showProgressModal = false;
  isSaving = false;

  // Form data
  selectedEmployeeId: number | null = null;
  selectedReviewId: number | null = null;
  formData: CreateGoalDto = {
    performanceReviewId: 0,
    title: '',
    description: '',
    dueDate: '',
  };

  // Progress form
  selectedGoal: Goal | null = null;
  progressFormData: UpdateGoalProgressDto = {
    goalId: 0,
    progressPercent: 0,
    status: '',
  };

  // Enum references
  GoalStatus = GoalStatus;

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
      this.goals = [];
      this.isLoading = false;
      return;
    }

    this.reviewService.getByEmployee(this.selectedEmployeeId).subscribe({
      next: (response) => {
        if (!response.hasError && response.data) {
          this.reviews = response.data;
          if (this.reviews.length > 0) {
            this.selectedReviewId = this.reviews[0].id;
            this.loadGoals();
          } else {
            this.goals = [];
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

  loadGoals(): void {
    if (!this.selectedReviewId) {
      this.goals = [];
      this.isLoading = false;
      return;
    }

    this.isLoading = true;
    this.error = null;

    // Get goals from the review report
    this.reviewService.getReport(this.selectedReviewId).subscribe({
      next: (response) => {
        if (!response.hasError && response.data) {
          this.goals = response.data.goals;
        } else {
          this.error = response.errorMessage || 'Failed to load goals';
          this.toastService.error(this.error);
        }
        this.isLoading = false;
      },
      error: () => {
        this.error = 'An error occurred while loading goals';
        this.toastService.error(this.error);
        this.isLoading = false;
      },
    });
  }

  onEmployeeChange(): void {
    this.selectedReviewId = null;
    this.goals = [];
    this.loadReviews();
  }

  onReviewChange(): void {
    this.loadGoals();
  }

  openCreateModal(): void {
    this.formData = {
      performanceReviewId: this.selectedReviewId || 0,
      title: '',
      description: '',
      dueDate: '',
    };
    this.showModal = true;
  }

  closeModal(): void {
    this.showModal = false;
  }

  saveGoal(): void {
    if (!this.formData.title?.trim()) {
      this.toastService.error('Goal title is required');
      return;
    }
    if (!this.formData.performanceReviewId) {
      this.toastService.error('Please select a review');
      return;
    }

    this.isSaving = true;

    this.goalService.create(this.formData).subscribe({
      next: (response) => {
        if (!response.hasError) {
          this.toastService.success('Goal created successfully');
          this.closeModal();
          this.loadGoals();
        } else {
          this.toastService.error(response.errorMessage || 'Failed to create goal');
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

  openProgressModal(goal: Goal): void {
    this.selectedGoal = goal;
    this.progressFormData = {
      goalId: goal.id,
      progressPercent: goal.progressPercent,
      status: this.getStatusString(goal.status),
    };
    this.showProgressModal = true;
  }

  closeProgressModal(): void {
    this.showProgressModal = false;
    this.selectedGoal = null;
  }

  updateProgress(): void {
    if (this.progressFormData.progressPercent < 0 || this.progressFormData.progressPercent > 100) {
      this.toastService.error('Progress must be between 0 and 100');
      return;
    }

    this.isSaving = true;

    this.goalService.updateProgress(this.progressFormData).subscribe({
      next: (response) => {
        if (!response.hasError) {
          this.toastService.success('Goal progress updated successfully');
          this.closeProgressModal();
          this.loadGoals();
        } else {
          this.toastService.error(response.errorMessage || 'Failed to update progress');
        }
        this.isSaving = false;
      },
      error: () => {
        this.toastService.error('Failed to update progress');
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

  getStatusLabel(status: GoalStatus): string {
    return getGoalStatusLabel(status);
  }

  getStatusClass(status: GoalStatus): string {
    return getGoalStatusClass(status);
  }

  getStatusString(status: GoalStatus): string {
    switch (status) {
      case GoalStatus.NotStarted:
        return 'NotStarted';
      case GoalStatus.OnTrack:
        return 'OnTrack';
      case GoalStatus.AtRisk:
        return 'AtRisk';
      case GoalStatus.Completed:
        return 'Completed';
      default:
        return 'NotStarted';
    }
  }

  formatDate(dateStr: string): string {
    if (!dateStr) return 'No due date';
    const date = new Date(dateStr);
    return date.toLocaleDateString('en-US', {
      year: 'numeric',
      month: 'short',
      day: 'numeric',
    });
  }

  getProgressColor(percent: number): string {
    if (percent >= 80) return 'bg-success';
    if (percent >= 50) return 'bg-info';
    if (percent >= 25) return 'bg-warning';
    return 'bg-danger';
  }

  getCompletedCount(): number {
    return this.goals.filter((g) => g.status === GoalStatus.Completed).length;
  }

  getOnTrackCount(): number {
    return this.goals.filter((g) => g.status === GoalStatus.OnTrack).length;
  }

  getAtRiskCount(): number {
    return this.goals.filter((g) => g.status === GoalStatus.AtRisk).length;
  }
}
