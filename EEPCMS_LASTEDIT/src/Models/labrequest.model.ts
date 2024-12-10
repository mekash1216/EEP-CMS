// src/Models/labrequest.model.ts

export interface Biochemistry {
  id: number;
  fbs: boolean;
  rbs: boolean;
  bun: boolean;
  creatinine: boolean;
  sgot: boolean;
  sgpt: boolean;
  alkaPho: boolean;
  tBilirubin: boolean;
  dBilirubin: boolean;
  albumin: boolean;
  uricAcid: boolean;
  tProtein: boolean;
  tCholesterol: boolean;
  hdl: boolean;
  ldl: boolean;
  triglyceride: boolean;
  amylase: boolean;
  ggt: boolean;
  lipase: boolean;
  ldh: boolean;
}

export interface Bacteriology {
  id: number;
  afb: boolean;
  wetSmear: boolean;
  gramStrin: boolean;
  koh: boolean;
}

export interface Serology {
  id: number;
  vdrl: boolean;
  widal: boolean;
  weilFelix: boolean;
  bloodGroup: boolean;
  xMatch: boolean;
  hBsAg: boolean;
  hBsAb: boolean;
  hPyloriAb: boolean;
  hPyloriAgStool: boolean;
  rf: boolean;
  aso: boolean;
  crp: boolean;
  hiv: boolean;
  fob: boolean;
  hcg: boolean;
  hepatitisCViralLoad: boolean;
  hepatitisBViralLoad: boolean;
}

export interface Electrolyte {
  id: number;
  sodium: boolean;
  potassium: boolean;
  chloride: boolean;
  calcium: boolean;
  magnesium: boolean;
  phosphorus: boolean;
}

export interface CancerMarker {
  id: number;
  cA125: boolean;
  cA199: boolean;
  cA153: boolean;
  cea: boolean;
  afp: boolean;
}

export interface CardiacMarker {
  id: number;
  ckmb: boolean;
  troponinT: boolean;
  dDimer: boolean;
}

export interface Coagulation {
  id: number;
  pt: boolean;
  aptt: boolean;
  inr: boolean;
}

export interface Hormone {
  id: number;
  tsh: boolean;
  freeT4: boolean;
  freeT3: boolean;
  totalT4: boolean;
  totalT3: boolean;
  totalBetaHCGT3: boolean;
  psa: boolean;
  fsh: boolean;
  lh: boolean;
  prolactin: boolean;
}

export interface OtherLab {
  id: number;
  vitB12e: boolean;
  vitD: boolean;
}
export interface Hematology {
  id: number;
  cbc: boolean;
  hgb: boolean;
  esr: boolean;
  bloodGroup: boolean;
  hba1c: boolean;
  bloodFilm: boolean;
  peripheralMorphology: boolean;
  malariaByAgCard: boolean;
}
export interface Parasitology {
  id: number;
  stoolDirect: boolean;
  urinalysis: boolean;
}


export interface LaboratoryRequest {
  id: number;
  dateOfRequest: string;
  isUrinalysisChecked: boolean;
  patientId: number;
  hospitalName:string;
  isParasitologyChecked: boolean;
  parasitology?:Parasitology;
  isHematologyChecked: boolean;
  hematology?:Hematology;
  biochemistry?: Biochemistry;
  isBiochemistryChecked: boolean;
  bacteriology?: Bacteriology;
  isBacteriologyChecked: boolean;
  serology?: Serology;
  isSerologyChecked: boolean;
  isElectrolyteChecked: boolean;
  electrolyte?: Electrolyte;
  isCancerMarkerChecked: boolean;
  cancerMarker?: CancerMarker;
  isCardiacMarkerChecked: boolean;
  cardiacMarker?: CardiacMarker;
  isCoagulationChecked: boolean;
  coagulation?: Coagulation;
  isHormoneChecked: boolean;
  hormone?: Hormone;
  isOtherLabChecked: boolean;
  otherLab?: OtherLab;

}

export interface LaboratoryTestResult {
  Id: number;
  Category: string;
  Test: string;
  Result: string;
  Gender: string;
  PatientId: number;
  age:number;
  isPregnant:boolean;
  phase:string;

}

