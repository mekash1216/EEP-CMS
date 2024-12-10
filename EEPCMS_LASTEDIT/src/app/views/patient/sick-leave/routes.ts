import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./sick-leave.component').then(m => m.SickLeaveComponent),
    data: {
      title: `Referral`
    }
  }
];
