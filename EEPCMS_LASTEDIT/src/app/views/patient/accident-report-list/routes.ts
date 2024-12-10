import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./accident-report-list.component').then(m => m.AccidentReportListComponent),
    data: {
      title: `User List`
    }
  }
];