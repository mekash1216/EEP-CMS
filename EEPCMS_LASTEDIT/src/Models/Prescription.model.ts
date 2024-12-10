export interface Prescription {
//prescriptionDate: string|number|Date;

    get(arg0: string): unknown;

    id: number;
    cardNumber: string;
    prescriptiondate: Date;
    nameOfPatient: string;
    age: number;
    sex: string;
    weight: number;
    isInpatient: boolean;
    isOutpatient: boolean;
    isEmergency: boolean;
    diagnosis: string;
    isApproved: boolean;
    prescriptionItems?: PrescriptionItem[
    
    ];
  }
  export interface PrescriptionItem {
    id: string;
    prescriptionId: string;
    stockId: string;
    quantity: number;
    isApproved:boolean;
    stockAvailable: number;
    isInternal: boolean;
  }
  export interface PatientReport {
    patientCardNo: string;
    date: string;
    gender: string;
    diagnosis: string;
  }