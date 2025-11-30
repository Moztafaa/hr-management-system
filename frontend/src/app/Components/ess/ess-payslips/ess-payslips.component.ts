import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ESSService } from '../../../Services/ess.service';
import { ToastService } from '../../../Services/toast.service';
import { AuthService } from '../../../Services/auth.service';
import { Payslip } from '../../../models/ess';

@Component({
  selector: 'app-ess-payslips',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './ess-payslips.component.html',
  styleUrls: ['./ess-payslips.component.css'],
})
export class EssPayslipsComponent implements OnInit {
  private essService = inject(ESSService);
  private toastService = inject(ToastService);
  private authService = inject(AuthService);

  payslips: Payslip[] = [];
  filteredPayslips: Payslip[] = [];
  selectedPayslip: Payslip | null = null;

  isLoading = true;
  isLoadingDetails = false;
  showDetails = false;
  error: string | null = null;

  // Filters
  selectedYear: number = new Date().getFullYear();
  availableYears: number[] = [];

  ngOnInit(): void {
    this.loadPayslips();
  }

  loadPayslips(): void {
    this.isLoading = true;
    this.error = null;

    this.essService.getPayslips().subscribe({
      next: (response) => {
        if (!response.hasError && response.data) {
          this.payslips = response.data;
          this.extractAvailableYears();
          this.applyFilter();
        } else {
          this.error = response.errorMessage || 'Failed to load payslips';
          this.toastService.error(this.error);
        }
        this.isLoading = false;
      },
      error: () => {
        this.error = 'An error occurred while loading payslips';
        this.toastService.error(this.error);
        this.isLoading = false;
      },
    });
  }

  extractAvailableYears(): void {
    const years = new Set<number>();
    this.payslips.forEach((p) => years.add(p.year));
    this.availableYears = Array.from(years).sort((a, b) => b - a);

    // If no payslips, add current year
    if (this.availableYears.length === 0) {
      this.availableYears = [new Date().getFullYear()];
    }

    // Ensure selected year is in the list
    if (!this.availableYears.includes(this.selectedYear)) {
      this.selectedYear = this.availableYears[0];
    }
  }

  applyFilter(): void {
    this.filteredPayslips = this.payslips
      .filter((p) => p.year === this.selectedYear)
      .sort((a, b) => b.month - a.month);
  }

  onYearChange(): void {
    this.applyFilter();
    this.closeDetails();
  }

  viewDetails(payslip: Payslip): void {
    this.selectedPayslip = payslip;
    this.showDetails = true;

    // Load full details if not already loaded
    if (!payslip.allowances || !payslip.deductions) {
      this.loadPayslipDetails(payslip.id);
    }
  }

  loadPayslipDetails(payslipId: number): void {
    this.isLoadingDetails = true;

    this.essService.getPayslip(payslipId).subscribe({
      next: (response) => {
        if (!response.hasError && response.data) {
          this.selectedPayslip = response.data;
        }
        this.isLoadingDetails = false;
      },
      error: () => {
        this.toastService.error('Failed to load payslip details');
        this.isLoadingDetails = false;
      },
    });
  }

  closeDetails(): void {
    this.showDetails = false;
    this.selectedPayslip = null;
  }

  downloadPdf(payslipId: number): void {
    const url = this.essService.getPayslipPdfUrl(payslipId);
    const token = this.authService.getToken();

    // Open in new tab with auth header
    window.open(`${url}?token=${token}`, '_blank');
  }

  getMonthName(month: number): string {
    const months = [
      'January',
      'February',
      'March',
      'April',
      'May',
      'June',
      'July',
      'August',
      'September',
      'October',
      'November',
      'December',
    ];
    return months[month - 1] || '';
  }

  formatCurrency(amount: number): string {
    return new Intl.NumberFormat('en-US', {
      style: 'currency',
      currency: 'USD',
    }).format(amount);
  }

  formatDate(dateString: string): string {
    return new Date(dateString).toLocaleDateString('en-US', {
      year: 'numeric',
      month: 'short',
      day: 'numeric',
    });
  }

  get totalNetSalary(): number {
    return this.filteredPayslips.reduce((sum, p) => sum + p.netSalary, 0);
  }

  get totalAllowances(): number {
    return this.filteredPayslips.reduce((sum, p) => sum + p.totalAllowances, 0);
  }

  get totalDeductions(): number {
    return this.filteredPayslips.reduce((sum, p) => sum + p.totalDeductions, 0);
  }
}
