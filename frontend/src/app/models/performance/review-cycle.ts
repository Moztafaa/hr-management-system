import { CycleFrequency, RatingScaleType } from './enums';

export interface ReviewCycle {
  id: number;
  name: string;
  frequency: CycleFrequency;
  startDate: string;
  endDate: string;
  ratingScale: RatingScaleType;
}

export interface CreateCycleDto {
  name: string;
  frequency: CycleFrequency;
  startDate: string;
  endDate: string;
  ratingScale: RatingScaleType;
}
