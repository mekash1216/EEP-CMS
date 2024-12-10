import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Doctor, SecondDoctor } from 'src/Models/doctor.model'; 
import { LaboratoryRequest } from 'src/Models/labrequest.model';
import { environment } from 'src/environment';

@Injectable({
  providedIn: 'root'
})
export class DoctorService {

  private baseUrl = `${environment.apiBaseUrl}`; // Common base URL

  constructor(private http: HttpClient) { }

  getDoctors(): Observable<Doctor[]> {
    return this.http.get<Doctor[]>(`${this.baseUrl}/Doctor`);
  }

  getseconddoctor(): Observable<SecondDoctor[]> {
    return this.http.get<SecondDoctor[]>(`${this.baseUrl}/RegExaminer`);
  }
  
  createAssignment(assignment: SecondDoctor): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/RegExaminer`, assignment);
  }

  getRequestById(id: number): Observable<LaboratoryRequest> {
    console.log(`Fetching request with ID: ${id}`);
    return this.http.get<LaboratoryRequest>(`${this.baseUrl}/LaboratoryRequests/${id}`);
  }

  updateRequest(id: number, updatedRequest: LaboratoryRequest): Observable<any> {
    console.log(`Updating request with ID: ${id}`);
    console.log('Request data:', updatedRequest);
    return this.http.put(`${this.baseUrl}/LaboratoryRequests/${id}`, updatedRequest, {
      headers: { 'Content-Type': 'application/json' }
    });
  }

  getAllRequests(): Observable<LaboratoryRequest[]> {
    return this.http.get<LaboratoryRequest[]>(`${this.baseUrl}/LaboratoryRequests`);
  }

  createRequest(request: LaboratoryRequest): Observable<LaboratoryRequest> {
    return this.http.post<LaboratoryRequest>(`${this.baseUrl}/LaboratoryRequests`, request);
  }

  deleteRequest(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/LaboratoryRequests/${id}`);
  }
}
