import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./assessmentform.component').then(m => m.AssessmentformComponent),
    data: {
      title: `Assessment  Form`
    }
  }
];