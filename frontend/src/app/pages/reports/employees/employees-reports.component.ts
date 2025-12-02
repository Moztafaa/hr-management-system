import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReportingService } from '../../../Services/reporting.service';
import {
    DepartmentBreakdown,
    DesignationBreakdown
} from '../../../models/reporting';

@Component({
    selector: 'app-employees-reports',
    standalone: true,
    imports: [CommonModule],
    templateUrl: './employees-reports.component.html',
    styleUrls: ['./employees-reports.component.css'],
})
export class EmployeesReportsComponent implements OnInit {
    private reportingService = inject(ReportingService);

    totalEmployees = signal<number>(0);
    departmentBreakdown = signal<DepartmentBreakdown>({});
    designationBreakdown = signal<DesignationBreakdown>({});
    isLoading = signal<boolean>(true);
    error = signal<string | null>(null);

    ngOnInit() {
        this.loadEmployeeMetrics();
    }

    loadEmployeeMetrics() {
        this.isLoading.set(true);
        this.error.set(null);

        // Load total employees
        this.reportingService.getTotalEmployees().subscribe({
            next: (data) => {
                this.totalEmployees.set(data);
            },
            error: (err) => {
                this.error.set('Failed to load total employees');
                console.error('Total employees error:', err);
            },
        });

        // Load employees by department
        this.reportingService.getEmployeesByDepartment().subscribe({
            next: (data) => {
                this.departmentBreakdown.set(data);
            },
            error: (err) => {
                this.error.set('Failed to load department breakdown');
                console.error('Department breakdown error:', err);
            },
        });

        // Load employees by designation
        this.reportingService.getEmployeesByDesignation().subscribe({
            next: (data) => {
                this.designationBreakdown.set(data);
                this.isLoading.set(false);
            },
            error: (err) => {
                this.error.set('Failed to load designation breakdown');
                this.isLoading.set(false);
                console.error('Designation breakdown error:', err);
            },
        });
    }

    // Helper methods to convert Record to array for *ngFor
    getDepartmentEntries(): Array<{ key: string; value: number }> {
        return Object.entries(this.departmentBreakdown()).map(([key, value]) => ({ key, value: value as number }));
    }

    getDesignationEntries(): Array<{ key: string; value: number }> {
        return Object.entries(this.designationBreakdown()).map(([key, value]) => ({ key, value: value as number }));
    }
}
