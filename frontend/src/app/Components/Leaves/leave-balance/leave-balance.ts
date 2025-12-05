import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { LeaveBalance as LeaveBalanceService } from '../../../Services/leave-balance/leave-balance';
import { LeaveType as LeaveTypeService } from '../../../Services/leave-types/leave-type';
import { LeaveBalance as LeaveBalanceModel } from '../../../models/leaveBalance';
import { Employee as EmployeeService } from '../../../Services/employee/employee';
@Component({
  selector: 'app-leave-balance',
  imports: [CommonModule],
  templateUrl: './leave-balance.html',
  styleUrl: './leave-balance.css',
})
export class LeaveBalance implements OnInit {
  private leaveBalanceService = inject(LeaveBalanceService);
  private employeeService = inject(EmployeeService);
  private leaveTypeService = inject(LeaveTypeService);
  leaveBalances: LeaveBalanceModel[] = [];
  isLoading = true;
  error: string | null = null;

  ngOnInit() {
    this.loadLeaveBalances();
  }

  loadLeaveBalances() {
    this.isLoading = true;
    this.leaveBalanceService.getLeaveBalances(this.employeeService.getEmployeeId()).subscribe({
      next: (response) => {
        if (!response.hasError && response.data) {
          this.leaveBalances = response.data;
          this.leaveBalances.forEach((leaveBalance) => {
            this.leaveTypeService.getLeaveType(leaveBalance.leaveTypeId).subscribe({
              next: (response) => {
                if (!response.hasError && response.data) {
                  leaveBalance.leaveTypeName = response.data.name;
                }
              },
              error: (err) => {
                console.error('Error loading leave type', err);
                this.error = 'An error occurred while loading leave type';
              },
            });
          });
        } else {
          this.error = response.errorMessage || 'Failed to load leave balances';
        }
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Error loading leave balances', err);
        this.error = 'An error occurred while loading leave balances';
        this.isLoading = false;
      },
    });
  }
}
