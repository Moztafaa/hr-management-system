import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class Employee {
  private apiUrl = 'https://localhost:7005/api/Employee'; // Backend API URL

  constructor(private http: HttpClient) { }  
  updateEmployeeImage(employeeId: number, base64Image: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/${employeeId}/update-image`, { base64Image });
  }
}
