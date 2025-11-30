import { ESSProfile } from './ess-profile';
import { LeaveBalance } from './leave-balance';
import { Payslip } from './payslip';

export interface ESSDashboard {
  profile?: ESSProfile;
  leaveBalances?: LeaveBalance[];
  recentPayslips?: Payslip[];
  pendingLeaveRequestsCount: number;
  approvedLeaveRequestsCount: number;
}
