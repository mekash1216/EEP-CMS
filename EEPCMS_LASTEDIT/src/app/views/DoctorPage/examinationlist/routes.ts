import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./examinationlist.component').then(m => m.ExaminationlistComponent),
    data: {
      title: `Appointemnt  List`
    }
  }
];