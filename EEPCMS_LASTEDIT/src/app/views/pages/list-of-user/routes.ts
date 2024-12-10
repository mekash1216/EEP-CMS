import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./list-of-user.component').then(m => m.ListOfUserComponent),
    data: {
      title: `User List`
    }
  }
];
