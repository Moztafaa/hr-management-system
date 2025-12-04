import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { PerformanceService } from '../../../Services/performance/performance';
import { Employee as EmployeeService } from '../../../Services/employee';
import { KPIDto } from '../../../models/performance-dtos';

@Component({
  selector: 'app-kpis-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './kpis-list.html',
  styleUrl: './kpis-list.css',
})
export class KPIsListComponent implements OnInit {
  private performanceService = inject(PerformanceService);
  private employeeService = inject(EmployeeService);

  kpis: KPIDto[] = [];
  isLoading = true;
  error: string | null = null;
  showCreateForm = false;

  createForm = {
    name: '',
    target: 0,
    unit: '',
    code: '',
  };

  reviews: any[] = [];

  ngOnInit() {
    this.loadReviews();
  }

  loadReviews() {
    const employeeId = this.employeeService.getEmployeeId();
    this.performanceService.getReviewsByEmployee(employeeId).subscribe({
      next: (res) => {
        if (!res.hasError) {
          this.reviews = res.data || [];
        }
      },
      error: (err) => {
        console.error('Error loading reviews', err);
      },
    });
  }

  toggleCreateForm() {
    this.showCreateForm = !this.showCreateForm;
    if (!this.showCreateForm) {
      this.resetForm();
    }
  }

  createKpi() {
    if (!this.createForm.name || !this.createForm.target) {
      this.error = 'Please fill in all required fields';
      return;
    }

    const payload = {
      name: this.createForm.name,
      target: this.createForm.target,
      unit: this.createForm.unit || undefined,
      code: this.createForm.code || undefined,
    };

    this.performanceService.createKpi(payload).subscribe({
      next: (res) => {
        if (!res.hasError) {
          alert('✔️ KPI created successfully');
          this.showCreateForm = false;
          this.resetForm();
          this.kpis.push(res.data!);
        } else {
          this.error = res.errorMessage || 'Failed to create KPI';
        }
      },
      error: (err) => {
        console.error('Error creating KPI', err);
        this.error = 'Error creating KPI';
      },
    });
  }

  resetForm() {
    this.createForm = {
      name: '',
      target: 0,
      unit: '',
      code: '',
    };
  }

  cancel() {
    this.showCreateForm = false;
    this.resetForm();
  }
}
