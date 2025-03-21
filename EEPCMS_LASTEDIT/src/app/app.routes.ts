import { RouterModule, Routes } from '@angular/router';
import { DefaultLayoutComponent } from './layout';
import { RoleGuard } from './auth/role.guard'; // Import the RoleGuard
import { LoginComponent } from './views/pages/login/login.component';
import { NgModule } from '@angular/core';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  },
  
  {
    path: '',
    component: DefaultLayoutComponent,
    canActivate: [RoleGuard],
    data: {
      expectedRoles: ['manager','lab','Admin','reception','StockMan','doctor','preliminary']
    },
    children: [
      {
        path: 'dashboard',
        loadChildren: () => import('./views/dashboard/routes').then((m) => m.routes),
        canActivate: [RoleGuard], 
        data: {
        
          expectedRoles: ['manager','lab','Admin','reception','StockMan','doctor','preliminary']
        }
      },


      {
        path: 'userlist',
        loadChildren: () => import('./views/pages/list-of-user/routes').then((m) => m.routes),
        canActivate: [RoleGuard],
        data: {
          expectedRoles: ['Admin'] 
        }
      },
      {
        path: 'patientlist',
        loadChildren: () => import('./views/patient/patientlist/routes').then((m) => m.routes),
        canActivate: [RoleGuard],
        data: {
          expectedRoles: ['reception','Admin'] 
        }
      },
      {
        path: 'doctor',
        loadChildren: () => import('./views/Doctor/doctor/routes').then((m) => m.routes),
        canActivate: [RoleGuard],
        data: {
          expectedRoles: ['preliminary','Admin']
        }
      },
      {
        path: 'examiner',
        loadChildren: () => import('./views/DoctorPage/listofpatient/routes').then((m) => m.routes),
        canActivate: [RoleGuard],
        data: {
          expectedRoles: ['doctor','Admin'] 
        }
      },

      {
        path: 'examinationlist',
        loadChildren: () => import('./views/DoctorPage/examinationlist/routes').then((m) => m.routes),
        canActivate: [RoleGuard],
        data: {
          expectedRoles: ['doctor','Admin']
        }
      },
      {
        path: 'appointment',
        loadChildren: () => import('./views/Appointment/appointmentlist/routes').then((m) => m.routes),
        canActivate: [RoleGuard],
        data: {
          expectedRoles: ['doctor','Admin'] 
        }
      },
      {
        path: 'Prescription',
        loadChildren: () => import('./views/prescription/routes').then((m) => m.routes),
        canActivate: [RoleGuard],
        data: {
          expectedRoles: ['doctor','StockMan','Admin'] 
        }
      },
      {
        path: 'labrequest/:patientId',
        loadChildren: () => import('./views/labrequest/routes').then((m) => m.routes),
        canActivate: [RoleGuard],
        data: {
          expectedRoles: ['doctor','lab','Admin'] 
        }
      },
      {
        path: 'labdetails/:id',
        loadChildren: () => import('./views/labdetails/routes').then((m) => m.routes),
        canActivate: [RoleGuard],
        data: {
          expectedRoles: ['lab','doctor','Admin'] 
        }
      },
      {
        path: 'lablist',
        loadChildren: () => import('./views/lablist/routes').then((m) => m.routes),
        canActivate: [RoleGuard],
        data: {
          expectedRoles: ['lab','doctor','Admin'] 
        }
      },
      {
        path: 'labresult',
        loadChildren: () => import('./views/labtestresult/routes').then((m) => m.routes),
        canActivate: [RoleGuard],
        data: {
          expectedRoles:  ['lab','doctor','Admin'] 
        }
      },
      {
        path: 'labresultdetail',
        loadChildren: () => import('./views/labtestresultdetail/routes').then((m) => m.routes),
        canActivate: [RoleGuard],
        data: {
          expectedRoles:  ['lab','doctor','Admin'] 
        }
      },
      {
        path: 'labresultform/:id',
        loadChildren: () => import('./views/labresultform/routes').then((m) => m.routes),
        canActivate: [RoleGuard],
        data: {
          expectedRoles: ['lab','doctor','Admin'] 
        }
      },
      {
        path: 'labedit/:id',
        loadChildren: () => import('./views/labedit/routes').then((m) => m.routes),
        canActivate: [RoleGuard],
        data: {
          expectedRoles:  ['doctor','Admin'] 
        }
      },
      {
        path: 'stocklist',
        loadChildren: () => import('./views/stock/stocklist/routes').then((m) => m.routes),
        canActivate: [RoleGuard],
        data: {
          expectedRoles: ['StockMan','Admin'] 
        }
      },
      {
        path: 'referral',
        loadChildren: () => import('./views/referral/routes').then((m) => m.routes),
        canActivate: [RoleGuard],
        data: {
          expectedRoles: ['doctor','reception','Admin'] 
        }
      },

      {
        path: 'external',
        loadChildren: () => import('./views/prescriptionprint/routes').then((m) => m.routes),
        canActivate: [RoleGuard],
        data: {
          expectedRoles: ['StockMan','Admin'] 
        }
      },
      {
        path: 'print-referral/:id',
        loadChildren: () => import('./views/print-referral/routes').then((m) => m.routes),
        canActivate: [RoleGuard],
        data: {
          expectedRoles: ['reception','doctor','Admin'] 
        }
      },
      {
        path: 'history/:id',
        loadChildren: () => import('./views/DoctorPage/patienthistory/routes').then((m) => m.routes),
        canActivate: [RoleGuard],
        data: {
          expectedRoles: ['doctor','Admin'] 
        }
      },
      {
        path: 'requestlist',
        loadChildren: () => import('./views/Request/requestlist/routes').then((m) => m.routes),
        canActivate: [RoleGuard],
        data: {
          expectedRoles: ['manager','Admin'] 
        }
      },
      {
        path: 'requestform/:id',
        loadChildren: () => import('./views/Request/requestform/routes').then((m) => m.routes),
        canActivate: [RoleGuard],
        data: {
          expectedRoles: ['manager','Admin'] 
        }
      },
      {
        path: 'Assessment',
        loadChildren: () => import('./views/DoctorPage/assessment/routes').then((m) => m.routes),
        canActivate: [RoleGuard],
        data: {
          expectedRoles: ['doctor','Admin'] 
        }
      },
      {
        path: 'Assessmentform/:patientId',
        loadChildren: () => import('./views/DoctorPage/assessmentform/routes').then((m)=>m.routes),
        canActivate: [RoleGuard],
        data: {
          expectedRoles: ['doctor','Admin'] 
        }
      },
      {
        path: 'Assessmentedit/:id',
        loadChildren: () => import('./views/DoctorPage/assessmentedit/routes').then((m)=>m.routes),
        canActivate: [RoleGuard],
        data: {
          expectedRoles: ['doctor','Admin'] 
        }
      },

      {
        path: 'Sickleave',
        loadChildren: () => import('./views/patient/sick-leave/routes').then((m)=>m.routes),
        canActivate: [RoleGuard],
        data: {
          expectedRoles: ['doctor','manager','Admin'] 
        }
      },

      {
        path: 'Accident',
        loadChildren: () => import('./views/patient/accident-report-list/routes').then((m)=>m.routes),
        canActivate: [RoleGuard],
        data: {
          expectedRoles: ['manager','Admin'] 
        }
      },
      {
        path: 'Listofuser',
        loadChildren: () => import('./views/pages/list-of-user/routes').then((m)=>m.routes),
        canActivate: [RoleGuard],
        data: {
          expectedRoles: ['Admin'] 
        }
      },
    ]
  },
  {
    path: '404',
    loadComponent: () => import('./views/pages/page404/page404.component').then(m => m.Page404Component),
    data: {
      title: 'Page 404'
    }
  },
  {
    path: '500',
    loadComponent: () => import('./views/pages/page500/page500.component').then(m => m.Page500Component),
    data: {
      title: 'Page 500'
    }
  },
  {
    path: 'login',
    loadComponent: () => import('./views/pages/login/login.component').then(m => m.LoginComponent),
    data: {
      title: 'Login Page'
    }
  },
  {
    path: 'register',
    loadComponent: () => import('./views/pages/register/register.component').then(m => m.RegisterComponent),
    data: {
      title: 'Register Page'
    }
  },
  {
    path: '**',
    redirectTo: 'login',
    pathMatch: 'full'
  },

  {
    path: 'login',
    loadComponent: () => import('./views/pages/login/login.component').then(m => m.LoginComponent),
    canActivate: [RoleGuard],  
    data: {
      title: 'Login Page'
    }
  },
  
];
