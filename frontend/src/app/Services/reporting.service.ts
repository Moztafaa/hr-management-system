import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {
    DashboardKpisDto,
    DepartmentBreakdown,
    DesignationBreakdown,
    PayrollTrend,
    CostBreakdown,
    SalaryComparison,
    ApplicationsPerJobPosting,
    PipelineData,
    StatusBreakdown,
    TypeBreakdown,
    MonthlyLeaveTrend
} from '../models/reporting';

@Injectable({
    providedIn: 'root'
})
export class ReportingService {
    private http = inject(HttpClient);
    private apiUrl = 'https://localhost:7005/api/Reporting';

    // Employee Metrics
    getTotalEmployees(): Observable<number> {
        return this.http.get<number>(`${this.apiUrl}/TotalEmployees`);
    }

    getEmployeesByDepartment(): Observable<DepartmentBreakdown> {
        return this.http.get<DepartmentBreakdown>(`${this.apiUrl}/EmployeesByDepartment`);
    }

    getEmployeesByDesignation(): Observable<DesignationBreakdown> {
        return this.http.get<DesignationBreakdown>(`${this.apiUrl}/EmployeesByDesignation`);
    }

    // Payroll Metrics
    getTotalPayroll(year: number, month: number): Observable<number> {
        return this.http.get<number>(`${this.apiUrl}/TotalPayroll/${year}/${month}`);
    }

    getPayrollTrend(year: number): Observable<PayrollTrend[]> {
        return this.http.get<PayrollTrend[]>(`${this.apiUrl}/PayrollTrend/${year}`);
    }

    getAllowanceCostBreakdown(year: number, month: number): Observable<CostBreakdown> {
        return this.http.get<CostBreakdown>(`${this.apiUrl}/AllowanceCostBreakdown/${year}/${month}`);
    }

    getDeductionCostBreakdown(year: number, month: number): Observable<CostBreakdown> {
        return this.http.get<CostBreakdown>(`${this.apiUrl}/DeductionCostBreakdown/${year}/${month}`);
    }

    getSalaryComparison(): Observable<SalaryComparison[]> {
        return this.http.get<SalaryComparison[]>(`${this.apiUrl}/SalaryComparison`);
    }

    // Recruitment Metrics
    getTotalJobPostings(): Observable<number> {
        return this.http.get<number>(`${this.apiUrl}/TotalJobPostings`);
    }

    getActiveJobPostings(): Observable<number> {
        return this.http.get<number>(`${this.apiUrl}/ActiveJobPostings`);
    }

    getApplicationsPerJobPosting(): Observable<Record<string, number>> {
        return this.http.get<Record<string, number>>(`${this.apiUrl}/ApplicationsPerJobPosting`);
    }

    getRecruitmentPipeline(): Observable<Record<string, number>> {
        return this.http.get<Record<string, number>>(`${this.apiUrl}/RecruitmentPipeline`);
    }

    getAverageApplicationReviewTime(): Observable<string> {
        return this.http.get<string>(`${this.apiUrl}/AverageApplicationReviewTime`);
    }

    // Leave Metrics
    getTotalLeaveRequests(): Observable<number> {
        return this.http.get<number>(`${this.apiUrl}/TotalLeaveRequests`);
    }

    getLeaveRequestsByStatus(): Observable<StatusBreakdown> {
        return this.http.get<StatusBreakdown>(`${this.apiUrl}/LeaveRequestsByStatus`);
    }

    getLeaveUsageByType(): Observable<TypeBreakdown> {
        return this.http.get<TypeBreakdown>(`${this.apiUrl}/LeaveUsageByType`);
    }

    getMonthlyLeaveTrend(year: number): Observable<MonthlyLeaveTrend[]> {
        return this.http.get<MonthlyLeaveTrend[]>(`${this.apiUrl}/MonthlyLeaveTrend/${year}`);
    }

    getAverageLeaveApprovalTime(): Observable<string> {
        return this.http.get<string>(`${this.apiUrl}/AverageLeaveApprovalTime`);
    }

    // Dashboard
    getDashboardKpis(year: number, month: number): Observable<DashboardKpisDto> {
        return this.http.get<DashboardKpisDto>(`${this.apiUrl}/DashboardKpis/${year}/${month}`);
    }
}
