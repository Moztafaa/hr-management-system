import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ESSService } from '../../../Services/ess.service';
import { ToastService } from '../../../Services/toast.service';
import {
  Resignation,
  CreateResignation,
  ResignationStatusLabels,
  ResignationStatusColors,
} from '../../../models/ess';

@Component({
  selector: 'app-ess-resignations',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './ess-resignations.component.html',
  styleUrls: ['./ess-resignations.component.css'],
})
export class EssResignationsComponent implements OnInit {
  private essService = inject(ESSService);
  private toastService = inject(ToastService);
  private fb = inject(FormBuilder);

  resignations: Resignation[] = [];
  activeResignation: Resignation | null = null;

  isLoading = true;
  isSubmitting = false;
  showModal = false;
  showWithdrawModal = false;
  error: string | null = null;

  ResignationStatusLabels = ResignationStatusLabels;
  ResignationStatusColors = ResignationStatusColors;

  resignationForm: FormGroup = this.fb.group({
    reason: ['', [Validators.required, Validators.minLength(10), Validators.maxLength(2000)]],
    lastWorkingDate: ['', Validators.required],
    isImmediateResignation: [false],
    handoverNotes: ['', Validators.maxLength(2000)],
  });

  selectedResignationForWithdraw: Resignation | null = null;

  ngOnInit(): void {
    this.loadData();
  }

  loadData(): void {
    this.isLoading = true;
    this.error = null;

    Promise.all([this.loadResignations(), this.loadActiveResignation()]).finally(() => {
      this.isLoading = false;
    });
  }

  private loadResignations(): Promise<void> {
    return new Promise((resolve) => {
      this.essService.getMyResignations().subscribe({
        next: (response) => {
          if (!response.hasError && response.data) {
            this.resignations = response.data;
          } else {
            this.error = response.errorMessage || 'Failed to load resignations';
          }
          resolve();
        },
        error: () => {
          this.error = 'An error occurred while loading resignations';
          resolve();
        },
      });
    });
  }

  private loadActiveResignation(): Promise<void> {
    return new Promise((resolve) => {
      this.essService.getActiveResignation().subscribe({
        next: (response) => {
          if (!response.hasError) {
            this.activeResignation = response.data;
          }
          resolve();
        },
        error: () => resolve(),
      });
    });
  }

  openModal(): void {
    if (this.activeResignation) {
      this.toastService.warning('You already have an active resignation request');
      return;
    }
    this.showModal = true;
    this.resignationForm.reset();
    this.resignationForm.patchValue({ isImmediateResignation: false });

    // Set default last working date to 30 days from now
    const defaultDate = new Date();
    defaultDate.setDate(defaultDate.getDate() + 30);
    this.resignationForm.patchValue({
      lastWorkingDate: defaultDate.toISOString().split('T')[0],
    });
  }

  closeModal(): void {
    this.showModal = false;
    this.resignationForm.reset();
  }

  submitResignation(): void {
    if (this.resignationForm.invalid) {
      this.resignationForm.markAllAsTouched();
      return;
    }

    const formValue = this.resignationForm.value;

    // Validate last working date
    const lastWorkingDate = new Date(formValue.lastWorkingDate);
    const today = new Date();
    today.setHours(0, 0, 0, 0);

    if (lastWorkingDate < today) {
      this.toastService.error('Last working date cannot be in the past');
      return;
    }

    this.isSubmitting = true;

    const request: CreateResignation = {
      reason: formValue.reason,
      lastWorkingDate: formValue.lastWorkingDate,
      isImmediateResignation: formValue.isImmediateResignation || false,
      handoverNotes: formValue.handoverNotes || undefined,
    };

    this.essService.submitResignation(request).subscribe({
      next: (response) => {
        if (!response.hasError) {
          this.toastService.success('Resignation request submitted successfully');
          this.closeModal();
          this.loadData();
        } else {
          this.toastService.error(response.errorMessage || 'Failed to submit resignation');
        }
        this.isSubmitting = false;
      },
      error: () => {
        this.toastService.error('An error occurred while submitting the resignation');
        this.isSubmitting = false;
      },
    });
  }

  openWithdrawModal(resignation: Resignation): void {
    this.selectedResignationForWithdraw = resignation;
    this.showWithdrawModal = true;
  }

  closeWithdrawModal(): void {
    this.showWithdrawModal = false;
    this.selectedResignationForWithdraw = null;
  }

  confirmWithdraw(): void {
    if (!this.selectedResignationForWithdraw) return;

    this.isSubmitting = true;

    this.essService.withdrawResignation(this.selectedResignationForWithdraw.id).subscribe({
      next: (response) => {
        if (!response.hasError) {
          this.toastService.success('Resignation withdrawn successfully');
          this.closeWithdrawModal();
          this.loadData();
        } else {
          this.toastService.error(response.errorMessage || 'Failed to withdraw resignation');
        }
        this.isSubmitting = false;
      },
      error: () => {
        this.toastService.error('An error occurred while withdrawing the resignation');
        this.isSubmitting = false;
      },
    });
  }

  getStatusClass(status: string): string {
    switch (status) {
      case 'Pending':
        return 'status-pending';
      case 'Approved':
        return 'status-approved';
      case 'Rejected':
        return 'status-rejected';
      case 'Withdrawn':
        return 'status-withdrawn';
      default:
        return '';
    }
  }

  formatDate(dateString: string): string {
    return new Date(dateString).toLocaleDateString('en-US', {
      year: 'numeric',
      month: 'short',
      day: 'numeric',
    });
  }

  calculateNoticePeriod(): number {
    const lastWorkingDate = this.resignationForm.get('lastWorkingDate')?.value;
    if (!lastWorkingDate) return 0;

    const today = new Date();
    today.setHours(0, 0, 0, 0);
    const lastDate = new Date(lastWorkingDate);
    const diffTime = lastDate.getTime() - today.getTime();
    const diffDays = Math.ceil(diffTime / (1000 * 60 * 60 * 24));

    return diffDays > 0 ? diffDays : 0;
  }

  get hasActiveResignation(): boolean {
    return this.activeResignation !== null;
  }

  get pendingCount(): number {
    return this.resignations.filter((r) => r.status === 'Pending').length;
  }

  get approvedCount(): number {
    return this.resignations.filter((r) => r.status === 'Approved').length;
  }

  get rejectedCount(): number {
    return this.resignations.filter((r) => r.status === 'Rejected').length;
  }

  getFieldError(fieldName: string): string | null {
    const control = this.resignationForm.get(fieldName);
    if (control?.touched && control?.errors) {
      if (control.errors['required']) return 'This field is required';
      if (control.errors['minlength'])
        return `Minimum ${control.errors['minlength'].requiredLength} characters required`;
      if (control.errors['maxlength'])
        return `Maximum ${control.errors['maxlength'].requiredLength} characters allowed`;
    }
    return null;
  }
}
