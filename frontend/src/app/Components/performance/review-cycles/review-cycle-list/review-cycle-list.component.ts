import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ReviewCycleService } from '../../../../Services/performance/review-cycle.service';
import { ToastService } from '../../../../Services/toast.service';
import {
  ReviewCycle,
  CreateCycleDto,
  CycleFrequency,
  RatingScaleType,
  getCycleFrequencyLabel,
  getRatingScaleLabel,
} from '../../../../models/performance';

@Component({
  selector: 'app-review-cycle-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './review-cycle-list.component.html',
  styleUrls: ['./review-cycle-list.component.css'],
})
export class ReviewCycleListComponent implements OnInit {
  private cycleService = inject(ReviewCycleService);
  private toastService = inject(ToastService);

  cycles: ReviewCycle[] = [];
  activeCycles: ReviewCycle[] = [];
  isLoading = true;
  error: string | null = null;

  // Modal state
  showModal = false;
  isSaving = false;

  // Form data
  formData: CreateCycleDto = {
    name: '',
    frequency: CycleFrequency.Annual,
    startDate: '',
    endDate: '',
    ratingScale: RatingScaleType.OneToFive,
  };

  // Enum references for template
  CycleFrequency = CycleFrequency;
  RatingScaleType = RatingScaleType;

  ngOnInit(): void {
    this.loadCycles();
  }

  loadCycles(): void {
    this.isLoading = true;
    this.error = null;

    this.cycleService.getAll().subscribe({
      next: (response) => {
        if (!response.hasError && response.data) {
          this.cycles = response.data;
          this.filterActiveCycles();
        } else {
          this.error = response.errorMessage || 'Failed to load review cycles';
          this.toastService.error(this.error);
        }
        this.isLoading = false;
      },
      error: () => {
        this.error = 'An error occurred while loading review cycles';
        this.toastService.error(this.error);
        this.isLoading = false;
      },
    });
  }

  filterActiveCycles(): void {
    const now = new Date();
    this.activeCycles = this.cycles.filter((cycle) => {
      const start = new Date(cycle.startDate);
      const end = new Date(cycle.endDate);
      return now >= start && now <= end;
    });
  }

  openCreateModal(): void {
    this.formData = {
      name: '',
      frequency: CycleFrequency.Annual,
      startDate: '',
      endDate: '',
      ratingScale: RatingScaleType.OneToFive,
    };
    this.showModal = true;
  }

  closeModal(): void {
    this.showModal = false;
  }

  saveCycle(): void {
    if (!this.formData.name?.trim()) {
      this.toastService.error('Cycle name is required');
      return;
    }
    if (!this.formData.startDate || !this.formData.endDate) {
      this.toastService.error('Start and end dates are required');
      return;
    }

    this.isSaving = true;

    this.cycleService.create(this.formData).subscribe({
      next: (response) => {
        if (!response.hasError) {
          this.toastService.success('Review cycle created successfully');
          this.closeModal();
          this.loadCycles();
        } else {
          this.toastService.error(response.errorMessage || 'Failed to create review cycle');
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

  getFrequencyLabel(frequency: CycleFrequency): string {
    return getCycleFrequencyLabel(frequency);
  }

  getRatingScaleLabel(scale: RatingScaleType): string {
    return getRatingScaleLabel(scale);
  }

  getFrequencyClass(frequency: CycleFrequency): string {
    switch (frequency) {
      case CycleFrequency.Annual:
        return 'bg-primary';
      case CycleFrequency.Quarterly:
        return 'bg-info';
      case CycleFrequency.Monthly:
        return 'bg-success';
      default:
        return 'bg-secondary';
    }
  }

  formatDate(dateStr: string): string {
    if (!dateStr) return 'N/A';
    const date = new Date(dateStr);
    return date.toLocaleDateString('en-US', {
      year: 'numeric',
      month: 'short',
      day: 'numeric',
    });
  }

  isActive(cycle: ReviewCycle): boolean {
    const now = new Date();
    const start = new Date(cycle.startDate);
    const end = new Date(cycle.endDate);
    return now >= start && now <= end;
  }

  getDaysRemaining(cycle: ReviewCycle): number {
    const now = new Date();
    const end = new Date(cycle.endDate);
    const diff = end.getTime() - now.getTime();
    return Math.max(0, Math.ceil(diff / (1000 * 60 * 60 * 24)));
  }
}
