import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../models/api-response';
import { LeaveType as LeaveTypeModel } from '../../models/leaveType';

@Injectable({
  providedIn: 'root',
})
export class LeaveType {
  private apiUrl = 'https://localhost:7005/api/LeaveType'; // Backend API URL
  //private apiUrl = 'http://localhost:5093/api/Employee'; // Backend API URL

  constructor(private http: HttpClient) { }
  getLeaveTypes(): Observable<ApiResponse<LeaveTypeModel[]>> {
    return this.http.get<ApiResponse<LeaveTypeModel[]>>(this.apiUrl);
  }
  getLeaveType(id: number): Observable<ApiResponse<LeaveTypeModel>> {
    return this.http.get<ApiResponse<LeaveTypeModel>>(`${this.apiUrl}/${id}`);
  }
}
