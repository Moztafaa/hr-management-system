export type TrainingStatus = 'Enrolled' | 'Completed' | 'Cancelled';

export interface EmployeeTraining {
  employeeId: number;
  trainingCourseId: number;
  status: TrainingStatus;
  enrollmentDate: string;
  completionDate?: string;
  rewardGiven: boolean;
  employeeName?: string;
  trainingCourseTitle?: string;
}

export interface EmployeeEnrollDto {
  employeeId: number;
  trainingCourseId: number;
}
