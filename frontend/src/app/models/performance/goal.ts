import { GoalStatus } from './enums';

export interface Goal {
  id: number;
  performanceReviewId: number;
  title: string;
  description?: string;
  status: GoalStatus;
  progressPercent: number;
  dueDate?: string;
}

export interface CreateGoalDto {
  performanceReviewId: number;
  title: string;
  description?: string;
  dueDate?: string;
}

export interface UpdateGoalProgressDto {
  goalId: number;
  progressPercent: number;
  status?: string;
}
