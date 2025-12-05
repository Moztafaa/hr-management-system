import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Employee as EmployeeService } from '../../../Services/employee/employee';
import { Employee as EmployeeServ } from '../../../Services/employee';
import { LeaveRequest as LeaveRequestService } from '../../../Services/leave-request/leave-request';
import { LeaveRequest as LeaveRequestModel, UpdateLeaveRequest } from '../../../models/leaveRequest';
import { LeaveType as LeaveTypeService } from '../../../Services/leave-types/leave-type';
import { LeaveApprovalService } from '../../../Services/leave-approval.service';
import { LeaveApprovalAction } from '../../../models/leaveApproval';
import { AuthService } from '../../../Services/auth.service';

@Component({
  selector: 'app-leave-requests',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './leave-requests.html',
  styleUrl: './leave-requests.css',
})
export class LeaveRequests implements OnInit {
  private leaveRequestService = inject(LeaveRequestService);
  private employeeService = inject(EmployeeService);
  private employeeServ = inject(EmployeeServ);
  private leaveTypeService = inject(LeaveTypeService);
  private leaveApprovalService = inject(LeaveApprovalService);
  private authService = inject(AuthService);
  private fb = inject(FormBuilder);

  leaveRequests: LeaveRequestModel[] = [];
  isLoading = true;
  error: string | null = null;
  currentUserId: number = 0;

  ngOnInit() {
    this.currentUserId = this.employeeService.getEmployeeId();
    this.loadLeaveRequests();
  }


  loadLeaveRequests() {
    this.isLoading = true;
    this.leaveRequestService.getLeaveRequests().subscribe({
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
      // Load Leave Type Name if not present (though backend might send it)
      if (!leaveRequest.leaveTypeName) {
        this.leaveTypeService.getLeaveType(leaveRequest.leaveTypeId).subscribe({
          next: (response) => {
            if (!response.hasError && response.data) {
              leaveRequest.leaveTypeName = response.data.name;
            }
          }
        });
      }

      // Load Employee Name if not present
      if (!leaveRequest.employeeName) {
        this.employeeServ.getEmployee(leaveRequest.employeeId).subscribe({
          next: (response) => {
            if (!response.hasError && response.data) {
              leaveRequest.employeeName = response.data.firstName + ' ' + response.data.lastName;
            }
          }
        });
      }

      // Load Reviewed By Name if present
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

  approve(request: LeaveRequestModel) {
    if (!confirm('Are you sure you want to approve this leave request?')) return;
    const action: LeaveApprovalAction = {
      leaveRequestId: request.id,
      approverId: this.getApprovalId(request.id)
    };

    this.leaveApprovalService.approve(action).subscribe({
      next: (res) => {
        if (!res.hasError && res.data) {
          alert('Leave request approved successfully');
          this.loadLeaveRequests();
        } else {
          alert('Failed to approve: ' + (res.errorMessage || 'Unknown error'));
        }
      },
      error: () => alert('Error approving request')
    });
  }

  reject(request: LeaveRequestModel) {
    const reason = prompt('Please enter rejection reason:');
    if (!reason) return;

    const action: LeaveApprovalAction = {
      leaveRequestId: request.id,
      approverId: this.getApprovalId(request.id)
    };

    this.leaveApprovalService.reject(action).subscribe({
      next: (res) => {
        if (!res.hasError && res.data) {
          alert('Leave request rejected successfully');
          this.loadLeaveRequests();
        } else {
          alert('Failed to reject: ' + (res.errorMessage || 'Unknown error'));
        }
      },
      error: () => alert('Error rejecting request')
    });
  }



  getApprovalId(requestId: number): number {
    for (let i = 0; i < this.leaveRequests.length; i++) {
      if (this.leaveRequests[i].id == requestId) {
        return this.leaveRequests[i].reviewedById;
      }
    }
    return 0;
  }

  getStatusBadgeClass(status: number): string {
    switch (status) {
      case 0: return 'bg-warning text-dark'; // Pending
      case 1: return 'bg-success'; // Approved
      case 2: return 'bg-danger'; // Rejected
      case 3: return 'bg-secondary'; // Cancelled
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

