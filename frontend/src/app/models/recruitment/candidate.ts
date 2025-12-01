export interface Candidate {
  id: number;
  firstName: string;
  lastName: string;
  fullName: string;
  email: string;
  phone?: string;
  resumeUrl?: string;
  linkedInUrl?: string;
  portfolioUrl?: string;
  address?: string;
  city?: string;
  country?: string;
  postalCode?: string;
  dateOfBirth?: string;
  gender?: string;
  yearsOfExperience?: number;
  currentCompany?: string;
  currentJobTitle?: string;
  currentSalary?: number;
  expectedSalary?: number;
  skills?: string;
  education?: string;
  certifications?: string;
  noticePeriodDays?: number;
  availableFrom?: string;
  preferredWorkLocation?: string;
  willingToRelocate: boolean;
  notes?: string;
  convertedToEmployeeId?: number;
  createdAt: string;
  updatedAt?: string;
}

export interface CreateCandidateDto {
  firstName: string;
  lastName: string;
  email: string;
  phone?: string;
  resumeUrl?: string;
  linkedInUrl?: string;
  portfolioUrl?: string;
  address?: string;
  city?: string;
  country?: string;
  postalCode?: string;
  dateOfBirth?: string;
  gender?: string;
  yearsOfExperience?: number;
  currentCompany?: string;
  currentJobTitle?: string;
  currentSalary?: number;
  expectedSalary?: number;
  skills?: string;
  education?: string;
  certifications?: string;
  noticePeriodDays?: number;
  availableFrom?: string;
  preferredWorkLocation?: string;
  willingToRelocate?: boolean;
  notes?: string;
}

export interface UpdateCandidateDto extends CreateCandidateDto {}
