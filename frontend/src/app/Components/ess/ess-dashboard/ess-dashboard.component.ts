import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { ESSService } from '../../../Services/ess.service';
import { ToastService } from '../../../Services/toast.service';
import { AuthService } from '../../../Services/auth.service';
import { ESSDashboard, LeaveStatus, LeaveStatusLabels } from '../../../models/ess';

@Component({
  selector: 'app-ess-dashboard',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './ess-dashboard.component.html',
  styleUrls: ['./ess-dashboard.component.css'],
})
export class EssDashboardComponent implements OnInit {
  private essService = inject(ESSService);
  private toastService = inject(ToastService);
  private authService = inject(AuthService);
  private router = inject(Router);

  dashboard: ESSDashboard | null = null;
  isLoading = true;
  error: string | null = null;

  LeaveStatus = LeaveStatus;
  LeaveStatusLabels = LeaveStatusLabels;
  currentYear = new Date().getFullYear();

  ngOnInit(): void {
    // Check if user is authenticated
    if (!this.authService.getToken()) {
      this.error = 'You must be logged in to view the dashboard';
      this.toastService.warning('Please login to access ESS features');
      this.router.navigate(['/pages/login']);
      return;
    }
    this.loadDashboard();
  }

  loadDashboard(): void {
    this.isLoading = true;
    this.error = null;

    this.essService.getDashboard().subscribe({
      next: (response) => {
        if (!response.hasError && response.data) {
          this.dashboard = response.data;
        } else {
          this.error = response.errorMessage || 'Failed to load dashboard';
          this.toastService.error(this.error);
        }
        this.isLoading = false;
      },
      error: (err: HttpErrorResponse) => {
        console.error('Dashboard load error:', err);
        if (err.status === 401) {
          this.error = 'Session expired. Please login again.';
          this.toastService.error(this.error);
          this.authService.logout();
          this.router.navigate(['/pages/login']);
        } else if (err.status === 0) {
          this.error = 'Cannot connect to server. Please check if the backend is running.';
          this.toastService.error(this.error);
        } else {
          this.error = `Error ${err.status}: ${
            err.message || 'An error occurred while loading the dashboard'
          }`;
          this.toastService.error(this.error);
        }
        this.isLoading = false;
      },
    });
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
}
