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
    roles: ["Admin","manager"],
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
    roles: ["Admin","reception"],
  },
  {
    name: 'Assigned Patient',
    url: 'doctor',
    iconComponent: { name: 'cil-user-follow' },
    roles: ["Admin","preliminary"],
  },
  {
    name: 'Patient List',
    url: 'examiner',
    iconComponent: { name: 'cil-list' },
    roles: ["Admin","doctor"],
  },
  {
    name: 'History                                                                                                                                                                Result',
    url: 'examinationlist',
    iconComponent: { name: 'cil-file' },
    roles: ["Admin","doctor"],
  },
  {
    name: 'Physical Examination',
    url: 'Assessment',
    iconComponent: { name: 'cil-user' }, 
    roles: ["Admin","doctor"],
  },
  {
    name: 'Appointment',
    url: 'appointment',
    iconComponent: { name: 'cil-calendar' },
    roles: ["Admin","doctor"],
  },
  {
    name: 'Prescription',
    url: '/Prescription',
    iconComponent: { name: 'cil-notes' }, 
    roles: ["Admin","StockMan", "doctor"],
  },
  {
    name: 'Laboratory',
    url: '/labrequest/:id',
    iconComponent: { name: 'cil-flask' },
    roles: ["Admin","lab"],
  },
  {
    name: 'Lab Request',
    url: '/lablist',
    iconComponent: { name: 'cil-user' }, 
    roles: ["Admin","lab", "doctor"],
  },
  {
    name: 'Lab Result',
    url: '/labresult',
    iconComponent: { name: 'cil-chart' },
    roles: ["Admin","lab", "doctor"],
  },
  {
    name: 'Stock',
    url: '/stocklist',
    iconComponent: { name: 'cil-box' },
    roles: ["Admin","StockMan"],
  },
  {
    name: 'Referral',
    url: '/referral',
    iconComponent: { name: 'cil-envelope-closed' },
    roles: ["Admin","doctor", "reception"],
  }
];

export function GetNavItemsByRole(role: string) {
  return navItems.filter(navItem => navItem.roles.includes(role));
}
