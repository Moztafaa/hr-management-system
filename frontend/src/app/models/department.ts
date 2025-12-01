export interface Department {
  id: number;
  name: string;
  description?: string;
  managerId?: number;
  employeeCount?: number;
  createdAt?: string;
  updatedAt?: string;
}

export interface CreateDepartmentDto {
  name: string;
  description?: string;
  managerId?: number;
}

export interface UpdateDepartmentDto extends CreateDepartmentDto {}
