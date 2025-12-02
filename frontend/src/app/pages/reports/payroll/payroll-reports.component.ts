import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ReportingService } from '../../../Services/reporting.service';
import {
    PayrollTrend,
    CostBreakdown,
    SalaryComparison
} from '../../../models/reporting';

@Component({
    selector: 'app-payroll-reports',
    standalone: true,
    imports: [CommonModule, FormsModule],
    templateUrl: './payroll-reports.component.html',
    styleUrls: ['./payroll-reports.component.css'],
})
export class PayrollReportsComponent implements OnInit {
    private reportingService = inject(ReportingService);

    currentYear = new Date().getFullYear();
    currentMonth = new Date().getMonth() + 1;
    selectedYear = this.currentYear;
    selectedMonth = this.currentMonth;

    totalPayroll = signal<number>(0);
    payrollTrend = signal<PayrollTrend[]>([]);
    allowanceBreakdown = signal<CostBreakdown>({});
    deductionBreakdown = signal<CostBreakdown>({});
    salaryComparison = signal<SalaryComparison[]>([]);

    isLoading = signal<boolean>(true);
    error = signal<string | null>(null);

    ngOnInit() {
        this.loadPayrollMetrics();
    }

    loadPayrollMetrics() {
        this.isLoading.set(true);
        this.error.set(null);

        // Load total payroll
        this.reportingService.getTotalPayroll(this.selectedYear, this.selectedMonth).subscribe({
            next: (data) => {
                this.totalPayroll.set(data);
            },
            error: () => {
                this.error.set('Failed to load total payroll');
            },
        });

        // Load payroll trend
        this.reportingService.getPayrollTrend(this.selectedYear).subscribe({
            next: (data) => {
                this.payrollTrend.set(data);
            },
            error: () => {
                this.error.set('Failed to load payroll trend');
            },
        });

        // Load allowance breakdown
        this.reportingService.getAllowanceCostBreakdown(this.selectedYear, this.selectedMonth).subscribe({
            next: (data) => {
                this.allowanceBreakdown.set(data);
            },
            error: () => {
                this.error.set('Failed to load allowance breakdown');
            },
        });

        // Load deduction breakdown
        this.reportingService.getDeductionCostBreakdown(this.selectedYear, this.selectedMonth).subscribe({
            next: (data) => {
                this.deductionBreakdown.set(data);
            },
            error: () => {
                this.error.set('Failed to load deduction breakdown');
            },
        });

        // Load salary comparison
        this.reportingService.getSalaryComparison().subscribe({
            next: (data) => {
                this.salaryComparison.set(data);
                this.isLoading.set(false);
            },
            error: () => {
                this.error.set('Failed to load salary comparison');
                this.isLoading.set(false);
            },
        });
    }

    onPeriodChange() {
        this.loadPayrollMetrics();
    }

    getAllowanceEntries(): Array<{ key: string; value: number }> {
        return Object.entries(this.allowanceBreakdown()).map(([key, value]) => ({ key, value: value as number }));
    }

    getDeductionEntries(): Array<{ key: string; value: number }> {
        return Object.entries(this.deductionBreakdown()).map(([key, value]) => ({ key, value: value as number }));
    }

    getMonthName(month: number): string {
        const months = ['January', 'February', 'March', 'April', 'May', 'June',
            'July', 'August', 'September', 'October', 'November', 'December'];
        return months[month - 1] || month.toString();
    }
}
