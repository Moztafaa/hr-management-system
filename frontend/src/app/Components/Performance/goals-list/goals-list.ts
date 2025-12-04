import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { PerformanceService } from '../../../Services/performance/performance';
import { Employee as EmployeeService } from '../../../Services/employee';
import { GoalDto } from '../../../models/performance-dtos';

@Component({
  selector: 'app-goals-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './goals-list.html',
  styleUrl: './goals-list.css',
})
export class GoalsListComponent implements OnInit {
  private performanceService = inject(PerformanceService);
  private employeeService = inject(EmployeeService);

  goals: GoalDto[] = [];
  isLoading = true;
  error: string | null = null;
  showCreateForm = false;
  editingGoalId: number | null = null;

  createForm = {
    title: '',
    description: '',
    dueDate: '',
    reviewId: 0,
  };

  updateForm = {
    progressPercent: 0,
    status: 'OnTrack',
  };

  reviews: any[] = [];
  statusOptions = ['NotStarted', 'OnTrack', 'AtRisk', 'Completed'];

  ngOnInit() {
    this.loadReviews();
  }

  loadReviews() {
    const employeeId = this.employeeService.getEmployeeId();
    this.performanceService.getReviewsByEmployee(employeeId).subscribe({
      next: (res) => {
        if (!res.hasError) {
          this.reviews = res.data || [];
        }
      },
      error: (err) => {
        console.error('Error loading reviews', err);
      },
    });
  }

  toggleCreateForm() {
    this.showCreateForm = !this.showCreateForm;
    if (!this.showCreateForm) {
      this.resetForm();
    }
  }

  createGoal() {
    if (!this.createForm.title || !this.createForm.dueDate || !this.createForm.reviewId) {
      this.error = 'Please fill in all required fields';
      return;
    }

    const payload = {
      performanceReviewId: this.createForm.reviewId,
      title: this.createForm.title,
      description: this.createForm.description,
      dueDate: this.createForm.dueDate,
    };

    this.performanceService.createGoal(payload).subscribe({
      next: (res) => {
        if (!res.hasError) {
          alert('✔️ Goal created successfully');
          this.showCreateForm = false;
          this.resetForm();
          this.goals.push(res.data!);
        } else {
          this.error = res.errorMessage || 'Failed to create goal';
        }
      },
      error: (err) => {
        console.error('Error creating goal', err);
        this.error = 'Error creating goal';
      },
    });
  }

  getStatusBadge(status: string): string {
    const statusMap: { [key: string]: string } = {
      NotStarted: 'Not Started',
      OnTrack: 'On Track',
      AtRisk: 'At Risk',
      Completed: 'Completed',
    };
    return statusMap[status] || status;
  }

  getStatusClass(status: string): string {
    const classMap: { [key: string]: string } = {
      NotStarted: 'bg-secondary',
      OnTrack: 'bg-success',
      AtRisk: 'bg-warning',
      Completed: 'bg-info',
    };
    return classMap[status] || 'bg-secondary';
  }

  resetForm() {
    this.createForm = {
      title: '',
      description: '',
      dueDate: '',
      reviewId: 0,
    };
  }

  cancel() {
    this.showCreateForm = false;
    this.resetForm();
  }

  toggleEditForm(goal: GoalDto) {
    if (this.editingGoalId === goal.id) {
      this.editingGoalId = null;
    } else {
      this.editingGoalId = goal.id;
      this.updateForm.progressPercent = goal.progressPercent;
      this.updateForm.status = goal.status;
    }
  }

  updateGoalProgress(goalId: number) {
    if (this.updateForm.progressPercent < 0 || this.updateForm.progressPercent > 100) {
      this.error = 'Progress must be between 0 and 100';
      return;
    }

    const payload = {
      goalId: goalId,
      progressPercent: this.updateForm.progressPercent,
      status: this.updateForm.status,
    };

    this.performanceService.updateGoalProgress(payload).subscribe({
      next: (res) => {
        if (!res.hasError) {
          alert('✔️ Goal updated successfully');
          this.editingGoalId = null;
          const goalIndex = this.goals.findIndex((g) => g.id === goalId);
          if (goalIndex >= 0) {
            this.goals[goalIndex].progressPercent = this.updateForm.progressPercent;
            this.goals[goalIndex].status = this.updateForm.status;
          }
        } else {
          this.error = res.errorMessage || 'Failed to update goal';
        }
      },
      error: (err) => {
        console.error('Error updating goal', err);
        this.error = 'Error updating goal';
      },
    });
  }

  cancelEdit() {
    this.editingGoalId = null;
  }
}
