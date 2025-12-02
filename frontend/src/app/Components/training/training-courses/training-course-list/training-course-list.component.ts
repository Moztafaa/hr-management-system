import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TrainingCourseService } from '../../../../Services/training/training-course.service';
import { ToastService } from '../../../../Services/toast.service';
import {
  TrainingCourse,
  CreateTrainingCourseDto,
  UpdateTrainingCourseDto,
} from '../../../../models/training';

@Component({
  selector: 'app-training-course-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './training-course-list.component.html',
  styleUrls: ['./training-course-list.component.css'],
})
export class TrainingCourseListComponent implements OnInit {
  private courseService = inject(TrainingCourseService);
  private toastService = inject(ToastService);

  courses: TrainingCourse[] = [];
  isLoading = true;
  error: string | null = null;

  // Modal state
  showModal = false;
  showDeleteModal = false;
  isEditing = false;
  isSaving = false;

  // Form data
  formData: CreateTrainingCourseDto | UpdateTrainingCourseDto = {
    title: '',
    description: '',
    durationHours: undefined,
  };
  selectedCourse: TrainingCourse | null = null;
  courseToDelete: TrainingCourse | null = null;

  ngOnInit(): void {
    this.loadCourses();
  }

  loadCourses(): void {
    this.isLoading = true;
    this.error = null;

    this.courseService.getAll().subscribe({
      next: (response) => {
        if (!response.hasError && response.data) {
          this.courses = response.data;
        } else {
          this.error = response.errorMessage || 'Failed to load training courses';
          this.toastService.error(this.error);
        }
        this.isLoading = false;
      },
      error: () => {
        this.error = 'An error occurred while loading training courses';
        this.toastService.error(this.error);
        this.isLoading = false;
      },
    });
  }

  openCreateModal(): void {
    this.isEditing = false;
    this.formData = {
      title: '',
      description: '',
      durationHours: undefined,
    };
    this.selectedCourse = null;
    this.showModal = true;
  }

  openEditModal(course: TrainingCourse): void {
    this.isEditing = true;
    this.selectedCourse = course;
    this.formData = {
      title: course.title,
      description: course.description || '',
      durationHours: course.durationHours,
    };
    this.showModal = true;
  }

  closeModal(): void {
    this.showModal = false;
    this.selectedCourse = null;
  }

  saveCourse(): void {
    if (!this.formData.title?.trim()) {
      this.toastService.error('Course title is required');
      return;
    }

    this.isSaving = true;

    if (this.isEditing && this.selectedCourse) {
      this.courseService
        .update(this.selectedCourse.id, this.formData as UpdateTrainingCourseDto)
        .subscribe({
          next: (response) => {
            if (!response.hasError) {
              this.toastService.success('Training course updated successfully');
              this.closeModal();
              this.loadCourses();
            } else {
              this.toastService.error(response.errorMessage || 'Failed to update training course');
            }
            this.isSaving = false;
          },
          error: (err) => {
            const errorMessage = this.extractValidationErrors(err);
            this.toastService.error(errorMessage);
            this.isSaving = false;
          },
        });
    } else {
      this.courseService.create(this.formData as CreateTrainingCourseDto).subscribe({
        next: (response) => {
          if (!response.hasError) {
            this.toastService.success('Training course created successfully');
            this.closeModal();
            this.loadCourses();
          } else {
            this.toastService.error(response.errorMessage || 'Failed to create training course');
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

  openDeleteModal(course: TrainingCourse): void {
    this.courseToDelete = course;
    this.showDeleteModal = true;
  }

  closeDeleteModal(): void {
    this.showDeleteModal = false;
    this.courseToDelete = null;
  }

  confirmDelete(): void {
    if (!this.courseToDelete) return;

    this.isSaving = true;
    this.courseService.delete(this.courseToDelete.id).subscribe({
      next: (response) => {
        if (!response.hasError) {
          this.toastService.success('Training course deleted successfully');
          this.closeDeleteModal();
          this.loadCourses();
        } else {
          this.toastService.error(response.errorMessage || 'Failed to delete training course');
        }
        this.isSaving = false;
      },
      error: () => {
        this.toastService.error('An error occurred while deleting the training course');
        this.isSaving = false;
      },
    });
  }

  formatDuration(hours?: number): string {
    if (!hours) return 'N/A';
    if (hours < 1) return `${Math.round(hours * 60)} min`;
    if (hours === 1) return '1 hour';
    return `${hours} hours`;
  }

  getTotalHours(): number {
    return this.courses.reduce((sum, c) => sum + (c.durationHours || 0), 0);
  }

  getAvgDuration(): string {
    if (this.courses.length === 0) return '0';
    return (this.getTotalHours() / this.courses.length).toFixed(1);
  }
}
