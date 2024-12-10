import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./prescriptionprint.component').then(m => m.PrescriptionprintComponent),
    data: {
    
    }
  }
];

