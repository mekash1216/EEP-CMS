export interface ClinicRequest {
    id: number;
    employeeId: string; 
    patientFirstName: string;
    patientLastName: string;
    patientDepartment: string;
    gender?: string;
    phoneNumber?: string;
    email?: string;
    workplace?: string;
    isManager?: boolean;
    position?: string;
    birthdate?: string;
    requestDate: Date;
    age:number;
    status?: 'Pending' | 'Accepted' | 'Rejected'; 
  }
  