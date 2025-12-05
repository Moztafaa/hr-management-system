import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiResponse } from '../models/api-response';
import {
  ESSProfile,
  UpdateESSProfile,
  ESSDashboard,
  LeaveBalance,
  LeaveRequest,
  CreateLeaveRequest,
  Payslip,
  Resignation,
  CreateResignation,
} from '../models/ess';
import { EmployeeTraining, TrainingRequest, TrainingCourse } from '../models/training';

export interface ESSTrainingRequestCreate {
  trainingCourseId: number;
  employeeNote?: string;
}

@Injectable({
  providedIn: 'root',
})
export class ESSService {
  private http = inject(HttpClient);
  private apiUrl = 'https://localhost:7005/api/ESS';

  // Profile endpoints
  getProfile(): Observable<ApiResponse<ESSProfile>> {
    return this.http.get<ApiResponse<ESSProfile>>(`${this.apiUrl}/profile`);
  }

  updateProfile(data: UpdateESSProfile): Observable<ApiResponse<boolean>> {
    return this.http.put<ApiResponse<boolean>>(`${this.apiUrl}/profile`, data);
  }

  // Dashboard endpoint
  getDashboard(): Observable<ApiResponse<ESSDashboard>> {
    return this.http.get<ApiResponse<ESSDashboard>>(`${this.apiUrl}/dashboard`);
  }

  // Leave request endpoints
  getLeaveRequests(): Observable<ApiResponse<LeaveRequest[]>> {
    return this.http.get<ApiResponse<LeaveRequest[]>>(`${this.apiUrl}/leave-requests`);
  }

  submitLeaveRequest(data: CreateLeaveRequest): Observable<ApiResponse<number>> {
    return this.http.post<ApiResponse<number>>(`${this.apiUrl}/leave-requests`, data);
  }

  // Leave balance endpoint
  getLeaveBalance(year?: number): Observable<ApiResponse<LeaveBalance[]>> {
    let params = new HttpParams();
    if (year) {
      params = params.set('year', year.toString());
    }
    return this.http.get<ApiResponse<LeaveBalance[]>>(`${this.apiUrl}/leave-balance`, { params });
  }

  // Payslip endpoints
  getPayslips(): Observable<ApiResponse<Payslip[]>> {
    return this.http.get<ApiResponse<Payslip[]>>(`${this.apiUrl}/payslips`);
  }

  getPayslip(payslipId: number): Observable<ApiResponse<Payslip>> {
    return this.http.get<ApiResponse<Payslip>>(`${this.apiUrl}/payslips/${payslipId}`);
  }

  // Get payslip PDF download URL
  getPayslipPdfUrl(payslipId: number): string {
    return `https://localhost:7005/api/payslips/${payslipId}/export/pdf`;
  }

  // Training endpoints
  getMyCourses(): Observable<ApiResponse<EmployeeTraining[]>> {
    return this.http.get<ApiResponse<EmployeeTraining[]>>(`${this.apiUrl}/training/my-courses`);
  }

  getMyTrainingRequests(): Observable<ApiResponse<TrainingRequest[]>> {
    return this.http.get<ApiResponse<TrainingRequest[]>>(`${this.apiUrl}/training/my-requests`);
  }

  getAvailableCourses(): Observable<ApiResponse<TrainingCourse[]>> {
    return this.http.get<ApiResponse<TrainingCourse[]>>(
      `${this.apiUrl}/training/available-courses`
    );
  }

  submitTrainingRequest(data: ESSTrainingRequestCreate): Observable<ApiResponse<TrainingRequest>> {
    return this.http.post<ApiResponse<TrainingRequest>>(`${this.apiUrl}/training/request`, data);
  }

  cancelTrainingRequest(requestId: number): Observable<ApiResponse<boolean>> {
    return this.http.delete<ApiResponse<boolean>>(`${this.apiUrl}/training/request/${requestId}`);
  }

  // Resignation endpoints
  getMyResignations(): Observable<ApiResponse<Resignation[]>> {
    return this.http.get<ApiResponse<Resignation[]>>(`${this.apiUrl}/resignations`);
  }

  getActiveResignation(): Observable<ApiResponse<Resignation | null>> {
    return this.http.get<ApiResponse<Resignation | null>>(`${this.apiUrl}/resignations/active`);
  }

  submitResignation(data: CreateResignation): Observable<ApiResponse<Resignation>> {
    return this.http.post<ApiResponse<Resignation>>(`${this.apiUrl}/resignations`, data);
  }

  withdrawResignation(resignationId: number): Observable<ApiResponse<boolean>> {
    return this.http.post<ApiResponse<boolean>>(
      `${this.apiUrl}/resignations/${resignationId}/withdraw`,
      {}
    );
  }
}
