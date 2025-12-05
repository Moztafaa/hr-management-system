export interface LeaveApproval {
  id: number;
  leaveRequestId: number;
  approverId: number;
  approverName: string;
  status: number; // 0: Pending, 1: Approved, 2: Rejected
  comments?: string;
  actionDate?: string;
  stepLevel: number;
  isFinalStep: boolean;
}

export interface LeaveApprovalAction {
  leaveRequestId: number;
  approverId: number;
}
