import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { PerformanceService } from '../../../Services/performance/performance';
import { Employee as EmployeeService } from '../../../Services/employee';
import { CompetencyDto, EmployeeCompetencyRatingDto } from '../../../models/performance-dtos';

@Component({
  selector: 'app-competencies',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './competencies.html',
  styleUrl: './competencies.css',
})
export class CompetenciesComponent implements OnInit {
  private performanceService = inject(PerformanceService);
  private employeeService = inject(EmployeeService);

  competencies: CompetencyDto[] = [];
  employeeRatings: EmployeeCompetencyRatingDto[] = [];
  isLoading = true;
  error: string | null = null;
  showCreateForm = false;
  showRateForm = false;

  createForm: { name: string; description?: string } = {
    name: '',
    description: '',
  };

  rateForm: { performanceReviewId: number; competencyId: number; rating: number; notes?: string } = {
    performanceReviewId: 0,
    competencyId: 0,
    rating: 3,
    notes: '',
  };

  reviews: any[] = [];

  ngOnInit() {
    this.loadCompetencies();
    this.loadReviews();
  }

  loadCompetencies() {
    this.isLoading = true;
    this.error = null;
    this.isLoading = false;
  }

  loadReviews() {
    const employeeId = this.employeeService.getEmployeeId();
    this.performanceService.getReviewsByEmployee(employeeId).subscribe({
      next: (res) => {
        if (!res.hasError) {
          this.reviews = res.data || [];
        }
      },
      error: (err) => console.error('Error loading reviews', err),
    });
  }

  toggleCreateForm() {
    this.showCreateForm = !this.showCreateForm;
    if (!this.showCreateForm) {
      this.resetCreateForm();
    }
  }

  toggleRateForm() {
    this.showRateForm = !this.showRateForm;
    if (!this.showRateForm) {
      this.resetRateForm();
    }
  }

  createCompetency() {
    if (!this.createForm.name) {
      this.error = 'Please fill in competency name';
      return;
    }

    const payload = {
      name: this.createForm.name,
      description: this.createForm.description,
    };

    this.performanceService.createCompetency(payload).subscribe({
      next: (res) => {
        if (!res.hasError) {
          alert('✔️ Competency created successfully');
          this.showCreateForm = false;
          this.resetCreateForm();
          this.competencies.push(res.data!);
        } else {
          this.error = res.errorMessage || 'Failed to create competency';
        }
      },
      error: (err) => {
        console.error('Error creating competency', err);
        this.error = 'Error creating competency';
      },
    });
  }

  rateCompetency() {
    if (!this.rateForm.performanceReviewId || !this.rateForm.competencyId) {
      this.error = 'Please select review and competency';
      return;
    }

    const payload = {
      performanceReviewId: this.rateForm.performanceReviewId,
      competencyId: this.rateForm.competencyId,
      rating: this.rateForm.rating,
      notes: this.rateForm.notes,
    };

    this.performanceService.rateCompetency(payload).subscribe({
      next: (res) => {
        if (!res.hasError) {
          alert('✔️ Competency rating saved successfully');
          this.showRateForm = false;
          this.resetRateForm();
          this.employeeRatings.push(res.data!);
        } else {
          this.error = res.errorMessage || 'Failed to rate competency';
        }
      },
      error: (err) => {
        console.error('Error rating competency', err);
        this.error = 'Error rating competency';
      },
    });
  }

  getRatingStars(rating: number): string[] {
    const stars: string[] = [];
    for (let i = 0; i < 5; i++) {
      stars.push(i < rating ? '★' : '☆');
    }
    return stars;
  }

  resetCreateForm() {
    this.createForm = {
      name: '',
      description: '',
    };
  }

  resetRateForm() {
    this.rateForm = {
      performanceReviewId: 0,
      competencyId: 0,
      rating: 3,
      notes: '',
    };
  }

  cancel() {
    this.showCreateForm = false;
    this.showRateForm = false;
    this.resetCreateForm();
    this.resetRateForm();
  }
}
