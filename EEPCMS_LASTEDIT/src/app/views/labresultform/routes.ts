import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./labresultform.component').then(m => m.LabresultformComponent),
    data: {
      title: `Laboratory`
    }
  }
];
