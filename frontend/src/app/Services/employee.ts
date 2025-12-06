import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {
  Employee as EmployeeModel,
  CreateEmployeeDto,
  UpdateEmployeeDto,
  RegisterEmployeeDto,
  RegisterEmployeeResponseDto,
} from '../models/employee';
import { ApiResponse } from '../models/api-response';
import { PagedResult } from '../models/paged-result';

@Injectable({
  providedIn: 'root',
})
export class EmployeeService {
  private http = inject(HttpClient);
  // Using the development URL as per requirements, ideally this should be in environment files
  private apiUrl = 'http://localhost:5093/api/Employee';
  private accountUrl = 'http://localhost:5093/api/Account';

  getEmployees(): Observable<ApiResponse<EmployeeModel[]>> {
    return this.http.get<ApiResponse<EmployeeModel[]>>(this.apiUrl);
  }

  getEmployee(id: number): Observable<ApiResponse<EmployeeModel>> {
    return this.http.get<ApiResponse<EmployeeModel>>(`${this.apiUrl}/${id}`);
  }

  createEmployee(employee: CreateEmployeeDto): Observable<ApiResponse<EmployeeModel>> {
    return this.http.post<ApiResponse<EmployeeModel>>(this.apiUrl, employee);
  }

  // Register employee - creates both User account and Employee record
  registerEmployee(
    employee: RegisterEmployeeDto
  ): Observable<ApiResponse<RegisterEmployeeResponseDto>> {
    return this.http.post<ApiResponse<RegisterEmployeeResponseDto>>(
      `${this.accountUrl}/register-employee`,
      employee
    );
  }

  updateEmployee(id: number, employee: UpdateEmployeeDto): Observable<ApiResponse<boolean>> {
    return this.http.put<ApiResponse<boolean>>(`${this.apiUrl}/${id}`, employee);
  }

  deleteEmployee(id: number): Observable<ApiResponse<boolean>> {
    return this.http.delete<ApiResponse<boolean>>(`${this.apiUrl}/${id}`);
  }

  updateEmployeeImage(employeeId: number, image: File): Observable<ApiResponse<EmployeeModel>> {
    const formData = new FormData();
    formData.append('image', image);
    return this.http.post<ApiResponse<EmployeeModel>>(
      `${this.apiUrl}/${employeeId}/update-image`,
      formData
    );
  }

  getPagedEmployees(pageNumber: number = 1, pageSize: number = 10): Observable<ApiResponse<PagedResult<EmployeeModel>>> {
    const params = `?pageNumber=${pageNumber}&pageSize=${pageSize}`;
    return this.http.get<ApiResponse<PagedResult<EmployeeModel>>>(`${this.apiUrl}/paged${params}`);
  }
}

// Keep the old class name as alias for backward compatibility
export { EmployeeService as Employee };
