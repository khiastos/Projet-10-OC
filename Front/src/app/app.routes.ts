import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: 'patients',
    loadChildren: () =>
      import('./features/patients/patients.routes')
        .then(m => m.patientsRoutes)
  },
  {
    path: '',
    redirectTo: 'patients',
    pathMatch: 'full'
  }
];
