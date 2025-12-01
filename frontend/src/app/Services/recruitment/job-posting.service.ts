import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../models/api-response';
import {
  JobPosting,
  CreateJobPostingDto,
  UpdateJobPostingDto,
} from '../../models/recruitment/job-posting';

@Injectable({
  providedIn: 'root',
})
export class JobPostingService {
  private http = inject(HttpClient);
  private apiUrl = 'http://localhost:5093/api/JobPosting';

  getAll(): Observable<ApiResponse<JobPosting[]>> {
    return this.http.get<ApiResponse<JobPosting[]>>(this.apiUrl);
  }

  getActive(): Observable<ApiResponse<JobPosting[]>> {
    return this.http.get<ApiResponse<JobPosting[]>>(`${this.apiUrl}/active`);
  }

  getById(id: number): Observable<ApiResponse<JobPosting>> {
    return this.http.get<ApiResponse<JobPosting>>(`${this.apiUrl}/${id}`);
  }

  getByDepartment(departmentId: number): Observable<ApiResponse<JobPosting[]>> {
    return this.http.get<ApiResponse<JobPosting[]>>(`${this.apiUrl}/department/${departmentId}`);
  }

  getByDesignation(designationId: number): Observable<ApiResponse<JobPosting[]>> {
    return this.http.get<ApiResponse<JobPosting[]>>(`${this.apiUrl}/designation/${designationId}`);
  }

  create(jobPosting: CreateJobPostingDto): Observable<ApiResponse<JobPosting>> {
    return this.http.post<ApiResponse<JobPosting>>(this.apiUrl, jobPosting);
  }

  update(id: number, jobPosting: UpdateJobPostingDto): Observable<ApiResponse<void>> {
    return this.http.put<ApiResponse<void>>(`${this.apiUrl}/${id}`, jobPosting);
  }

  delete(id: number): Observable<ApiResponse<void>> {
    return this.http.delete<ApiResponse<void>>(`${this.apiUrl}/${id}`);
  }
}
