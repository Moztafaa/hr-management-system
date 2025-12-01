import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { Employee as EmployeeService } from '../../../Services/employee/employee';
import { Employee as EmployeeModel } from '../../../models/employee';
import { Employee as EmployeeServ } from '../../../Services/employee';
import { LeaveRequest as LeaveRequestService } from '../../../Services/leave-request/leave-request';
import { LeaveRequest as LeaveRequestModel } from '../../../models/leaveRequest';
import { LeaveType as LeaveTypeService } from '../../../Services/leave-types/leave-type';
@Component({
  selector: 'app-employee-leave-request',
  imports: [CommonModule],
  templateUrl: './Employee-leave-request.html',
  styleUrl: './Employee-leave-request.css',
})
export class EmployeeLeaveRequest implements OnInit {
  private leaveRequestService = inject(LeaveRequestService);
  private employeeService = inject(EmployeeService);
  private employeeServ = inject(EmployeeServ);
  private leaveTypeService = inject(LeaveTypeService);
  leaveRequests: LeaveRequestModel[] = [];
  isLoading = true;
  error: string | null = null;

  ngOnInit() {
    this.loadLeaveBalances();
  }

  loadLeaveBalances() {
    this.isLoading = true;
    this.leaveRequestService.getEmployeeLeaveRequests(this.employeeService.getEmployeeId()).subscribe({
      next: (response) => {
        if (!response.hasError && response.data) {
          this.leaveRequests = response.data;
          this.leaveRequests.forEach((leaveRequest) => {
            this.leaveTypeService.getLeaveType(leaveRequest.leaveTypeId).subscribe({
              next: (response) => {
                if (!response.hasError && response.data) {
                  leaveRequest.leaveTypeName = response.data.name;
                }
              },
              error: (err) => {
                console.error('Error loading leave type', err);
                this.error = 'An error occurred while loading leave type';
              },
            });

            this.employeeServ.getEmployee(leaveRequest.reviewedById).subscribe({
              next: (response) => {
                if (!response.hasError && response.data) {
                  leaveRequest.reviewedBy = response.data.firstName + ' ' + response.data.lastName;
                }
              },
              error: (err) => {
                console.error('Error loading employee', err);
                this.error = 'An error occurred while loading employee';
              },
            });


          });
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
}
