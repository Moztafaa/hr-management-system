import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ESSService } from '../../../Services/ess.service';
import { ToastService } from '../../../Services/toast.service';
import { ESSProfile, UpdateESSProfile } from '../../../models/ess';

@Component({
  selector: 'app-ess-profile',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './ess-profile.component.html',
  styleUrls: ['./ess-profile.component.css'],
})
export class EssProfileComponent implements OnInit {
  private essService = inject(ESSService);
  private toastService = inject(ToastService);
  private fb = inject(FormBuilder);

  profile: ESSProfile | null = null;
  isLoading = true;
  isSaving = false;
  isEditing = false;
  error: string | null = null;

  profileForm: FormGroup = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    contactNumber: [''],
    address: [''],
  });

  ngOnInit(): void {
    this.loadProfile();
  }

  loadProfile(): void {
    this.isLoading = true;
    this.error = null;

    this.essService.getProfile().subscribe({
      next: (response) => {
        if (!response.hasError && response.data) {
          this.profile = response.data;
          this.updateFormValues();
        } else {
          this.error = response.errorMessage || 'Failed to load profile';
          this.toastService.error(this.error);
        }
        this.isLoading = false;
      },
      error: () => {
        this.error = 'An error occurred while loading the profile';
        this.toastService.error(this.error);
        this.isLoading = false;
      },
    });
  }

  updateFormValues(): void {
    if (this.profile) {
      this.profileForm.patchValue({
        email: this.profile.email,
        contactNumber: this.profile.contactNumber || '',
        address: this.profile.address || '',
      });
    }
  }

  toggleEdit(): void {
    this.isEditing = !this.isEditing;
    if (!this.isEditing) {
      this.updateFormValues();
    }
  }

  saveProfile(): void {
    if (this.profileForm.invalid) {
      this.profileForm.markAllAsTouched();
      return;
    }

    this.isSaving = true;
    const updateData: UpdateESSProfile = {
      email: this.profileForm.value.email,
      contactNumber: this.profileForm.value.contactNumber || undefined,
      address: this.profileForm.value.address || undefined,
    };

    this.essService.updateProfile(updateData).subscribe({
      next: (response) => {
        if (!response.hasError) {
          this.toastService.success('Profile updated successfully');
          this.isEditing = false;
          this.loadProfile();
        } else {
          this.toastService.error(response.errorMessage || 'Failed to update profile');
        }
        this.isSaving = false;
      },
      error: () => {
        this.toastService.error('An error occurred while updating the profile');
        this.isSaving = false;
      },
    });
  }

  formatDate(dateString: string): string {
    return new Date(dateString).toLocaleDateString('en-US', {
      year: 'numeric',
      month: 'long',
      day: 'numeric',
    });
  }

  getFieldError(fieldName: string): string | null {
    const control = this.profileForm.get(fieldName);
    if (control?.touched && control?.errors) {
      if (control.errors['required']) return `${fieldName} is required`;
      if (control.errors['email']) return 'Please enter a valid email address';
    }
    return null;
  }
}
