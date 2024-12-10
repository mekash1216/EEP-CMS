import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./prescription-item.component').then(m => m.PrescriptionItemComponent),
    data: {
   
    }
  }
];

