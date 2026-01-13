import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { PatientsService } from '../services/patients.service';
import { Patient } from '../models/patients.model';
import { Location } from '@angular/common';


@Component({
  standalone: true,
  selector: 'app-patient-detail',
  imports: [CommonModule],
  templateUrl: './patient-detail.page.html',
  styleUrls: ['./patients.css']
})
export class PatientDetailPage implements OnInit {

  patient?: Patient;
  loading = true;

  constructor(
    private route: ActivatedRoute,
    private patientsService: PatientsService,
    private cdr: ChangeDetectorRef,
    private location: Location
  ) {}

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));

    this.patientsService.getById(id).subscribe({
      next: (patient: Patient) => {
        this.patient = patient;
        this.loading = false;
        this.cdr.markForCheck();
      },
      error: () => {
        this.loading = false;
        this.cdr.markForCheck();
      }
    });
  }

  goBack(): void {
    this.location.back();
  }
}