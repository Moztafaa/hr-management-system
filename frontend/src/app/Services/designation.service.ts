import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiResponse } from '../models/api-response';
import {
  Designation,
  CreateDesignationDto,
  UpdateDesignationDto,
  DesignationWithEmployees,
} from '../models/designation';

@Injectable({
  providedIn: 'root',
})
export class DesignationService {
  private http = inject(HttpClient);
  private apiUrl = 'http://localhost:5093/api/Designation';

  getAll(): Observable<ApiResponse<Designation[]>> {
    return this.http.get<ApiResponse<Designation[]>>(this.apiUrl);
  }

  getById(id: number): Observable<ApiResponse<Designation>> {
    return this.http.get<ApiResponse<Designation>>(`${this.apiUrl}/${id}`);
  }

  getWithEmployees(id: number): Observable<ApiResponse<DesignationWithEmployees>> {
    return this.http.get<ApiResponse<DesignationWithEmployees>>(`${this.apiUrl}/${id}/employees`);
  }

  create(designation: CreateDesignationDto): Observable<ApiResponse<Designation>> {
    return this.http.post<ApiResponse<Designation>>(this.apiUrl, designation);
  }

  update(id: number, designation: UpdateDesignationDto): Observable<ApiResponse<boolean>> {
    return this.http.put<ApiResponse<boolean>>(`${this.apiUrl}/${id}`, designation);
  }

  delete(id: number): Observable<ApiResponse<boolean>> {
    return this.http.delete<ApiResponse<boolean>>(`${this.apiUrl}/${id}`);
  }
}
