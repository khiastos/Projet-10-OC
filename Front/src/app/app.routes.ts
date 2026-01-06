import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'patients',
    pathMatch: 'full'
  },
  {
    path: 'patients',
    loadChildren: () =>
      import('./features/patients/patients.routes')
        .then(m => m.PATIENTS_ROUTES)
  },
  {
    path: 'login',
    loadComponent: () =>
      import('./features/Identity/pages/login.component')
        .then(m => m.LoginComponent)
  }
];
