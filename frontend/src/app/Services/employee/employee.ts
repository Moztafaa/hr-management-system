import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../models/api-response';
import { Employee as EmployeeModel } from '../../models/employee';

@Injectable({
  providedIn: 'root',
})
export class Employee {
  private apiUrl = 'https://localhost:7005/api/Employee'; // Backend API URL
  //private apiUrl = 'http://localhost:5093/api/Employee'; // Backend API URL

  constructor(private http: HttpClient) { }

  getEmployee(id: number): Observable<ApiResponse<EmployeeModel>> {
    return this.http.get<ApiResponse<EmployeeModel>>(`${this.apiUrl}/${id}`);
  }

  updateEmployeeImage(employeeId: number, file: File): Observable<any> {
    const formData = new FormData();
    formData.append("Image", file); // لازم يكون نفس اسم الفيلد في DTO

    return this.http.post(`${this.apiUrl}/${employeeId}/update-image`, formData);
  }

  getEmployeeId(): number {
    const employeeId = localStorage.getItem('userId');
    if (!employeeId) return 0;
    return parseInt(employeeId);
  }
}
