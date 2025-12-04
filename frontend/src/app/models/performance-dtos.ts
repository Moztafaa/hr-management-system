// Review Cycles
export interface ReviewCycleDto {
  id: number;
  name: string;
  frequency: string;
  startDate: Date;
  endDate: Date;
  ratingScale: string;
}

export interface CreateCycleDto {
  name: string;
  frequency: string;
  startDate: string;
  endDate: string;
  ratingScale: string;
}

// Performance Reviews
export interface PerformanceReviewDto {
  id: number;
  employeeId: number;
  reviewCycleId: number;
  status: string;
  finalRating?: number;
}

export interface CreateReviewDto {
  employeeId: number;
  reviewCycleId: number;
}

// Goals
export interface GoalDto {
  id: number;
  performanceReviewId: number;
  title: string;
  description?: string;
  status: string;
  progressPercent: number;
  dueDate: Date;
}

export interface CreateGoalDto {
  performanceReviewId: number;
  title: string;
  description?: string;
  dueDate: string;
}

export interface UpdateGoalProgressDto {
  goalId: number;
  progressPercent: number;
  status: string;
}

// KPIs
export interface KPIDto {
  id: number;
  code: string;
  name: string;
  unit?: string;
  target: number;
  description?: string;
}

export interface CreateKpiDto {
  code?: string;
  name: string;
  unit?: string;
  target: number;
  description?: string;
}

export interface KPIResultDto {
  id: number;
  kpiId: number;
  actual: number;
  period: string;
}

export interface CreateKpiResultDto {
  kpiId: number;
  actual: number;
  period: string;
}

// Feedback
export interface FeedbackDto {
  id: number;
  performanceReviewId: number;
  fromEmployeeId: number;
  type: string;
  comments: string;
  submittedAt: Date;
}

export interface CreateFeedbackDto {
  performanceReviewId: number;
  fromEmployeeId: number;
  type: string;
  comments: string;
}

// Competencies
export interface CompetencyDto {
  id: number;
  name: string;
  description?: string;
}

export interface CreateCompetencyDto {
  name: string;
  description?: string;
}

export interface RateCompetencyDto {
  performanceReviewId: number;
  competencyId: number;
  rating: number;
  notes?: string;
}

export interface EmployeeCompetencyRatingDto {
  id: number;
  performanceReviewId: number;
  competencyId: number;
  rating: number;
  notes?: string;
}

// Performance Report
export interface PerformanceReportDto {
  reviewId: number;
  employeeId: number;
  overallRating: number;
  goalsAchievementRate: number;
  competencyAverageRating: number;
}
