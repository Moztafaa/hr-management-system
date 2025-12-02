export enum TrainingRequestStatus {
  Pending = 0,
  Approved = 1,
  Rejected = 2,
}

export interface TrainingRequest {
  id: number;
  employeeId: number;
  employeeName: string;
  trainingCourseId: number;
  courseTitle: string;
  status: TrainingRequestStatus;
  requestDate: string;
  reviewedByManagerId?: number;
  managerName?: string;
  reviewedAt?: string;
  managerNote?: string;
  employeeNote?: string;
}

export interface CreateTrainingRequestDto {
  employeeId: number;
  trainingCourseId: number;
  employeeNote?: string;
}
