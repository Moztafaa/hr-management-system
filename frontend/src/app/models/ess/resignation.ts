export interface Resignation {
  id: number;
  employeeId: number;
  employeeName?: string;
  departmentName?: string;
  designationName?: string;
  reason: string;
  submissionDate: string;
  lastWorkingDate: string;
  noticePeriodDays: number;
  isImmediateResignation: boolean;
  handoverNotes?: string;
  status: ResignationStatus | string; // Backend returns string
  reviewedById?: number;
  reviewerName?: string;
  reviewedAt?: string;
  rejectionReason?: string;
  createdAt: string;
  updatedAt?: string;
}

export interface CreateResignation {
  employeeId?: number;
  reason: string;
  lastWorkingDate: string;
  isImmediateResignation: boolean;
  handoverNotes?: string;
}

export interface WithdrawResignation {
  resignationId: number;
  employeeId: number;
  withdrawalReason?: string;
}

export interface ResignationApproval {
  id: number;
  resignationId: number;
  approverId: number;
  approverName?: string;
  level: LevelApproval | string; // Backend returns string
  status: ResignationStatus | string; // Backend returns string
  actionDate?: string;
  comments?: string;
}

export interface ResignationApprovalAction {
  resignationId: number;
  approverId: number;
  level: LevelApproval;
  comments?: string;
}

// Status is returned as string from backend due to JsonStringEnumConverter
export type ResignationStatus = 'Pending' | 'Approved' | 'Rejected' | 'Withdrawn';

export const ResignationStatusLabels: Record<string, string> = {
  Pending: 'Pending',
  Approved: 'Approved',
  Rejected: 'Rejected',
  Withdrawn: 'Withdrawn',
};

export const ResignationStatusColors: Record<string, string> = {
  Pending: 'warning',
  Approved: 'success',
  Rejected: 'danger',
  Withdrawn: 'secondary',
};

// Level is returned as string from backend due to JsonStringEnumConverter
export type LevelApproval = 'Manager' | 'HR';

export const LevelApprovalLabels: Record<LevelApproval, string> = {
  Manager: 'Manager',
  HR: 'HR',
};
