import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { DeductionService } from '../../../Services/deduction.service';
import { DeductionDto, CreateDeductionDto } from '../../../models/deduction';
import { ToastService } from '../../../Services/toast.service';

@Component({
    selector: 'app-deduction-list',
    standalone: true,
    imports: [CommonModule, FormsModule],
    templateUrl: './deduction-list.component.html',
    styleUrls: ['./deduction-list.component.css'],
})
export class DeductionListComponent implements OnInit {
    private service = inject(DeductionService);
    private toastService = inject(ToastService);

    deductions: DeductionDto[] = [];
    isLoading = true;
    error: string | null = null;

    // Modal state
    showModal = false;
    showDeleteModal = false;
    showDetailsModal = false;
    isEditing = false;
    isSaving = false;

    // Form data
    formData: CreateDeductionDto = { name: '', amount: 0, isPercentage: false };
    selectedDeduction: DeductionDto | null = null;
    deductionToDelete: DeductionDto | null = null;
    deductionToView: DeductionDto | null = null;
    searchTerm: string = '';

    get filteredDeductions() {
        if (!this.searchTerm) return this.deductions;
        return this.deductions.filter(item =>
            item.name.toLowerCase().includes(this.searchTerm.toLowerCase())
        );
    }

    ngOnInit() {
        this.loadDeductions();
    }

    loadDeductions() {
        this.isLoading = true;
        this.service.getAll().subscribe({
            next: (res) => {
                if (!res.hasError && res.data) {
                    this.deductions = res.data;
                } else {
                    this.error = res.errorMessage;
                }
                this.isLoading = false;
            },
            error: () => {
                this.error = 'Failed to load deductions';
                this.isLoading = false;
            },
        });
    }

    openCreateModal() {
        this.isEditing = false;
        this.formData = { name: '', amount: 0, isPercentage: false };
        this.showModal = true;
    }

    openEditModal(deduction: DeductionDto) {
        this.isEditing = true;
        this.selectedDeduction = deduction;
        this.formData = {
            name: deduction.name,
            amount: deduction.amount,
            isPercentage: deduction.isPercentage,
        };
        this.showModal = true;
    }

    closeModal() {
        this.showModal = false;
        this.selectedDeduction = null;
    }

    saveDeduction() {
        if (!this.formData.name.trim()) {
            this.toastService.error('Name is required');
            return;
        }

        if (this.formData.amount <= 0) {
            this.toastService.error('Amount must be greater than 0');
            return;
        }

        this.isSaving = true;
        if (this.isEditing && this.selectedDeduction) {
            this.service.update(this.selectedDeduction.id, this.formData).subscribe({
                next: (res) => {
                    if (res && !res.hasError) {
                        this.toastService.success('Deduction updated successfully');
                        this.closeModal();
                        this.loadDeductions();
                    } else if (res && res.hasError) {
                        this.toastService.error(res.errorMessage || 'Failed to update deduction');
                    } else {
                        // Handle null response (API might return 204 No Content)
                        this.toastService.success('Deduction updated successfully');
                        this.closeModal();
                        this.loadDeductions();
                    }
                    this.isSaving = false;
                },
                error: () => {
                    this.toastService.error('An error occurred');
                    this.isSaving = false;
                },
            });
        } else {
            this.service.create(this.formData).subscribe({
                next: (res) => {
                    if (!res.hasError) {
                        this.toastService.success('Deduction created successfully');
                        this.closeModal();
                        this.loadDeductions();
                    } else {
                        this.toastService.error(res.errorMessage || 'Failed to create deduction');
                    }
                    this.isSaving = false;
                },
                error: () => {
                    this.toastService.error('An error occurred');
                    this.isSaving = false;
                },
            });
        }
    }

    openDeleteModal(deduction: DeductionDto) {
        this.deductionToDelete = deduction;
        this.showDeleteModal = true;
    }

    closeDeleteModal() {
        this.showDeleteModal = false;
        this.deductionToDelete = null;
    }

    confirmDelete() {
        if (!this.deductionToDelete) return;

        this.isSaving = true;
        this.service.delete(this.deductionToDelete.id).subscribe({
            next: (res) => {
                if (res && !res.hasError) {
                    this.toastService.success('Deduction deleted successfully');
                    this.closeDeleteModal();
                    this.loadDeductions();
                } else if (res && res.hasError) {
                    this.toastService.error(res.errorMessage || 'Failed to delete deduction');
                } else {
                    // Handle null response (API might return 204 No Content)
                    this.toastService.success('Deduction deleted successfully');
                    this.closeDeleteModal();
                    this.loadDeductions();
                }
                this.isSaving = false;
            },
            error: () => {
                this.toastService.error('An error occurred');
                this.isSaving = false;
            },
        });
    }

    openDetailsModal(deduction: DeductionDto) {
        this.deductionToView = deduction;
        this.showDetailsModal = true;
    }

    closeDetailsModal() {
        this.showDetailsModal = false;
        this.deductionToView = null;
    }

    formatCurrency(amount: number): string {
        return new Intl.NumberFormat('en-US', {
            style: 'currency',
            currency: 'USD',
        }).format(amount);
    }
}
