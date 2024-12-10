import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./lablist.component').then(m => m.LablistComponent),
    data: {
      title: `Lab List`
    }
  }
];

