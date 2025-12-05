import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Employee as EmployeeService } from '../../../Services/employee/employee';
import { Employee as EmployeeModel } from '../../../models/employee';
import { Employee as EmployeeServ } from '../../../Services/employee';
import { LeaveRequest as LeaveRequestService } from '../../../Services/leave-request/leave-request';
import { LeaveRequest as LeaveRequestModel, CreateLeaveRequest } from '../../../models/leaveRequest';
import { LeaveType as LeaveTypeService } from '../../../Services/leave-types/leave-type';
import { LeaveType as LeaveTypeModel } from '../../../models/leaveType';

@Component({
  selector: 'app-employee-leave-request',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './Employee-leave-request.html',
  styleUrl: './Employee-leave-request.css',
})
export class EmployeeLeaveRequest implements OnInit {
  private leaveRequestService = inject(LeaveRequestService);
  private employeeService = inject(EmployeeService);
  private employeeServ = inject(EmployeeServ);
  private leaveTypeService = inject(LeaveTypeService);
  private fb = inject(FormBuilder);

  leaveRequests: LeaveRequestModel[] = [];
  leaveTypes: LeaveTypeModel[] = [];
  isLoading = true;
  error: string | null = null;
  showForm = false;
  createForm: FormGroup;
  isSubmitting = false;
  editingRequestId: number | null = null;

  constructor() {
    this.createForm = this.fb.group({
      leaveTypeId: ['', Validators.required],
      startDate: ['', Validators.required],
      endDate: ['', Validators.required],
      reason: ['']
    });
  }

  ngOnInit() {
    this.loadData();
  }

  loadData() {
    this.isLoading = true;
    const employeeId = this.employeeService.getEmployeeId();

    // Load Leave Types
    this.leaveTypeService.getLeaveTypes().subscribe({
      next: (res) => {
        if (!res.hasError && res.data) {
          this.leaveTypes = res.data;
        }
      }
    });

    // Load Requests
    this.leaveRequestService.getEmployeeLeaveRequests(employeeId).subscribe({
      next: (response) => {
        if (!response.hasError && response.data) {
          this.leaveRequests = response.data;
          this.enrichLeaveRequests();
        } else {
          this.error = response.errorMessage || 'Failed to load leave requests';
        }
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Error loading leave requests', err);
        this.error = 'An error occurred while loading leave requests';
        this.isLoading = false;
      },
    });
  }

  enrichLeaveRequests() {
    this.leaveRequests.forEach((leaveRequest) => {
      if (!leaveRequest.leaveTypeName) {
        this.leaveTypeService.getLeaveType(leaveRequest.leaveTypeId).subscribe({
          next: (response) => {
            if (!response.hasError && response.data) {
              leaveRequest.leaveTypeName = response.data.name;
            }
          }
        });
      }

      if (leaveRequest.reviewedById && !leaveRequest.reviewedBy) {
        this.employeeServ.getEmployee(leaveRequest.reviewedById).subscribe({
          next: (response) => {
            if (!response.hasError && response.data) {
              leaveRequest.reviewedBy = response.data.firstName + ' ' + response.data.lastName;
            }
          }
        });
      }
    });
  }

  toggleForm() {
    this.showForm = !this.showForm;
    if (!this.showForm) {
      this.createForm.reset();
      this.editingRequestId = null;
    }
  }

  cancelRequest(id: number) {
    if (!confirm('Are you sure you want to cancel this request?')) return;

    this.leaveRequestService.delete(id).subscribe({
      next: (res) => {
        if (!res.hasError) {
          alert('Request cancelled successfully');
          this.loadData();
        } else {
          alert('Failed to cancel: ' + res.errorMessage);
        }
      },
      error: () => alert('Error cancelling request')
    });
  }

  editRequest(request: LeaveRequestModel) {
    this.showForm = true;
    this.editingRequestId = request.id;
    this.createForm.patchValue({
      leaveTypeId: request.leaveTypeId,
      startDate: request.startDate.split('T')[0],
      endDate: request.endDate.split('T')[0],
      reason: request.reason
    });
  }

  onSubmit() {
    if (this.createForm.invalid) return;

    this.isSubmitting = true;
    const formValue = this.createForm.value;

    if (this.editingRequestId) {
      const updateRequest = {
        leaveTypeId: Number(formValue.leaveTypeId),
        startDate: formValue.startDate,
        endDate: formValue.endDate,
        reason: formValue.reason
      };
      this.leaveRequestService.update(this.editingRequestId, updateRequest).subscribe({
        next: (res) => {
          if (!res.hasError) {
            this.showForm = false;
            this.createForm.reset();
            this.editingRequestId = null;
            this.loadData();
            alert('Request updated successfully');
          } else {
            alert('Failed to update: ' + res.errorMessage);
          }
          this.isSubmitting = false;
        },
        error: () => {
          alert('Error updating request');
          this.isSubmitting = false;
        }
      });
    } else {
      const request: CreateLeaveRequest = {
        employeeId: this.employeeService.getEmployeeId(),
        leaveTypeId: Number(formValue.leaveTypeId),
        startDate: formValue.startDate,
        endDate: formValue.endDate,
        reason: formValue.reason
      };

      this.leaveRequestService.create(request).subscribe({
        next: (res) => {
          if (!res.hasError) {
            this.showForm = false;
            this.createForm.reset();
            this.loadData();
            alert('Leave request submitted successfully');
          } else {
            alert('Failed to submit request: ' + res.errorMessage);
          }
          this.isSubmitting = false;
        },
        error: (err) => {
          alert('Error submitting request');
          this.isSubmitting = false;
        }
      });
    }
  }

  getStatusBadgeClass(status: number): string {
    switch (status) {
      case 0: return 'bg-warning text-dark';
      case 1: return 'bg-success';
      case 2: return 'bg-danger';
      case 3: return 'bg-secondary';
      default: return 'bg-secondary';
    }
  }

  getStatusText(status: number): string {
    switch (status) {
      case 0: return 'Pending';
      case 1: return 'Approved';
      case 2: return 'Rejected';
      case 3: return 'Cancelled';
      default: return 'Unknown';
    }
  }
}
