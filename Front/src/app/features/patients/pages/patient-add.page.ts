import { ChangeDetectorRef, Component } from '@angular/core';
import { CommonModule, Location } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

import { PatientsService } from '../services/patients.service';
import { CreatePatientDto } from '../models/patients.model';

@Component({
  standalone: true,
  templateUrl: './patient-add.page.html',
  imports: [CommonModule, FormsModule],
  styleUrls: ['./patients.css']
})
export class PatientFormPage {

  model: CreatePatientDto = {
    firstName: '',
    lastName: '',
    dateOfBirth: '',
    gender: '',
    address: '',
    phoneNumber: ''
  };

  constructor(
    private patientsService: PatientsService,
    private router: Router,
    private location: Location,
  ) {}

  save(): void {
    this.patientsService.create(this.model).subscribe(() => {
      this.router.navigate(['/patients']);
    });
  }
  goBack(): void {
    this.location.back();
  }
}
