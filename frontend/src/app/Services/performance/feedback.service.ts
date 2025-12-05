import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../models/api-response';
import { Feedback, CreateFeedbackDto, FeedbackType } from '../../models/performance';

@Injectable({
  providedIn: 'root',
})
export class FeedbackService {
  private http = inject(HttpClient);
  private apiUrl = 'http://localhost:5093/api/Performance';

  create(feedback: CreateFeedbackDto): Observable<ApiResponse<Feedback>> {
    return this.http.post<ApiResponse<Feedback>>(`${this.apiUrl}/feedbacks`, feedback);
  }

  getByReviewAndType(reviewId: number, type: FeedbackType): Observable<ApiResponse<Feedback[]>> {
    return this.http.get<ApiResponse<Feedback[]>>(
      `${this.apiUrl}/reviews/${reviewId}/feedback-by-type?type=${type}`
    );
  }
}
