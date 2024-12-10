import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./requestlist.component').then(m => m.RequestlistComponent),
    data: {
      title: `Referral`
    }
  }
];

