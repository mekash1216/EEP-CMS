import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./prescription.component').then(m => m.PrescriptionComponent),
    data: {
      title: $localize`Prescription`
    }
  }
];

