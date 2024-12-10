import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Assignment } from 'src/Models/assignment.model'; 
import { Assign } from 'src/Models/assign.model'; 
import { environment } from 'src/environment';

@Injectable({
  providedIn: 'root'
})
export class AssignmentService {
  
  private apiUrl = `${environment.apiBaseUrl}/Assignment`; 
  
  constructor(private http: HttpClient) { }
  
  createAssignment(assignment: Assignment): Observable<any> {
    return this.http.post<any>(this.apiUrl, assignment);
  }
  
  getAssignments(): Observable<Assign[]> {
    return this.http.get<Assign[]>(this.apiUrl);
  }
  
  deleteAssignment(id: number): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/${id}`);
  }
  
  updateAssign(assignments: Assign): Observable<Assign> {
    return this.http.put<Assign>(`${this.apiUrl}/${assignments.id}`, assignments, {
      headers: { 'Content-Type': 'application/json-patch+json' }
    });
  }
}
