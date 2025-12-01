import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../models/api-response';
import { LeaveRequest as LeaveRequestModel } from '../../models/leaveRequest';

@Injectable({
  providedIn: 'root',
})
export class LeaveRequest {
  private apiUrl = 'https://localhost:7005/api/LeaveRequest'; // Backend API URL
  //private apiUrl = 'http://localhost:5093/api/Employee'; // Backend API URL

  constructor(private http: HttpClient) { }
  getEmployeeLeaveRequests(id: number): Observable<ApiResponse<LeaveRequestModel[]>> {
    return this.http.get<ApiResponse<LeaveRequestModel[]>>(`${this.apiUrl}/employee/${id}`);
  }
  getLeaveRequests(): Observable<ApiResponse<LeaveRequestModel[]>> {
    return this.http.get<ApiResponse<LeaveRequestModel[]>>(`${this.apiUrl}`);
  }
}
