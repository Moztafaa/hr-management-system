import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../models/api-response';

export interface LeaveApprovalAction {
  leaveRequestId: number;
  approverId: number;
  level: number; // 1 = Manager, 2 = HR
}

@Injectable({
  providedIn: 'root',
})
export class LeaveApproval {
  private apiUrl = 'https://localhost:7005/api/LeaveApproval';

  constructor(private http: HttpClient) { }

  approve(dto: LeaveApprovalAction): Observable<ApiResponse<boolean>> {
    return this.http.post<ApiResponse<boolean>>(`${this.apiUrl}/approve`, dto);
  }

  reject(dto: LeaveApprovalAction): Observable<ApiResponse<boolean>> {
    return this.http.post<ApiResponse<boolean>>(`${this.apiUrl}/reject`, dto);
  }

  // get approvals assigned to an approver
  getApprovalsByApprover(approverId: number): Observable<ApiResponse<any[]>> {
    return this.http.get<ApiResponse<any[]>>(`${this.apiUrl}/approver/${approverId}`);
  }
}
