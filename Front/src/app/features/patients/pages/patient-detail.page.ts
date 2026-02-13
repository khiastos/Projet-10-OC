import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { PatientsService } from '../services/patients.service';
import { Patient } from '../models/patients.model';
import { Location } from '@angular/common';
import { NotesService } from '../../notes/services/notes.service';
import { Note, NoteCreate } from '../../notes/models/notes.model';
import { FormsModule } from '@angular/forms';
import { RiskAssessmentResult, RiskAssessmentService } from '../../risk-assessment/services/risk-assessment.service';
import { RiskTranslationPipe } from '../../risk-assessment/pipes/risk-translation.pipe';


@Component({
  standalone: true,
  selector: 'app-patient-detail',
  imports: [CommonModule, FormsModule, RiskTranslationPipe],
  templateUrl: './patient-detail.page.html',
  styleUrls: ['./patients.css']
})
export class PatientDetailPage implements OnInit {

  patient?: Patient;
  notes: Note[] = [];
  patientLoading = true;
  notesLoading = true;
  newNoteContent = '';
  // Pour l'édition des notes, null = pas d'édition en cours, string = id de la note en cours d'édition
  editingNoteId: string | null = null;
  // Pour stocker le contenu en cours d'édition
  editingContent = '';
  risk? : RiskAssessmentResult;


  constructor(
    private route: ActivatedRoute,
    private patientsService: PatientsService,
    private notesService: NotesService,
    private cdr: ChangeDetectorRef,
    private location: Location,
    private riskService: RiskAssessmentService
  ) {}

ngOnInit(): void {
  // Récupérer l'ID du patient depuis les paramètres de la route
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

          this.riskService.getRiskAssessment(id.toString()).subscribe({
            next: (risk) => {
              this.risk = risk;
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
  // Trim = supprimer les espaces inutiles, vérifier que le contenu n'est pas vide
  if (!this.patient || !this.newNoteContent.trim()) {
    return;
  }

  const newNote: NoteCreate = {
    // Vérifie l'id pour l'attribuer au bon patient
    patientId: this.patient.id,
    // Nettoie le contenu de la note
    note: this.newNoteContent.trim()
  };

  // Appel et déclenche le service pour créer la note
  this.notesService.create(newNote).subscribe({
    next: (createdNote) => {
      // Ajoute la note créée à la liste locale
      this.notes.push(createdNote);
      // Réinitialise le champ de saisie et désactive le bouton "ajouter"
      this.newNoteContent = '';
      this.cdr.detectChanges();
    },
    error: (err) => {
      console.error('Erreur ajout note', err);
    }
  });
}

// Démarrer l'édition d'une note
startEditNote(note: Note): void {
  // Passe la note en mode édition
  this.editingNoteId = note.id;
  // Met le texte actuel dans la variable d'édition
  this.editingContent = note.note;
}

cancelEditNote(): void {
  this.editingNoteId = null;
  this.editingContent = '';
}

saveNote(note: Note): void {
  const content = this.editingContent.trim();
  if (!content) return;

  this.notesService.update(note.id, { note: content }).subscribe({
    next: (updatedNote) => {
      // Met à jour la note dans la liste locale
      const index = this.notes.findIndex(n => n.id === note.id);
      // Permet de vérifier si la note existe avant de la mettre à jour
      if (index !== -1) {
        // Stocke la note en local
        this.notes[index] = updatedNote;
      }
      // Réinitialise l'état d'édition
      this.editingNoteId = null;
      this.editingContent = '';
      this.cdr.detectChanges();
    },
    error: (err) => {
      console.error('Erreur mise à jour note', err);
    }
  });
}

deleteNote(note: Note): void {
  const confirmed = confirm('Êtes-vous sûr de vouloir supprimer cette note ?');
  if (!confirmed) return;

  this.notesService.delete(note.id).subscribe({
    next: () => {
      // Supprime la note de la liste locale 
      this.notes = this.notes.filter(n => n.id !== note.id);
      this.cdr.detectChanges();
    },
    error: (err) => {
      console.error('Erreur suppression note', err);
    }
  });
}
  goBack(): void {
    this.location.back();
  }
}