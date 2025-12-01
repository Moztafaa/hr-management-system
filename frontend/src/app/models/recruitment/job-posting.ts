export interface JobPosting {
  id: number;
  title: string;
  description?: string;
  requirements?: string;
  postedDate: string;
  closingDate?: string;
  isActive?: boolean;
  departmentId: number;
  designationId: number;
  departmentName?: string;
  designationName?: string;
  createdAt?: string;
  updatedAt?: string;
}

export interface CreateJobPostingDto {
  title: string;
  description?: string;
  requirements?: string;
  postedDate: string;
  closingDate?: string;
  isActive?: boolean;
  departmentId: number;
  designationId: number;
}

export interface UpdateJobPostingDto extends CreateJobPostingDto {}
