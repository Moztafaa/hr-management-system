import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../models/api-response';
import {
  TrainingRequest,
  CreateTrainingRequestDto,
  TrainingRequestStatus,
} from '../../models/training';

@Injectable({
  providedIn: 'root',
})
export class TrainingRequestService {
  private http = inject(HttpClient);
  private apiUrl = 'http://localhost:5093/api/TrainingRequest';

  getAll(): Observable<ApiResponse<TrainingRequest[]>> {
    return this.http.get<ApiResponse<TrainingRequest[]>>(this.apiUrl);
  }

  getById(id: number): Observable<ApiResponse<TrainingRequest>> {
    return this.http.get<ApiResponse<TrainingRequest>>(`${this.apiUrl}/${id}`);
  }

  getByEmployee(employeeId: number): Observable<ApiResponse<TrainingRequest[]>> {
    return this.http.get<ApiResponse<TrainingRequest[]>>(
      `${this.apiUrl}/by-employee/${employeeId}`
    );
  }

  getByStatus(status: TrainingRequestStatus): Observable<ApiResponse<TrainingRequest[]>> {
    return this.http.get<ApiResponse<TrainingRequest[]>>(`${this.apiUrl}/by-status/${status}`);
  }

  create(request: CreateTrainingRequestDto): Observable<ApiResponse<TrainingRequest>> {
    return this.http.post<ApiResponse<TrainingRequest>>(this.apiUrl, request);
  }

  review(
    id: number,
    managerId: number,
    approved: boolean,
    managerNote?: string
  ): Observable<ApiResponse<TrainingRequest>> {
    return this.http.post<ApiResponse<TrainingRequest>>(
      `${this.apiUrl}/${id}/review?managerId=${managerId}&approve=${approved}`,
      managerNote ?? null,
      { headers: { 'Content-Type': 'application/json' } }
    );
  }

  delete(id: number): Observable<ApiResponse<boolean>> {
    return this.http.delete<ApiResponse<boolean>>(`${this.apiUrl}/${id}`);
  }
}
