import { ChangeDetectorRef, Component } from '@angular/core';
import { CommonModule, Location } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { PatientsService } from '../services/patients.service';
import { UpdatePatientDto } from '../models/patients.model';

@Component({
  standalone: true,
  templateUrl: './patient-edit.page.html',
  imports: [CommonModule, FormsModule],
  styleUrls: ['./patients.css']
})
export class PatientEditPage {
  id!: number;
  model!: UpdatePatientDto;

  constructor(
    private route: ActivatedRoute,
    private patientsService: PatientsService,
    private router: Router,
    private location: Location,
    private cdr: ChangeDetectorRef,
  ) {}

  ngOnInit() {
    this.id = Number(this.route.snapshot.paramMap.get('id'));

    this.patientsService.getById(this.id).subscribe(p => {
      this.model = {
        firstName: p.firstName,
        lastName: p.lastName,
        dateOfBirth: p.dateOfBirth.substring(0, 10),
        gender: p.gender,
        address: p.address,
        phoneNumber: p.phoneNumber
      };
      this.cdr.markForCheck();
    });
  }

  save() {
    this.patientsService.update(this.id, this.model).subscribe(() => {
      this.router.navigate(['/patients']);
    });
  }
   goBack(): void {
    this.location.back();
  }
}

 
