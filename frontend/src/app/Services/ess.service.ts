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
} from '../models/ess';

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
    return `http://localhost:5093/api/payslips/${payslipId}/export/pdf`;
  }
}
