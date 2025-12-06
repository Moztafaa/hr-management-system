import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiResponse } from '../models/api-response';

export interface DashboardStats {
    totalEmployees: number;
    totalDepartments: number;
    activeLeaveRequests: number;
    pendingApplications: number;
    todayAttendance: number;
    thisMonthPayslips: number;
}

export interface DepartmentEmployeeCount {
    departmentName: string;
    employeeCount: number;
}

export interface DesignationDistribution {
    designationTitle: string;
    count: number;
}

export interface RecentActivity {
    id: number;
    type: string;
    description: string;
    timestamp: Date;
    icon: string;
    color: string;
}

export interface LeaveStatusSummary {
    status: string;
    count: number;
    color: string;
}

@Injectable({
    providedIn: 'root',
})
export class DashboardService {
    private http = inject(HttpClient);
    private apiUrl = 'https://localhost:7005/api';

    getDashboardStats(): Observable<ApiResponse<DashboardStats>> {
        return this.http.get<ApiResponse<DashboardStats>>(
            `${this.apiUrl}/Dashboard/stats`
        );
    }

    getDepartmentEmployeeCounts(): Observable<
        ApiResponse<DepartmentEmployeeCount[]>
    > {
        return this.http.get<ApiResponse<DepartmentEmployeeCount[]>>(
            `${this.apiUrl}/Department`
        );
    }

    getDesignationDistribution(): Observable<
        ApiResponse<DesignationDistribution[]>
    > {
        return this.http.get<ApiResponse<DesignationDistribution[]>>(
            `${this.apiUrl}/Designation`
        );
    }
}
