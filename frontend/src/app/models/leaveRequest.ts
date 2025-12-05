export interface LeaveRequest {
  id: number;
  employeeId: number;
  employeeName: string;
  leaveTypeId: number;
  leaveTypeName: string;
  startDate: string;
  endDate: string;
  totalDays: number;
  status: number; // 0: Pending, 1: Approved, 2: Rejected, 3: Cancelled
  reason?: string;
  comments?: string;
  reviewedById: number;
  reviewedBy?: string;
  reviewedAt?: string;
  createdAt: string;
}

export interface CreateLeaveRequest {
  employeeId: number;
  leaveTypeId: number;
  startDate: string;
  endDate: string;
  reason?: string;
}

export interface UpdateLeaveRequest {
  leaveTypeId: number;
  startDate: string;
  endDate: string;
  reason?: string;
}