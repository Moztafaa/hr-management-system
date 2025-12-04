import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { PerformanceService } from '../../../Services/performance/performance';
import { ReviewCycleDto } from '../../../models/performance-dtos';

@Component({
  selector: 'app-review-cycles',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './review-cycles.html',
  styleUrl: './review-cycles.css',
})
export class ReviewCyclesComponent implements OnInit {
  private performanceService = inject(PerformanceService);

  cycles: ReviewCycleDto[] = [];
  isLoading = true;
  error: string | null = null;
  showCreateForm = false;

  createForm = {
    name: '',
    description: '',
    startDate: '',
    endDate: '',
    frequency: 'Annual',
    ratingScale: 'OneToFive',
  };

  ngOnInit() {
    this.loadCycles();
  }

  loadCycles() {
    this.isLoading = true;
    this.error = null;
    this.performanceService.getReviewCycles().subscribe({
      next: (res) => {
        if (!res.hasError && res.data) {
          this.cycles = res.data;
        } else {
          this.error = res.errorMessage || 'Failed to load review cycles';
        }
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Error loading cycles', err);
        this.error = 'Error loading review cycles';
        this.isLoading = false;
      },
    });
  }

  toggleCreateForm() {
    this.showCreateForm = !this.showCreateForm;
    if (!this.showCreateForm) {
      this.resetForm();
    }
  }

  createCycle() {
    if (!this.createForm.name || !this.createForm.startDate || !this.createForm.endDate) {
      this.error = 'Please fill in all required fields';
      return;
    }

    this.performanceService.createReviewCycle(this.createForm).subscribe({
      next: (res) => {
        if (!res.hasError) {
          alert('✔️ Review cycle created successfully');
          this.showCreateForm = false;
          this.resetForm();
          this.loadCycles();
        } else {
          this.error = res.errorMessage || 'Failed to create review cycle';
        }
      },
      error: (err) => {
        console.error('Error creating cycle', err);
        this.error = 'Error creating review cycle';
      },
    });
  }

  resetForm() {
    this.createForm = {
      name: '',
      description: '',
      startDate: '',
      endDate: '',
      frequency: 'Annual',
      ratingScale: 'OneToFive',
    };
  }

  cancel() {
    this.showCreateForm = false;
    this.resetForm();
  }
}
