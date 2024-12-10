export interface Patient {
  id?: number; 
  cardNo: string;
  firstName: string;
  lastName: string;
  gender: string;
  dateOfBirth: Date;
  age: number;
  phoneNumber: string;
  requestId?: number;
  requestStatus?: string;
  patientDepartment: string;
  email: string;
  workplace: string;
  position: string;
  assigneddate: string;
}


export interface Witness {
  id: number;
  firstName: string;
  lastName: string;
}

export interface AccidentReport {
  id: number;
  patientId: number;
  patient: Patient;
  department: string;
  injuryDate: string;
  injuryTime: string;
  causeOfInjury: string;
  incidentDetails: string;
  preventiveAction: string;
  reporterName: string;
  departmentHeadName: string;
  witnesses: Witness[];
}

export interface AccidentReportCreate {
  patientId: number;
  department: string;
  injuryDate: string;
  injuryTime: string;
  causeOfInjury: string;
  incidentDetails: string;
  preventiveAction: string;
  reporterName: string;
  departmentHeadName: string;
  witnesses: Witness[];
}
