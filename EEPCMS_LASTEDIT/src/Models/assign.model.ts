
export interface Assign {
    
    id: number;
    patientId:number,
    patientCardNumber: string;
    patientAge: number;
    patientFirstName: string;
    patientLastName: string;
    weight:string;
    doctorId:number;
    doctorName: string;
    assignedDate: Date;
    systolicPressure:number;
    diastolicPressure:number;
    respiratoryRate:number;
    pulseRate:number;
    temperature:number;
    spO2:number;
    triage:string;

    respiratoryRateCategory: string;
    pulseRateCategory: string;
    bloodPressureCategory:string;
    temperatureCategory:string;
    spO2Category:string;
  }
  
