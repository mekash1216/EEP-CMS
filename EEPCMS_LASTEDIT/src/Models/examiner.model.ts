export interface Examiner{
    id: string;
    patientId: number;
    patientCardNumber: string;
    patientAge: string;
    patientFirstName: string;
    patientLastName: string;
    sex?: string; 
    weight: number;
    isInpatient?: boolean; 
    isOutpatient?: boolean;
    isEmergency?: boolean; 
    pressure: string;
    doctorName: string;
    assignedDate:Date;
}

export interface Examinerpage{
    id: number;
    patientId: number;
    doctorId: number;
    assignmentId: number;
    regExaminerId:number;
    assignedDate:Date;
    }
   
    export interface PatientAppointment {
        id: number;
        examinerId:string;
        appointmentDateTime: Date;
        reason:string;
        // patientId: string;
        // regExaminerId: string;
        //appointmentTime: string;
       // reason: string;
      }
      export interface AppointmentList {
        id: number;
        appointmentDateTime: Date;
        patientCardNumber: number;
        patientFirstName: string;
        patientLastName: string;
        reason:string;
      }
      
      
export interface Referral {
  id?: number;
    referralDate: Date; 
    investigationResult: string;
    reason: string;
    rxgiven: string;
    diagnosis: string;
    from: string;
    clinicalfinding: string;
    to: string;
    examinerId: number; 

    
  }



export interface DisplayReferral {
  id: number;
  referralDate: string;
  investigationResult: string;
  Reason: string;
  Rxgiven: string;
  Diagnosis: string;
  From: string;
  Clinicalfinding: string;
  To: string;
  examiner: Examiner;
  
}


export interface PhysicalExamination {
  id: number;
  patientId: number;
  generalAppearance: string;
  vitalSigns: string;
  headAndNeck: string;
  cardiovascular: string;
  respiratory: string;
  abdomen: string;
  examinationDate: Date;
}


export interface PhysicalAssessment {
  id: number;
  patientId: number;
  heent: string;
  glands: string;
  chest: string;
  cvs: string;
  abdomen: string;
  genitoUninary: string;
  musculoSkeleton: string;
  skin: string;
  cns: string;
  date: Date;
  assessment:string;
  
}

export interface LaboratoryTestResult {
  id: number;              
  patientId: number;       
  category: string;      
  test: string;         
  result: string;         
  referenceRange: string;  
  units: string;          
  gender: string;          
  severity: string;      
  age: number;  
  testDate: Date;           
}

export interface Assessment {
  code:string; 
  shortDescription: string;
}
export interface SpecialLaboratoryTestResult {
  id: number;
  patientId: number;
  category: string;
  test: string;
  color: string;
  consistency: string;
  selectionone: string;
  cellCount: string;
}
