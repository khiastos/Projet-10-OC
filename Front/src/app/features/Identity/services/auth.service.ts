import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";
import { LoginModel } from "../models/login.model";

@Injectable({ providedIn: 'root' })
export class AuthService {
  private readonly tokenKey = 'jwt';

  constructor(private http: HttpClient) {}

  login(credentials: LoginModel) {
    return this.http.post<{ token: string }>('/api/auth/login', credentials);
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


