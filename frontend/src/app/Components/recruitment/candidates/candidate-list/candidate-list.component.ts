import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CandidateService } from '../../../../Services/recruitment/candidate.service';
import {
  Candidate,
  CreateCandidateDto,
  UpdateCandidateDto,
} from '../../../../models/recruitment/candidate';
import { ToastService } from '../../../../Services/toast.service';
import { debounceTime, distinctUntilChanged, Subject } from 'rxjs';

@Component({
  selector: 'app-candidate-list',
  standalone: true,
  imports: [CommonModule, RouterModule, ReactiveFormsModule],
  templateUrl: './candidate-list.component.html',
  styleUrls: ['./candidate-list.component.css'],
})
export class CandidateListComponent implements OnInit {
  private candidateService = inject(CandidateService);
  private toastService = inject(ToastService);
  private fb = inject(FormBuilder);

  candidates: Candidate[] = [];
  filteredCandidates: Candidate[] = [];
  isLoading = true;
  error: string | null = null;
  searchTerm = '';
  private searchSubject = new Subject<string>();

  // Modal state
  showModal = false;
  isEditing = false;
  editingId: number | null = null;
  isSubmitting = false;

  // View detail modal
  showDetailModal = false;
  selectedCandidate: Candidate | null = null;

  // Delete confirmation
  showDeleteModal = false;
  deletingCandidate: Candidate | null = null;
  isDeleting = false;

  candidateForm: FormGroup = this.fb.group({
    firstName: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(100)]],
    lastName: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(100)]],
    email: ['', [Validators.required, Validators.email, Validators.maxLength(255)]],
    phone: ['', Validators.maxLength(20)],
    dateOfBirth: [''],
    gender: [''],
    yearsOfExperience: ['', [Validators.min(0), Validators.max(50)]],
    currentCompany: ['', Validators.maxLength(200)],
    currentJobTitle: ['', Validators.maxLength(200)],
    currentSalary: ['', Validators.min(0)],
    expectedSalary: ['', Validators.min(0)],
    skills: ['', Validators.maxLength(2000)],
    education: ['', Validators.maxLength(1000)],
    certifications: ['', Validators.maxLength(1000)],
    noticePeriodDays: ['', [Validators.min(0), Validators.max(365)]],
    availableFrom: [''],
    resumeUrl: ['', Validators.maxLength(500)],
    linkedInUrl: ['', Validators.maxLength(500)],
    portfolioUrl: ['', Validators.maxLength(500)],
    address: ['', Validators.maxLength(500)],
    city: ['', Validators.maxLength(100)],
    country: ['', Validators.maxLength(100)],
    postalCode: ['', Validators.maxLength(20)],
    preferredWorkLocation: ['', Validators.maxLength(200)],
    willingToRelocate: [false],
    notes: ['', Validators.maxLength(2000)],
  });

  ngOnInit(): void {
    this.loadCandidates();
    this.setupSearch();
  }

  setupSearch(): void {
    this.searchSubject.pipe(debounceTime(300), distinctUntilChanged()).subscribe((term) => {
      if (term.trim()) {
        this.searchCandidates(term);
      } else {
        this.filteredCandidates = [...this.candidates];
      }
    });
  }

  onSearchInput(event: Event): void {
    const input = event.target as HTMLInputElement;
    this.searchTerm = input.value;
    this.searchSubject.next(this.searchTerm);
  }

  searchCandidates(term: string): void {
    this.isLoading = true;
    this.candidateService.search(term).subscribe({
      next: (response) => {
        if (!response.hasError && response.data) {
          this.filteredCandidates = response.data;
        } else {
          this.filteredCandidates = [];
        }
        this.isLoading = false;
      },
      error: () => {
        this.filteredCandidates = [];
        this.isLoading = false;
      },
    });
  }

  loadCandidates(): void {
    this.isLoading = true;
    this.error = null;

    this.candidateService.getAll().subscribe({
      next: (response) => {
        if (!response.hasError && response.data) {
          this.candidates = response.data;
          this.filteredCandidates = [...this.candidates];
        } else {
          this.error = response.errorMessage || 'Failed to load candidates';
        }
        this.isLoading = false;
      },
      error: () => {
        this.error = 'An error occurred while loading candidates';
        this.isLoading = false;
      },
    });
  }

  openCreateModal(): void {
    this.isEditing = false;
    this.editingId = null;
    this.candidateForm.reset({ willingToRelocate: false });
    this.showModal = true;
  }

  openEditModal(candidate: Candidate): void {
    this.isEditing = true;
    this.editingId = candidate.id;
    this.candidateForm.patchValue({
      firstName: candidate.firstName,
      lastName: candidate.lastName,
      email: candidate.email,
      phone: candidate.phone,
      dateOfBirth: candidate.dateOfBirth ? candidate.dateOfBirth.split('T')[0] : '',
      gender: candidate.gender,
      yearsOfExperience: candidate.yearsOfExperience,
      currentCompany: candidate.currentCompany,
      currentJobTitle: candidate.currentJobTitle,
      currentSalary: candidate.currentSalary,
      expectedSalary: candidate.expectedSalary,
      skills: candidate.skills,
      education: candidate.education,
      certifications: candidate.certifications,
      noticePeriodDays: candidate.noticePeriodDays,
      availableFrom: candidate.availableFrom ? candidate.availableFrom.split('T')[0] : '',
      resumeUrl: candidate.resumeUrl,
      linkedInUrl: candidate.linkedInUrl,
      portfolioUrl: candidate.portfolioUrl,
      address: candidate.address,
      city: candidate.city,
      country: candidate.country,
      postalCode: candidate.postalCode,
      preferredWorkLocation: candidate.preferredWorkLocation,
      willingToRelocate: candidate.willingToRelocate,
      notes: candidate.notes,
    });
    this.showModal = true;
  }

  viewDetails(candidate: Candidate): void {
    this.selectedCandidate = candidate;
    this.showDetailModal = true;
  }

  closeDetailModal(): void {
    this.showDetailModal = false;
    this.selectedCandidate = null;
  }

  closeModal(): void {
    this.showModal = false;
    this.candidateForm.reset();
  }

  submitForm(): void {
    if (this.candidateForm.invalid) {
      this.candidateForm.markAllAsTouched();
      return;
    }

    this.isSubmitting = true;
    const formData = this.candidateForm.value;

    // Clean up empty strings to undefined for optional fields
    const cleanData: CreateCandidateDto = {
      firstName: formData.firstName,
      lastName: formData.lastName,
      email: formData.email,
      phone: formData.phone || undefined,
      dateOfBirth: formData.dateOfBirth || undefined,
      gender: formData.gender || undefined,
      yearsOfExperience: formData.yearsOfExperience
        ? Number(formData.yearsOfExperience)
        : undefined,
      currentCompany: formData.currentCompany || undefined,
      currentJobTitle: formData.currentJobTitle || undefined,
      currentSalary: formData.currentSalary ? Number(formData.currentSalary) : undefined,
      expectedSalary: formData.expectedSalary ? Number(formData.expectedSalary) : undefined,
      skills: formData.skills || undefined,
      education: formData.education || undefined,
      certifications: formData.certifications || undefined,
      noticePeriodDays: formData.noticePeriodDays ? Number(formData.noticePeriodDays) : undefined,
      availableFrom: formData.availableFrom || undefined,
      resumeUrl: formData.resumeUrl || undefined,
      linkedInUrl: formData.linkedInUrl || undefined,
      portfolioUrl: formData.portfolioUrl || undefined,
      address: formData.address || undefined,
      city: formData.city || undefined,
      country: formData.country || undefined,
      postalCode: formData.postalCode || undefined,
      preferredWorkLocation: formData.preferredWorkLocation || undefined,
      willingToRelocate: formData.willingToRelocate || false,
      notes: formData.notes || undefined,
    };

    if (this.isEditing && this.editingId) {
      this.candidateService.update(this.editingId, cleanData).subscribe({
        next: (response) => {
          if (!response.hasError) {
            this.toastService.show('Candidate updated successfully', 'success');
            this.closeModal();
            this.loadCandidates();
          } else {
            this.toastService.show(response.errorMessage || 'Failed to update candidate', 'error');
          }
          this.isSubmitting = false;
        },
        error: () => {
          this.toastService.show('An error occurred while updating', 'error');
          this.isSubmitting = false;
        },
      });
    } else {
      this.candidateService.create(cleanData).subscribe({
        next: (response) => {
          if (!response.hasError) {
            this.toastService.show('Candidate created successfully', 'success');
            this.closeModal();
            this.loadCandidates();
          } else {
            this.toastService.show(response.errorMessage || 'Failed to create candidate', 'error');
          }
          this.isSubmitting = false;
        },
        error: () => {
          this.toastService.show('An error occurred while creating', 'error');
          this.isSubmitting = false;
        },
      });
    }
  }

  confirmDelete(candidate: Candidate): void {
    this.deletingCandidate = candidate;
    this.showDeleteModal = true;
  }

  closeDeleteModal(): void {
    this.showDeleteModal = false;
    this.deletingCandidate = null;
  }

  deleteCandidate(): void {
    if (!this.deletingCandidate) return;

    this.isDeleting = true;
    this.candidateService.delete(this.deletingCandidate.id).subscribe({
      next: (response) => {
        if (!response.hasError) {
          this.toastService.show('Candidate deleted successfully', 'success');
          this.closeDeleteModal();
          this.loadCandidates();
        } else {
          this.toastService.show(response.errorMessage || 'Failed to delete candidate', 'error');
        }
        this.isDeleting = false;
      },
      error: () => {
        this.toastService.show('An error occurred while deleting', 'error');
        this.isDeleting = false;
      },
    });
  }

  formatDate(date: string | undefined): string {
    if (!date) return '-';
    return new Date(date).toLocaleDateString();
  }

  getInitials(firstName: string, lastName: string): string {
    return `${firstName.charAt(0)}${lastName.charAt(0)}`.toUpperCase();
  }
}
