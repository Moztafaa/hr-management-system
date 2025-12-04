import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { PerformanceService } from '../../../Services/performance/performance';
import { Employee as EmployeeService } from '../../../Services/employee';
import { FeedbackDto } from '../../../models/performance-dtos';

@Component({
  selector: 'app-feedback-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './feedback-list.html',
  styleUrl: './feedback-list.css',
})
export class FeedbackListComponent implements OnInit {
  private performanceService = inject(PerformanceService);
  private employeeService = inject(EmployeeService);

  feedbacks: FeedbackDto[] = [];
  reviews: any[] = [];
  isLoading = true;
  error: string | null = null;
  showCreateForm = false;

  createForm: {
    performanceReviewId: number;
    fromEmployeeId: number;
    type: string;
    comments: string;
  } = {
    performanceReviewId: 0,
    fromEmployeeId: 0,
    type: 'Manager',
    comments: '',
  };

  feedbackTypes = ['Manager', 'Peer', 'Self'];

  ngOnInit() {
    this.loadReviews();
    const employeeId = this.employeeService.getEmployeeId();
    this.createForm.fromEmployeeId = employeeId;
  }

  loadReviews() {
    const employeeId = this.employeeService.getEmployeeId();
    this.performanceService.getReviewsByEmployee(employeeId).subscribe({
      next: (res) => {
        if (!res.hasError) {
          this.reviews = res.data || [];
          this.isLoading = false;
        }
      },
      error: (err) => {
        console.error('Error loading reviews', err);
        this.isLoading = false;
      },
    });
  }

  loadFeedbacks(reviewId: number) {
    this.performanceService.getFeedbackByType(reviewId, '').subscribe({
      next: (res) => {
        if (!res.hasError) {
          this.feedbacks = res.data || [];
        }
      },
      error: (err) => console.error('Error loading feedbacks', err),
    });
  }

  toggleCreateForm() {
    this.showCreateForm = !this.showCreateForm;
    if (!this.showCreateForm) {
      this.resetForm();
    }
  }

  addFeedback() {
    if (!this.createForm.performanceReviewId || !this.createForm.comments) {
      this.error = 'Please fill in review and comments';
      return;
    }

    const payload = {
      performanceReviewId: this.createForm.performanceReviewId,
      fromEmployeeId: this.createForm.fromEmployeeId,
      type: this.createForm.type,
      comments: this.createForm.comments,
    };

    this.performanceService.addFeedback(payload).subscribe({
      next: (res) => {
        if (!res.hasError) {
          alert('✔️ Feedback submitted successfully');
          this.showCreateForm = false;
          this.resetForm();
          this.loadFeedbacks(this.createForm.performanceReviewId);
        } else {
          this.error = res.errorMessage || 'Failed to submit feedback';
        }
      },
      error: (err) => {
        console.error('Error submitting feedback', err);
        this.error = 'Error submitting feedback';
      },
    });
  }

  getTypeColor(type: string): string {
    const colors: { [key: string]: string } = {
      Manager: 'danger',
      Peer: 'warning',
      Self: 'info',
    };
    return colors[type] || 'secondary';
  }

  resetForm() {
    this.createForm = {
      performanceReviewId: 0,
      fromEmployeeId: this.employeeService.getEmployeeId(),
      type: 'Manager',
      comments: '',
    };
    this.error = null;
  }

  cancel() {
    this.showCreateForm = false;
    this.resetForm();
  }
}
