import { Component, EventEmitter, Input, Output, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { JobApplicationService } from '../../../Services/recruitment/job-application.service';
import { CandidateService } from '../../../Services/recruitment/candidate.service';
import { JobPosting } from '../../../models/recruitment/job-posting';
import {
  CreateJobApplicationDto,
  ApplicationSources,
} from '../../../models/recruitment/job-application';
import { CreateCandidateDto, Candidate } from '../../../models/recruitment/candidate';

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
  private candidateService = inject(CandidateService);

  currentStep = signal(1);
  isLoading = signal(false);
  error = signal<string | null>(null);
  successMessage = signal<string | null>(null);

  // Candidate mode: 'new' for new candidates, 'existing' for returning candidates
  candidateMode = signal<'new' | 'existing'>('new');
  existingCandidateEmail = signal('');
  existingCandidate = signal<Candidate | null>(null);
  isSearchingCandidate = signal(false);

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
    source: 'CompanyWebsite',
    coverLetter: '',
    expectedSalary: undefined as number | undefined,
  };

  setCandidateMode(mode: 'new' | 'existing'): void {
    this.candidateMode.set(mode);
    this.error.set(null);
    this.existingCandidate.set(null);
    this.existingCandidateEmail.set('');
  }

  lookupCandidate(): void {
    const email = this.existingCandidateEmail().trim();
    if (!email) {
      this.error.set('Please enter your email address');
      return;
    }

    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailRegex.test(email)) {
      this.error.set('Please enter a valid email address');
      return;
    }

    this.isSearchingCandidate.set(true);
    this.error.set(null);

    this.candidateService.getByEmail(email).subscribe({
      next: (response) => {
        this.isSearchingCandidate.set(false);
        if (!response.hasError && response.data) {
          this.existingCandidate.set(response.data);
          this.error.set(null);
        } else {
          this.error.set('No candidate found with this email. Please apply as a new candidate.');
          this.existingCandidate.set(null);
        }
      },
      error: () => {
        this.isSearchingCandidate.set(false);
        this.error.set('No candidate found with this email. Please apply as a new candidate.');
        this.existingCandidate.set(null);
      },
    });
  }

  proceedWithExistingCandidate(): void {
    if (this.existingCandidate()) {
      this.currentStep.set(2);
    }
  }

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
    // For new candidates, validate step 1
    if (this.candidateMode() === 'new' && !this.validateStep1()) {
      this.currentStep.set(1);
      return;
    }

    // For existing candidates, ensure we have a candidate
    if (this.candidateMode() === 'existing' && !this.existingCandidate()) {
      this.error.set('Please look up your candidate profile first');
      return;
    }

    this.isLoading.set(true);
    this.error.set(null);

    let application: CreateJobApplicationDto;

    if (this.candidateMode() === 'existing' && this.existingCandidate()) {
      // Use existing candidate ID
      application = {
        jobPostingId: this.job.id,
        candidateId: this.existingCandidate()!.id,
        source: this.applicationInfo.source,
        coverLetter: this.applicationInfo.coverLetter || undefined,
        expectedSalary: this.applicationInfo.expectedSalary,
      };
    } else {
      // Sanitize candidate info - convert empty strings to undefined
      const sanitizedCandidateInfo = { ...this.candidateInfo };
      if (!sanitizedCandidateInfo.phone?.trim()) sanitizedCandidateInfo.phone = undefined;
      if (!sanitizedCandidateInfo.linkedInUrl?.trim())
        sanitizedCandidateInfo.linkedInUrl = undefined;
      if (!sanitizedCandidateInfo.portfolioUrl?.trim())
        sanitizedCandidateInfo.portfolioUrl = undefined;
      if (!sanitizedCandidateInfo.resumeUrl?.trim()) sanitizedCandidateInfo.resumeUrl = undefined;
      if (!sanitizedCandidateInfo.currentCompany?.trim())
        sanitizedCandidateInfo.currentCompany = undefined;
      if (!sanitizedCandidateInfo.currentJobTitle?.trim())
        sanitizedCandidateInfo.currentJobTitle = undefined;
      if (!sanitizedCandidateInfo.skills?.trim()) sanitizedCandidateInfo.skills = undefined;
      if (!sanitizedCandidateInfo.education?.trim()) sanitizedCandidateInfo.education = undefined;
      if (!sanitizedCandidateInfo.preferredWorkLocation?.trim())
        sanitizedCandidateInfo.preferredWorkLocation = undefined;

      application = {
        jobPostingId: this.job.id,
        candidateInfo: sanitizedCandidateInfo,
        source: this.applicationInfo.source,
        coverLetter: this.applicationInfo.coverLetter || undefined,
        expectedSalary: this.applicationInfo.expectedSalary,
      };
    }

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
