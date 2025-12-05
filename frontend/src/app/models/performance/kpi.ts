export interface KPI {
  id: number;
  code: string;
  name: string;
  unit?: string;
  target: number;
  description?: string;
}

export interface CreateKpiDto {
  code: string;
  name: string;
  unit?: string;
  target: number;
  description?: string;
}

export interface KPIResult {
  id: number;
  performanceReviewId: number;
  kpiId: number;
  actual: number;
  weightedScore?: number;
  kpiName?: string;
  kpiCode?: string;
  kpiTarget?: number;
}

export interface CreateKpiResultDto {
  performanceReviewId: number;
  kpiId: number;
  actual: number;
}
