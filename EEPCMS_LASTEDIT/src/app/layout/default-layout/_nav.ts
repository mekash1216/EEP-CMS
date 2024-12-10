import { INavData } from '@coreui/angular';

export const navItems: INavData[] = [
  {
    name: 'Dashboard',
    url: '/dashboard',
    iconComponent: { name: 'cil-speedometer' },
    badge: {
      color: 'info',
      text: 'NEW'
    },
    roles: ["Admin", "manager", "lab", "doctor", "reception", "preliminary", "StockMan"],
  },
  {
    name: 'User Account',
    url: 'userlist',
    iconComponent: { name: 'cil-people' },
    roles: ["Admin"],
  },
  {
    name: 'Request',
    url: 'requestlist',
    iconComponent: { name: 'cil-task' },
    roles: ["manager"],
  },
  {
    name: 'Insurance',
    url: 'Accident',
    iconComponent: { name: 'cil-user' },
    roles: ["manager", "Admin"],
  },
  {
    name: 'Sick Leave',
    url: 'Sickleave',
    iconComponent: { name: 'cil-user' }, 
    roles: ["manager", "doctor", "Admin"],
  },
  {
    name: 'New Patient',
    url: 'patientlist',
    iconComponent: { name: 'cil-user-follow' },
    roles: ["reception"],
  },
  {
    name: 'Assigned Patient',
    url: 'doctor',
    iconComponent: { name: 'cil-user-follow' },
    roles: ["preliminary"],
  },
  {
    name: 'Patient List',
    url: 'examiner',
    iconComponent: { name: 'cil-list' },
    roles: ["doctor"],
  },
  {
    name: 'History                                                                                                                                                                Result',
    url: 'examinationlist',
    iconComponent: { name: 'cil-file' },
    roles: ["doctor"],
  },
  {
    name: 'Physical Examination',
    url: 'Assessment',
    iconComponent: { name: 'cil-user' }, 
    roles: ["doctor"],
  },
  {
    name: 'Appointment',
    url: 'appointment',
    iconComponent: { name: 'cil-calendar' },
    roles: ["doctor"],
  },
  {
    name: 'Prescription',
    url: '/Prescription',
    iconComponent: { name: 'cil-notes' }, 
    roles: ["StockMan", "doctor"],
  },
  {
    name: 'Laboratory',
    url: '/labrequest/:id',
    iconComponent: { name: 'cil-flask' },
    roles: ["lab"],
  },
  {
    name: 'Lab Request',
    url: '/lablist',
    iconComponent: { name: 'cil-user' }, 
    roles: ["lab", "doctor"],
  },
  {
    name: 'Lab Result',
    url: '/labresult',
    iconComponent: { name: 'cil-chart' },
    roles: ["lab", "doctor"],
  },
  {
    name: 'Stock',
    url: '/stocklist',
    iconComponent: { name: 'cil-box' },
    roles: ["StockMan"],
  },
  {
    name: 'Referral',
    url: '/referral',
    iconComponent: { name: 'cil-envelope-closed' },
    roles: ["doctor", "reception"],
  }
];

export function GetNavItemsByRole(role: string) {
  return navItems.filter(navItem => navItem.roles.includes(role));
}
