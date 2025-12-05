import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LeaveApprovalService } from '../../../Services/leave-approval.service';
import { LeaveRequest as LeaveRequestService } from '../../../Services/leave-request/leave-request';
import { Employee as EmployeeAuthService } from '../../../Services/employee/employee';
import { LeaveApproval } from '../../../models/leaveApproval';
import { LeaveRequest } from '../../../models/leaveRequest';

@Component({
    selector: 'app-my-approvals',
    standalone: true,
    imports: [CommonModule],
    template: `
    <div class="container-fluid p-4">
      <h2>My Approvals</h2>
      
      <div class="card shadow-sm mt-4">
        <div class="card-body p-0">
            <div *ngIf="isLoading" class="p-4 text-center">
                <div class="spinner-border text-primary" role="status">
                    <span class="visually-hidden">Loading...</span>
                </div>
            </div>

            <div *ngIf="!isLoading && approvals.length === 0" class="p-4 text-center text-muted">
                No pending approvals found.
            </div>

            <div *ngIf="!isLoading && approvals.length > 0" class="table-responsive">
                <table class="table table-hover align-middle mb-0">
                    <thead>
                        <tr>
                            <th>Request ID</th>
                            <th>Employee</th>
                            <th>Leave Type</th>
                            <th>Dates</th>
                            <th>Status</th>
                            <th>Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr *ngFor="let item of enrichedApprovals">
                            <td>#{{ item.approval.leaveRequestId }}</td>
                            <td>{{ item.request?.employeeName || 'Loading...' }}</td>
                            <td>{{ item.request?.leaveTypeName || 'Loading...' }}</td>
                            <td>
                                <div *ngIf="item.request">
                                    {{ item.request.startDate | date }} - {{ item.request.endDate | date }}
                                    <br><small class="text-muted">{{ item.request.totalDays }} days</small>
                                </div>
                            </td>
                            <td>
                                <span class="badge" [ngClass]="getStatusBadgeClass(item.approval.status)">
                                    {{ getStatusText(item.approval.status) }}
                                </span>
                            </td>
                            <td>
                                <div class="btn-group" *ngIf="item.approval.status === 0">
                                    <button class="btn btn-sm btn-success" (click)="approve(item.approval)" title="Approve">
                                        <i class="eva eva-checkmark-outline"></i>
                                    </button>
                                    <button class="btn btn-sm btn-danger" (click)="reject(item.approval)" title="Reject">
                                        <i class="eva eva-close-outline"></i>
                                    </button>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
      </div>
    </div>
  `
})
export class MyApprovalsComponent implements OnInit {
    private leaveApprovalService = inject(LeaveApprovalService);
    private leaveRequestService = inject(LeaveRequestService);
    private employeeService = inject(EmployeeAuthService);

    approvals: LeaveApproval[] = [];
    enrichedApprovals: { approval: LeaveApproval, request?: LeaveRequest }[] = [];
    isLoading = true;

    ngOnInit() {
        this.loadApprovals();
    }

    loadApprovals() {
        this.isLoading = true;
        const approverId = this.employeeService.getEmployeeId();

        if (!approverId) {
            console.error('No approver ID found');
            this.isLoading = false;
            return;
        }

        this.leaveApprovalService.getByApprover(approverId).subscribe({
            next: (res) => {
                if (!res.hasError && res.data) {
                    this.approvals = res.data;
                    this.enrichApprovals();
                }
                this.isLoading = false;
            },
            error: (err) => {
                console.error(err);
                this.isLoading = false;
            }
        });
    }

    enrichApprovals() {
        this.enrichedApprovals = this.approvals.map(a => ({ approval: a }));

        this.enrichedApprovals.forEach(item => {
            this.leaveRequestService.getById(item.approval.leaveRequestId).subscribe({
                next: (res) => {
                    if (!res.hasError && res.data) {
                        item.request = res.data;
                    }
                }
            });
        });
    }

    approve(approval: LeaveApproval) {
        if (!confirm('Approve this request?')) return;
        const action = {
            leaveRequestId: approval.leaveRequestId,
            approverId: approval.approverId,
            level: 1
        };
        this.leaveApprovalService.approve(action).subscribe({
            next: (res) => {
                if (res.data) {
                    alert('Approved successfully');
                    this.loadApprovals();
                } else {
                    alert('Failed to approve');
                }
            }
        });
    }

    reject(approval: LeaveApproval) {
        const reason = prompt('Enter rejection reason:');
        if (reason === null) return;

        const action = {
            leaveRequestId: approval.leaveRequestId,
            approverId: approval.approverId,
            level: 1
        };
        this.leaveApprovalService.reject(action).subscribe({
            next: (res) => {
                if (res.data) {
                    alert('Rejected successfully');
                    this.loadApprovals();
                } else {
                    alert('Failed to reject');
                }
            }
        });
    }

    getStatusBadgeClass(status: number): string {
        switch (status) {
            case 0: return 'bg-warning text-dark';
            case 1: return 'bg-success';
            case 2: return 'bg-danger';
            default: return 'bg-secondary';
        }
    }

    getStatusText(status: number): string {
        switch (status) {
            case 0: return 'Pending';
            case 1: return 'Approved';
            case 2: return 'Rejected';
            default: return 'Unknown';
        }
    }
}
