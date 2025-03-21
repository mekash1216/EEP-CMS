import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Examiner, Examinerpage } from '../../Models/examiner.model';
import { environment } from '../../environment';

@Injectable({
  providedIn: 'root'
})
export class ExaminerService {
  private apiUrl = `${environment.apiBaseUrl}/Examiner`; 

  constructor(private http: HttpClient) {}

  getExaminers(): Observable<Examiner[]> {
    return this.http.get<Examiner[]>(this.apiUrl);
  }

  createAssignment(assignment: Examinerpage): Observable<any> {
    return this.http.post<any>(this.apiUrl, assignment);
  }

  deleteExamine(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  getExaminerById(examinerId: string): Observable<Examiner> {
    return this.http.get<Examiner>(`${this.apiUrl}/${examinerId}`);
  }
}
