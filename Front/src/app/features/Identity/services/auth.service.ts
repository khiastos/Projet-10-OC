import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";
import { LoginModel } from "../models/login.model";

@Injectable({providedIn: 'root'})
export class AuthService {
    private readonly apiUrl = '/api/auth';

    constructor(private http: HttpClient) {}

    login(credentials: LoginModel): Observable<void> {

        // btoa = encode le password en Base64 càd HTTP basic
        const encoded = btoa(`${credentials.username}:${credentials.password}`);

        const headers = new HttpHeaders({
            Authorization: `Basic ${encoded}`
        });

        return this.http.get<void>(this.apiUrl, { headers });
    } 
}
