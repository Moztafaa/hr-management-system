import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../models/api-response';
import {
  Competency,
  CreateCompetencyDto,
  RateCompetencyDto,
  EmployeeCompetencyRating,
} from '../../models/performance';

@Injectable({
  providedIn: 'root',
})
export class CompetencyService {
  private http = inject(HttpClient);
  private apiUrl = 'http://localhost:5093/api/Performance';

  create(competency: CreateCompetencyDto): Observable<ApiResponse<Competency>> {
    return this.http.post<ApiResponse<Competency>>(`${this.apiUrl}/competencies`, competency);
  }

  rate(dto: RateCompetencyDto): Observable<ApiResponse<EmployeeCompetencyRating>> {
    return this.http.post<ApiResponse<EmployeeCompetencyRating>>(
      `${this.apiUrl}/competencies/rate`,
      dto
    );
  }
}
