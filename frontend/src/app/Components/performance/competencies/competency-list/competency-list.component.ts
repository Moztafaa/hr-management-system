import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CompetencyService } from '../../../../Services/performance/competency.service';
import { PerformanceReviewService } from '../../../../Services/performance/performance-review.service';
import { EmployeeService } from '../../../../Services/employee';
import { ToastService } from '../../../../Services/toast.service';
import {
  Competency,
  CreateCompetencyDto,
  RateCompetencyDto,
  EmployeeCompetencyRating,
  PerformanceReview,
} from '../../../../models/performance';
import { Employee } from '../../../../models/employee';

@Component({
  selector: 'app-competency-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './competency-list.component.html',
  styleUrls: ['./competency-list.component.css'],
})
export class CompetencyListComponent implements OnInit {
  private competencyService = inject(CompetencyService);
  private reviewService = inject(PerformanceReviewService);
  private employeeService = inject(EmployeeService);
  private toastService = inject(ToastService);

  competencyRatings: EmployeeCompetencyRating[] = [];
  reviews: PerformanceReview[] = [];
  employees: Employee[] = [];
  isLoading = true;
  error: string | null = null;

  // Modal state
  showCompetencyModal = false;
  showRateModal = false;
  isSaving = false;

  // Form data
  selectedEmployeeId: number | null = null;
  selectedReviewId: number | null = null;

  competencyFormData: CreateCompetencyDto = {
    name: '',
    description: '',
  };

  rateFormData: RateCompetencyDto = {
    performanceReviewId: 0,
    competencyId: 0,
    rating: 3,
    notes: '',
  };

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
      this.competencyRatings = [];
      this.isLoading = false;
      return;
    }

    this.reviewService.getByEmployee(this.selectedEmployeeId).subscribe({
      next: (response) => {
        if (!response.hasError && response.data) {
          this.reviews = response.data;
          if (this.reviews.length > 0) {
            this.selectedReviewId = this.reviews[0].id;
            this.loadCompetencyRatings();
          } else {
            this.competencyRatings = [];
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

  loadCompetencyRatings(): void {
    if (!this.selectedReviewId) {
      this.competencyRatings = [];
      this.isLoading = false;
      return;
    }

    this.isLoading = true;
    this.error = null;

    this.reviewService.getReport(this.selectedReviewId).subscribe({
      next: (response) => {
        if (!response.hasError && response.data) {
          this.competencyRatings = response.data.competencyRatings;
        } else {
          this.error = response.errorMessage || 'Failed to load competency ratings';
          this.toastService.error(this.error);
        }
        this.isLoading = false;
      },
      error: () => {
        this.error = 'An error occurred while loading competency ratings';
        this.toastService.error(this.error);
        this.isLoading = false;
      },
    });
  }

  onEmployeeChange(): void {
    this.selectedReviewId = null;
    this.competencyRatings = [];
    this.loadReviews();
  }

  onReviewChange(): void {
    this.loadCompetencyRatings();
  }

  openCompetencyModal(): void {
    this.competencyFormData = {
      name: '',
      description: '',
    };
    this.showCompetencyModal = true;
  }

  closeCompetencyModal(): void {
    this.showCompetencyModal = false;
  }

  saveCompetency(): void {
    if (!this.competencyFormData.name?.trim()) {
      this.toastService.error('Competency name is required');
      return;
    }

    this.isSaving = true;

    this.competencyService.create(this.competencyFormData).subscribe({
      next: (response) => {
        if (!response.hasError) {
          this.toastService.success('Competency created successfully');
          this.closeCompetencyModal();
        } else {
          this.toastService.error(response.errorMessage || 'Failed to create competency');
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

  openRateModal(): void {
    if (!this.selectedReviewId) {
      this.toastService.error('Please select a review first');
      return;
    }
    this.rateFormData = {
      performanceReviewId: this.selectedReviewId,
      competencyId: 0,
      rating: 3,
      notes: '',
    };
    this.showRateModal = true;
  }

  closeRateModal(): void {
    this.showRateModal = false;
  }

  saveRating(): void {
    if (!this.rateFormData.competencyId) {
      this.toastService.error('Please enter a competency ID');
      return;
    }
    if (this.rateFormData.rating < 1 || this.rateFormData.rating > 5) {
      this.toastService.error('Rating must be between 1 and 5');
      return;
    }

    this.isSaving = true;

    this.competencyService.rate(this.rateFormData).subscribe({
      next: (response) => {
        if (!response.hasError) {
          this.toastService.success('Competency rated successfully');
          this.closeRateModal();
          this.loadCompetencyRatings();
        } else {
          this.toastService.error(response.errorMessage || 'Failed to rate competency');
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

  getRatingStars(rating: number): string[] {
    const stars: string[] = [];
    for (let i = 1; i <= 5; i++) {
      stars.push(i <= rating ? 'eva-star' : 'eva-star-outline');
    }
    return stars;
  }

  getRatingClass(rating: number): string {
    if (rating >= 4) return 'text-success';
    if (rating >= 3) return 'text-info';
    if (rating >= 2) return 'text-warning';
    return 'text-danger';
  }

  getAverageRating(): string {
    if (this.competencyRatings.length === 0) return '0.0';
    const sum = this.competencyRatings.reduce((acc, r) => acc + r.rating, 0);
    return (sum / this.competencyRatings.length).toFixed(1);
  }

  getExcellentCount(): number {
    return this.competencyRatings.filter((r) => r.rating >= 4).length;
  }
}
