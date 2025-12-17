import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Patient } from '../models/patients.model';

@Injectable({
  providedIn: 'root'
})
export class PatientsService {

  private readonly apiUrl = '/api/patients';

  constructor(private http: HttpClient) {}

  getAll(): Observable<Patient[]> {
    return this.http.get<Patient[]>(this.apiUrl);
  }
}
