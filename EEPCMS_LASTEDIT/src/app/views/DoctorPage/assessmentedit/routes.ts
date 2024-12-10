import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./assessmentedit.component').then(m => m.AssessmenteditComponent),
    data: {
      title: `Assessment  List`
    }
  }
];