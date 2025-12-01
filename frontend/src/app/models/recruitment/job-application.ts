import { Candidate, CreateCandidateDto } from './candidate';
import { JobPosting } from './job-posting';

export interface JobApplication {
  id: number;
  candidateId: number;
  jobPostingId: number;
  applicationDate: string;
  status: string;
  source: string;
  coverLetter?: string;
  notes?: string;
  reviewedBy?: number;
  reviewedDate?: string;
  interviewDate?: string;
  interviewFeedback?: string;
  interviewRating?: number;
  expectedSalary?: number;
  offeredSalary?: number;
  rejectionReason?: string;
  assignedRecruiterId?: number;
  currentStage: string;
  createdAt: string;
  updatedAt?: string;
}

export interface JobApplicationDetail extends JobApplication {
  candidate: Candidate;
  jobPosting: JobPosting;
}

export interface CreateJobApplicationDto {
  candidateId?: number;
  candidateInfo?: CreateCandidateDto;
  jobPostingId: number;
  source: string;
  coverLetter?: string;
  expectedSalary?: number;
  notes?: string;
  assignedRecruiterId?: number;
}

export interface UpdateJobApplicationStatusDto {
  jobApplicationId: number;
  status: string;
  currentStage?: string;
  notes?: string;
  reviewedBy?: number;
  reviewedDate?: string;
  interviewDate?: string;
  interviewFeedback?: string;
  interviewRating?: number;
  rejectionReason?: string;
  offeredSalary?: number;
}

export enum ApplicationStatus {
  Applied = 'Applied',
  UnderReview = 'UnderReview',
  Shortlisted = 'Shortlisted',
  Interviewed = 'Interviewed',
  Offered = 'Offered',
  Accepted = 'Accepted',
  Rejected = 'Rejected',
  Withdrawn = 'Withdrawn',
}

export const ApplicationStatusLabels: Record<string, string> = {
  [ApplicationStatus.Applied]: 'Applied',
  [ApplicationStatus.UnderReview]: 'Under Review',
  [ApplicationStatus.Shortlisted]: 'Shortlisted',
  [ApplicationStatus.Interviewed]: 'Interviewed',
  [ApplicationStatus.Offered]: 'Offered',
  [ApplicationStatus.Accepted]: 'Accepted',
  [ApplicationStatus.Rejected]: 'Rejected',
  [ApplicationStatus.Withdrawn]: 'Withdrawn',
};

export const ApplicationStatusColors: Record<string, string> = {
  [ApplicationStatus.Applied]: 'bg-secondary',
  [ApplicationStatus.UnderReview]: 'bg-info',
  [ApplicationStatus.Shortlisted]: 'bg-primary',
  [ApplicationStatus.Interviewed]: 'bg-warning text-dark',
  [ApplicationStatus.Offered]: 'bg-success',
  [ApplicationStatus.Accepted]: 'bg-success',
  [ApplicationStatus.Rejected]: 'bg-danger',
  [ApplicationStatus.Withdrawn]: 'bg-dark',
};

export const ApplicationSources = [
  'LinkedIn',
  'Indeed',
  'Company Website',
  'Referral',
  'Job Fair',
  'Recruitment Agency',
  'Other',
];
