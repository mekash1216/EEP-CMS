import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/service/user.service';
import { ClinicRequest } from 'src/Models/ClinicRequest.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { User } from 'src/Models/user.model';

@Component({
  selector: 'app-requestlist',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './requestlist.component.html',
  styleUrls: ['./requestlist.component.scss']
})
export class RequestlistComponent implements OnInit {
  requests: ClinicRequest[] = [];
  filteredRequests: ClinicRequest[] = [];
  paginatedRequests: ClinicRequest[] = [];
  searchTerm: string = '';
  currentPage: number = 1;
  pageSize: number = 5;
  totalPages: number = 1;
  currentUser: User | null = null;
  constructor(private clinicRequestService: UserService, private router: Router, private toastr: ToastrService) {}

  ngOnInit(): void {
    this.clinicRequestService.currentUser.subscribe(user => {
      this.currentUser = user;
      this.loadRequests(); // Ensure requests are loaded once user data is available
    });
  }

  loadRequests(): void {
    this.clinicRequestService.getAll().subscribe(data => {
      this.requests = data;
      this.filterRequests(); 
    });
  }

  filterRequests(): void {
    this.filteredRequests = this.requests.filter(request =>
      request.patientFirstName.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
      request.patientLastName.toLowerCase().includes(this.searchTerm.toLowerCase())
      
    );
    this.currentPage = 1; // Reset to first page on new search
    this.calculatePagination();
  }

  calculatePagination(): void {
    this.totalPages = Math.ceil(this.filteredRequests.length / this.pageSize);
    this.paginate();
  }

  paginate(): void {
    const start = (this.currentPage - 1) * this.pageSize;
    const end = start + this.pageSize;
    this.paginatedRequests = this.filteredRequests.slice(start, end);
  }

  prevPage(): void {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.paginate();
    }
  }

  nextPage(): void {
    if (this.currentPage < this.totalPages) {
      this.currentPage++;
      this.paginate();
    }
  }

  addNewRequest(): void {
    this.router.navigate(['/requestform/:id']);
  }

  edit(requestId: number): void {
    this.router.navigate(['/requestform', requestId]);
  }

  delete(id: number): void {
    this.clinicRequestService.delete(id).subscribe(() => {
      this.loadRequests();
    });
  }

  assignPatient(requestId: number): void {
    this.clinicRequestService.assignPatient(requestId).subscribe(() => {
      this.toastr.success('Patient assigned successfully!'); 
      const request = this.requests.find(req => req.id === requestId);
      if (request) {
        request.status = 'Pending';
        request.requestDate = new Date(); 
      }
  
      this.loadRequests(); 
    }, (error) => {
      console.error('Error assigning patient:', error);
      let errorMessage = 'An error occurred while assigning the patient.';
      if (error.error && error.error.message) {
        errorMessage = error.error.message;
      }
      this.toastr.error(errorMessage); 
    });
  }
  
}
