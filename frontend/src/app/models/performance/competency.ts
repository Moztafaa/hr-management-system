export interface Competency {
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

export interface EmployeeCompetencyRating {
  id: number;
  performanceReviewId: number;
  competencyId: number;
  rating: number;
  notes?: string;
  competencyName?: string;
}
