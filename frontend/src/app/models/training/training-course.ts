export interface TrainingCourse {
  id: number;
  title: string;
  description?: string;
  durationHours?: number;
}

export interface CreateTrainingCourseDto {
  title: string;
  description?: string;
  durationHours?: number;
}

export interface UpdateTrainingCourseDto {
  title?: string;
  description?: string;
  durationHours?: number;
}
