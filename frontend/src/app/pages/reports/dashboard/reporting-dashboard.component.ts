import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ReportingService } from '../../../Services/reporting.service';
import { DashboardKpisDto } from '../../../models/reporting';

@Component({
    selector: 'app-reporting-dashboard',
    standalone: true,
    imports: [CommonModule, FormsModule],
    templateUrl: './reporting-dashboard.component.html',
    styleUrls: ['./reporting-dashboard.component.css'],
})
export class ReportingDashboardComponent implements OnInit {
    private reportingService = inject(ReportingService);

    kpis = signal<DashboardKpisDto | null>(null);
    isLoading = signal<boolean>(true);
    error = signal<string | null>(null);

    currentYear = new Date().getFullYear();
    currentMonth = new Date().getMonth() + 1;

    selectedYear = this.currentYear;
    selectedMonth = this.currentMonth;

    ngOnInit() {
        this.loadKpis();
    }

    loadKpis() {
        this.isLoading.set(true);
        this.reportingService.getDashboardKpis(this.selectedYear, this.selectedMonth).subscribe({
            next: (data) => {
                this.kpis.set(data);
                this.isLoading.set(false);
            },
            error: (err) => {
                this.error.set('Failed to load dashboard KPIs');
                this.isLoading.set(false);
            },
        });
    }

    onPeriodChange() {
        this.loadKpis();
    }
}
