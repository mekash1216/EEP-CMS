import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./appointmentlist.component').then(m => m.AppointmentlistComponent),
    data: {
      title: `Appointemnt  List`
    }
  }
];