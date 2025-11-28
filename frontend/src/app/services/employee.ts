import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Employee as EmployeeModel } from '../models/employee';
import { ApiResponse } from '../models/api-response';

@Injectable({
  providedIn: 'root',
})
export class Employee {
  private http = inject(HttpClient);
  // Using the development URL as per requirements, ideally this should be in environment files
  private apiUrl = 'http://localhost:5093/api/Employee';

  getEmployees(): Observable<ApiResponse<EmployeeModel[]>> {
    return this.http.get<ApiResponse<EmployeeModel[]>>(this.apiUrl);
  }

  getEmployee(id: number): Observable<ApiResponse<EmployeeModel>> {
    return this.http.get<ApiResponse<EmployeeModel>>(`${this.apiUrl}/${id}`);
  }

  createEmployee(employee: any): Observable<ApiResponse<EmployeeModel>> {
    return this.http.post<ApiResponse<EmployeeModel>>(this.apiUrl, employee);
  }

  updateEmployee(id: number, employee: any): Observable<ApiResponse<EmployeeModel>> {
    return this.http.put<ApiResponse<EmployeeModel>>(`${this.apiUrl}/${id}`, employee);
  }

  deleteEmployee(id: number): Observable<ApiResponse<boolean>> {
    return this.http.delete<ApiResponse<boolean>>(`${this.apiUrl}/${id}`);
  }
}
