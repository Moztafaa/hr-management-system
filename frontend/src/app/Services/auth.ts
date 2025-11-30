import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class Auth {
  private apiUrl = 'https://localhost:7005/api/Account'; // Backend API URL

  constructor(private http: HttpClient) { }

  login(data: any): Observable<any> {
    console.log("LOGIN URL:", `${this.apiUrl}/login`);
    console.log("LOGIN DATA:", data);
    return this.http.post(`${this.apiUrl}/login`, data);
  }

  register(data: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/register-employee`, data);
  }

  saveToken(token: string) {
    localStorage.setItem('jwtToken', token);
  }

  saveUserId(userId: number) {
    localStorage.setItem('userId', userId.toString());
  }

  getToken(): string | null {
    return localStorage.getItem('jwtToken');
  }
}
