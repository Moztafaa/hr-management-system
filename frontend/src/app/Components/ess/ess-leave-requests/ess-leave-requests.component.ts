import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ESSService } from '../../../Services/ess.service';
import { LeaveTypeService } from '../../../Services/leave-type.service';
import { ToastService } from '../../../Services/toast.service';
import { ApiResponse } from '../../../models/api-response';
import {
  LeaveRequest,
  CreateLeaveRequest,
  LeaveStatus,
  LeaveStatusLabels,
  LeaveBalance,
  LeaveType,
} from '../../../models/ess';

@Component({
  selector: 'app-ess-leave-requests',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './ess-leave-requests.component.html',
  styleUrls: ['./ess-leave-requests.component.css'],
})
export class EssLeaveRequestsComponent implements OnInit {
  private essService = inject(ESSService);
  private leaveTypeService = inject(LeaveTypeService);
  private toastService = inject(ToastService);
  private fb = inject(FormBuilder);
  private route = inject(ActivatedRoute);
  private http = inject(HttpClient);

  leaveRequests: LeaveRequest[] = [];
  leaveBalances: LeaveBalance[] = [];
  leaveTypes: LeaveType[] = [];

  isLoading = true;
  isSubmitting = false;
  showModal = false;
  error: string | null = null;

  LeaveStatus = LeaveStatus;
  LeaveStatusLabels = LeaveStatusLabels;

  leaveForm: FormGroup = this.fb.group({
    leaveTypeId: ['', Validators.required],
    startDate: ['', Validators.required],
    endDate: ['', Validators.required],
    reason: [''],
  });

  ngOnInit(): void {
    this.loadData();

    // Check if we should open the modal
    this.route.queryParams.subscribe((params) => {
      if (params['action'] === 'new') {
        this.openModal();
      }
    });
  }

  loadData(): void {
    this.isLoading = true;
    this.error = null;

    // Load all data in parallel
    Promise.all([
      this.loadLeaveRequests(),
      this.loadLeaveBalances(),
      this.loadLeaveTypes(),
    ]).finally(() => {
      this.isLoading = false;
    });
  }

  private loadLeaveRequests(): Promise<void> {
    return new Promise((resolve) => {
      this.essService.getLeaveRequests().subscribe({
        next: (response) => {
          if (!response.hasError && response.data) {
            this.leaveRequests = response.data;
          } else {
            this.error = response.errorMessage || 'Failed to load leave requests';
          }
          resolve();
        },
        error: () => {
          this.error = 'An error occurred while loading leave requests';
          resolve();
        },
      });
    });
  }

  private loadLeaveBalances(): Promise<void> {
    return new Promise((resolve) => {
      this.essService.getLeaveBalance().subscribe({
        next: (response) => {
          if (!response.hasError && response.data) {
            this.leaveBalances = response.data;
          }
          resolve();
        },
        error: () => resolve(),
      });
    });
  }

  private loadLeaveTypes(): Promise<void> {
    return new Promise((resolve) => {
      this.leaveTypeService.getAll().subscribe({
        next: (response) => {
          if (!response.hasError && response.data) {
            this.leaveTypes = response.data;
          }
          resolve();
        },
        error: () => resolve(),
      });
    });
  }

  openModal(): void {
    this.showModal = true;
    this.leaveForm.reset();
    // Set default dates
    const today = new Date().toISOString().split('T')[0];
    this.leaveForm.patchValue({
      startDate: today,
      endDate: today,
    });
  }

  closeModal(): void {
    this.showModal = false;
    this.leaveForm.reset();
  }

  submitLeaveRequest(): void {
    if (this.leaveForm.invalid) {
      this.leaveForm.markAllAsTouched();
      return;
    }

    const formValue = this.leaveForm.value;

    // Validate date range
    const startDate = new Date(formValue.startDate);
    const endDate = new Date(formValue.endDate);

    if (endDate < startDate) {
      this.toastService.error('End date cannot be before start date');
      return;
    }

    this.isSubmitting = true;

    const request: CreateLeaveRequest = {
      employeeId: 0, // Will be set by backend from JWT
      leaveTypeId: parseInt(formValue.leaveTypeId),
      startDate: formValue.startDate,
      endDate: formValue.endDate,
      reason: formValue.reason || undefined,
    };

    this.essService.submitLeaveRequest(request).subscribe({
      next: (response) => {
        if (!response.hasError) {
          this.toastService.success('Leave request submitted successfully');
          this.closeModal();
          this.loadData();
        } else {
          this.toastService.error(response.errorMessage || 'Failed to submit leave request');
        }
        this.isSubmitting = false;
      },
      error: () => {
        this.toastService.error('An error occurred while submitting the request');
        this.isSubmitting = false;
      },
    });
  }

  getLeaveTypeName(leaveTypeId: number): string {
    const leaveType = this.leaveTypes.find((lt) => lt.id === leaveTypeId);
    return leaveType?.name || `Leave Type ${leaveTypeId}`;
  }

  getBalance(leaveTypeId: number): LeaveBalance | undefined {
    return this.leaveBalances.find((b) => b.leaveTypeId === leaveTypeId);
  }

  getStatusClass(status: LeaveStatus): string {
    switch (status) {
      case LeaveStatus.Pending:
        return 'status-pending';
      case LeaveStatus.Approved:
        return 'status-approved';
      case LeaveStatus.Rejected:
        return 'status-rejected';
      default:
        return '';
    }
  }

  formatDate(dateString: string): string {
    return new Date(dateString).toLocaleDateString('en-US', {
      year: 'numeric',
      month: 'short',
      day: 'numeric',
    });
  }

  calculateDays(): number {
    const startDate = this.leaveForm.get('startDate')?.value;
    const endDate = this.leaveForm.get('endDate')?.value;

    if (!startDate || !endDate) return 0;

    const start = new Date(startDate);
    const end = new Date(endDate);
    const diffTime = Math.abs(end.getTime() - start.getTime());
    const diffDays = Math.ceil(diffTime / (1000 * 60 * 60 * 24)) + 1;

    return diffDays > 0 ? diffDays : 0;
  }

  get pendingCount(): number {
    return this.leaveRequests.filter((r) => r.status === LeaveStatus.Pending).length;
  }

  get approvedCount(): number {
    return this.leaveRequests.filter((r) => r.status === LeaveStatus.Approved).length;
  }

  get rejectedCount(): number {
    return this.leaveRequests.filter((r) => r.status === LeaveStatus.Rejected).length;
  }

  getFieldError(fieldName: string): string | null {
    const control = this.leaveForm.get(fieldName);
    if (control?.touched && control?.errors) {
      if (control.errors['required']) return 'This field is required';
    }
    return null;
  }

  editRequest(request: LeaveRequest): void {
    // For ESS module, we can reopen the modal with the request data
    this.leaveForm.patchValue({
      leaveTypeId: request.leaveTypeId,
      startDate: request.startDate.split('T')[0],
      endDate: request.endDate.split('T')[0],
      reason: request.reason || ''
    });
    this.showModal = true;
  }

  deleteRequest(requestId: number): void {
    if (!confirm('Are you sure you want to delete this leave request? This action cannot be undone.')) {
      return;
    }

    // Call the delete API endpoint
    this.http.delete<ApiResponse<void>>(`https://localhost:7005/api/LeaveRequest/${requestId}`).subscribe({
      next: (response) => {
        if (!response.hasError) {
          this.toastService.success('Leave request deleted successfully');
          this.loadData();
        } else {
          this.toastService.error(response.errorMessage || 'Failed to delete leave request');
        }
      },
      error: () => {
        this.toastService.error('An error occurred while deleting the request');
      },
    });
  }
}
