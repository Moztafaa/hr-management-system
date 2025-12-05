export interface TrainingRequest {
  id: number;
  employeeId: number;
  employeeName: string;
  trainingCourseId: number;
  courseTitle: string;
  status: string | TrainingRequestStatus; // Allow both
  requestDate: string;
  reviewedByManagerId?: number;
  managerName?: string;
  reviewedAt?: string;
  managerNote?: string;
  employeeNote?: string;
}

export enum TrainingRequestStatus {
  Pending = 0,
  Approved = 1,
  Rejected = 2,
}

export interface CreateTrainingRequestDto {
  employeeId: number;
  trainingCourseId: number;
  employeeNote?: string;
}