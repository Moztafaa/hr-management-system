import { Component, EventEmitter, OnInit, Output, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { JobPostingService } from '../../../Services/recruitment/job-posting.service';
import { JobPosting } from '../../../models/recruitment/job-posting';

@Component({
  selector: 'app-career-section',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './career-section.component.html',
  styleUrl: './career-section.component.css',
})
export class CareerSectionComponent implements OnInit {
  @Output() applyClick = new EventEmitter<JobPosting>();

  private jobPostingService = inject(JobPostingService);

  jobs = signal<JobPosting[]>([]);
  filteredJobs = signal<JobPosting[]>([]);
  isLoading = signal(true);
  error = signal<string | null>(null);

  searchTerm = '';
  selectedDepartment = '';
  departments = signal<string[]>([]);

  ngOnInit(): void {
    this.loadJobs();
  }

  loadJobs(): void {
    this.isLoading.set(true);
    this.error.set(null);

    this.jobPostingService.getActive().subscribe({
      next: (response) => {
        this.isLoading.set(false);
        if (!response.hasError && response.data) {
          this.jobs.set(response.data);
          this.filteredJobs.set(response.data);
          this.extractDepartments(response.data);
        } else {
          this.error.set(response.errorMessage || 'Failed to load job postings');
        }
      },
      error: (err) => {
        this.isLoading.set(false);
        this.error.set('Unable to load job postings. Please try again later.');
        console.error('Error loading jobs:', err);
      },
    });
  }

  extractDepartments(jobs: JobPosting[]): void {
    const deptSet = new Set<string>();
    jobs.forEach((job) => {
      if (job.departmentName) {
        deptSet.add(job.departmentName);
      }
    });
    this.departments.set(Array.from(deptSet).sort());
  }

  filterJobs(): void {
    let result = this.jobs();

    if (this.searchTerm.trim()) {
      const term = this.searchTerm.toLowerCase();
      result = result.filter(
        (job) =>
          job.title.toLowerCase().includes(term) ||
          job.description?.toLowerCase().includes(term) ||
          job.departmentName?.toLowerCase().includes(term)
      );
    }

    if (this.selectedDepartment) {
      result = result.filter((job) => job.departmentName === this.selectedDepartment);
    }

    this.filteredJobs.set(result);
  }

  clearFilters(): void {
    this.searchTerm = '';
    this.selectedDepartment = '';
    this.filteredJobs.set(this.jobs());
  }

  onApplyClick(job: JobPosting): void {
    this.applyClick.emit(job);
  }

  formatDate(dateString: string | undefined): string {
    if (!dateString) return 'Open';
    const date = new Date(dateString);
    return date.toLocaleDateString('en-US', { month: 'short', day: 'numeric', year: 'numeric' });
  }

  isClosingSoon(dateString: string | undefined): boolean {
    if (!dateString) return false;
    const closingDate = new Date(dateString);
    const today = new Date();
    const diffTime = closingDate.getTime() - today.getTime();
    const diffDays = Math.ceil(diffTime / (1000 * 60 * 60 * 24));
    return diffDays <= 7 && diffDays > 0;
  }
}
