export interface Department {
  id: number;
  name: string;
  description?: string;
  managerId?: number;
  employeeCount?: number;
  createdAt?: string;
  updatedAt?: string;
}

export interface EmployeeSummary {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
  position?: string;
}

export interface DepartmentWithEmployees extends Department {
  employees: EmployeeSummary[];
}

export interface CreateDepartmentDto {
  name: string;
  description?: string;
  managerId?: number;
}

export interface UpdateDepartmentDto extends CreateDepartmentDto {}
