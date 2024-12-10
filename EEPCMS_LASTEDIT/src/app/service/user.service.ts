import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { BehaviorSubject, Observable, throwError, of, forkJoin } from 'rxjs';
import { LoginRequest } from 'src/models/login-request.model';
import { LoginResponse } from 'src/models/login-response.model';
import { environment } from 'src/environment';
import { ChangePassword, User, User2 } from 'src/Models/user.model';
import { AccidentReport, AccidentReportCreate, Patient } from 'src/Models/patient.model';
import { ClinicRequest } from 'src/Models/ClinicRequest.model';
import { Doctor } from 'src/Models/doctor.model';
import { Employee } from 'src/Models/employee.model';
import { SickLeave } from 'src/Models/SickLeave.model';
import { Assignment } from 'src/Models/assignment.model';
import { Assessment, PhysicalAssessment } from 'src/Models/examiner.model';
import { ToastrService } from 'ngx-toastr';
import { tap, catchError, map } from 'rxjs/operators';
import { Router } from '@angular/router';
import { PatientReport } from 'src/Models/prescription.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private currentUserSubject: BehaviorSubject<User | null> = new BehaviorSubject<User | null>(null);
  public currentUser: Observable<User | null> = this.currentUserSubject.asObservable();
  public loading = false; 

  constructor(private http: HttpClient, private toastr: ToastrService, private router: Router) {
    const token = localStorage.getItem('token');
    const email = localStorage.getItem('user-email');
    const roles = localStorage.getItem('user-roles')?.split(',') || [];
    const firstName = localStorage.getItem('user-firstName') ?? undefined;  
  const lastName = localStorage.getItem('user-lastName') ?? undefined;

    if (token && email) {
      this.currentUserSubject.next({ email, roles,    firstName,
        lastName });
    }
  }

  setUser(user: User): void {
    const roles = user.roles || [];
    this.currentUserSubject.next(user);
    localStorage.setItem('user-email', user.email);
    localStorage.setItem('user-roles', roles.join(','));

    if (user.firstName) localStorage.setItem('user-firstName', user.firstName);
  if (user.lastName) localStorage.setItem('user-lastName', user.lastName);
  }

  login(model: LoginRequest): Observable<LoginResponse> {
    this.loading = true; 
    return this.http.post<LoginResponse>(`${environment.apiBaseUrl}/Auth/login`, model).pipe(
      tap(response => {
        this.loading = false; 
        if (response && response.token) {
          localStorage.setItem('token', response.token);
          this.setUser({
            email: response.email,
            roles: response.roles || [],
            firstName: response.firstName,
          lastName: response.lastName
          });
        } else {
          this.toastr.error('Login failed. Please check your credentials.');
        }
      }),
      catchError(error => {
        this.loading = false; 
        this.toastr.error('An error occurred during login.');
        return of({ token: '', email: '', roles: [], isActive: false });
      })
    );
  }

  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('user-email');
    localStorage.removeItem('user-roles');
    this.currentUserSubject.next(null);
    this.router.navigate(['/login']);
  }

  private auth = `${environment.apiBaseUrl}/Auth`;
  
  getAllUsers(): Observable<any> {
    return this.http.get(`${this.auth}/profile`);
  }

  updateUserStatus(userId: string, isActive: boolean): Observable<void> {
    return this.http.post<void>(`${this.auth}/update-active-status/${userId}`, { isActive });
  }

  registerloginUser(user: any) {
    return this.http.post(`${this.auth}/register`, user, { responseType: 'text' });
  }

  deletelogUser(email: string): Observable<any> {
    return this.http.delete(`${this.auth}/delete/${email}`, { responseType: 'text' });
  }

  changePassword(changePasswordDto: ChangePassword): Observable<any> {
    return this.http.post<any>(`${this.auth}/change-password`, changePasswordDto).pipe(
      catchError(error => {
        console.error('Error during password change:', error);
        return throwError(error);
      })
    );
  }

  updateProfile(id: string, userData: User2): Observable<any> {
    return this.http.post(`${this.auth}/update-profile/${id}`, userData, {
      headers: { 'Content-Type': 'application/json-patch+json' }
    });
  }

  private apiUrl2 = `${environment.apiBaseUrl}/Patient`;

  getPatients(): Observable<Patient[]> {
    return this.http.get<Patient[]>(this.apiUrl2);
  }

  getPatientBiId(id: number): Observable<Patient> {
    return this.http.get<Patient>(`${this.apiUrl2}/${id}`);
  }

  addPatient(patient: Patient): Observable<Patient> {
    return this.http.post<Patient>(this.apiUrl2, patient);
  }

  editPatient(id: string, patient: Patient): Observable<Patient> {
    return this.http.put<Patient>(`${this.apiUrl2}/${id}`, patient);
  }

  updatePatient(patient: Patient): Observable<Patient> {
    return this.http.put<Patient>(`${this.apiUrl2}/${patient.id}`, patient);
  }

  deletePatient(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl2}/${id}`);
  }

  getPatientsByYear(year: number): Observable<Patient[]> {
    return this.http.get<Patient[]>(`${this.apiUrl2}/yearly/${year}`);
  }

  getPatientsByMonth(year: number, month: number): Observable<Patient[]> {
    return this.http.get<Patient[]>(`${this.apiUrl2}/monthly/${year}/${month}`);
  }

  getPatientsByDay(date: string): Observable<Patient[]> {
    return this.http.get<Patient[]>(`${this.apiUrl2}/daily/${date}`);
  }

  getPatientsByWeek(startDate: Date, endDate: Date): Observable<Patient[]> {
    const dates = this.getDatesBetween(startDate, endDate);
    const patientObservables = dates.map(date => this.getPatientsByDay(this.formatDate(date)));
  
    return forkJoin(patientObservables).pipe(
      map((patientArrays: Patient[][]) => patientArrays.flat()) 
    );
  }
  

  private getDatesBetween(startDate: Date, endDate: Date): Date[] {
    const dates: Date[] = [];
    let currentDate = new Date(startDate);

    while (currentDate <= endDate) {
      dates.push(new Date(currentDate));
      currentDate.setDate(currentDate.getDate() + 1);
    }

    return dates;
  }

  private formatDate(date: Date): string {
    return date.toISOString().split('T')[0]; // Format as YYYY-MM-DD
  }
  private apiUrl4 = `${environment.apiBaseUrl}/Doctor`;

  getDoctors(): Observable<Doctor[]> {
    return this.http.get<Doctor[]>(this.apiUrl4);
  }

  assignDoctorToPatient(assignment: any): Observable<any> {
    return this.http.post<any>(`${this.apiUrl4}/assign`, assignment);
  }

  private apiUrl3 = `${environment.apiBaseUrl}/Patient/assignDoctor`;

  registerAssignment(assignment: Assignment): Observable<any> {
    const payload = {
      patientId: assignment.patientId,
    };
    return this.http.post<any>(this.apiUrl3, payload);
  }

  getEmployeeById(employeeId: string): Observable<Employee> {
    return this.http.get<Employee>(`${environment.apiBaseUrl}/Employees/${employeeId}`);
  }

  private request1 = `${environment.apiBaseUrl}/ClinicRequests`;

  getAll(): Observable<ClinicRequest[]> {
    return this.http.get<ClinicRequest[]>(this.request1);
  }

  getById(id: string): Observable<ClinicRequest> {
    return this.http.get<ClinicRequest>(`${this.request1}/${id}`);
  }

  create(request: ClinicRequest): Observable<ClinicRequest> {
    return this.http.post<ClinicRequest>(this.request1, request);
  }

  update(id: number, request: ClinicRequest): Observable<void> {
    return this.http.put<void>(`${this.request1}/${id}`, request);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.request1}/${id}`);
  }

  updateStatus(id: number, status: 'Accepted' | 'Rejected'): Observable<void> {
    return this.http.patch<void>(`${this.request1}/${id}/status`, { status });
  }

  assignPatient(requestId: number): Observable<void> {
    return this.http.post<void>(`${this.request1}/assign-patient/${requestId}`, {});
  }

  approveRequestByCardNo(cardNo: string): Observable<any> {
    return this.http.post(`${this.request1}/approve-by-cardno`, JSON.stringify(cardNo), {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      }),
      responseType: 'text' as 'json'
    });
  }

  rejectRequestByCardNo(cardNo: string): Observable<any> {
    const payload = JSON.stringify(cardNo);
    return this.http.post(`${this.request1}/reject-by-cardno`, payload, {
      headers: { 'Content-Type': 'application/json' },
      responseType: 'text'
    });
  }

  private assess = `${environment.apiBaseUrl}/PhysicalAssessment`;

  getAllAssess(): Observable<PhysicalAssessment[]> {
    return this.http.get<PhysicalAssessment[]>(this.assess);
  }

  getByIdAssess(id: number): Observable<PhysicalAssessment> {
    return this.http.get<PhysicalAssessment>(`${this.assess}/${id}`);
  }

  createAssess(physicalAssessment: PhysicalAssessment): Observable<PhysicalAssessment> {
    return this.http.post<PhysicalAssessment>(this.assess, physicalAssessment);
  }

  updateAssess(id: number, physicalAssessment: PhysicalAssessment): Observable<void> {
    return this.http.put<void>(`${this.assess}/${id}`, physicalAssessment);
  }

  deleteAssess(id: number): Observable<void> {
    return this.http.delete<void>(`${this.assess}/${id}`);
  }

  getByPatientId(patientId: number): Observable<PhysicalAssessment[]> {
    return this.http.get<PhysicalAssessment[]>(`${this.assess}/byPatient/${patientId}`);
  }

  showSuccess(message: string, title: string) {
    this.toastr.success(message, title);
  }

  showError(message: string, title: string) {
    this.toastr.error(message, title);
  }

  showInfo(message: string, title: string) {
    this.toastr.info(message, title);
  }

  showWarning(message: string, title: string) {
    this.toastr.warning(message, title);
  }

  private sickleave = `${environment.apiBaseUrl}/sickleave`;

  getSickLeaves(): Observable<SickLeave[]> {
    return this.http.get<SickLeave[]>(this.sickleave);
  }

  getExpiredSickLeaves(): Observable<SickLeave[]> {
    return this.http.get<SickLeave[]>(`${this.sickleave}/expired`);
  }

  addSickLeave(sickLeaveData: any): Observable<any> {
    return this.http.post(this.sickleave, sickLeaveData);
  }

  deleteSickLeave(id: number): Observable<void> {
    return this.http.delete<void>(`${this.sickleave}/${id}`);
  }

  updateSickLeave(sickLeave: SickLeave): Observable<void> {
    return this.http.put<void>(`${this.sickleave}/${sickLeave.id}`, sickLeave, {
      headers: { 'Content-Type': 'application/json' }
    });
  }

  private Accident = `${environment.apiBaseUrl}/AccidentReport`;

  createAccidentReport(report: AccidentReportCreate): Observable<AccidentReport> {
    return this.http.post<AccidentReport>(this.Accident, report, {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    });
  }

  getAccidentReports(): Observable<AccidentReport[]> {
    return this.http.get<AccidentReport[]>(this.Accident);
  }

  getAccidentReportById(id: number): Observable<AccidentReport> {
    return this.http.get<AccidentReport>(`${this.Accident}/${id}`);
  }

  addAccidentReport(report: any): Observable<void> {
    return this.http.post<void>(this.Accident, report);
  }

  updateAccidentReport(id: number, report: AccidentReportCreate): Observable<AccidentReport> {
    return this.http.put<AccidentReport>(`${this.Accident}/${id}`, report, {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    });
  }

  deleteAccidentReport(id: number): Observable<void> {
    return this.http.delete<void>(`${this.Accident}/${id}`);
  }

  getActiveAppointments(): Observable<any[]> {
    return this.http.get<any[]>(`${environment.apiBaseUrl}/appointment/active`);
  }


  private ICD = `${environment.apiBaseUrl}/ICD10Codes2024`;


  getICD10Codes(searchTerm: string = '', pageNumber: number = 1, pageSize: number = 8000, sortOrder: string = 'asc'): Observable<Assessment[]> {
    const params = {
      searchTerm,
      pageNumber: pageNumber.toString(),
      pageSize: pageSize.toString(),
      sortOrder,
    };

    return this.http.get<Assessment[]>(this.ICD, { params });
  }

  private report = environment.apiBaseUrl;

  getPrescriptionReports(): Observable<PatientReport[]> {
    return this.http.get<PatientReport[]>(`${this.report}/Prescriptions/prescription-report`);
  }

}
