import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../models/api-response';
import { LeaveRequest as LeaveRequestModel, CreateLeaveRequest, UpdateLeaveRequest } from '../../models/leaveRequest';

@Injectable({
  providedIn: 'root',
})
export class LeaveRequest {
  private http = inject(HttpClient);
  private apiUrl = 'https://localhost:7005/api/LeaveRequest';

  getEmployeeLeaveRequests(id: number): Observable<ApiResponse<LeaveRequestModel[]>> {
    return this.http.get<ApiResponse<LeaveRequestModel[]>>(`${this.apiUrl}/employee/${id}`);
  }

  getLeaveRequests(): Observable<ApiResponse<LeaveRequestModel[]>> {
    return this.http.get<ApiResponse<LeaveRequestModel[]>>(`${this.apiUrl}`);
  }

  getById(id: number): Observable<ApiResponse<LeaveRequestModel>> {
    return this.http.get<ApiResponse<LeaveRequestModel>>(`${this.apiUrl}/${id}`);
  }

  create(request: CreateLeaveRequest): Observable<ApiResponse<LeaveRequestModel>> {
    return this.http.post<ApiResponse<LeaveRequestModel>>(this.apiUrl, request);
  }

  update(id: number, request: UpdateLeaveRequest): Observable<ApiResponse<void>> {
    return this.http.put<ApiResponse<void>>(`${this.apiUrl}/${id}`, request);
  }

  delete(id: number): Observable<ApiResponse<void>> {
    return this.http.delete<ApiResponse<void>>(`${this.apiUrl}/${id}`);
  }

  getByManager(managerId: number): Observable<ApiResponse<LeaveRequestModel[]>> {
    return this.http.get<ApiResponse<LeaveRequestModel[]>>(`${this.apiUrl}/manager/${managerId}`);
  }
}
