import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { LeaveType as LeaveTypeService } from '../../../Services/leave-types/leave-type';
import { LeaveRequest as LeaveRequestService } from '../../../Services/leave-request/leave-request';
import { LeaveBalance as LeaveBalanceService } from '../../../Services/leave-balance/leave-balance';
import { Employee as EmployeeIdService } from '../../../Services/employee/employee';
import { Employee as EmployeeDetailService } from '../../../Services/employee';
import { LeaveType as LeaveTypeModel } from '../../../models/leaveType';
import { LeaveBalance as LeaveBalanceModel } from '../../../models/leaveBalance';
import { Employee as EmployeeModel } from '../../../models/employee';

@Component({
  selector: 'app-add-leave-request',
  imports: [CommonModule, FormsModule],
  templateUrl: './add-leave-request.html',
  styleUrl: './add-leave-request.css',
})
export class AddLeaveRequest implements OnInit {
  private leaveTypeService = inject(LeaveTypeService);
  private leaveRequestService = inject(LeaveRequestService);
  private leaveBalanceService = inject(LeaveBalanceService);
  private employeeIdService = inject(EmployeeIdService);
  private employeeDetailService = inject(EmployeeDetailService);
  private router = inject(Router);

  leaveTypes: LeaveTypeModel[] = [];
  leaveBalances: Map<number, LeaveBalanceModel> = new Map();
  employee: EmployeeModel | null = null;
  payload = {
    leaveTypeId: 0,
    startDate: '',
    endDate: '',
  };
  isSubmitting = false;
  error: string | null = null;
  calculatedDays = 0;

  ngOnInit() {
    this.loadEmployeeData();
    this.loadLeaveTypes();
    this.loadLeaveBalances();
  }

  loadEmployeeData() {
    const empId = this.employeeIdService.getEmployeeId();
    this.employeeDetailService.getEmployee(empId).subscribe({
      next: (res) => {
        if (!res.hasError && res.data) {
          this.employee = res.data;
        }
      },
      error: (err) => console.error('Error loading employee', err),
    });
  }

  loadLeaveTypes() {
    this.leaveTypeService.getLeaveTypes().subscribe({
      next: (res) => {
        if (!res.hasError && res.data) this.leaveTypes = res.data;
        else this.error = res.errorMessage || 'Failed to load leave types';
      },
      error: (err) => {
        console.error(err);
        this.error = 'Error loading leave types';
      },
    });
  }

  loadLeaveBalances() {
    const empId = this.employeeIdService.getEmployeeId();
    this.leaveBalanceService.getLeaveBalances(empId).subscribe({
      next: (res) => {
        if (!res.hasError && res.data) {
          res.data.forEach((lb) => {
            this.leaveBalances.set(lb.leaveTypeId, lb);
          });
        }
      },
      error: (err) => console.error('Error loading leave balances', err),
    });
  }

  onDateChange() {
    if (this.payload.startDate && this.payload.endDate) {
      const start = new Date(this.payload.startDate);
      const end = new Date(this.payload.endDate);
      const diffTime = Math.abs(end.getTime() - start.getTime());
      this.calculatedDays = Math.ceil(diffTime / (1000 * 60 * 60 * 24)) + 1; // Include both start and end dates
    }
  }

  getAvailableBalance(leaveTypeId: number): number {
    const balance = this.leaveBalances.get(leaveTypeId);
    return balance ? balance.remainingDays : 0;
  }

  submit() {
    this.error = null;
    if (!this.payload.leaveTypeId) {
      this.error = 'Please select a leave type';
      return;
    }
    if (!this.payload.startDate || !this.payload.endDate) {
      this.error = 'Please select start and end dates';
      return;
    }
    if (this.payload.startDate > this.payload.endDate) {
      this.error = 'End date must be after start date';
      return;
    }

    const available = this.getAvailableBalance(Number(this.payload.leaveTypeId));
    if (this.calculatedDays > available) {
      this.error = `Not enough leave balance. Available: ${available} days, Requested: ${this.calculatedDays} days`;
      return;
    }

    this.isSubmitting = true;
    const body = {
      employeeId: this.employeeIdService.getEmployeeId(),
      leaveTypeId: Number(this.payload.leaveTypeId),
      startDate: this.payload.startDate,
      endDate: this.payload.endDate,
    };

this.leaveRequestService.submitLeaveRequest(body).subscribe({
  next: (res) => {
    this.isSubmitting = false;
    if (!res.hasError) {
      alert('✔️ Leave request submitted successfully');
      this.router.navigate(['/pages/ess/leave-requests']);
    } else {
      this.error = res.errorMessage || 'Failed to submit leave request';
    }
  },
  error: (err: any) => {
    console.error('Submit error:', err);
    this.isSubmitting = false;
    if (err.error?.errorMessage) {
      this.error = err.error.errorMessage;
    } else if (err.status === 0) {
      this.error = 'Cannot reach backend. Check if API is running and URL is correct.';
    } else {
      this.error = `Error submitting leave request (status: ${err.status})`;
    }
  },
});

  }
  cancel() {
    this.router.navigate(['/pages/ess/leave-requests']);
  }
}
