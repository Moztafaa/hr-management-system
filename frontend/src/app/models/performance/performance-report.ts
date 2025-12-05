import { Goal } from './goal';
import { KPIResult } from './kpi';
import { EmployeeCompetencyRating } from './competency';
import { Feedback } from './feedback';

export interface PerformanceReport {
  reviewId: number;
  finalRating?: number;
  goals: Goal[];
  kpiResults: KPIResult[];
  competencyRatings: EmployeeCompetencyRating[];
  feedbacks: Feedback[];
}
