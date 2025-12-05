export interface LeaveType {
  id: number;
  name: string;
  description: string;
  maxDays: number;
  canCarryForward: boolean;
  carryForwardLimit: number;
  isPaid: boolean;
}