import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./labtestresult.component').then(m => m.LabtestresultComponent),
    data: {
      title: `Laboratory Result`
    }
  }
];
