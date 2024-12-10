import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./doctor.component').then(m => m.DoctorComponent),
    data: {
      title: `User List`
    }
  }
];
