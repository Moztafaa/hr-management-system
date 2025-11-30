import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiResponse } from '../models/api-response';
import { LeaveType } from '../models/ess/leave-type';

@Injectable({
  providedIn: 'root',
})
export class LeaveTypeService {
  private http = inject(HttpClient);
  private apiUrl = 'https://localhost:7005/api/LeaveType';

  getAll(): Observable<ApiResponse<LeaveType[]>> {
    return this.http.get<ApiResponse<LeaveType[]>>(this.apiUrl);
  }

  getById(id: number): Observable<ApiResponse<LeaveType>> {
    return this.http.get<ApiResponse<LeaveType>>(`${this.apiUrl}/${id}`);
  }
}
