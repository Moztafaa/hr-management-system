import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { LeaveType as LeaveTypeService } from '../../../Services/leave-types/leave-type';
import { LeaveType as LeaveTypeModel } from '../../../models/leaveType';
@Component({
  selector: 'app-leave-type',
  imports: [CommonModule],
  templateUrl: './leave-type.html',
  styleUrl: './leave-type.css',
})
export class LeaveType implements OnInit {
  private leaveTypeService = inject(LeaveTypeService);
  leaveTypes: LeaveTypeModel[] = [];
  isLoading = true;
  error: string | null = null;

  ngOnInit() {
    this.loadLeaveTypes();
  }

  loadLeaveTypes() {
    this.isLoading = true;
    this.leaveTypeService.getLeaveTypes().subscribe({
      next: (response) => {
        if (!response.hasError && response.data) {
          this.leaveTypes = response.data;
        } else {
          this.error = response.errorMessage || 'Failed to load leave types';
        }
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Error loading leave types', err);
        this.error = 'An error occurred while loading leave types';
        this.isLoading = false;
      },
    });
  }
}
