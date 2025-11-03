export interface RegisterEmployee {
  email: string;                 // required
  password: string;              // required
  confirmPassword: string;       // required
  phoneNumber?: string | null;   // optional
  firstName?: string | null;
  lastName?: string | null;
  dateOfBirth?: string | null;   // ISO string أو null
  gender?: string | null;
  hireDate?: string | null;      // ISO string أو null
  efF_Start?: string | null;     // ISO string أو null
  efF_End?: string | null;       // ISO string أو null
  contactNumber?: string | null; // optional
  address?: string | null;
  basicSalary?: number | null;   // optional
  roles?: string[];              // optional
}
