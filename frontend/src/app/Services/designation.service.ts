import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiResponse } from '../models/api-response';
import { Designation, CreateDesignationDto, UpdateDesignationDto } from '../models/designation';

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

  create(designation: CreateDesignationDto): Observable<ApiResponse<Designation>> {
    return this.http.post<ApiResponse<Designation>>(this.apiUrl, designation);
  }

  update(id: number, designation: UpdateDesignationDto): Observable<ApiResponse<void>> {
    return this.http.put<ApiResponse<void>>(`${this.apiUrl}/${id}`, designation);
  }

  delete(id: number): Observable<ApiResponse<void>> {
    return this.http.delete<ApiResponse<void>>(`${this.apiUrl}/${id}`);
  }
}
