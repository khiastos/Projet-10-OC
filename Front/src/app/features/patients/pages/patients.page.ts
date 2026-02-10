import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

import { PatientsService } from '../services/patients.service';
import { Patient } from '../models/patients.model';
import { AuthService } from '../../Identity/services/auth.service';


@Component({
  standalone: true,
  templateUrl: './patients.page.html',
  imports: [CommonModule],
  styleUrls: ['./patients.css'],
})
export class PatientsPage {
  patients: Patient[] = [];
  loading = true;

  constructor(
    private patientsService: PatientsService,
    private router: Router,
    private authService: AuthService,
    // Obligé pour refresh la vue quand la liste charge
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit() {
    this.loadPatients();

    this.authService.isLoggedIn$.subscribe(isLogged => {

      if (!isLogged) {
        this.router.navigate(['/login']);
        this.cdr.detectChanges();
        return;
      }

      this.loadPatients();
    });
  }

  loadPatients(): void {
    this.loading = true;

    this.patientsService.getAll().subscribe({
      next: (patients: Patient[]) => {
        this.patients = patients;
        this.loading = false;
        this.cdr.detectChanges();
      },
      error: () => {
        this.loading = false;
      }
    });
  }

  openPatient(id: number) {
    this.router.navigate(['/patients', id]);
  }

  createPatient() {
    this.router.navigate(['/patients/new']);
  }

  editPatient(id: number) {
    this.router.navigate(['/patients/edit', id]);
  }

  // Ouvre un popup de confirmation avant de supprimer
  deletePatient(id: number) {
    if (!confirm('Supprimer ce patient ?')) return;

    this.patientsService.delete(id).subscribe(() => {
      this.loadPatients();
    });
  }
}
