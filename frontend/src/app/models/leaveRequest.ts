export interface LeaveRequest {
  id: number;
  employeeId: number;
  employeeName: string;
  leaveTypeId: number;
  leaveTypeName: string;
  startDate: string;
  endDate: string;
  totalDays: number;
  status: number;
  reviewedById: number;
  reviewedBy: string;
  reviewedAt: string;
}