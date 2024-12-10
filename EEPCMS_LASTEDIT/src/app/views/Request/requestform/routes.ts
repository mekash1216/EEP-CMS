import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./requestform.component').then(m => m.RequestformComponent),
    data: {
      title: `Referral`
    }
  }
];

