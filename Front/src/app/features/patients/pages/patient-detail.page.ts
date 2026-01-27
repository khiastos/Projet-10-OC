import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { PatientsService } from '../services/patients.service';
import { Patient } from '../models/patients.model';
import { Location } from '@angular/common';
import { NotesService } from '../../notes/services/notes.service';
import { Note, NoteCreate } from '../../notes/models/notes.model';
import { FormsModule } from '@angular/forms';


@Component({
  standalone: true,
  selector: 'app-patient-detail',
  imports: [CommonModule, FormsModule],
  templateUrl: './patient-detail.page.html',
  styleUrls: ['./patients.css']
})
export class PatientDetailPage implements OnInit {

  patient?: Patient;
  notes: Note[] = [];
  patientLoading = true;
  notesLoading = true;
  newNoteContent = '';

  constructor(
    private route: ActivatedRoute,
    private patientsService: PatientsService,
    private notesService: NotesService,
    private cdr: ChangeDetectorRef,
    private location: Location
  ) {}

  ngOnInit(): void {
    // récupérer l'ID du patient depuis les paramètres de la route
    const id = Number(this.route.snapshot.paramMap.get('id'));

      this.patientsService.getById(id).subscribe({
        next: (patient: Patient) => {
          this.patient = patient;
          this.patientLoading = false;

      this.notesService.getByPatientId(id).subscribe({
        next: (notes) => {
          this.notes = notes ?? [];
          this.notesLoading = false;
          this.cdr.detectChanges();
        },
        error: () => {
          this.notes = [];
          this.notesLoading = false;
          this.cdr.detectChanges();
        }
      });
    },
    error: () => {
      this.patientLoading = false;
      this.notes = [];
      this.notesLoading = false;
      this.cdr.detectChanges();
    }
  });
}

addNote(): void {
  if (!this.patient || !this.newNoteContent.trim()) {
    return;
  }

  const newNote: NoteCreate = {
    patientId: this.patient.id,
    note: this.newNoteContent.trim()
  };

  this.notesService.create(newNote).subscribe({
    next: (createdNote) => {
      // Ajout immédiat dans la liste
      this.notes.unshift(createdNote);
      this.newNoteContent = '';
    },
    error: (err) => {
      console.error('Erreur ajout note', err);
    }
  });
}

  goBack(): void {
    this.location.back();
  }
}