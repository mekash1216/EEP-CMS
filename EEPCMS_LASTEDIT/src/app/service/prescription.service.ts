import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { Prescription, PrescriptionItem } from '../../Models/prescription.model';
import { AppointmentList, Referral, PhysicalExamination, PatientAppointment,
   LaboratoryTestResult, SpecialLaboratoryTestResult } from '../../Models/examiner.model';
import { environment } from '../../environment';

@Injectable({
  providedIn: 'root'
})
export class PrescriptionService {
  private baseUrl = `${environment.apiBaseUrl}`; 

  private selectedReferralSubject: BehaviorSubject<any> = new BehaviorSubject<any>(null);

  constructor(private http: HttpClient) {}

  // Prescriptions API Endpoints
  getAllPrescriptions(): Observable<Prescription[]> {
    return this.http.get<Prescription[]>(`${this.baseUrl}/Prescriptions`);
  }

  getPrescriptionById(id: string): Observable<Prescription> {
    return this.http.get<Prescription>(`${this.baseUrl}/Prescriptions/${id}`);
  }

  createPrescription(prescription: Prescription): Observable<any> {
    return this.http.post(`${this.baseUrl}/Prescriptions`, prescription);
  }

  updatePrescription(id: string, prescription: Prescription): Observable<any> {
    return this.http.put(`${this.baseUrl}/Prescriptions/${id}`, prescription);
  }

  deletePrescription(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/Prescriptions/${id}`);
  }

  // PrescriptionItems API Endpoints
  createPrescriptionItem(prescriptionItem: PrescriptionItem): Observable<any> {
    return this.http.post(`${this.baseUrl}/PrescriptionItems`, prescriptionItem);
  }

  getPrescriptionItemById(id: string): Observable<PrescriptionItem> {
    return this.http.get<PrescriptionItem>(`${this.baseUrl}/PrescriptionItems/${id}`);
  }

  deletePrescriptionItem(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/PrescriptionItems/${id}`);
  }

  getAllPrescriptionItems(): Observable<any> {
    return this.http.get(`${this.baseUrl}/PrescriptionItems`);
  }

  approvePrescriptionItem(id: string): Observable<any> {
    return this.http.post(`${this.baseUrl}/PrescriptionItems/approve/${id}`, {});
  }

  getApprovePrescriptions(): Observable<PrescriptionItem[]> {
    return this.http.get<PrescriptionItem[]>(`${this.baseUrl}/PrescriptionItems/approved`);
  }

  unapprovePrescriptionItem(id: string): Observable<any> {
    return this.http.delete<any>(`${this.baseUrl}/PrescriptionItems/unapprove/${id}`);
  }

  createAppointment(appointment: PatientAppointment): Observable<PatientAppointment> {
    return this.http.post<PatientAppointment>(`${this.baseUrl}/Appointment`, appointment);
  }

  getAppointments(): Observable<AppointmentList[]> {
    return this.http.get<AppointmentList[]>(`${this.baseUrl}/Appointment`);
  }

  getReferrals(): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/Referral`);
  }

  getReferral(id: number): Observable<Referral> {
    return this.http.get<Referral>(`${this.baseUrl}/Referral/${id}`);
  }

  createReferral(referral: Referral): Observable<Referral> {
    return this.http.post<Referral>(`${this.baseUrl}/Referral`, referral);
  }

  updateReferral(referral: Referral): Observable<Referral> {
    return this.http.put<Referral>(`${this.baseUrl}/Referral/${referral.id}`, referral);
  }

  deleteReferral(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/Referral/${id}`);
  }

  setSelectedReferral(referral: any) {
    this.selectedReferralSubject.next(referral);
  }

  getSelectedReferral(): Observable<any> {
    return this.selectedReferralSubject.asObservable();
  }

  getReferralsByPatient(patientId: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/Referral/ByPatient/${patientId}`);
  }

  getAppointmentsByPatient(patientId: number): Observable<AppointmentList[]> {
    return this.http.get<AppointmentList[]>(`${this.baseUrl}/Appointment/Appointments/ByPatient/${patientId}`);
  }

  getPrescriptionsByPatient(patientId: number): Observable<Prescription[]> {
    return this.http.get<Prescription[]>(`${this.baseUrl}/Prescriptions/ByPatient/${patientId}`);
  }

  getExaminationsByPatientId(patientId: number): Observable<PhysicalExamination[]> {
    return this.http.get<PhysicalExamination[]>(`${this.baseUrl}/PhysicalExaminations/byPatient/${patientId}`);
  }

  addPhysicalExamination(examination: PhysicalExamination): Observable<PhysicalExamination> {
    return this.http.post<PhysicalExamination>(`${this.baseUrl}/PhysicalExaminations`, examination);
  }

  getAllExaminations(): Observable<PhysicalExamination[]> {
    return this.http.get<PhysicalExamination[]>(`${this.baseUrl}/PhysicalExaminations`);
  }

  getExaminationById(id: number): Observable<PhysicalExamination> {
    return this.http.get<PhysicalExamination>(`${this.baseUrl}/PhysicalExaminations/${id}`);
  }

  updateExamination(id: number, examination: PhysicalExamination): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/PhysicalExaminations/${id}`, examination);
  }

  deleteExamination(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/PhysicalExaminations/${id}`);
  }
  getLabResultsByPatientId(patientId: number): Observable<LaboratoryTestResult[]> {
    const url = `${this.baseUrl}/LaboratoryTestResult/results/patient/${patientId}`; 
    return this.http.get<LaboratoryTestResult[]>(url);
   
  }
  getSpecialTestsByPatient(patientId: number): Observable<SpecialLaboratoryTestResult[]> {
    return this.http.get<SpecialLaboratoryTestResult[]>(`${this.baseUrl}/LabBloodFilmTest/patient/${patientId}`);
  }
}
