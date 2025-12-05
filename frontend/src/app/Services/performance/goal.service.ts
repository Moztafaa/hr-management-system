import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../models/api-response';
import { Goal, CreateGoalDto, UpdateGoalProgressDto } from '../../models/performance';

@Injectable({
  providedIn: 'root',
})
export class GoalService {
  private http = inject(HttpClient);
  private apiUrl = 'http://localhost:5093/api/Performance';

  create(goal: CreateGoalDto): Observable<ApiResponse<Goal>> {
    return this.http.post<ApiResponse<Goal>>(`${this.apiUrl}/goals`, goal);
  }

  updateProgress(dto: UpdateGoalProgressDto): Observable<ApiResponse<boolean>> {
    return this.http.put<ApiResponse<boolean>>(`${this.apiUrl}/goals/progress`, dto);
  }
}
