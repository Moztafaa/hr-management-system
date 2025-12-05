import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { LeaveBalance as LeaveBalanceService } from '../../../Services/leave-balance/leave-balance';
import { Employee as EmployeeService } from '../../../Services/employee';
import { Employee as EmployeeModel } from '../../../models/employee';
import { LeaveType as LeaveTypeService } from '../../../Services/leave-types/leave-type';
import { LeaveType } from '../../../models/leaveType';

@Component({
    selector: 'app-leave-balance-management',
    standalone: true,
    imports: [CommonModule, ReactiveFormsModule],
    template: `
    <div class="container-fluid p-4">
      <h2>Leave Balance Management</h2>
      
      <div class="row mt-4">
        <!-- Actions Card -->
        <div class="col-md-12 mb-4">
            <div class="card shadow-sm">
                <div class="card-header bg-light">
                    <h5 class="mb-0">Actions</h5>
                </div>
                <div class="card-body">
                    <div class="d-flex gap-3">
                        <button class="btn btn-primary" (click)="resetAnnual()">
                            <i class="eva eva-refresh-outline me-2"></i>Reset Annual Balances
                        </button>
                    </div>
                </div>
            </div>
        </div>

        <!-- Allocate / Deduct Form -->
        <div class="col-md-6">
            <div class="card shadow-sm">
                <div class="card-header bg-light">
                    <h5 class="mb-0">Allocate / Deduct Leave</h5>
                </div>
                <div class="card-body">
                    <form [formGroup]="actionForm" (ngSubmit)="onSubmit()">
                        <div class="mb-3">
                            <label class="form-label">Action Type</label>
                            <select class="form-select" formControlName="actionType">
                                <option value="allocate">Allocate Initial Balance</option>
                                <option value="deduct">Deduct Leave</option>
                            </select>
                        </div>

                        <div class="mb-3">
                            <label class="form-label">Employee</label>
                            <select class="form-select" formControlName="employeeId">
                                <option value="">Select Employee</option>
                                <option *ngFor="let emp of employees" [value]="emp.id">
                                    {{ emp.firstName }} {{ emp.lastName }}
                                </option>
                            </select>
                        </div>

                        <div *ngIf="actionForm.get('actionType')?.value === 'deduct'">
                            <div class="mb-3">
                                <label class="form-label">Leave Type</label>
                                <select class="form-select" formControlName="leaveTypeId">
                                    <option value="">Select Type</option>
                                    <option *ngFor="let type of leaveTypes" [value]="type.id">{{ type.name }}</option>
                                </select>
                            </div>
                            <div class="mb-3">
                                <label class="form-label">Days to Deduct</label>
                                <input type="number" class="form-control" formControlName="days">
                            </div>
                            <div class="mb-3">
                                <label class="form-label">Reason</label>
                                <textarea class="form-control" formControlName="reason"></textarea>
                            </div>
                        </div>

                        <button type="submit" class="btn btn-success" [disabled]="actionForm.invalid || isSubmitting">
                            Submit
                        </button>
                    </form>
                </div>
            </div>
        </div>
      </div>
    </div>
  `
})
export class LeaveBalanceManagementComponent implements OnInit {
    private leaveBalanceService = inject(LeaveBalanceService);
    private employeeService = inject(EmployeeService);
    private leaveTypeService = inject(LeaveTypeService);
    private fb = inject(FormBuilder);

    employees: EmployeeModel[] = [];
    leaveTypes: LeaveType[] = [];
    actionForm: FormGroup;
    isSubmitting = false;

    constructor() {
        this.actionForm = this.fb.group({
            actionType: ['allocate', Validators.required],
            employeeId: ['', Validators.required],
            leaveTypeId: [''],
            days: [''],
            reason: ['']
        });

        // Dynamic validation
        this.actionForm.get('actionType')?.valueChanges.subscribe(type => {
            if (type === 'deduct') {
                this.actionForm.get('leaveTypeId')?.setValidators(Validators.required);
                this.actionForm.get('days')?.setValidators([Validators.required, Validators.min(0.5)]);
                this.actionForm.get('reason')?.setValidators(Validators.required);
            } else {
                this.actionForm.get('leaveTypeId')?.clearValidators();
                this.actionForm.get('days')?.clearValidators();
                this.actionForm.get('reason')?.clearValidators();
            }
            this.actionForm.get('leaveTypeId')?.updateValueAndValidity();
            this.actionForm.get('days')?.updateValueAndValidity();
            this.actionForm.get('reason')?.updateValueAndValidity();
        });
    }

    ngOnInit() {
        this.loadData();
    }

    loadData() {
        this.employeeService.getEmployees().subscribe({
            next: (res) => {
                if (!res.hasError && res.data) this.employees = res.data;
            }
        });
        this.leaveTypeService.getLeaveTypes().subscribe({
            next: (res) => {
                if (!res.hasError && res.data) this.leaveTypes = res.data;
            }
        });
    }

    resetAnnual() {
        if (!confirm('Are you sure you want to reset annual balances for ALL employees? This cannot be undone.')) return;

        this.leaveBalanceService.resetAnnual().subscribe({
            next: (res) => {
                if (!res.hasError) alert('Annual balances reset successfully');
                else alert('Failed to reset: ' + res.errorMessage);
            },
            error: () => alert('Error resetting balances')
        });
    }

    onSubmit() {
        if (this.actionForm.invalid) return;
        this.isSubmitting = true;
        const val = this.actionForm.value;

        if (val.actionType === 'allocate') {
            this.leaveBalanceService.allocate(Number(val.employeeId)).subscribe({
                next: (res) => {
                    if (!res.hasError) alert('Allocated successfully');
                    else alert('Failed: ' + res.errorMessage);
                    this.isSubmitting = false;
                },
                error: () => {
                    alert('Error allocating');
                    this.isSubmitting = false;
                }
            });
        } else {
            const request = {
                employeeId: Number(val.employeeId),
                leaveTypeId: Number(val.leaveTypeId),
                days: val.days,
                reason: val.reason
            };
            this.leaveBalanceService.deduct(request).subscribe({
                next: (res) => {
                    if (!res.hasError) alert('Deducted successfully');
                    else alert('Failed: ' + res.errorMessage);
                    this.isSubmitting = false;
                },
                error: () => {
                    alert('Error deducting');
                    this.isSubmitting = false;
                }
            });
        }
    }
}
