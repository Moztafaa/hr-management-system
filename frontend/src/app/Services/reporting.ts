import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiResponse } from '../models/api-response';
import {
    DepartmentBreakdown,
    DesignationBreakdown,
    PayrollTrend,
    CostBreakdown,
    SalaryComparison,
    ApplicationsPerJobPosting,
    PipelineData,
    StatusBreakdown,
    TypeBreakdown,
    MonthlyLeaveTrend,
    DashboardKpisDto,
} from '../models/reporting';

@Injectable({
    providedIn: 'root',
})
export class ReportingService {
    private http = inject(HttpClient);
    private apiUrl = 'http://localhost:5093/api/Reporting';

    // Employee Metrics
    getTotalEmployees(): Observable<ApiResponse<number>> {
        return this.http.get<ApiResponse<number>>(`${this.apiUrl}/TotalEmployees`);
    }

    getEmployeesByDepartment(): Observable<ApiResponse<DepartmentBreakdown[]>> {
        return this.http.get<ApiResponse<DepartmentBreakdown[]>>(`${this.apiUrl}/EmployeesByDepartment`);
    }

    getEmployeesByDesignation(): Observable<ApiResponse<DesignationBreakdown[]>> {
        return this.http.get<ApiResponse<DesignationBreakdown[]>>(`${this.apiUrl}/EmployeesByDesignation`);
    }

    // Payroll Metrics
    getTotalPayroll(year: number, month: number): Observable<ApiResponse<number>> {
        return this.http.get<ApiResponse<number>>(`${this.apiUrl}/TotalPayroll/${year}/${month}`);
    }

    getPayrollTrend(year: number): Observable<ApiResponse<PayrollTrend[]>> {
        return this.http.get<ApiResponse<PayrollTrend[]>>(`${this.apiUrl}/PayrollTrend/${year}`);
    }

    getAllowanceCostBreakdown(year: number, month: number): Observable<ApiResponse<CostBreakdown[]>> {
        return this.http.get<ApiResponse<CostBreakdown[]>>(
            `${this.apiUrl}/AllowanceCostBreakdown/${year}/${month}`
        );
    }

    getDeductionCostBreakdown(year: number, month: number): Observable<ApiResponse<CostBreakdown[]>> {
        return this.http.get<ApiResponse<CostBreakdown[]>>(
            `${this.apiUrl}/DeductionCostBreakdown/${year}/${month}`
        );
    }

    getSalaryComparison(): Observable<ApiResponse<SalaryComparison>> {
        return this.http.get<ApiResponse<SalaryComparison>>(`${this.apiUrl}/SalaryComparison`);
    }

    // Recruitment Metrics
    getTotalJobPostings(): Observable<ApiResponse<number>> {
        return this.http.get<ApiResponse<number>>(`${this.apiUrl}/TotalJobPostings`);
    }

    getActiveJobPostings(): Observable<ApiResponse<number>> {
        return this.http.get<ApiResponse<number>>(`${this.apiUrl}/ActiveJobPostings`);
    }

    getApplicationsPerJobPosting(): Observable<ApiResponse<ApplicationsPerJobPosting[]>> {
        return this.http.get<ApiResponse<ApplicationsPerJobPosting[]>>(
            `${this.apiUrl}/ApplicationsPerJobPosting`
        );
    }

    getRecruitmentPipeline(): Observable<ApiResponse<PipelineData[]>> {
        return this.http.get<ApiResponse<PipelineData[]>>(`${this.apiUrl}/RecruitmentPipeline`);
    }

    getAverageApplicationReviewTime(): Observable<ApiResponse<string>> {
        return this.http.get<ApiResponse<string>>(`${this.apiUrl}/AverageApplicationReviewTime`);
    }

    // Leave Metrics
    getTotalLeaveRequests(): Observable<ApiResponse<number>> {
        return this.http.get<ApiResponse<number>>(`${this.apiUrl}/TotalLeaveRequests`);
    }

    getLeaveRequestsByStatus(): Observable<ApiResponse<StatusBreakdown[]>> {
        return this.http.get<ApiResponse<StatusBreakdown[]>>(`${this.apiUrl}/LeaveRequestsByStatus`);
    }

    getLeaveUsageByType(): Observable<ApiResponse<TypeBreakdown[]>> {
        return this.http.get<ApiResponse<TypeBreakdown[]>>(`${this.apiUrl}/LeaveUsageByType`);
    }

    getMonthlyLeaveTrend(year: number): Observable<ApiResponse<MonthlyLeaveTrend[]>> {
        return this.http.get<ApiResponse<MonthlyLeaveTrend[]>>(`${this.apiUrl}/MonthlyLeaveTrend/${year}`);
    }

    getAverageLeaveApprovalTime(): Observable<ApiResponse<string>> {
        return this.http.get<ApiResponse<string>>(`${this.apiUrl}/AverageLeaveApprovalTime`);
    }

    // Dashboard
    getDashboardKpis(year: number, month: number): Observable<ApiResponse<DashboardKpisDto>> {
        return this.http.get<ApiResponse<DashboardKpisDto>>(
            `${this.apiUrl}/DashboardKpis/${year}/${month}`
        );
    }
}
