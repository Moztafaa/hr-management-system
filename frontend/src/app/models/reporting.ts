export interface DashboardKpisDto {
    totalEmployees: number;
    totalPayroll: number;
    totalAllowances: number;
    totalDeductions: number;
    activeJobPostings: number;
    pendingLeaveRequests: number;
}

// API returns dictionary format: { "Department Name": count }
export type DepartmentBreakdown = Record<string, number>;

// API returns dictionary format: { "Designation Title": count }
export type DesignationBreakdown = Record<string, number>;

export interface PayrollTrend {
    month: number;
    totalPayroll: number;
}

// API returns dictionary format: { "Name": amount }
export type CostBreakdown = Record<string, number>;

export interface SalaryComparison {
    minSalary: number;
    maxSalary: number;
    avgSalary: number;
}

export interface ApplicationsPerJobPosting {
    jobTitle: string;
    applicationCount: number;
}

export interface PipelineData {
    stage: string;
    count: number;
}

// API returns dictionary format: { "Status": count }
export type StatusBreakdown = Record<string, number>;

// API returns dictionary format: { "LeaveType": count }
export type TypeBreakdown = Record<string, number>;

export interface MonthlyLeaveTrend {
    month: number;
    year: number;
    totalLeaves: number;
}
