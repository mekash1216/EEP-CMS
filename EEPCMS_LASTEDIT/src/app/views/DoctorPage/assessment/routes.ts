import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./assessment.component').then(m => m.AssessmentComponent),
    data: {
      title: `Assessment  List`
    }
  }
];