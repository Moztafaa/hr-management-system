import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../models/api-response';
import { PerformanceReview, CreateReviewDto, PerformanceReport } from '../../models/performance';

@Injectable({
  providedIn: 'root',
})
export class PerformanceReviewService {
  private http = inject(HttpClient);
  private apiUrl = 'http://localhost:5093/api/Performance';

  getByEmployee(employeeId: number): Observable<ApiResponse<PerformanceReview[]>> {
    return this.http.get<ApiResponse<PerformanceReview[]>>(
      `${this.apiUrl}/reviews/employee/${employeeId}`
    );
  }

  create(review: CreateReviewDto): Observable<ApiResponse<PerformanceReview>> {
    return this.http.post<ApiResponse<PerformanceReview>>(`${this.apiUrl}/reviews`, review);
  }

  close(reviewId: number, finalRating?: number): Observable<ApiResponse<PerformanceReview>> {
    const params = finalRating !== undefined ? `?finalRating=${finalRating}` : '';
    return this.http.post<ApiResponse<PerformanceReview>>(
      `${this.apiUrl}/reviews/${reviewId}/close${params}`,
      {}
    );
  }

  getReport(reviewId: number): Observable<ApiResponse<PerformanceReport>> {
    return this.http.get<ApiResponse<PerformanceReport>>(
      `${this.apiUrl}/reviews/${reviewId}/report`
    );
  }
}
