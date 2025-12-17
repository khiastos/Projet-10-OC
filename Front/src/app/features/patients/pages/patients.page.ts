import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PatientsService } from '../services/patients.service';
import { Patient } from '../models/patients.model';

@Component({
  selector: 'app-patients',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './patients.page.html',
  styleUrls: ['./patients.page.css']
})
export class PatientsPage implements OnInit {

  patients: Patient[] = [];
  loading = true;

  constructor(
    private patientsService: PatientsService,
    private cdr: ChangeDetectorRef
  ) {
    console.log('PatientsPage CONSTRUCTOR');
  }

  ngOnInit(): void {
    console.log('PatientsPage ngOnInit');

    this.patientsService.getAll().subscribe({
      next: (patients) => {
        console.log('PATIENTS RECUS', patients);

        this.patients = patients;
        this.loading = false;

        this.cdr.markForCheck();
      },
      error: (err) => {
        console.error(err);
        this.loading = false;
        this.cdr.markForCheck();
      }
    });
  }
}
