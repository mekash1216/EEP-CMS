import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./labtestresultdetail.component').then(m => m.LabtestresultdetailComponent),
    data: {
      title: `Laboratory Result`
    }
  }
];
