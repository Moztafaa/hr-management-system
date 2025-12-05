import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ESSService, ESSTrainingRequestCreate } from '../../../Services/ess.service';
import { ToastService } from '../../../Services/toast.service';
import {
  EmployeeTraining,
  TrainingRequest,
  TrainingRequestStatus,
  TrainingCourse,
  TrainingStatus,
} from '../../../models/training';

type TabType = 'courses' | 'requests' | 'available';

@Component({
  selector: 'app-ess-training',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './ess-training.component.html',
  styleUrls: ['./ess-training.component.css'],
})
export class EssTrainingComponent implements OnInit {
  private essService = inject(ESSService);
  private toastService = inject(ToastService);

  // Data
  myCourses: EmployeeTraining[] = [];
  myRequests: TrainingRequest[] = [];
  availableCourses: TrainingCourse[] = [];

  // UI State
  activeTab: TabType = 'courses';
  isLoading = true;
  error: string | null = null;

  // Modal State
  showRequestModal = false;
  showCancelModal = false;
  isSubmitting = false;

  // Form Data
  selectedCourse: TrainingCourse | null = null;
  employeeNote = '';

  // Cancel Modal
  requestToCancel: TrainingRequest | null = null;

  // Expose enums to template
  TrainingRequestStatus = TrainingRequestStatus;

  ngOnInit(): void {
    this.loadData();
  }

  loadData(): void {
    this.isLoading = true;
    this.error = null;

    Promise.all([this.loadMyCourses(), this.loadMyRequests(), this.loadAvailableCourses()]).finally(
      () => {
        this.isLoading = false;
      }
    );
  }

  private loadMyCourses(): Promise<void> {
    return new Promise((resolve) => {
      this.essService.getMyCourses().subscribe({
        next: (response) => {
          if (!response.hasError && response.data) {
            this.myCourses = response.data;
          } else {
            this.error = response.errorMessage || 'Failed to load courses';
          }
          resolve();
        },
        error: () => {
          this.error = 'An error occurred while loading courses';
          resolve();
        },
      });
    });
  }

  private loadMyRequests(): Promise<void> {
    return new Promise((resolve) => {
      this.essService.getMyTrainingRequests().subscribe({
        next: (response) => {
          if (!response.hasError && response.data) {
            this.myRequests = response.data;
          }
          resolve();
        },
        error: () => resolve(),
      });
    });
  }

  private loadAvailableCourses(): Promise<void> {
    return new Promise((resolve) => {
      this.essService.getAvailableCourses().subscribe({
        next: (response) => {
          if (!response.hasError && response.data) {
            this.availableCourses = response.data;
          }
          resolve();
        },
        error: () => resolve(),
      });
    });
  }

  setActiveTab(tab: TabType): void {
    this.activeTab = tab;
  }

  // Request Modal
  openRequestModal(course: TrainingCourse): void {
    this.selectedCourse = course;
    this.employeeNote = '';
    this.showRequestModal = true;
  }

  closeRequestModal(): void {
    this.showRequestModal = false;
    this.selectedCourse = null;
    this.employeeNote = '';
  }

  submitRequest(): void {
    if (!this.selectedCourse) return;

    this.isSubmitting = true;
    const request: ESSTrainingRequestCreate = {
      trainingCourseId: this.selectedCourse.id,
      employeeNote: this.employeeNote || undefined,
    };

    this.essService.submitTrainingRequest(request).subscribe({
      next: (response) => {
        if (!response.hasError) {
          this.toastService.success('Training request submitted successfully');
          this.closeRequestModal();
          this.loadData();
        } else {
          this.toastService.error(response.errorMessage || 'Failed to submit request');
        }
        this.isSubmitting = false;
      },
      error: () => {
        this.toastService.error('An error occurred while submitting the request');
        this.isSubmitting = false;
      },
    });
  }

  // Cancel Modal
  openCancelModal(request: TrainingRequest): void {
    this.requestToCancel = request;
    this.showCancelModal = true;
  }

  closeCancelModal(): void {
    this.showCancelModal = false;
    this.requestToCancel = null;
  }

  confirmCancel(): void {
    if (!this.requestToCancel) return;

    this.isSubmitting = true;
    this.essService.cancelTrainingRequest(this.requestToCancel.id).subscribe({
      next: (response) => {
        if (!response.hasError) {
          this.toastService.success('Training request cancelled successfully');
          this.closeCancelModal();
          this.loadData();
        } else {
          this.toastService.error(response.errorMessage || 'Failed to cancel request');
        }
        this.isSubmitting = false;
      },
      error: () => {
        this.toastService.error('An error occurred while cancelling the request');
        this.isSubmitting = false;
      },
    });
  }

  // Helper method to convert string status to enum
  private getStatusEnum(status: string | TrainingRequestStatus): TrainingRequestStatus {
    if (typeof status === 'number') {
      return status;
    }
    
    switch (status?.toLowerCase()) {
      case 'pending':
      case '0':
        return TrainingRequestStatus.Pending;
      case 'approved':
      case '1':
        return TrainingRequestStatus.Approved;
      case 'rejected':
      case '2':
        return TrainingRequestStatus.Rejected;
      default:
        return TrainingRequestStatus.Pending;
    }
  }

  // Check if status is pending
  isPending(status: string | TrainingRequestStatus): boolean {
    return this.getStatusEnum(status) === TrainingRequestStatus.Pending;
  }

  // Updated helper methods to handle both string and enum
  getStatusBadgeClass(status: string | TrainingRequestStatus): string {
    const statusEnum = this.getStatusEnum(status);
    
    switch (statusEnum) {
      case TrainingRequestStatus.Pending:
        return 'status-pending';
      case TrainingRequestStatus.Approved:
        return 'status-approved';
      case TrainingRequestStatus.Rejected:
        return 'status-rejected';
      default:
        return '';
    }
  }

  getStatusText(status: string | TrainingRequestStatus): string {
    const statusEnum = this.getStatusEnum(status);
    
    switch (statusEnum) {
      case TrainingRequestStatus.Pending:
        return 'Pending';
      case TrainingRequestStatus.Approved:
        return 'Approved';
      case TrainingRequestStatus.Rejected:
        return 'Rejected';
      default:
        return 'Unknown';
    }
  }

  getCourseStatusClass(status: TrainingStatus): string {
    switch (status) {
      case 'Enrolled':
        return 'status-enrolled';
      case 'Completed':
        return 'status-completed';
      case 'Cancelled':
        return 'status-cancelled';
      default:
        return '';
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

  // Updated stats methods to handle string status
  get enrolledCount(): number {
    return this.myCourses.filter((c) => c.status === 'Enrolled').length;
  }

  get completedCount(): number {
    return this.myCourses.filter((c) => c.status === 'Completed').length;
  }

  get cancelledCount(): number {
    return this.myCourses.filter((c) => c.status === 'Cancelled').length;
  }

  get pendingRequestsCount(): number {
    return this.myRequests.filter((r) => this.isPending(r.status)).length;
  }

  get approvedRequestsCount(): number {
    return this.myRequests.filter((r) => 
      this.getStatusEnum(r.status) === TrainingRequestStatus.Approved
    ).length;
  }

  get rejectedRequestsCount(): number {
    return this.myRequests.filter((r) => 
      this.getStatusEnum(r.status) === TrainingRequestStatus.Rejected
    ).length;
  }
}