import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../models/api-response';
import { LeaveBalance as LeaveBalanceModel } from '../../models/leaveBalance';

@Injectable({
  providedIn: 'root',
})
export class LeaveBalance {
  private http = inject(HttpClient);
  private apiUrl = 'https://localhost:7005/api/LeaveBalance';

  getLeaveBalances(id: number): Observable<ApiResponse<LeaveBalanceModel[]>> {
    return this.http.get<ApiResponse<LeaveBalanceModel[]>>(`${this.apiUrl}/employee-currentyear-leavebalance/${id}`);
  }

  getAllEmployeeBalances(id: number): Observable<ApiResponse<LeaveBalanceModel[]>> {
    return this.http.get<ApiResponse<LeaveBalanceModel[]>>(`${this.apiUrl}/employee-leavebalance/${id}`);
  }

  allocate(employeeId: number): Observable<ApiResponse<boolean>> {
    return this.http.post<ApiResponse<boolean>>(`${this.apiUrl}/allocate/${employeeId}`, {});
  }

  deduct(request: any): Observable<ApiResponse<boolean>> {
    return this.http.post<ApiResponse<boolean>>(`${this.apiUrl}/deduct`, request);
  }

  resetAnnual(): Observable<ApiResponse<boolean>> {
    return this.http.post<ApiResponse<boolean>>(`${this.apiUrl}/reset-annual`, {});
  }
}
