import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiResponse } from '../models/api-response';
import { LeaveApproval, LeaveApprovalAction } from '../models/leaveApproval';

@Injectable({
    providedIn: 'root',
})
export class LeaveApprovalService {
    private http = inject(HttpClient);
    private apiUrl = 'https://localhost:7005/api/LeaveApproval';

    approve(action: LeaveApprovalAction): Observable<ApiResponse<boolean>> {
        return this.http.post<ApiResponse<boolean>>(`${this.apiUrl}/approve`, action);
    }

    reject(action: LeaveApprovalAction): Observable<ApiResponse<boolean>> {
        return this.http.post<ApiResponse<boolean>>(`${this.apiUrl}/reject`, action);
    }

    getByApprover(approverId: number): Observable<ApiResponse<LeaveApproval[]>> {
        return this.http.get<ApiResponse<LeaveApproval[]>>(`${this.apiUrl}/approver/${approverId}`);
    }

    getRequestApprovals(leaveRequestId: number): Observable<ApiResponse<LeaveApproval[]>> {
        return this.http.get<ApiResponse<LeaveApproval[]>>(`${this.apiUrl}/request/${leaveRequestId}`);
    }
}
