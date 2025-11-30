export interface LeaveBalance {
  id: number;
  employeeId: number;
  leaveTypeId: number;
  leaveTypeName?: string;
  totalAllocated: number;
  usedDays: number;
  remainingDays: number;
  year: number;
}
