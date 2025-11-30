export interface ESSProfile {
  id: number;
  firstName: string;
  lastName: string;
  fullName: string;
  dateOfBirth: string;
  gender?: string;
  hireDate: string;
  email: string;
  contactNumber?: string;
  address?: string;
  departmentName?: string;
  designationName?: string;
  deptId: number;
  designationId: number;
}

export interface UpdateESSProfile {
  email: string;
  contactNumber?: string;
  address?: string;
}
