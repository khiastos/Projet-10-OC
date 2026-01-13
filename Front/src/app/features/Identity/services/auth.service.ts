import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { LoginModel } from "../models/login.model";

@Injectable({ providedIn: 'root' })
export class AuthService {
  private readonly tokenKey = 'jwt';

  constructor(private http: HttpClient) {}

  login(credentials: LoginModel) {
    return this.http.post<{ token: string }>(
      '/api/auth/login',
      credentials
    );
  }

  register(credentials: LoginModel) {
    return this.http.post('/api/auth/register', credentials);
  }

  logout(): void {
    localStorage.removeItem(this.tokenKey);
  }

  isAuthenticated(): boolean {
    return !!localStorage.getItem(this.tokenKey);
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }
}