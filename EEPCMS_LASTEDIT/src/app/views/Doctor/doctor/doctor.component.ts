// doctor.component.ts

import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Assign } from 'src/Models/assign.model'; 
import { AssignmentService } from 'src/app/service/assignment.service';
import { FormsModule } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Patient } from 'src/Models/patient.model';
import { EditdoctorComponent } from 'src/app/views/Doctor/editdoctor/editdoctor.component';
import { UserService } from 'src/app/service/user.service';
import { Doctor, SecondDoctor } from 'src/Models/doctor.model';
import { Examinerpage } from 'src/Models/examiner.model';
import { AssignexaminerComponent } from 'src/app/views/Doctor/assignexaminer/assignexaminer.component';
import { Assignment } from 'src/Models/assignment.model';
import { DoctorService } from 'src/app/service/doctor.service';
import { User2 } from 'src/Models/user.model';
@Component({
  selector: 'app-doctor',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    EditdoctorComponent,
    AssignexaminerComponent
  ],
  templateUrl: './doctor.component.html',
  styleUrls: ['./doctor.component.scss']
})
export class DoctorComponent implements OnInit {
  assignments: Assign[] = [];
  selectedassign: Assign | null = null;
  filteredAssignments: Assign[] = [];
  searchTerm: string = '';
  currentPage: number = 1;
  itemsPerPage: number = 5; // Adjust as needed
  showEditModal: boolean = false;
  showAssignModal: boolean = false;
  patients: Patient[] = [];
  filteredPatients: Patient[] = [];
  selectedAssignment: Assign | null = null;
  doctors: User2[] = [];
  
  isAssignModalOpen = false; 
  assignment:Examinerpage[]= [];
  selectedPatient: Assign | null = null;
  constructor(
    private assignmentService: AssignmentService,
    private toastr: ToastrService,
    private userService: UserService,
    private doctorService: DoctorService
  
  ) { }
  closeModal() {
    this.isAssignModalOpen = false; // Close modal
    // Optionally reset selectedAssignment or perform other actions
  }
  fetchAssignments(): void {
    this.assignmentService.getAssignments().subscribe(
      (assignments: Assign[]) => {
        this.assignments = assignments;
        this.filteredAssignments = assignments;
        this.applySearchFilter();
      },
      (error) => {
        console.error('Error fetching assignments:', error);
      }
    );
  }

  applySearchFilter(): void {
    if (!this.searchTerm) {
      this.filteredAssignments = this.assignments;
    } else {
      this.filteredAssignments = this.assignments.filter(assignment =>
        assignment.patientFirstName.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
        assignment.patientLastName.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
        assignment.patientCardNumber.toLowerCase().includes(this.searchTerm.toLowerCase()) 
        // assignment.doctorName.toLowerCase().includes(this.searchTerm.toLowerCase())
      );
    }
    this.resetPagination();
  }

  onPageChange(page: number): void {
    if (page >= 1 && page <= this.totalPages) {
      this.currentPage = page;
    }
  }

  getPaginatedAssignments(): Assign[] {
    const startIndex = (this.currentPage - 1) * this.itemsPerPage;
    return this.filteredAssignments.slice(startIndex, startIndex + this.itemsPerPage);
  }

  resetPagination(): void {
    this.currentPage = 1;
  }

  clearSearch(): void {
    this.searchTerm = '';
    this.applySearchFilter();
  }

  deleteAssignment(id: number): void {
    this.assignmentService.deleteAssignment(id).subscribe(
      () => {
        this.toastr.success('Assignment deleted successfully!', 'Success');
        this.fetchAssignments(); // Refresh assignments after deletion
      },
      (error) => {
        console.error('Error deleting assignment:', error);
        this.toastr.error('Failed to delete assignment', 'Error');
      }
    );
  }

  openEditModal(assignment: Assign): void {
    this.selectedassign = assignment;
    this.showEditModal = true;
  }

  closeEditModal(): void {
    this.showEditModal = false;
    // Optionally refresh data after closing modal
    this.fetchAssignments();
  }

  loadPatients(): void {
    this.userService.getPatients().subscribe(
      (patients: Patient[]) => {
        this.patients = patients;
        this.filteredPatients = patients;
      },
      (error) => {
        console.error('Error fetching patients:', error);
      }
    );
  }

   ngOnInit(): void {
    this.editPatient();
   this.loadDoctors(); 
    
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

  editPatient() {
    this.assignmentService.getAssignments().subscribe(
      (assignments: Assign[]) => {
        this.assignments = assignments;
        this.filteredAssignments = assignments; 
      },
      (error) => {
        console.error('Error fetching users:', error);
      }
    );
  }

  get totalPages(): number {
    return Math.ceil(this.filteredAssignments.length / this.itemsPerPage);
  }

  get totalPagesArray(): number[] {
    return Array(this.totalPages).fill(0).map((x, i) => i + 1);
  }

  openAssignModal(assignment: Assign) {
    this.selectedAssignment = assignment;
    console.log(this.selectedAssignment);
    this.isAssignModalOpen = true;
  
  }



  closeAssignModal(): void {
    this.isAssignModalOpen = false;
  }
}
