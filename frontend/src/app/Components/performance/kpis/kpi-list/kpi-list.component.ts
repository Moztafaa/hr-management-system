import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { KpiService } from '../../../../Services/performance/kpi.service';
import { PerformanceReviewService } from '../../../../Services/performance/performance-review.service';
import { EmployeeService } from '../../../../Services/employee';
import { ToastService } from '../../../../Services/toast.service';
import {
  KPI,
  CreateKpiDto,
  KPIResult,
  CreateKpiResultDto,
  PerformanceReview,
} from '../../../../models/performance';
import { Employee } from '../../../../models/employee';

@Component({
  selector: 'app-kpi-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './kpi-list.component.html',
  styleUrls: ['./kpi-list.component.css'],
})
export class KpiListComponent implements OnInit {
  private kpiService = inject(KpiService);
  private reviewService = inject(PerformanceReviewService);
  private employeeService = inject(EmployeeService);
  private toastService = inject(ToastService);

  kpis: KPI[] = [];
  kpiResults: KPIResult[] = [];
  reviews: PerformanceReview[] = [];
  employees: Employee[] = [];
  isLoading = true;
  error: string | null = null;

  // Modal state
  showKpiModal = false;
  showResultModal = false;
  isSaving = false;

  // Form data
  selectedEmployeeId: number | null = null;
  selectedReviewId: number | null = null;

  kpiFormData: CreateKpiDto = {
    code: '',
    name: '',
    unit: '',
    target: 0,
    description: '',
  };

  resultFormData: CreateKpiResultDto = {
    performanceReviewId: 0,
    kpiId: 0,
    actual: 0,
  };

  selectedKpi: KPI | null = null;

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
      this.kpiResults = [];
      this.isLoading = false;
      return;
    }

    this.reviewService.getByEmployee(this.selectedEmployeeId).subscribe({
      next: (response) => {
        if (!response.hasError && response.data) {
          this.reviews = response.data;
          if (this.reviews.length > 0) {
            this.selectedReviewId = this.reviews[0].id;
            this.loadKpiResults();
          } else {
            this.kpiResults = [];
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

  loadKpiResults(): void {
    if (!this.selectedReviewId) {
      this.kpiResults = [];
      this.isLoading = false;
      return;
    }

    this.isLoading = true;
    this.error = null;

    this.reviewService.getReport(this.selectedReviewId).subscribe({
      next: (response) => {
        if (!response.hasError && response.data) {
          this.kpiResults = response.data.kpiResults;
        } else {
          this.error = response.errorMessage || 'Failed to load KPI results';
          this.toastService.error(this.error);
        }
        this.isLoading = false;
      },
      error: () => {
        this.error = 'An error occurred while loading KPI results';
        this.toastService.error(this.error);
        this.isLoading = false;
      },
    });
  }

  onEmployeeChange(): void {
    this.selectedReviewId = null;
    this.kpiResults = [];
    this.loadReviews();
  }

  onReviewChange(): void {
    this.loadKpiResults();
  }

  openKpiModal(): void {
    this.kpiFormData = {
      code: '',
      name: '',
      unit: '',
      target: 0,
      description: '',
    };
    this.showKpiModal = true;
  }

  closeKpiModal(): void {
    this.showKpiModal = false;
  }

  saveKpi(): void {
    if (!this.kpiFormData.code?.trim() || !this.kpiFormData.name?.trim()) {
      this.toastService.error('KPI code and name are required');
      return;
    }

    this.isSaving = true;

    this.kpiService.create(this.kpiFormData).subscribe({
      next: (response) => {
        if (!response.hasError && response.data) {
          this.toastService.success('KPI created successfully');
          this.kpis.push(response.data);
          this.closeKpiModal();
        } else {
          this.toastService.error(response.errorMessage || 'Failed to create KPI');
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

  openResultModal(): void {
    if (!this.selectedReviewId) {
      this.toastService.error('Please select a review first');
      return;
    }
    this.resultFormData = {
      performanceReviewId: this.selectedReviewId,
      kpiId: 0,
      actual: 0,
    };
    this.showResultModal = true;
  }

  closeResultModal(): void {
    this.showResultModal = false;
    this.selectedKpi = null;
  }

  saveResult(): void {
    if (!this.resultFormData.kpiId) {
      this.toastService.error('Please enter a KPI ID');
      return;
    }

    this.isSaving = true;

    this.kpiService.addResult(this.resultFormData).subscribe({
      next: (response) => {
        if (!response.hasError) {
          this.toastService.success('KPI result added successfully');
          this.closeResultModal();
          this.loadKpiResults();
        } else {
          this.toastService.error(response.errorMessage || 'Failed to add KPI result');
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

  getAchievementPercentage(result: KPIResult): number {
    if (!result.kpiTarget || result.kpiTarget === 0) return 0;
    return Math.round((result.actual / result.kpiTarget) * 100);
  }

  getAchievementClass(result: KPIResult): string {
    const pct = this.getAchievementPercentage(result);
    if (pct >= 100) return 'bg-success';
    if (pct >= 75) return 'bg-info';
    if (pct >= 50) return 'bg-warning';
    return 'bg-danger';
  }

  getAchievementWidth(result: KPIResult): number {
    return Math.min(this.getAchievementPercentage(result), 100);
  }

  getTargetsMetCount(): number {
    return this.kpiResults.filter((r) => this.getAchievementPercentage(r) >= 100).length;
  }

  getBelowTargetCount(): number {
    return this.kpiResults.filter((r) => this.getAchievementPercentage(r) < 100).length;
  }
}
