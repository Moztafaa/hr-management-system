import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../models/api-response';
import {
  Candidate,
  CreateCandidateDto,
  UpdateCandidateDto,
} from '../../models/recruitment/candidate';

@Injectable({
  providedIn: 'root',
})
export class CandidateService {
  private http = inject(HttpClient);
  private apiUrl = 'http://localhost:5093/api/Candidate';

  getAll(): Observable<ApiResponse<Candidate[]>> {
    return this.http.get<ApiResponse<Candidate[]>>(this.apiUrl);
  }

  getById(id: number): Observable<ApiResponse<Candidate>> {
    return this.http.get<ApiResponse<Candidate>>(`${this.apiUrl}/${id}`);
  }

  getWithApplications(id: number): Observable<ApiResponse<Candidate>> {
    return this.http.get<ApiResponse<Candidate>>(`${this.apiUrl}/${id}/applications`);
  }

  getByEmail(email: string): Observable<ApiResponse<Candidate>> {
    return this.http.get<ApiResponse<Candidate>>(
      `${this.apiUrl}/email/${encodeURIComponent(email)}`
    );
  }

  search(searchTerm: string): Observable<ApiResponse<Candidate[]>> {
    const params = new HttpParams().set('searchTerm', searchTerm);
    return this.http.get<ApiResponse<Candidate[]>>(`${this.apiUrl}/search`, { params });
  }

  checkEmailExists(email: string): Observable<ApiResponse<boolean>> {
    return this.http.get<ApiResponse<boolean>>(
      `${this.apiUrl}/check-email/${encodeURIComponent(email)}`
    );
  }

  create(candidate: CreateCandidateDto): Observable<ApiResponse<Candidate>> {
    return this.http.post<ApiResponse<Candidate>>(this.apiUrl, candidate);
  }

  update(id: number, candidate: UpdateCandidateDto): Observable<ApiResponse<void>> {
    return this.http.put<ApiResponse<void>>(`${this.apiUrl}/${id}`, candidate);
  }

  delete(id: number): Observable<ApiResponse<void>> {
    return this.http.delete<ApiResponse<void>>(`${this.apiUrl}/${id}`);
  }
}
