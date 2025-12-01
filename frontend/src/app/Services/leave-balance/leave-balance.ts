import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../models/api-response';
import { LeaveBalance as LeaveBalanceModel } from '../../models/leaveBalance';

@Injectable({
  providedIn: 'root',
})
export class LeaveBalance {
  private apiUrl = 'https://localhost:7005/api/LeaveBalance/employee-currentyear-leavebalance'; // Backend API URL
  //private apiUrl = 'http://localhost:5093/api/Employee'; // Backend API URL

  constructor(private http: HttpClient) { }
  getLeaveBalances(id: number): Observable<ApiResponse<LeaveBalanceModel[]>> {
    return this.http.get<ApiResponse<LeaveBalanceModel[]>>(`${this.apiUrl}/${id}`);
  }
}
