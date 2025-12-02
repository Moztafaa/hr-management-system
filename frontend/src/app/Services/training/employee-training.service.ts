import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../models/api-response';
import { EmployeeTraining, EmployeeEnrollDto } from '../../models/training';

@Injectable({
  providedIn: 'root',
})
export class EmployeeTrainingService {
  private http = inject(HttpClient);
  private apiUrl = 'http://localhost:5093/api/EmployeeTraining';

  enroll(enrollment: EmployeeEnrollDto): Observable<ApiResponse<EmployeeTraining>> {
    return this.http.post<ApiResponse<EmployeeTraining>>(`${this.apiUrl}/enroll`, enrollment);
  }

  complete(employeeId: number, courseId: number): Observable<ApiResponse<boolean>> {
    return this.http.post<ApiResponse<boolean>>(
      `${this.apiUrl}/${employeeId}/${courseId}/complete`,
      {}
    );
  }

  cancel(employeeId: number, courseId: number): Observable<ApiResponse<boolean>> {
    return this.http.post<ApiResponse<boolean>>(
      `${this.apiUrl}/${employeeId}/${courseId}/cancel`,
      {}
    );
  }

  getByEmployee(employeeId: number): Observable<ApiResponse<EmployeeTraining[]>> {
    return this.http.get<ApiResponse<EmployeeTraining[]>>(
      `${this.apiUrl}/by-employee/${employeeId}`
    );
  }

  getByCourse(courseId: number): Observable<ApiResponse<EmployeeTraining[]>> {
    return this.http.get<ApiResponse<EmployeeTraining[]>>(`${this.apiUrl}/by-course/${courseId}`);
  }
}
