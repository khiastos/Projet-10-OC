import { Routes } from '@angular/router';
import { AuthGuard } from './features/Identity/services/auth.guard';

export const routes: Routes = [
  {
    path: 'login',
    loadComponent: () =>
      import('./features/Identity/pages/login.component')
        .then(m => m.LoginComponent)
  },
  {
    path: 'patients',
    canActivate: [AuthGuard],
    loadComponent: () =>
      import('./features/patients/pages/patients.page')
        .then(m => m.PatientsPage)
  },
  {
    path: 'patients/new',
    canActivate: [AuthGuard],
    loadComponent: () =>
      import('./features/patients/pages/patient-add.page')
        .then(m => m.PatientFormPage)
  },
  {
    path: 'patients/edit/:id',
    canActivate: [AuthGuard],
    loadComponent: () =>
      import('./features/patients/pages/patient-edit.page')
        .then(m => m.PatientEditPage)
  },
  {
    path: 'patients/:id',
    canActivate: [AuthGuard],
    loadComponent: () =>
      import('./features/patients/pages/patient-detail.page')
        .then(m => m.PatientDetailPage)
  },
  {
    path: '',
    redirectTo: 'patients',
    pathMatch: 'full'
  }
];
