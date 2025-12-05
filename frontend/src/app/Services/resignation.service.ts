import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiResponse } from '../models/api-response';
import {
  Resignation,
  ResignationApproval,
  ResignationApprovalAction,
} from '../models/ess/resignation';

@Injectable({ providedIn: 'root' })
export class ResignationService {
  private http = inject(HttpClient);
  private apiUrl = 'http://localhost:5093/api/Resignation';
  private approvalApiUrl = 'http://localhost:5093/api/ResignationApproval';

  // Admin/HR endpoints
  getAllResignations(): Observable<ApiResponse<Resignation[]>> {
    return this.http.get<ApiResponse<Resignation[]>>(this.apiUrl);
  }

  getResignationById(id: number): Observable<ApiResponse<Resignation>> {
    return this.http.get<ApiResponse<Resignation>>(`${this.apiUrl}/${id}`);
  }

  getResignationsByEmployee(employeeId: number): Observable<ApiResponse<Resignation[]>> {
    return this.http.get<ApiResponse<Resignation[]>>(`${this.apiUrl}/employee/${employeeId}`);
  }

  getPendingForApprover(approverId: number): Observable<ApiResponse<Resignation[]>> {
    return this.http.get<ApiResponse<Resignation[]>>(
      `${this.apiUrl}/approver/${approverId}/pending`
    );
  }

  deleteResignation(id: number): Observable<ApiResponse<boolean>> {
    return this.http.delete<ApiResponse<boolean>>(`${this.apiUrl}/${id}`);
  }

  // Approval endpoints
  approveResignation(action: ResignationApprovalAction): Observable<ApiResponse<boolean>> {
    return this.http.post<ApiResponse<boolean>>(`${this.approvalApiUrl}/approve`, action);
  }

  rejectResignation(
    action: ResignationApprovalAction,
    rejectionReason: string
  ): Observable<ApiResponse<boolean>> {
    return this.http.post<ApiResponse<boolean>>(
      `${this.approvalApiUrl}/reject?rejectionReason=${encodeURIComponent(rejectionReason)}`,
      action
    );
  }

  getApprovalsByResignation(resignationId: number): Observable<ApiResponse<ResignationApproval[]>> {
    return this.http.get<ApiResponse<ResignationApproval[]>>(
      `${this.approvalApiUrl}/resignation/${resignationId}`
    );
  }

  getApprovalsByApprover(approverId: number): Observable<ApiResponse<ResignationApproval[]>> {
    return this.http.get<ApiResponse<ResignationApproval[]>>(
      `${this.approvalApiUrl}/approver/${approverId}`
    );
  }
}
