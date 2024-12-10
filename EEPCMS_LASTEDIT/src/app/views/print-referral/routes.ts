import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./print-referral.component').then(m => m.PrintReferralComponent),
    data: {
      title: `Referral`
    }
  }
];

