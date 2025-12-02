import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../models/api-response';
import {
  TrainingCourse,
  CreateTrainingCourseDto,
  UpdateTrainingCourseDto,
} from '../../models/training';

@Injectable({
  providedIn: 'root',
})
export class TrainingCourseService {
  private http = inject(HttpClient);
  private apiUrl = 'http://localhost:5093/api/TrainingCourse';

  getAll(): Observable<ApiResponse<TrainingCourse[]>> {
    return this.http.get<ApiResponse<TrainingCourse[]>>(this.apiUrl);
  }

  getById(id: number): Observable<ApiResponse<TrainingCourse>> {
    return this.http.get<ApiResponse<TrainingCourse>>(`${this.apiUrl}/${id}`);
  }

  create(course: CreateTrainingCourseDto): Observable<ApiResponse<TrainingCourse>> {
    return this.http.post<ApiResponse<TrainingCourse>>(this.apiUrl, course);
  }

  update(id: number, course: UpdateTrainingCourseDto): Observable<ApiResponse<TrainingCourse>> {
    return this.http.put<ApiResponse<TrainingCourse>>(`${this.apiUrl}/${id}`, course);
  }

  delete(id: number): Observable<ApiResponse<boolean>> {
    return this.http.delete<ApiResponse<boolean>>(`${this.apiUrl}/${id}`);
  }
}
