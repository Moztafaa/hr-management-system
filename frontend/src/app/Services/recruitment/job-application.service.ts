import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../models/api-response';
import {
  JobApplication,
  JobApplicationDetail,
  CreateJobApplicationDto,
  UpdateJobApplicationStatusDto,
} from '../../models/recruitment/job-application';

@Injectable({
  providedIn: 'root',
})
export class JobApplicationService {
  private http = inject(HttpClient);
  private apiUrl = 'http://localhost:5093/api/JobApplication';

  getAll(): Observable<ApiResponse<JobApplication[]>> {
    return this.http.get<ApiResponse<JobApplication[]>>(this.apiUrl);
  }

  getById(id: number): Observable<ApiResponse<JobApplication>> {
    return this.http.get<ApiResponse<JobApplication>>(`${this.apiUrl}/${id}`);
  }

  getDetail(id: number): Observable<ApiResponse<JobApplicationDetail>> {
    return this.http.get<ApiResponse<JobApplicationDetail>>(`${this.apiUrl}/${id}/detail`);
  }

  getByJobPosting(jobPostingId: number): Observable<ApiResponse<JobApplication[]>> {
    return this.http.get<ApiResponse<JobApplication[]>>(
      `${this.apiUrl}/jobposting/${jobPostingId}`
    );
  }

  getByCandidate(candidateId: number): Observable<ApiResponse<JobApplication[]>> {
    return this.http.get<ApiResponse<JobApplication[]>>(`${this.apiUrl}/candidate/${candidateId}`);
  }

  getByStatus(status: string): Observable<ApiResponse<JobApplication[]>> {
    return this.http.get<ApiResponse<JobApplication[]>>(`${this.apiUrl}/status/${status}`);
  }

  getApplicationCount(jobPostingId: number): Observable<ApiResponse<number>> {
    return this.http.get<ApiResponse<number>>(`${this.apiUrl}/count/${jobPostingId}`);
  }

  checkIfApplied(candidateId: number, jobPostingId: number): Observable<ApiResponse<boolean>> {
    const params = new HttpParams()
      .set('candidateId', candidateId.toString())
      .set('jobPostingId', jobPostingId.toString());
    return this.http.get<ApiResponse<boolean>>(`${this.apiUrl}/check-applied`, { params });
  }

  create(application: CreateJobApplicationDto): Observable<ApiResponse<JobApplication>> {
    return this.http.post<ApiResponse<JobApplication>>(this.apiUrl, application);
  }

  updateStatus(
    id: number,
    statusUpdate: UpdateJobApplicationStatusDto
  ): Observable<ApiResponse<void>> {
    return this.http.put<ApiResponse<void>>(`${this.apiUrl}/${id}/status`, statusUpdate);
  }

  delete(id: number): Observable<ApiResponse<void>> {
    return this.http.delete<ApiResponse<void>>(`${this.apiUrl}/${id}`);
  }
}
