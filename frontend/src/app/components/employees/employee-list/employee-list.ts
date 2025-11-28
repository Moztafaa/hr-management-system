import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Employee } from '../../../services/employee';
import { Employee as EmployeeModel } from '../../../models/employee';

@Component({
  selector: 'app-employee-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './employee-list.html',
  styleUrls: ['./employee-list.css'],
})
export class EmployeeList implements OnInit {
  private employeeService = inject(Employee);
  employees: EmployeeModel[] = [];
  isLoading = true;
  error: string | null = null;

  ngOnInit() {
    this.loadEmployees();
  }

  loadEmployees() {
    this.isLoading = true;
    this.employeeService.getEmployees().subscribe({
      next: (response) => {
        if (!response.hasError && response.data) {
          this.employees = response.data;
        } else {
          this.error = response.errorMessage || 'Failed to load employees';
        }
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Error loading employees', err);
        this.error = 'An error occurred while loading employees';
        this.isLoading = false;
      },
    });
  }
}
