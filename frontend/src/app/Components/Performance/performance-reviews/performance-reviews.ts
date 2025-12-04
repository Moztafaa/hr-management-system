import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { PerformanceService } from '../../../Services/performance/performance';
import { Employee as EmployeeService } from '../../../Services/employee';
import { PerformanceReviewDto, ReviewCycleDto } from '../../../models/performance-dtos';

@Component({
  selector: 'app-performance-reviews',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './performance-reviews.html',
  styleUrl: './performance-reviews.css',
})
export class PerformanceReviewsComponent implements OnInit {
  private performanceService = inject(PerformanceService);
  private employeeService = inject(EmployeeService);

  reviews: PerformanceReviewDto[] = [];
  cycles: ReviewCycleDto[] = [];
  employees: any[] = [];
  isLoading = true;
  error: string | null = null;
  showCreateForm = false;

  createForm = {
    employeeId: 0,
    reviewCycleId: 0,
  };

  ngOnInit() {
    this.loadEmployees();
    this.loadReviews();
    this.loadCycles();
  }

  loadReviews() {
    this.isLoading = true;
    this.error = null;
    this.performanceService.getReviews().subscribe({
      next: (res) => {
        if (!res.hasError && res.data) {
          this.reviews = res.data;
        } else {
          this.error = res.errorMessage || 'Failed to load performance reviews';
        }
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Error loading reviews', err);
        this.error = 'Error loading performance reviews';
        this.isLoading = false;
      },
    });
  }

  loadCycles() {
    this.performanceService.getReviewCycles().subscribe({
      next: (res) => {
        if (!res.hasError && res.data) {
          this.cycles = res.data;
        }
      },
      error: (err) => console.error('Error loading cycles', err),
    });
  }

  loadEmployees() {
    this.employeeService.getEmployees().subscribe({
      next: (res) => {
        if (!res.hasError && res.data) {
          this.employees = res.data;
        }
      },
      error: (err) => console.error('Error loading employees', err),
    });
  }

  getCycleName(cycleId: number): string {
    const c = this.cycles.find((x) => x.id === cycleId);
    return c ? c.name : `Cycle #${cycleId}`;
  }

  toggleCreateForm() {
    this.showCreateForm = !this.showCreateForm;
    if (!this.showCreateForm) {
      this.resetForm();
    }
  }

  createReview() {
    if (!this.createForm.employeeId || !this.createForm.reviewCycleId) {
      this.error = 'Please select both employee and review cycle';
      return;
    }

    const payload = {
      employeeId: this.createForm.employeeId,
      reviewCycleId: this.createForm.reviewCycleId,
    };

    this.performanceService.createReview(payload).subscribe({
      next: (res) => {
        if (!res.hasError) {
          alert('✔️ Performance review created successfully');
          this.showCreateForm = false;
          this.resetForm();
          this.loadReviews();
        } else {
          this.error = res.errorMessage || 'Failed to create review';
        }
      },
      error: (err) => {
        console.error('Error creating review', err);
        this.error = 'Error creating review';
      },
    });
  }

  closeReview(reviewId: number) {
    if (!confirm('Are you sure you want to close this review?')) return;

    this.performanceService.closeReview(reviewId).subscribe({
      next: (res) => {
        if (!res.hasError) {
          alert('✔️ Review closed successfully');
          this.loadReviews();
        } else {
          alert(res.errorMessage || 'Failed to close review');
        }
      },
      error: (err) => {
        console.error('Error closing review', err);
        alert('Error closing review');
      },
    });
  }

  getStatusBadge(status: string): string {
    const statusMap: { [key: string]: string } = {
      Draft: 'Draft',
      InProgress: 'In Progress',
      Submitted: 'Submitted',
      Closed: 'Closed',
    };
    return status ? statusMap[status] || status : 'Unknown';
  }

  getStatusClass(status: string): string {
    const classMap: { [key: string]: string } = {
      Draft: 'bg-secondary',
      InProgress: 'bg-warning',
      Submitted: 'bg-info',
      Closed: 'bg-success',
    };
    return status ? classMap[status] || 'bg-secondary' : 'bg-secondary';
  }

  resetForm() {
    this.createForm = {
      employeeId: 0,
      reviewCycleId: 0,
    };
  }

  cancel() {
    this.showCreateForm = false;
    this.resetForm();
  }
}
