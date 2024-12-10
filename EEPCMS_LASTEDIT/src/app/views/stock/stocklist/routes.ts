import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./stocklist.component').then(m => m.StockListComponent),
    data: {
      title: `Stock List`
    }
  }
];