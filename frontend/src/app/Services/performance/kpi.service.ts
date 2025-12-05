import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../models/api-response';
import { KPI, CreateKpiDto, KPIResult, CreateKpiResultDto } from '../../models/performance';

@Injectable({
  providedIn: 'root',
})
export class KpiService {
  private http = inject(HttpClient);
  private apiUrl = 'http://localhost:5093/api/Performance';

  create(kpi: CreateKpiDto): Observable<ApiResponse<KPI>> {
    return this.http.post<ApiResponse<KPI>>(`${this.apiUrl}/kpis`, kpi);
  }

  addResult(result: CreateKpiResultDto): Observable<ApiResponse<KPIResult>> {
    return this.http.post<ApiResponse<KPIResult>>(`${this.apiUrl}/kpis/results`, result);
  }
}
