export interface Employee {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
  contactNumber?: string;
  dateOfBirth?: string;
  gender?: string;
  hireDate?: string;
  deptId?: number;
  designationId?: number;
  basicSalary?: number;
  address?: string;
  // These might need to be fetched separately or joined in backend if needed for display
  departmentName?: string;
  designationName?: string;
  status?: string;
  avatar?: string;
}
