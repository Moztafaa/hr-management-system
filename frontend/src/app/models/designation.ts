import { EmployeeSummary } from './department';

export interface Designation {
  id: number;
  title: string;
  description?: string;
  createdAt?: string;
  updatedAt?: string;
}

export interface DesignationWithEmployees extends Designation {
  employees: EmployeeSummary[];
}

export interface CreateDesignationDto {
  title: string;
  description?: string;
}

export interface UpdateDesignationDto extends CreateDesignationDto {}
