import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./patienthistory.component').then(m => m.PatienthistoryComponent),
    data: {
      title: `Patient List`
    }
  }
];