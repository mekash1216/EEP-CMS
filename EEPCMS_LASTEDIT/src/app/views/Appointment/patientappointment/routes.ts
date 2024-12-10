import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./patientappointment.component').then(m => m.PatientappointmentComponent),
    data: {
      title: `Patient List`
    }
  }
];