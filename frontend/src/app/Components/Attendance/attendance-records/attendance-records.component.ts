import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Employee as EmployeeService } from '../../../Services/employee/employee';
import { ApiResponse } from '../../../models/api-response';

@Component({
    selector: 'app-attendance-records',
    standalone: true,
    imports: [CommonModule],
    template: `
    <div class="container-fluid p-4">
      <h2>Attendance Records</h2>
      <div class="card shadow-sm mt-4">
        <div class="card-body p-0">
            <div *ngIf="isLoading" class="text-center py-5">
                <div class="spinner-border text-primary" role="status">
                    <span class="visually-hidden">Loading...</span>
                </div>
            </div>
            <div *ngIf="!isLoading && records.length === 0" class="alert alert-info m-3">
                No attendance records found.
            </div>
            <div *ngIf="!isLoading && records.length > 0" class="table-responsive">
                <table class="table table-hover mb-0">
                    <thead class="table-light">
                        <tr>
                            <th>Date</th>
                            <th>Check In</th>
                            <th>Check Out</th>
                            <th>Status</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr *ngFor="let record of records">
                            <td>{{ record.date | date }}</td>
                            <td>{{ record.checkInTime | date:'shortTime' }}</td>
                            <td>{{ record.checkOutTime | date:'shortTime' }}</td>
                            <td>
                                <span class="badge" [ngClass]="{'bg-success': record.checkOutTime, 'bg-warning': !record.checkOutTime}">
                                    {{ record.checkOutTime ? 'Completed' : 'Active' }}
                                </span>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
      </div>
    </div>
  `,
    styles: [`
    /* Table Styling from employee-list.css */
    :host ::ng-deep .card {
      background-color: var(--card-bg) !important;
      border: 1px solid var(--border-basic) !important;
      color: var(--text-color) !important;
    }
    
    :host ::ng-deep .table {
      color: var(--text-color) !important;
      margin-bottom: 0;
      background-color: transparent !important;
    }

    :host ::ng-deep .table thead th {
      background-color: var(--header-bg) !important;
      color: var(--text-hint) !important;
      border-bottom: 1px solid var(--border-basic) !important;
      font-weight: 600;
      text-transform: uppercase;
      font-size: 0.75rem;
      padding: 1rem;
      letter-spacing: 0.5px;
    }

    :host ::ng-deep .table-light {
      background-color: var(--header-bg) !important;
    }

    :host ::ng-deep .table tbody td {
      border-bottom: 1px solid var(--border-basic) !important;
      padding: 1rem;
      vertical-align: middle;
      color: var(--text-color) !important;
      background-color: transparent !important;
    }

    :host ::ng-deep .table-hover tbody tr:hover {
      background-color: var(--hover-bg) !important;
      color: var(--text-color) !important;
    }

    :host ::ng-deep .table-hover tbody tr:hover td {
      background-color: transparent !important;
      color: var(--text-color) !important;
    }
    
    h2 { color: var(--text-color); }
  `]
})
export class AttendanceRecordsComponent implements OnInit {
    private http = inject(HttpClient);
    private employeeService = inject(EmployeeService);
    private apiUrl = 'https://localhost:7005/api/Attendance';

    records: any[] = [];
    isLoading = true;

    ngOnInit() {
        this.loadRecords();
    }

    loadRecords() {
        const employeeId = this.employeeService.getEmployeeId();
        this.http.get<any>(`${this.apiUrl}/employee/${employeeId}`).subscribe({
            next: (res) => {
                // Assuming response format matches others or is direct list?
                // The instructions say "Attendance data". Let's assume standard ApiResponse or direct list.
                // Copilot instructions say: "Response format: { data: T, errorMessage: string, hasError: boolean }"
                // But Attendance Controller section says "Response: Attendance data".
                // I'll assume it might be wrapped.
                if (res && res.data) {
                    this.records = res.data;
                } else if (Array.isArray(res)) {
                    this.records = res;
                }
                this.isLoading = false;
            },
            error: (err) => {
                console.error(err);
                this.isLoading = false;
            }
        });
    }
}
