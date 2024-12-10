import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./referral.component').then(m => m.ReferralComponent),
    data: {
      title: `Referral`
    }
  }
];

