
import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    data: {
      title: 'Patient'
    },
    children: [
      {
        path: '',
        redirectTo: 'pretesting',
        pathMatch: 'full'
      },
    
      {
        path: 'testedpatient',
        loadComponent: () => import('./testedpatient/testedpatient.component').then(m => m.TestedpatientComponent),
        data: {
          title: 'userlist'
        }
      },
      {
        path: 'pretesting',
        loadComponent: () => import('./pretesting/pretesting.component').then(m => m.PretestingComponent),
        data: {
          title: 'createaccount'
        }
      }
      
    ]
  }
];


