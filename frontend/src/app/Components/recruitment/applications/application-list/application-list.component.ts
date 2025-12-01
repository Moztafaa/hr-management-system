import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterModule } from '@angular/router';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  FormsModule,
  Validators,
} from '@angular/forms';
import { JobApplicationService } from '../../../../Services/recruitment/job-application.service';
import { FilterPipe } from '../../../../pipes/filter.pipe';
import { JobPostingService } from '../../../../Services/recruitment/job-posting.service';
import { CandidateService } from '../../../../Services/recruitment/candidate.service';
import {
  JobApplication,
  JobApplicationDetail,
  CreateJobApplicationDto,
  UpdateJobApplicationStatusDto,
  ApplicationStatus,
  ApplicationStatusLabels,
  ApplicationStatusColors,
  ApplicationSources,
} from '../../../../models/recruitment/job-application';
import { JobPosting } from '../../../../models/recruitment/job-posting';
import { Candidate, CreateCandidateDto } from '../../../../models/recruitment/candidate';
import { ToastService } from '../../../../Services/toast.service';

@Component({
  selector: 'app-application-list',
  standalone: true,
  imports: [CommonModule, RouterModule, ReactiveFormsModule, FormsModule, FilterPipe],
  templateUrl: './application-list.component.html',
  styleUrls: ['./application-list.component.css'],
})
export class ApplicationListComponent implements OnInit {
  private applicationService = inject(JobApplicationService);
  private jobPostingService = inject(JobPostingService);
  private candidateService = inject(CandidateService);
  private toastService = inject(ToastService);
  private fb = inject(FormBuilder);
  private route = inject(ActivatedRoute);

  applications: JobApplication[] = [];
  jobPostings: JobPosting[] = [];
  candidates: Candidate[] = [];
  isLoading = true;
  error: string | null = null;

  // Filters
  filterJobId: number | null = null;
  filterCandidateId: number | null = null;
  filterStatus: string = '';

  // Status constants
  statusOptions = Object.values(ApplicationStatus);
  statusLabels = ApplicationStatusLabels;
  statusColors = ApplicationStatusColors;
  applicationSources = ApplicationSources;

  // Create modal state
  showCreateModal = false;
  isSubmitting = false;
  useExistingCandidate = true;

  // Status update modal
  showStatusModal = false;
  selectedApplication: JobApplicationDetail | null = null;
  isLoadingDetail = false;

  // Delete confirmation
  showDeleteModal = false;
  deletingApplication: JobApplication | null = null;
  isDeleting = false;

  // Application form
  applicationForm: FormGroup = this.fb.group({
    jobPostingId: ['', Validators.required],
    candidateId: [''],
    source: ['', [Validators.required, Validators.maxLength(50)]],
    coverLetter: ['', Validators.maxLength(5000)],
    expectedSalary: ['', Validators.min(0)],
    notes: ['', Validators.maxLength(1000)],
    // New candidate fields (conditional)
    firstName: [''],
    lastName: [''],
    email: [''],
    phone: [''],
  });

  // Status update form
  statusForm: FormGroup = this.fb.group({
    status: ['', Validators.required],
    currentStage: ['', Validators.maxLength(50)],
    notes: ['', Validators.maxLength(2000)],
    interviewDate: [''],
    interviewFeedback: ['', Validators.maxLength(2000)],
    interviewRating: ['', [Validators.min(1), Validators.max(10)]],
    offeredSalary: ['', Validators.min(0)],
    rejectionReason: ['', Validators.maxLength(1000)],
  });

  ngOnInit(): void {
    this.route.queryParams.subscribe((params) => {
      if (params['jobId']) {
        this.filterJobId = Number(params['jobId']);
      }
      if (params['candidateId']) {
        this.filterCandidateId = Number(params['candidateId']);
      }
      this.loadData();
    });
  }

  loadData(): void {
    this.loadApplications();
    this.loadJobPostings();
    this.loadCandidates();
  }

  loadApplications(): void {
    this.isLoading = true;
    this.error = null;

    let request;
    if (this.filterJobId) {
      request = this.applicationService.getByJobPosting(this.filterJobId);
    } else if (this.filterCandidateId) {
      request = this.applicationService.getByCandidate(this.filterCandidateId);
    } else if (this.filterStatus) {
      request = this.applicationService.getByStatus(this.filterStatus);
    } else {
      request = this.applicationService.getAll();
    }

    request.subscribe({
      next: (response) => {
        if (!response.hasError && response.data) {
          this.applications = response.data;
        } else {
          this.error = response.errorMessage || 'Failed to load applications';
        }
        this.isLoading = false;
      },
      error: () => {
        this.error = 'An error occurred while loading applications';
        this.isLoading = false;
      },
    });
  }

  loadJobPostings(): void {
    this.jobPostingService.getAll().subscribe({
      next: (response) => {
        if (!response.hasError && response.data) {
          this.jobPostings = response.data;
        }
      },
    });
  }

  loadCandidates(): void {
    this.candidateService.getAll().subscribe({
      next: (response) => {
        if (!response.hasError && response.data) {
          this.candidates = response.data;
        }
      },
    });
  }

  onFilterChange(): void {
    this.loadApplications();
  }

  clearFilters(): void {
    this.filterJobId = null;
    this.filterCandidateId = null;
    this.filterStatus = '';
    this.loadApplications();
  }

  // Create modal methods
  openCreateModal(): void {
    this.applicationForm.reset({ source: 'Company Website' });
    this.useExistingCandidate = true;
    this.updateCandidateValidation();
    this.showCreateModal = true;
  }

  closeCreateModal(): void {
    this.showCreateModal = false;
    this.applicationForm.reset();
  }

  toggleCandidateMode(): void {
    this.useExistingCandidate = !this.useExistingCandidate;
    this.updateCandidateValidation();
  }

  updateCandidateValidation(): void {
    if (this.useExistingCandidate) {
      this.applicationForm.get('candidateId')?.setValidators(Validators.required);
      this.applicationForm.get('firstName')?.clearValidators();
      this.applicationForm.get('lastName')?.clearValidators();
      this.applicationForm.get('email')?.clearValidators();
    } else {
      this.applicationForm.get('candidateId')?.clearValidators();
      this.applicationForm
        .get('firstName')
        ?.setValidators([Validators.required, Validators.minLength(2)]);
      this.applicationForm
        .get('lastName')
        ?.setValidators([Validators.required, Validators.minLength(2)]);
      this.applicationForm.get('email')?.setValidators([Validators.required, Validators.email]);
    }
    this.applicationForm.get('candidateId')?.updateValueAndValidity();
    this.applicationForm.get('firstName')?.updateValueAndValidity();
    this.applicationForm.get('lastName')?.updateValueAndValidity();
    this.applicationForm.get('email')?.updateValueAndValidity();
  }

  submitApplication(): void {
    if (this.applicationForm.invalid) {
      this.applicationForm.markAllAsTouched();
      return;
    }

    this.isSubmitting = true;
    const formData = this.applicationForm.value;

    const createDto: CreateJobApplicationDto = {
      jobPostingId: Number(formData.jobPostingId),
      source: formData.source,
      coverLetter: formData.coverLetter || undefined,
      expectedSalary: formData.expectedSalary ? Number(formData.expectedSalary) : undefined,
      notes: formData.notes || undefined,
    };

    if (this.useExistingCandidate) {
      createDto.candidateId = Number(formData.candidateId);
    } else {
      createDto.candidateInfo = {
        firstName: formData.firstName,
        lastName: formData.lastName,
        email: formData.email,
        phone: formData.phone || undefined,
      };
    }

    this.applicationService.create(createDto).subscribe({
      next: (response) => {
        if (!response.hasError) {
          this.toastService.show('Application submitted successfully', 'success');
          this.closeCreateModal();
          this.loadApplications();
          this.loadCandidates(); // Refresh candidates if new one was created
        } else {
          this.toastService.show(response.errorMessage || 'Failed to submit application', 'error');
        }
        this.isSubmitting = false;
      },
      error: () => {
        this.toastService.show('An error occurred while submitting', 'error');
        this.isSubmitting = false;
      },
    });
  }

  // Status update methods
  openStatusModal(application: JobApplication): void {
    this.isLoadingDetail = true;
    this.showStatusModal = true;

    this.applicationService.getDetail(application.id).subscribe({
      next: (response) => {
        if (!response.hasError && response.data) {
          this.selectedApplication = response.data;
          this.statusForm.patchValue({
            status: response.data.status,
            currentStage: response.data.currentStage,
            notes: response.data.notes,
            interviewDate: response.data.interviewDate
              ? response.data.interviewDate.split('T')[0]
              : '',
            interviewFeedback: response.data.interviewFeedback,
            interviewRating: response.data.interviewRating,
            offeredSalary: response.data.offeredSalary,
            rejectionReason: response.data.rejectionReason,
          });
        } else {
          this.toastService.show('Failed to load application details', 'error');
          this.closeStatusModal();
        }
        this.isLoadingDetail = false;
      },
      error: () => {
        this.toastService.show('Failed to load application details', 'error');
        this.closeStatusModal();
        this.isLoadingDetail = false;
      },
    });
  }

  closeStatusModal(): void {
    this.showStatusModal = false;
    this.selectedApplication = null;
    this.statusForm.reset();
  }

  updateStatus(): void {
    if (!this.selectedApplication || this.statusForm.invalid) return;

    this.isSubmitting = true;
    const formData = this.statusForm.value;

    const updateDto: UpdateJobApplicationStatusDto = {
      jobApplicationId: this.selectedApplication.id,
      status: formData.status,
      currentStage: formData.currentStage || undefined,
      notes: formData.notes || undefined,
      interviewDate: formData.interviewDate || undefined,
      interviewFeedback: formData.interviewFeedback || undefined,
      interviewRating: formData.interviewRating ? Number(formData.interviewRating) : undefined,
      offeredSalary: formData.offeredSalary ? Number(formData.offeredSalary) : undefined,
      rejectionReason: formData.rejectionReason || undefined,
    };

    this.applicationService.updateStatus(this.selectedApplication.id, updateDto).subscribe({
      next: (response) => {
        if (!response.hasError) {
          this.toastService.show('Status updated successfully', 'success');
          this.closeStatusModal();
          this.loadApplications();
        } else {
          this.toastService.show(response.errorMessage || 'Failed to update status', 'error');
        }
        this.isSubmitting = false;
      },
      error: () => {
        this.toastService.show('An error occurred while updating', 'error');
        this.isSubmitting = false;
      },
    });
  }

  // Delete methods
  confirmDelete(application: JobApplication): void {
    this.deletingApplication = application;
    this.showDeleteModal = true;
  }

  closeDeleteModal(): void {
    this.showDeleteModal = false;
    this.deletingApplication = null;
  }

  deleteApplication(): void {
    if (!this.deletingApplication) return;

    this.isDeleting = true;
    this.applicationService.delete(this.deletingApplication.id).subscribe({
      next: (response) => {
        if (!response.hasError) {
          this.toastService.show('Application deleted successfully', 'success');
          this.closeDeleteModal();
          this.loadApplications();
        } else {
          this.toastService.show(response.errorMessage || 'Failed to delete application', 'error');
        }
        this.isDeleting = false;
      },
      error: () => {
        this.toastService.show('An error occurred while deleting', 'error');
        this.isDeleting = false;
      },
    });
  }

  // Helper methods
  getJobTitle(jobPostingId: number): string {
    const job = this.jobPostings.find((j) => j.id === jobPostingId);
    return job?.title || 'Unknown';
  }

  getCandidateName(candidateId: number): string {
    const candidate = this.candidates.find((c) => c.id === candidateId);
    return candidate ? `${candidate.firstName} ${candidate.lastName}` : 'Unknown';
  }

  getCandidateEmail(candidateId: number): string {
    const candidate = this.candidates.find((c) => c.id === candidateId);
    return candidate?.email || '';
  }

  formatDate(date: string | undefined): string {
    if (!date) return '-';
    return new Date(date).toLocaleDateString();
  }

  formatDateTime(date: string | undefined): string {
    if (!date) return '-';
    return new Date(date).toLocaleString();
  }

  getStatusLabel(status: string): string {
    return this.statusLabels[status] || status;
  }

  getStatusColor(status: string): string {
    return this.statusColors[status] || 'bg-secondary';
  }

  getInitials(firstName: string, lastName: string): string {
    return `${firstName?.charAt(0) || ''}${lastName?.charAt(0) || ''}`.toUpperCase();
  }
}
