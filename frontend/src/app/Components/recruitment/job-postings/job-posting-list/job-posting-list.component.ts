import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { JobPostingService } from '../../../../Services/recruitment/job-posting.service';
import { DepartmentService } from '../../../../Services/department.service';
import { DesignationService } from '../../../../Services/designation.service';
import {
  JobPosting,
  CreateJobPostingDto,
  UpdateJobPostingDto,
} from '../../../../models/recruitment/job-posting';
import { Department } from '../../../../models/department';
import { Designation } from '../../../../models/designation';
import { ToastService } from '../../../../Services/toast.service';

@Component({
  selector: 'app-job-posting-list',
  standalone: true,
  imports: [CommonModule, RouterModule, ReactiveFormsModule],
  templateUrl: './job-posting-list.component.html',
  styleUrls: ['./job-posting-list.component.css'],
})
export class JobPostingListComponent implements OnInit {
  private jobPostingService = inject(JobPostingService);
  private departmentService = inject(DepartmentService);
  private designationService = inject(DesignationService);
  private toastService = inject(ToastService);
  private fb = inject(FormBuilder);

  jobPostings: JobPosting[] = [];
  departments: Department[] = [];
  designations: Designation[] = [];
  isLoading = true;
  error: string | null = null;
  showActiveOnly = false;

  // Modal state
  showModal = false;
  isEditing = false;
  editingId: number | null = null;
  isSubmitting = false;

  // Delete confirmation
  showDeleteModal = false;
  deletingJob: JobPosting | null = null;
  isDeleting = false;

  jobForm: FormGroup = this.fb.group({
    title: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(200)]],
    description: [''],
    requirements: [''],
    postedDate: ['', Validators.required],
    closingDate: [''],
    isActive: [true],
    departmentId: ['', Validators.required],
    designationId: ['', Validators.required],
  });

  ngOnInit(): void {
    this.loadData();
  }

  loadData(): void {
    this.loadJobPostings();
    this.loadDepartments();
    this.loadDesignations();
  }

  loadJobPostings(): void {
    this.isLoading = true;
    this.error = null;

    const request = this.showActiveOnly
      ? this.jobPostingService.getActive()
      : this.jobPostingService.getAll();

    request.subscribe({
      next: (response) => {
        if (!response.hasError && response.data) {
          this.jobPostings = response.data;
        } else {
          this.error = response.errorMessage || 'Failed to load job postings';
        }
        this.isLoading = false;
      },
      error: () => {
        this.error = 'An error occurred while loading job postings';
        this.isLoading = false;
      },
    });
  }

  loadDepartments(): void {
    this.departmentService.getAll().subscribe({
      next: (response) => {
        if (!response.hasError && response.data) {
          this.departments = response.data;
        }
      },
    });
  }

  loadDesignations(): void {
    this.designationService.getAll().subscribe({
      next: (response) => {
        if (!response.hasError && response.data) {
          this.designations = response.data;
        }
      },
    });
  }

  toggleActiveFilter(): void {
    this.showActiveOnly = !this.showActiveOnly;
    this.loadJobPostings();
  }

  openCreateModal(): void {
    this.isEditing = false;
    this.editingId = null;
    this.jobForm.reset({
      isActive: true,
      postedDate: new Date().toISOString().split('T')[0],
    });
    this.showModal = true;
  }

  openEditModal(job: JobPosting): void {
    this.isEditing = true;
    this.editingId = job.id;
    this.jobForm.patchValue({
      title: job.title,
      description: job.description,
      requirements: job.requirements,
      postedDate: job.postedDate ? job.postedDate.split('T')[0] : '',
      closingDate: job.closingDate ? job.closingDate.split('T')[0] : '',
      isActive: job.isActive,
      departmentId: job.departmentId,
      designationId: job.designationId,
    });
    this.showModal = true;
  }

  closeModal(): void {
    this.showModal = false;
    this.jobForm.reset();
  }

  submitForm(): void {
    if (this.jobForm.invalid) {
      this.jobForm.markAllAsTouched();
      return;
    }

    this.isSubmitting = true;
    const formData = this.jobForm.value;

    if (this.isEditing && this.editingId) {
      const updateData: UpdateJobPostingDto = {
        ...formData,
        departmentId: Number(formData.departmentId),
        designationId: Number(formData.designationId),
      };

      this.jobPostingService.update(this.editingId, updateData).subscribe({
        next: (response) => {
          if (!response.hasError) {
            this.toastService.show('Job posting updated successfully', 'success');
            this.closeModal();
            this.loadJobPostings();
          } else {
            this.toastService.show(
              response.errorMessage || 'Failed to update job posting',
              'error'
            );
          }
          this.isSubmitting = false;
        },
        error: () => {
          this.toastService.show('An error occurred while updating', 'error');
          this.isSubmitting = false;
        },
      });
    } else {
      const createData: CreateJobPostingDto = {
        ...formData,
        departmentId: Number(formData.departmentId),
        designationId: Number(formData.designationId),
      };

      this.jobPostingService.create(createData).subscribe({
        next: (response) => {
          if (!response.hasError) {
            this.toastService.show('Job posting created successfully', 'success');
            this.closeModal();
            this.loadJobPostings();
          } else {
            this.toastService.show(
              response.errorMessage || 'Failed to create job posting',
              'error'
            );
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

  confirmDelete(job: JobPosting): void {
    this.deletingJob = job;
    this.showDeleteModal = true;
  }

  closeDeleteModal(): void {
    this.showDeleteModal = false;
    this.deletingJob = null;
  }

  deleteJob(): void {
    if (!this.deletingJob) return;

    this.isDeleting = true;
    this.jobPostingService.delete(this.deletingJob.id).subscribe({
      next: (response) => {
        if (!response.hasError) {
          this.toastService.show('Job posting deleted successfully', 'success');
          this.closeDeleteModal();
          this.loadJobPostings();
        } else {
          this.toastService.show(response.errorMessage || 'Failed to delete job posting', 'error');
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

  isExpired(closingDate: string | undefined): boolean {
    if (!closingDate) return false;
    return new Date(closingDate) < new Date();
  }
}
