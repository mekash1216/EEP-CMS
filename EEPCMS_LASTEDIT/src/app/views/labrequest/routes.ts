import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./labrequest.component').then(m => m.LabrequestComponent),
    data: {
      title: `Laboratory`
    }
  }
];
