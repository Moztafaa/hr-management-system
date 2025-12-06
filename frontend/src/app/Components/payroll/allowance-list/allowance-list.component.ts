import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AllowanceService } from '../../../Services/allowance.service';
import { AllowanceDto, CreateAllowanceDto } from '../../../models/allowance';
import { ToastService } from '../../../Services/toast.service';

@Component({
    selector: 'app-allowance-list',
    standalone: true,
    imports: [CommonModule, FormsModule],
    templateUrl: './allowance-list.component.html',
    styleUrls: ['./allowance-list.component.css'],
})
export class AllowanceListComponent implements OnInit {
    private service = inject(AllowanceService);
    private toastService = inject(ToastService);

    allowances: AllowanceDto[] = [];
    isLoading = true;
    error: string | null = null;

    // Modal state
    showModal = false;
    showDeleteModal = false;
    showDetailsModal = false;
    isEditing = false;
    isSaving = false;

    // Form data
    formData: CreateAllowanceDto = { name: '', amount: 0, isPercentage: false };
    selectedAllowance: AllowanceDto | null = null;
    allowanceToDelete: AllowanceDto | null = null;
    allowanceToView: AllowanceDto | null = null;
    searchTerm: string = '';

    get filteredAllowances() {
        if (!this.searchTerm) return this.allowances;
        return this.allowances.filter(item =>
            item.name.toLowerCase().includes(this.searchTerm.toLowerCase())
        );
    }

    ngOnInit() {
        this.loadAllowances();
    }

    loadAllowances() {
        this.isLoading = true;
        this.service.getAll().subscribe({
            next: (res) => {
                if (!res.hasError && res.data) {
                    this.allowances = res.data;
                } else {
                    this.error = res.errorMessage;
                }
                this.isLoading = false;
            },
            error: () => {
                this.error = 'Failed to load allowances';
                this.isLoading = false;
            },
        });
    }

    openCreateModal() {
        this.isEditing = false;
        this.formData = { name: '', amount: 0, isPercentage: false };
        this.showModal = true;
    }

    openEditModal(allowance: AllowanceDto) {
        this.isEditing = true;
        this.selectedAllowance = allowance;
        this.formData = {
            name: allowance.name,
            amount: allowance.amount,
            isPercentage: allowance.isPercentage,
        };
        this.showModal = true;
    }

    closeModal() {
        this.showModal = false;
        this.selectedAllowance = null;
    }

    saveAllowance() {
        if (!this.formData.name.trim()) {
            this.toastService.error('Name is required');
            return;
        }

        if (this.formData.amount <= 0) {
            this.toastService.error('Amount must be greater than 0');
            return;
        }

        this.isSaving = true;
        if (this.isEditing && this.selectedAllowance) {
            this.service.update(this.selectedAllowance.id, this.formData).subscribe({
                next: (res) => {
                    if (res && !res.hasError) {
                        this.toastService.success('Allowance updated successfully');
                        this.closeModal();
                        this.loadAllowances();
                    } else if (res && res.hasError) {
                        this.toastService.error(res.errorMessage || 'Failed to update allowance');
                    } else {
                        // Handle null response (API might return 204 No Content)
                        this.toastService.success('Allowance updated successfully');
                        this.closeModal();
                        this.loadAllowances();
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
                        this.toastService.success('Allowance created successfully');
                        this.closeModal();
                        this.loadAllowances();
                    } else {
                        this.toastService.error(res.errorMessage || 'Failed to create allowance');
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

    openDeleteModal(allowance: AllowanceDto) {
        this.allowanceToDelete = allowance;
        this.showDeleteModal = true;
    }

    closeDeleteModal() {
        this.showDeleteModal = false;
        this.allowanceToDelete = null;
    }

    confirmDelete() {
        if (!this.allowanceToDelete) return;

        this.isSaving = true;
        this.service.delete(this.allowanceToDelete.id).subscribe({
            next: (res) => {
                if (res && !res.hasError) {
                    this.toastService.success('Allowance deleted successfully');
                    this.closeDeleteModal();
                    this.loadAllowances();
                } else if (res && res.hasError) {
                    this.toastService.error(res.errorMessage || 'Failed to delete allowance');
                } else {
                    // Handle null response (API might return 204 No Content)
                    this.toastService.success('Allowance deleted successfully');
                    this.closeDeleteModal();
                    this.loadAllowances();
                }
                this.isSaving = false;
            },
            error: () => {
                this.toastService.error('An error occurred');
                this.isSaving = false;
            },
        });
    }

    openDetailsModal(allowance: AllowanceDto) {
        this.allowanceToView = allowance;
        this.showDetailsModal = true;
    }

    closeDetailsModal() {
        this.showDetailsModal = false;
        this.allowanceToView = null;
    }

    formatCurrency(amount: number): string {
        return new Intl.NumberFormat('en-US', {
            style: 'currency',
            currency: 'USD',
        }).format(amount);
    }
}
