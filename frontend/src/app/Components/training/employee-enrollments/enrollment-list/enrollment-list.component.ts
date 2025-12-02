import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { EmployeeTrainingService } from '../../../../Services/training/employee-training.service';
import { TrainingCourseService } from '../../../../Services/training/training-course.service';
import { ToastService } from '../../../../Services/toast.service';
import { Employee as EmployeeService } from '../../../../Services/employee';
import { Employee as EmployeeModel } from '../../../../models/employee';
import {
  EmployeeTraining,
  EmployeeEnrollDto,
  TrainingCourse,
  TrainingStatus,
} from '../../../../models/training';

@Component({
  selector: 'app-enrollment-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './enrollment-list.component.html',
  styleUrls: ['./enrollment-list.component.css'],
})
export class EnrollmentListComponent implements OnInit {
  private enrollmentService = inject(EmployeeTrainingService);
  private courseService = inject(TrainingCourseService);
  private employeeService = inject(EmployeeService);
  private toastService = inject(ToastService);

  enrollments: EmployeeTraining[] = [];
  courses: TrainingCourse[] = [];
  employees: EmployeeModel[] = [];
  isLoading = true;
  isLoadingCourses = false;
  isLoadingEmployees = false;
  error: string | null = null;

  // Filter
  filterBy: 'all' | 'employee' | 'course' = 'all';
  selectedEmployeeId: number | null = null;
  selectedCourseId: number | null = null;

  // Modal state
  showEnrollModal = false;
  showActionModal = false;
  isSaving = false;

  // Form data for enrollment
  enrollFormData: EmployeeEnrollDto = {
    employeeId: 0,
    trainingCourseId: 0,
  };

  // Action modal state
  actionType: 'complete' | 'cancel' | null = null;
  selectedEnrollment: EmployeeTraining | null = null;

  ngOnInit(): void {
    this.loadCourses();
    this.loadEmployees();
    this.loadEnrollments();
  }

  loadEnrollments(): void {
    this.isLoading = true;
    this.error = null;

    // For now, load by course since there's no getAll endpoint
    // We'll load from the first course or show empty if no courses
    if (this.filterBy === 'employee' && this.selectedEmployeeId) {
      this.enrollmentService.getByEmployee(this.selectedEmployeeId).subscribe({
        next: (response) => {
          if (!response.hasError && response.data) {
            this.enrollments = response.data;
          } else {
            this.error = response.errorMessage || 'Failed to load enrollments';
            this.toastService.error(this.error);
          }
          this.isLoading = false;
        },
        error: () => {
          this.error = 'An error occurred while loading enrollments';
          this.toastService.error(this.error);
          this.isLoading = false;
        },
      });
    } else if (this.filterBy === 'course' && this.selectedCourseId) {
      this.enrollmentService.getByCourse(this.selectedCourseId).subscribe({
        next: (response) => {
          if (!response.hasError && response.data) {
            this.enrollments = response.data;
          } else {
            this.error = response.errorMessage || 'Failed to load enrollments';
            this.toastService.error(this.error);
          }
          this.isLoading = false;
        },
        error: () => {
          this.error = 'An error occurred while loading enrollments';
          this.toastService.error(this.error);
          this.isLoading = false;
        },
      });
    } else {
      // Load all by iterating through courses (or employees)
      this.enrollments = [];
      if (this.courses.length === 0) {
        this.isLoading = false;
        return;
      }

      let loadedCount = 0;
      const allEnrollments: EmployeeTraining[] = [];

      this.courses.forEach((course) => {
        this.enrollmentService.getByCourse(course.id).subscribe({
          next: (response) => {
            if (!response.hasError && response.data) {
              allEnrollments.push(...response.data);
            }
            loadedCount++;
            if (loadedCount === this.courses.length) {
              // Remove duplicates based on employeeId + trainingCourseId
              const uniqueMap = new Map<string, EmployeeTraining>();
              allEnrollments.forEach((e) => {
                const key = `${e.employeeId}-${e.trainingCourseId}`;
                if (!uniqueMap.has(key)) {
                  uniqueMap.set(key, e);
                }
              });
              this.enrollments = Array.from(uniqueMap.values());
              this.isLoading = false;
            }
          },
          error: () => {
            loadedCount++;
            if (loadedCount === this.courses.length) {
              this.enrollments = allEnrollments;
              this.isLoading = false;
            }
          },
        });
      });
    }
  }

  loadCourses(): void {
    this.isLoadingCourses = true;
    this.courseService.getAll().subscribe({
      next: (response) => {
        if (!response.hasError && response.data) {
          this.courses = response.data;
          // Load enrollments after courses are loaded
          if (this.filterBy === 'all') {
            this.loadEnrollments();
          }
        }
        this.isLoadingCourses = false;
      },
      error: () => {
        this.isLoadingCourses = false;
      },
    });
  }

  loadEmployees(): void {
    this.isLoadingEmployees = true;
    this.employeeService.getEmployees().subscribe({
      next: (response) => {
        if (!response.hasError && response.data) {
          this.employees = response.data;
        }
        this.isLoadingEmployees = false;
      },
      error: () => {
        this.isLoadingEmployees = false;
      },
    });
  }

  onFilterChange(): void {
    if (this.filterBy === 'all') {
      this.selectedEmployeeId = null;
      this.selectedCourseId = null;
      this.loadEnrollments();
    }
  }

  onEmployeeFilterChange(): void {
    if (this.selectedEmployeeId) {
      this.filterBy = 'employee';
      this.selectedCourseId = null;
      this.loadEnrollments();
    }
  }

  onCourseFilterChange(): void {
    if (this.selectedCourseId) {
      this.filterBy = 'course';
      this.selectedEmployeeId = null;
      this.loadEnrollments();
    }
  }

  clearFilters(): void {
    this.filterBy = 'all';
    this.selectedEmployeeId = null;
    this.selectedCourseId = null;
    this.loadEnrollments();
  }

  openEnrollModal(): void {
    this.enrollFormData = {
      employeeId: 0,
      trainingCourseId: 0,
    };
    this.showEnrollModal = true;
  }

  closeEnrollModal(): void {
    this.showEnrollModal = false;
  }

  enrollEmployee(): void {
    if (!this.enrollFormData.employeeId || !this.enrollFormData.trainingCourseId) {
      this.toastService.error('Please select both employee and course');
      return;
    }

    this.isSaving = true;
    this.enrollmentService.enroll(this.enrollFormData).subscribe({
      next: (response) => {
        if (!response.hasError) {
          this.toastService.success('Employee enrolled successfully');
          this.closeEnrollModal();
          this.loadEnrollments();
        } else {
          this.toastService.error(response.errorMessage || 'Failed to enroll employee');
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

  openActionModal(enrollment: EmployeeTraining, action: 'complete' | 'cancel'): void {
    this.selectedEnrollment = enrollment;
    this.actionType = action;
    this.showActionModal = true;
  }

  closeActionModal(): void {
    this.showActionModal = false;
    this.selectedEnrollment = null;
    this.actionType = null;
  }

  confirmAction(): void {
    if (!this.selectedEnrollment || !this.actionType) return;

    this.isSaving = true;
    const { employeeId, trainingCourseId } = this.selectedEnrollment;

    const action$ =
      this.actionType === 'complete'
        ? this.enrollmentService.complete(employeeId, trainingCourseId)
        : this.enrollmentService.cancel(employeeId, trainingCourseId);

    action$.subscribe({
      next: (response) => {
        if (!response.hasError) {
          const actionText = this.actionType === 'complete' ? 'completed' : 'cancelled';
          this.toastService.success(`Training ${actionText} successfully`);
          this.closeActionModal();
          this.loadEnrollments();
        } else {
          this.toastService.error(response.errorMessage || `Failed to ${this.actionType} training`);
        }
        this.isSaving = false;
      },
      error: () => {
        this.toastService.error(`An error occurred while processing the request`);
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

  getStatusBadgeClass(status: TrainingStatus): string {
    switch (status) {
      case 'Enrolled':
        return 'bg-primary bg-opacity-10 text-primary';
      case 'Completed':
        return 'bg-success bg-opacity-10 text-success';
      case 'Cancelled':
        return 'bg-secondary bg-opacity-10 text-secondary';
      default:
        return 'bg-light text-dark';
    }
  }

  getStatusIcon(status: TrainingStatus): string {
    switch (status) {
      case 'Enrolled':
        return 'eva-clock-outline';
      case 'Completed':
        return 'eva-checkmark-circle-2-outline';
      case 'Cancelled':
        return 'eva-close-circle-outline';
      default:
        return 'eva-question-mark-circle-outline';
    }
  }

  formatDate(dateString?: string): string {
    if (!dateString) return 'N/A';
    return new Date(dateString).toLocaleDateString('en-US', {
      year: 'numeric',
      month: 'short',
      day: 'numeric',
    });
  }

  getEmployeeName(employeeId: number): string {
    const employee = this.employees.find((e) => e.id === employeeId);
    return employee ? `${employee.firstName} ${employee.lastName}` : 'Unknown';
  }

  getCourseName(courseId: number): string {
    const course = this.courses.find((c) => c.id === courseId);
    return course ? course.title : 'Unknown';
  }

  getEmployeeFullName(employee: EmployeeModel): string {
    return `${employee.firstName} ${employee.lastName}`;
  }

  getEnrolledCount(): number {
    return this.enrollments.filter((e) => e.status === 'Enrolled').length;
  }

  getCompletedCount(): number {
    return this.enrollments.filter((e) => e.status === 'Completed').length;
  }

  getCancelledCount(): number {
    return this.enrollments.filter((e) => e.status === 'Cancelled').length;
  }
}
