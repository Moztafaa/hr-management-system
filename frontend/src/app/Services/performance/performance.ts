import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../models/api-response';
import {
  CreateCycleDto,
  ReviewCycleDto,
  CreateReviewDto,
  PerformanceReviewDto,
  CreateGoalDto,
  GoalDto,
  UpdateGoalProgressDto,
  CreateKpiDto,
  KPIDto,
  CreateKpiResultDto,
  KPIResultDto,
  CreateFeedbackDto,
  FeedbackDto,
  CreateCompetencyDto,
  CompetencyDto,
  RateCompetencyDto,
  EmployeeCompetencyRatingDto,
  PerformanceReportDto,
} from '../../models/performance-dtos';

@Injectable({
  providedIn: 'root',
})
export class PerformanceService {
  private http = new HttpClient((null as unknown) as any);
  private apiUrl = 'https://localhost:7005/api/Performance';

  constructor(http: HttpClient) {
    this.http = http;
  }

  // Cycles
  getReviewCycles(): Observable<ApiResponse<ReviewCycleDto[]>> {
    return this.http.get<ApiResponse<ReviewCycleDto[]>>(`${this.apiUrl}/cycles`);
  }

  getActiveCycles(at?: string): Observable<ApiResponse<ReviewCycleDto[]>> {
    const q = at ? `?at=${encodeURIComponent(at)}` : '';
    return this.http.get<ApiResponse<ReviewCycleDto[]>>(`${this.apiUrl}/cycles/active${q}`);
  }

  createReviewCycle(dto: any): Observable<ApiResponse<ReviewCycleDto>> {
    const payload: CreateCycleDto = {
      name: dto.name,
      frequency: dto.frequency,
      startDate: dto.startDate,
      endDate: dto.endDate,
      ratingScale: (dto.ratingScale || dto.ratingScaleType) as string,
    };
    return this.http.post<ApiResponse<ReviewCycleDto>>(`${this.apiUrl}/cycles`, payload);
  }

  // Reviews
  getReviews(): Observable<ApiResponse<PerformanceReviewDto[]>> {
    return this.http.get<ApiResponse<PerformanceReviewDto[]>>(`${this.apiUrl}/reviews`);
  }

  getReviewsByEmployee(employeeId: number): Observable<ApiResponse<PerformanceReviewDto[]>> {
    return this.http.get<ApiResponse<PerformanceReviewDto[]>>(`${this.apiUrl}/reviews/employee/${employeeId}`);
  }

  createReview(dto: any): Observable<ApiResponse<PerformanceReviewDto>> {
    const payload: CreateReviewDto = {
      employeeId: dto.employeeId,
      reviewCycleId: dto.reviewCycleId || dto.cycleId,
    };
    return this.http.post<ApiResponse<PerformanceReviewDto>>(`${this.apiUrl}/reviews`, payload);
  }

  closeReview(reviewId: number, finalRating?: number): Observable<ApiResponse<PerformanceReviewDto>> {
    const body = finalRating !== undefined ? finalRating : null;
    return this.http.post<ApiResponse<PerformanceReviewDto>>(`${this.apiUrl}/reviews/${reviewId}/close`, body);
  }

  // Goals
  createGoal(dto: CreateGoalDto): Observable<ApiResponse<GoalDto>> {
    return this.http.post<ApiResponse<GoalDto>>(`${this.apiUrl}/goals`, dto);
  }

  updateGoalProgress(dto: UpdateGoalProgressDto): Observable<ApiResponse<boolean>> {
    return this.http.put<ApiResponse<boolean>>(`${this.apiUrl}/goals/progress`, dto);
  }

  // KPIs
  createKpi(dto: CreateKpiDto): Observable<ApiResponse<KPIDto>> {
    return this.http.post<ApiResponse<KPIDto>>(`${this.apiUrl}/kpis`, dto);
  }

  addKpiResult(dto: CreateKpiResultDto): Observable<ApiResponse<KPIResultDto>> {
    return this.http.post<ApiResponse<KPIResultDto>>(`${this.apiUrl}/kpis/results`, dto);
  }

  // Feedback
  addFeedback(dto: CreateFeedbackDto): Observable<ApiResponse<FeedbackDto>> {
    return this.http.post<ApiResponse<FeedbackDto>>(`${this.apiUrl}/feedbacks`, dto);
  }

  getFeedbackByType(reviewId: number, type: string): Observable<ApiResponse<FeedbackDto[]>> {
    return this.http.get<ApiResponse<FeedbackDto[]>>(`${this.apiUrl}/reviews/${reviewId}/feedback-by-type?type=${encodeURIComponent(
      type
    )}`);
  }

  // Competencies
  createCompetency(dto: CreateCompetencyDto): Observable<ApiResponse<CompetencyDto>> {
    return this.http.post<ApiResponse<CompetencyDto>>(`${this.apiUrl}/competencies`, dto);
  }

  rateCompetency(dto: RateCompetencyDto): Observable<ApiResponse<EmployeeCompetencyRatingDto>> {
    return this.http.post<ApiResponse<EmployeeCompetencyRatingDto>>(`${this.apiUrl}/competencies/rate`, dto);
  }

  // Reports
  getReviewReport(reviewId: number): Observable<ApiResponse<PerformanceReportDto>> {
    return this.http.get<ApiResponse<PerformanceReportDto>>(`${this.apiUrl}/reviews/${reviewId}/report`);
  }
}
