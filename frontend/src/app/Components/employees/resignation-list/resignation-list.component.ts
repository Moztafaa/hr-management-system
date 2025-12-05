import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ResignationService } from '../../../Services/resignation.service';
import { ToastService } from '../../../Services/toast.service';
import {
  Resignation,
  ResignationStatusLabels,
  ResignationStatusColors,
} from '../../../models/ess/resignation';

@Component({
  selector: 'app-resignation-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './resignation-list.component.html',
  styleUrls: ['./resignation-list.component.css'],
})
export class ResignationListComponent implements OnInit {
  private resignationService = inject(ResignationService);
  private toastService = inject(ToastService);

  resignations: Resignation[] = [];
  filteredResignations: Resignation[] = [];

  isLoading = true;
  error: string | null = null;

  // Modal state
  showApprovalModal = false;
  showRejectionModal = false;
  selectedResignation: Resignation | null = null;
  approvalComments = '';
  rejectionReason = '';
  isSubmitting = false;

  // Filter state
  statusFilter: string = 'all';

  ResignationStatusLabels = ResignationStatusLabels;
  ResignationStatusColors = ResignationStatusColors;

  ngOnInit(): void {
    this.loadResignations();
  }

  loadResignations(): void {
    this.isLoading = true;
    this.error = null;

    this.resignationService.getAllResignations().subscribe({
      next: (response) => {
        if (!response.hasError && response.data) {
          this.resignations = response.data;
          this.applyFilter();
        } else {
          this.error = response.errorMessage || 'Failed to load resignations';
        }
        this.isLoading = false;
      },
      error: () => {
        this.error = 'An error occurred while loading resignations';
        this.isLoading = false;
      },
    });
  }

  applyFilter(): void {
    if (this.statusFilter === 'all') {
      this.filteredResignations = [...this.resignations];
    } else {
      this.filteredResignations = this.resignations.filter((r) => r.status === this.statusFilter);
    }
  }

  onFilterChange(): void {
    this.applyFilter();
  }

  openApprovalModal(resignation: Resignation): void {
    this.selectedResignation = resignation;
    this.approvalComments = '';
    this.showApprovalModal = true;
  }

  closeApprovalModal(): void {
    this.showApprovalModal = false;
    this.selectedResignation = null;
    this.approvalComments = '';
  }

  openRejectionModal(resignation: Resignation): void {
    this.selectedResignation = resignation;
    this.rejectionReason = '';
    this.showRejectionModal = true;
  }

  closeRejectionModal(): void {
    this.showRejectionModal = false;
    this.selectedResignation = null;
    this.rejectionReason = '';
  }

  confirmApproval(): void {
    if (!this.selectedResignation) return;

    this.isSubmitting = true;

    this.resignationService
      .approveResignation({
        resignationId: this.selectedResignation.id,
        approverId: this.selectedResignation.reviewedById || 0,
        level: 'Manager',
        comments: this.approvalComments || undefined,
      })
      .subscribe({
        next: (response) => {
          if (!response.hasError) {
            this.toastService.success('Resignation approved successfully');
            this.closeApprovalModal();
            this.loadResignations();
          } else {
            this.toastService.error(response.errorMessage || 'Failed to approve resignation');
          }
          this.isSubmitting = false;
        },
        error: () => {
          this.toastService.error('An error occurred while approving');
          this.isSubmitting = false;
        },
      });
  }

  confirmRejection(): void {
    if (!this.selectedResignation || !this.rejectionReason.trim()) {
      this.toastService.warning('Please provide a rejection reason');
      return;
    }

    this.isSubmitting = true;

    this.resignationService
      .rejectResignation(
        {
          resignationId: this.selectedResignation.id,
          approverId: this.selectedResignation.reviewedById || 0,
          level: 'Manager',
          comments: this.rejectionReason,
        },
        this.rejectionReason
      )
      .subscribe({
        next: (response) => {
          if (!response.hasError) {
            this.toastService.success('Resignation rejected');
            this.closeRejectionModal();
            this.loadResignations();
          } else {
            this.toastService.error(response.errorMessage || 'Failed to reject resignation');
          }
          this.isSubmitting = false;
        },
        error: () => {
          this.toastService.error('An error occurred while rejecting');
          this.isSubmitting = false;
        },
      });
  }

  getStatusClass(status: string): string {
    switch (status) {
      case 'Pending':
        return 'bg-warning text-dark';
      case 'Approved':
        return 'bg-success';
      case 'Rejected':
        return 'bg-danger';
      case 'Withdrawn':
        return 'bg-secondary';
      default:
        return 'bg-light';
    }
  }

  formatDate(dateString: string): string {
    return new Date(dateString).toLocaleDateString('en-US', {
      year: 'numeric',
      month: 'short',
      day: 'numeric',
    });
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

  get totalCount(): number {
    return this.resignations.length;
  }
}
