import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./labedit.component').then(m => m.LabeditComponent),
    data: {
      title: `Lab Edit`
    }
  }
];

