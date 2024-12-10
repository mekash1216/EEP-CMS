import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./testedpatient.component').then(m => m.TestedpatientComponent),
    data: {
      title: `Patient Info`
    }
  }
];