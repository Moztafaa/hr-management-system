import { ReviewStatus } from './enums';

export interface PerformanceReview {
  id: number;
  employeeId: number;
  reviewCycleId: number;
  status: ReviewStatus;
  finalRating?: number;
  employeeName?: string;
  reviewCycleName?: string;
}

export interface CreateReviewDto {
  employeeId: number;
  reviewCycleId: number;
}
