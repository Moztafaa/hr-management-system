export interface Payslip {
  id: number;
  employeeId: number;
  employeeName: string;
  basicSalary: number;
  totalAllowances: number;
  totalDeductions: number;
  netSalary: number;
  month: number;
  year: number;
  generatedAt: string;
  allowances?: PayslipAllowance[];
  deductions?: PayslipDeduction[];
}

export interface PayslipAllowance {
  id: number;
  name: string;
  amount: number;
  isPercentage: boolean;
}

export interface PayslipDeduction {
  id: number;
  name: string;
  amount: number;
  isPercentage: boolean;
}
