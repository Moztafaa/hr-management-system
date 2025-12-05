import { FeedbackType } from './enums';

export interface Feedback {
  id: number;
  performanceReviewId: number;
  fromEmployeeId: number;
  type: FeedbackType;
  comments: string;
  submittedAt: string;
  fromEmployeeName?: string;
}

export interface CreateFeedbackDto {
  performanceReviewId: number;
  fromEmployeeId: number;
  type: FeedbackType;
  comments: string;
}
