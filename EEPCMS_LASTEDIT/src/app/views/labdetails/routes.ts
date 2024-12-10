import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./labdetails.component').then(m => m.LabdetailsComponent),
    data: {
      title: `Lab Details`
    }
  }
];

