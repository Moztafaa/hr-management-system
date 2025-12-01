export interface RegisterEmployee {
  email: string; // required
  deptId?: number;
  designationId?: number;
  password: string; // required
  confirmPassword: string; // required
  phoneNumber?: string | null; // optional
  firstName?: string | null;
  lastName?: string | null;
  dateOfBirth?: string | null; // ISO string or null
  gender?: string | null;
  hireDate?: string | null; // ISO string or null
  efF_Start?: string | null; // ISO string or null
  efF_End?: string | null; // ISO string or null
  contactNumber?: string | null; // optional
  address?: string | null;
  basicSalary?: number | null; // optional
  roles?: string[]; // optional
}
