import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./default-layout.component').then(m => m.DefaultLayoutComponent),
 
  }
];