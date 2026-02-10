import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { LoginModel } from "../models/login.model";
import { BehaviorSubject } from "rxjs";
import { tap } from "rxjs/operators";

@Injectable({ providedIn: 'root' })
export class AuthService {

  private readonly tokenKey = 'jwt';
public isLoggedIn$ = new BehaviorSubject<boolean>(
  !!localStorage.getItem(this.tokenKey)
);
  constructor(private http: HttpClient) {}

  login(credentials: LoginModel) {
  return this.http.post<{ token: string }>(
    '/api/auth/login',
    credentials
  ).pipe(
    tap(response => {
  localStorage.setItem(this.tokenKey, response.token);
  this.isLoggedIn$.next(true);
})
  );
}

  register(credentials: LoginModel) {
    return this.http.post('/api/auth/register', credentials);
  }

  logout(): void {
    localStorage.removeItem(this.tokenKey);
    this.isLoggedIn$.next(false);
  }

  isAuthenticated(): boolean {
    return !!localStorage.getItem(this.tokenKey);
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }
}