import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms'; // Import FormsModule here
import { Router } from '@angular/router';
import { LaboratoryRequest } from 'src/Models/labrequest.model';
import { UserService } from 'src/app/service/user.service';
import { Patient } from 'src/Models/patient.model';
@Component({
  selector: 'app-lablist',
  standalone: true,
  imports: [CommonModule, FormsModule], // Add FormsModule to imports
  templateUrl: './lablist.component.html',
  styleUrls: ['./lablist.component.scss']
})
export class LablistComponent implements OnInit {
  laboratoryRequests: LaboratoryRequest[] = [];
  filteredRequests: LaboratoryRequest[] = [];
  paginatedRequests: LaboratoryRequest[] = [];
  searchTerm: string = '';
  pageSize: number = 5;
  currentPage: number = 1;
  pageSizes: number[] = [5, 10, 20];
  totalPages: number = 1;
  pageNumbers: number[] = [];
  patients:Patient[]=[];

  constructor(private http: HttpClient, private router: Router,private patientService:UserService) {}

  ngOnInit(): void {
    this.fetchLaboratoryRequests();
    this.fetchAllPatients();
  }
  fetchAllPatients(): void {
    this.patientService.getPatients().subscribe(patients => {
      this.patients = patients; // Store patients as an array
      this.filterRequests(); // Refresh requests to update patient data
    });
  }
  fetchLaboratoryRequests(): void {
    this.http.get<LaboratoryRequest[]>('https://localhost:7292/api/LaboratoryRequests')
      .subscribe(data => {
        this.laboratoryRequests = data;
        this.filteredRequests = data;
        this.paginateRequests();
      });
  }

  filterRequests(): void {
    this.filteredRequests = this.laboratoryRequests.filter(request =>
      request.id.toString().includes(this.searchTerm) ||
      request.dateOfRequest.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
      request.patientId.toString().includes(this.searchTerm)
    );
    this.paginateRequests();
  }

  paginateRequests(): void {
    const start = (this.currentPage - 1) * this.pageSize;
    const end = start + this.pageSize;
    this.paginatedRequests = this.filteredRequests.slice(start, end);

    this.totalPages = Math.ceil(this.filteredRequests.length / this.pageSize);
    this.pageNumbers = Array.from({ length: this.totalPages }, (_, i) => i + 1);
  }

  changePage(page: number): void {
    if (page < 1 || page > this.totalPages) return;
    this.currentPage = page;
    this.paginateRequests();
  }

  viewDetails(id: number): void {
    this.router.navigate(['/labdetails', id]);
  }

  deleteRequest(id: number): void {
    if (confirm('Are you sure you want to delete this request?')) {
      this.http.delete(`https://localhost:7292/api/LaboratoryRequests/${id}`)
        .subscribe(() => {
          this.laboratoryRequests = this.laboratoryRequests.filter(request => request.id !== id);
          this.filteredRequests = this.filteredRequests.filter(request => request.id !== id);
          this.paginateRequests();
        });
    }
  }

  testresult(requestId: number) {
    this.router.navigate(['/labresultform', requestId]);
  }
  getPatientDetails(patientId: number): { name: string; gender: string } {
    const patient = this.patients.find(p => p.id === patientId);
    return patient ? { name: patient.firstName, gender: patient.gender } : { name: 'Loading...', gender: 'Loading...' };
  }
}
