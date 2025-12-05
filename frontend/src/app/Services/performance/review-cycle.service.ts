import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../models/api-response';
import { ReviewCycle, CreateCycleDto } from '../../models/performance';

@Injectable({
  providedIn: 'root',
})
export class ReviewCycleService {
  private http = inject(HttpClient);
  private apiUrl = 'http://localhost:5093/api/Performance';

  getAll(): Observable<ApiResponse<ReviewCycle[]>> {
    return this.http.get<ApiResponse<ReviewCycle[]>>(`${this.apiUrl}/cycles`);
  }

  getActive(at?: string): Observable<ApiResponse<ReviewCycle[]>> {
    const params = at ? `?at=${at}` : '';
    return this.http.get<ApiResponse<ReviewCycle[]>>(`${this.apiUrl}/cycles/active${params}`);
  }

  create(cycle: CreateCycleDto): Observable<ApiResponse<ReviewCycle>> {
    return this.http.post<ApiResponse<ReviewCycle>>(`${this.apiUrl}/cycles`, cycle);
  }
}
