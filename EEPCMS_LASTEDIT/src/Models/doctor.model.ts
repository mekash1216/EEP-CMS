import { Patient } from 'src/Models/patient.model';
export interface Doctor{
    id: string;
    name: string;
    assignedPatients: Patient[];
}

export interface SecondDoctor{
    id: string;
    title: string;
  firstName: string;
  lastName: string;
  specialization: string;
}