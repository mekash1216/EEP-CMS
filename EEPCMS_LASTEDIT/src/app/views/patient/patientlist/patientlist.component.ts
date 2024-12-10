import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { UserService } from 'src/app/service/user.service';
import { Patient } from 'src/Models/patient.model';
import { Doctor } from 'src/Models/doctor.model';
import { RegisterpatientComponent } from 'src/app/views/patient/registerpatient/registerpatient.component';
import { AssignComponent } from 'src/app/views/patient/assign/assign.component';
import { EditPatientComponent } from 'src/app/views/patient/edit-patient/edit-patient.component'
import { ToastrService } from 'ngx-toastr';
import { ClinicRequest } from 'src/Models/ClinicRequest.model';
import { User2 } from 'src/Models/user.model';
@Component({
  selector: 'app-patientlist',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    RegisterpatientComponent,
    AssignComponent,
    EditPatientComponent
  ],

  templateUrl: './patientlist.component.html',
  styleUrls: ['./patientlist.component.scss']
})
export class PatientlistComponent implements OnInit {

  patients: Patient[] = [];
  filteredPatients: Patient[] = [];
  paginatedPatients: Patient[] = [];
  searchTerm: string = '';
  currentPage: number = 1;
  itemsPerPage: number = 5;
  isEditModalOpen = false;
  isRegisterModalOpen = false;
  isAssignModalOpen = false; 
  selectedPatient: Patient | null = null;
  doctors: User2[] = []; 
  selectedDoctor: Doctor | null = null;
  assignedDoctor: Doctor | null = null; 
  assignedDate: Date | null = null;
  patientList: Patient[] = []; 
  newRequestCount: number = 0;
  requests: ClinicRequest[] = [];
  constructor(private userService: UserService,private toastr:ToastrService) {}

  ngOnInit(): void {
    this.loadPatients();
    this.loadDoctors(); 
    this.loadRequests();
  }
 
  loadPatients() {
    this.userService.getPatients().subscribe(
      (patients: Patient[]) => {
        this.patients = patients;
        this.filteredPatients = patients;
        this.updateNewRequestCount();
        this.updatePagination();
      },
      (error) => {
        console.error('Error fetching patients:', error);
      }
    );
  }


  loadRequests() {
    this.userService.getAll().subscribe(
      (requests: ClinicRequest[]) => {
        this.requests = requests;
        this.mergePatientData(); 
        this.updateNewRequestCount();
      },
      (error) => {
        console.error('Error fetching requests:', error);
      }
    );
  }



  mergePatientData() {
    this.patients.forEach(patient => {
      const request = this.requests.find(r => r.employeeId === patient.cardNo);
      if (request) {
        patient.requestStatus = request.status; 
      }
    });
    this.filteredPatients = [...this.patients];
    this.updatePagination();
  }

  updateNewRequestCount() {
    this.newRequestCount = this.patients.filter(p => p.requestStatus === 'Pending').length;
  }

  loadDoctors() {
    this.userService.getAllUsers().subscribe(
      (doctors: User2[]) => {
        this.doctors = doctors;
      },
      (error) => {
        console.error('Error fetching doctors:', error);
      }
    );
  }

  searchUsers() {
    if (!this.searchTerm) {
      this.filteredPatients = this.patients;
    } else {
      this.filteredPatients = this.patients.filter(patient =>
        patient.firstName.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
        patient.lastName.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
        patient.cardNo.toLowerCase().includes(this.searchTerm.toLowerCase())
      );
    }
    this.currentPage = 1; // Reset to first page on new search
    this.updatePagination();
  }
  updatePagination() {
    const startIndex = (this.currentPage - 1) * this.itemsPerPage;
    this.paginatedPatients = this.filteredPatients.slice(startIndex, startIndex + this.itemsPerPage);
  }
  changePage(page: number) {
    if (page < 1 || page > this.totalPageCount) return; 
    this.currentPage = page;
    this.updatePagination();
  }
  get totalPageCount(): number {
    return Math.ceil(this.filteredPatients.length / this.itemsPerPage);
  }

  openRegistration() {
    this.isRegisterModalOpen = true;
  }

  handleRegistrationSuccess(newPatient: Patient) {
    this.isRegisterModalOpen = false;
    this.patients.unshift(newPatient);
    this.searchUsers(); 
  }

  closeRegistration() {
    this.isRegisterModalOpen = false;
  }

  editPatient(patient: Patient) {
    this.selectedPatient = patient;
    this.isEditModalOpen = true;
  }

  deleteUser(id: number) {
    this.userService.deletePatient(id).subscribe(
      () => {
        this.patients = this.patients.filter(patient => patient.id !== id);
        this.filteredPatients = this.filteredPatients.filter(patient => patient.id !== id);
      },
      (error) => {
        console.error('Error deleting patient:', error);
      }
    );
  }

  
    assignPatient(patient: Patient) {
      this.selectedPatient = patient;
      this.isAssignModalOpen = true;
    }
    
  

  closeAssignModal() {
    this.isAssignModalOpen = false;
    this.selectedDoctor = null;
  }


  isAssignedToCurrentDoctor(): boolean {
    return !!this.assignedDoctor && !!this.selectedDoctor && this.assignedDoctor.id === this.selectedDoctor.id;
  }

  closeEdit() {
    this.isEditModalOpen = false;
  }
  handlePatientUpdated(updatedPatient: Patient) {
    // Update the patient in your list
    const index = this.patients.findIndex(p => p.id === updatedPatient.id);
    if (index !== -1) {
      this.patients[index] = updatedPatient;
      this.filteredPatients = [...this.patients]; // Update filtered list if needed
    }
    this.closeEdit();

  }
  openAssignModal(patient: Patient) {
    this.selectedPatient = patient;
    this.isAssignModalOpen = true;
  }
  
  onAssignModalClose() {
    this.closeAssignModal();
  }





  approvePatient(cardNo: string): void {
    this.userService.approveRequestByCardNo(cardNo).subscribe({
      next: response => {
   
        this.toastr.success('Request approved successfully', 'Success');
        this.updateRequestStatus(cardNo, 'Accepted');
      },
      error: err => {
        
        this.toastr.error('Failed to approve request', 'Error');
      }
    });
  }

  private updateRequestStatus(cardNo: string, status: string): void {
    const patient = this.patients.find(p => p.cardNo === cardNo);
    if (patient) {
      patient.requestStatus = status;
    }
  }


  rejectPatient(cardNo: string): void {
    this.userService.rejectRequestByCardNo(cardNo).subscribe({
      next: response => {
        console.log('Request rejected:', response);
        this.toastr.success('Request rejected successfully', 'Success');
        this.updateRequestStatus(cardNo, 'Rejected');
      },
      error: err => {
        console.error('Error rejecting request:', err);
        this.toastr.error('Failed to reject request', 'Error');
      }
    });
  }
  

  

  
}
