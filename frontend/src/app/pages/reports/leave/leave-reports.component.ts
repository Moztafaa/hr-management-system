import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ReportingService } from '../../../Services/reporting.service';
import {
    StatusBreakdown,
    TypeBreakdown,
    MonthlyLeaveTrend
} from '../../../models/reporting';

@Component({
    selector: 'app-leave-reports',
    standalone: true,
    imports: [CommonModule, FormsModule],
    templateUrl: './leave-reports.component.html',
    styleUrls: ['./leave-reports.component.css'],
})
export class LeaveReportsComponent implements OnInit {
    private reportingService = inject(ReportingService);

    currentYear = new Date().getFullYear();
    selectedYear = this.currentYear;

    totalLeaveRequests = signal<number>(0);
    statusBreakdown = signal<StatusBreakdown>({});
    typeBreakdown = signal<TypeBreakdown>({});
    monthlyTrend = signal<MonthlyLeaveTrend[]>([]);
    avgApprovalTime = signal<string>('');

    isLoading = signal<boolean>(true);
    error = signal<string | null>(null);

    ngOnInit() {
        this.loadLeaveMetrics();
    }

    loadLeaveMetrics() {
        this.isLoading.set(true);
        this.error.set(null);

        // Load total leave requests
        this.reportingService.getTotalLeaveRequests().subscribe({
            next: (data) => {
                this.totalLeaveRequests.set(data);
            },
            error: () => {
                this.error.set('Failed to load total leave requests');
            },
        });

        // Load leave requests by status
        this.reportingService.getLeaveRequestsByStatus().subscribe({
            next: (data) => {
                this.statusBreakdown.set(data);
            },
            error: () => {
                this.error.set('Failed to load status breakdown');
            },
        });

        // Load leave usage by type
        this.reportingService.getLeaveUsageByType().subscribe({
            next: (data) => {
                this.typeBreakdown.set(data);
            },
            error: () => {
                this.error.set('Failed to load type breakdown');
            },
        });

        // Load monthly leave trend
        this.reportingService.getMonthlyLeaveTrend(this.selectedYear).subscribe({
            next: (data) => {
                this.monthlyTrend.set(data);
            },
            error: () => {
                this.error.set('Failed to load monthly trend');
            },
        });

        // Load average approval time
        this.reportingService.getAverageLeaveApprovalTime().subscribe({
            next: (data) => {
                this.avgApprovalTime.set(data);
                this.isLoading.set(false);
            },
            error: () => {
                this.error.set('Failed to load average approval time');
                this.isLoading.set(false);
            },
        });
    }

    onYearChange() {
        this.loadLeaveMetrics();
    }

    getStatusEntries(): Array<{ key: string; value: number }> {
        return Object.entries(this.statusBreakdown()).map(([key, value]) => ({ key, value: value as number }));
    }

    getTypeEntries(): Array<{ key: string; value: number }> {
        return Object.entries(this.typeBreakdown()).map(([key, value]) => ({ key, value: value as number }));
    }

    getMonthName(month: number): string {
        const months = ['January', 'February', 'March', 'April', 'May', 'June',
            'July', 'August', 'September', 'October', 'November', 'December'];
        return months[month - 1] || month.toString();
    }
}
