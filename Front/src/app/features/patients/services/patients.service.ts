import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CreatePatientDto, UpdatePatientDto } from '../models/patients.model';

// Service pour gérer les opérations CRUD des patients, c'est mon controlleur en back
@Injectable({ providedIn: 'root' })
export class PatientsService {
  private apiUrl = '/api/patients';
  constructor(private http: HttpClient) {}

  getAll() {
    return this.http.get<any[]>(this.apiUrl);
  }

  getById(id: number) {
    return this.http.get<any>(`${this.apiUrl}/${id}`);
  }

  create(dto: CreatePatientDto) {
    return this.http.post(this.apiUrl, dto);
  }

  update(id: number, dto: UpdatePatientDto) {
    return this.http.put(`${this.apiUrl}/${id}`, dto);
  }

  delete(id: number) {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}

