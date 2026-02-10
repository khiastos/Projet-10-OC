import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Note, NoteCreate, NoteUpdate } from '../models/notes.model';

// Service pour gérer les opérations CRUD des notes
@Injectable({ providedIn: 'root' })
export class NotesService {
  private apiUrl = '/api/notes';
  constructor(private http: HttpClient) {}

    getByPatientId(patientId: number) {
    return this.http.get<Note[]>(`${this.apiUrl}/patient/${patientId}`);
    }

    create(note: NoteCreate) {
    return this.http.post<Note>(this.apiUrl, note);
    }

    update(id: string, note: NoteUpdate) {
    return this.http.put<Note>(`${this.apiUrl}/${id}`, note);
    }

    delete(id: string) {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
    }
}
