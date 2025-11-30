import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class Attendance {
  private apiUrl = 'https://localhost:7005/api/Attendance'; // Backend API URL
  constructor(private http: HttpClient) {}
  checkIn(employeeId: number, file: File): Observable<any> {
    const formData = new FormData();
    formData.append('Image', file); // لازم يكون نفس اسم الفيلد في DTO

    return this.http.post(`${this.apiUrl}/${employeeId}/check-in`, formData);
  }
  checkOut(employeeId: number, file: File): Observable<any> {
    const formData = new FormData();
    formData.append('Image', file); // لازم يكون نفس اسم الفيلد في DTO

    return this.http.post(`${this.apiUrl}/${employeeId}/check-out`, formData);
  }
}
