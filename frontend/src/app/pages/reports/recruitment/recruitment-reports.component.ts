import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReportingService } from '../../../Services/reporting.service';

@Component({
    selector: 'app-recruitment-reports',
    standalone: true,
    imports: [CommonModule],
    templateUrl: './recruitment-reports.component.html',
    styleUrls: ['./recruitment-reports.component.css']
})
export class RecruitmentReportsComponent implements OnInit {
    private reportingService = inject(ReportingService);

    totalJobPostings = signal<number>(0);
    activeJobPostings = signal<number>(0);
    avgReviewTime = signal<string>('');
    applicationsPerJob = signal<Record<string, number>>({});
    pipelineData = signal<Record<string, number>>({});

    isLoading = signal<boolean>(true);
    error = signal<string | null>(null);

    ngOnInit() {
        this.loadRecruitmentMetrics();
    }

    loadRecruitmentMetrics() {
        this.isLoading.set(true);
        this.error.set(null);

        // Load metrics in parallel
        this.reportingService.getTotalJobPostings().subscribe({
            next: (data) => this.totalJobPostings.set(data),
            error: (err) => console.error('Error loading total jobs', err)
        });

        this.reportingService.getActiveJobPostings().subscribe({
            next: (data) => this.activeJobPostings.set(data),
            error: (err) => console.error('Error loading active jobs', err)
        });

        this.reportingService.getAverageApplicationReviewTime().subscribe({
            next: (data) => this.avgReviewTime.set(data),
            error: (err) => console.error('Error loading avg review time', err)
        });

        this.reportingService.getApplicationsPerJobPosting().subscribe({
            next: (data) => this.applicationsPerJob.set(data),
            error: (err) => console.error('Error loading apps per job', err)
        });

        this.reportingService.getRecruitmentPipeline().subscribe({
            next: (data) => {
                this.pipelineData.set(data);
                this.isLoading.set(false);
            },
            error: (err) => {
                this.error.set('Failed to load recruitment metrics');
                this.isLoading.set(false);
                console.error('Error loading pipeline', err);
            }
        });
    }

    getApplicationsPerJobEntries(): Array<{ jobTitle: string; applicationCount: number }> {
        return Object.entries(this.applicationsPerJob()).map(([key, value]) => ({
            jobTitle: key,
            applicationCount: value as number
        }));
    }

    getPipelineEntries(): Array<{ stage: string; count: number }> {
        return Object.entries(this.pipelineData()).map(([key, value]) => ({
            stage: key,
            count: value as number
        }));
    }
}
