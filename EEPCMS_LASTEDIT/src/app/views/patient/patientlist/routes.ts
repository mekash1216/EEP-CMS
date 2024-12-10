import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./patientlist.component').then(m => m.PatientlistComponent),
    data: {
      title: `User List`
    }
  }
];