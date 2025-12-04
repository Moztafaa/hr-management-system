import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { LeaveRequest as LeaveRequestService } from '../../../Services/leave-request/leave-request';
import { LeaveApproval as LeaveApprovalService } from '../../../Services/leave-approvals/leave-approval';
import { Employee as EmployeeIdService } from '../../../Services/employee/employee';
import { Employee as EmployeeDetailService } from '../../../Services/employee';
import { LeaveRequest as LeaveRequestModel } from '../../../models/leaveRequest';
import { LeaveType as LeaveTypeService } from '../../../Services/leave-types/leave-type';

@Component({
  selector: 'app-leave-approvals',
  imports: [CommonModule],
  templateUrl: './leave-approvals.html',
  styleUrl: './leave-approvals.css',
})
export class LeaveApprovals implements OnInit {
  private leaveRequestService = inject(LeaveRequestService);
  private leaveApprovalService = inject(LeaveApprovalService);
  private employeeIdService = inject(EmployeeIdService);
  private employeeDetailService = inject(EmployeeDetailService);
  private leaveTypeService = inject(LeaveTypeService);

  approvals: LeaveRequestModel[] = [];
  isLoading = true;
  error: string | null = null;

  ngOnInit() {
    this.loadApprovals();
  }

  loadApprovals() {
    this.isLoading = true;
    const approverId = this.employeeIdService.getEmployeeId();
    this.leaveRequestService.getLeaveRequestsByReviewer(approverId).subscribe({
      next: (res) => {
        if (!res.hasError && res.data) {
          this.approvals = res.data;
          // Load leave type names and employee names
          this.approvals.forEach((lr) => {
            this.leaveTypeService.getLeaveType(lr.leaveTypeId).subscribe({
              next: (r) => {
                if (!r.hasError && r.data) lr.leaveTypeName = r.data.name;
              },
              error: () => { /* ignore */ },
            });

            this.employeeDetailService.getEmployee(lr.employeeId).subscribe({
              next: (r: any) => {
                if (!r.hasError && r.data) {
                  lr.employeeName = r.data.firstName + ' ' + r.data.lastName;
                }
              },
              error: () => { /* ignore */ },
            });
          });
        } else {
          this.error = res.errorMessage || 'Failed to load approvals';
        }
        this.isLoading = false;
      },
      error: (err) => {
        console.error(err);
        this.error = 'Error loading approvals';
        this.isLoading = false;
      },
    });
  }

  getStatusBadge(status: number): string {
    const statusMap: { [key: number]: string } = {
      0: 'Pending',
      1: 'Approved',
      2: 'Rejected',
    };
    return statusMap[status] || 'Unknown';
  }

  getStatusClass(status: number): string {
    const classMap: { [key: number]: string } = {
      0: 'bg-warning',
      1: 'bg-success',
      2: 'bg-danger',
    };
    return classMap[status] || 'bg-secondary';
  }

  approve(leaveRequestId: number) {
    if (!confirm('Are you sure you want to approve this leave request?')) return;

    const dto = { leaveRequestId, approverId: this.employeeIdService.getEmployeeId(), level: 1 };
    this.leaveApprovalService.approve(dto).subscribe({
      next: (res) => {
        if (!res.hasError) {
          alert('✔️ Leave approved');
          this.loadApprovals();
        } else {
          alert(res.errorMessage || 'Failed to approve');
        }
      },
      error: (err) => {
        console.error(err);
        alert('Error approving leave request');
      },
    });
  }

  reject(leaveRequestId: number) {
    if (!confirm('Are you sure you want to reject this leave request?')) return;

    const dto = { leaveRequestId, approverId: this.employeeIdService.getEmployeeId(), level: 1 };
    this.leaveApprovalService.reject(dto).subscribe({
      next: (res) => {
        if (!res.hasError) {
          alert('❌ Leave rejected');
          this.loadApprovals();
        } else {
          alert(res.errorMessage || 'Failed to reject');
        }
      },
      error: (err) => {
        console.error(err);
        alert('Error rejecting leave request');
      },
    });
  }
}

