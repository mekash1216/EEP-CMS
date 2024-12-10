import { Patient } from 'src/Models/patient.model';
export interface SickLeave {
    id: number;
    startDate: string;
    endDate: string;
    reason: string;
    isExpired: boolean;
    patientId: number;
    patient: Patient;
  }