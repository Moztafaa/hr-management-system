export interface LeaveRequest {
  id: number;
  employeeId: number;
  leaveTypeId: number;
  leaveTypeName?: string;
  startDate: string;
  endDate: string;
  totalDays: number;
  status: LeaveStatus;
  reason?: string;
  reviewedById?: number;
  reviewedAt?: string;
  createdAt?: string;
}

export interface CreateLeaveRequest {
  employeeId: number;
  leaveTypeId: number;
  startDate: string;
  endDate: string;
  reason?: string;
}

export enum LeaveStatus {
  Pending = 0,
  Approved = 1,
  Rejected = 2,
}

export const LeaveStatusLabels: Record<LeaveStatus, string> = {
  [LeaveStatus.Pending]: 'Pending',
  [LeaveStatus.Approved]: 'Approved',
  [LeaveStatus.Rejected]: 'Rejected',
};
