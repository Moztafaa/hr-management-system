import { Component, EventEmitter, Input, Output, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { JobApplicationService } from '../../../Services/recruitment/job-application.service';
import { JobPosting } from '../../../models/recruitment/job-posting';
import {
  CreateJobApplicationDto,
  ApplicationSources,
} from '../../../models/recruitment/job-application';
import { CreateCandidateDto } from '../../../models/recruitment/candidate';

@Component({
  selector: 'app-job-apply-modal',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './job-apply-modal.component.html',
  styleUrl: './job-apply-modal.component.css',
})
export class JobApplyModalComponent {
  @Input({ required: true }) job!: JobPosting;
  @Output() close = new EventEmitter<void>();
  @Output() applicationSuccess = new EventEmitter<void>();

  private jobApplicationService = inject(JobApplicationService);

  currentStep = signal(1);
  isLoading = signal(false);
  error = signal<string | null>(null);
  successMessage = signal<string | null>(null);

  sources = ApplicationSources;

  // Candidate Info
  candidateInfo: CreateCandidateDto = {
    firstName: '',
    lastName: '',
    email: '',
    phone: '',
    linkedInUrl: '',
    portfolioUrl: '',
    resumeUrl: '',
    yearsOfExperience: undefined,
    currentCompany: '',
    currentJobTitle: '',
    skills: '',
    education: '',
    preferredWorkLocation: '',
    willingToRelocate: false,
  };

  // Application Info
  applicationInfo = {
    source: 'Company Website',
    coverLetter: '',
    expectedSalary: undefined as number | undefined,
  };

  nextStep(): void {
    if (this.currentStep() === 1 && this.validateStep1()) {
      this.currentStep.set(2);
    }
  }

  prevStep(): void {
    if (this.currentStep() > 1) {
      this.currentStep.update((s) => s - 1);
    }
  }

  validateStep1(): boolean {
    if (!this.candidateInfo.firstName?.trim()) {
      this.error.set('First name is required');
      return false;
    }
    if (!this.candidateInfo.lastName?.trim()) {
      this.error.set('Last name is required');
      return false;
    }
    if (!this.candidateInfo.email?.trim()) {
      this.error.set('Email is required');
      return false;
    }
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailRegex.test(this.candidateInfo.email)) {
      this.error.set('Please enter a valid email address');
      return false;
    }
    this.error.set(null);
    return true;
  }

  submitApplication(): void {
    if (!this.validateStep1()) {
      this.currentStep.set(1);
      return;
    }

    this.isLoading.set(true);
    this.error.set(null);

    const application: CreateJobApplicationDto = {
      jobPostingId: this.job.id,
      candidateInfo: this.candidateInfo,
      source: this.applicationInfo.source,
      coverLetter: this.applicationInfo.coverLetter || undefined,
      expectedSalary: this.applicationInfo.expectedSalary,
    };

    this.jobApplicationService.create(application).subscribe({
      next: (response) => {
        this.isLoading.set(false);
        if (!response.hasError && response.data) {
          this.successMessage.set('Your application has been submitted successfully!');
          setTimeout(() => {
            this.applicationSuccess.emit();
          }, 2000);
        } else {
          this.error.set(response.errorMessage || 'Failed to submit application');
        }
      },
      error: (err) => {
        this.isLoading.set(false);
        this.error.set(
          err.error?.errorMessage || 'An error occurred while submitting your application'
        );
      },
    });
  }

  onBackdropClick(event: MouseEvent): void {
    if ((event.target as HTMLElement).classList.contains('modal-overlay')) {
      this.close.emit();
    }
  }

  onCloseClick(): void {
    this.close.emit();
  }
}
