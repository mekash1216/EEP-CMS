import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Doctor } from 'src/Models/doctor.model';
import { Patient } from 'src/Models/patient.model';
import { Assignment } from 'src/Models/assignment.model';
import { AssignmentService } from 'src/app/service/assignment.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { User2 } from 'src/Models/user.model';

@Component({
  selector: 'app-assign',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './assign.component.html',
  styleUrls: ['./assign.component.scss']
})
export class AssignComponent {
  @Input() selectedPatient: Patient | null = null; 
  private _doctors: User2[] = [];
  @Input() set doctors(allDoctors: User2[]) {
    this._doctors = allDoctors.filter(doctor => doctor.roles.includes('preliminary')); 
  }
  get doctors(): User2[] {
    return this._doctors;
  }
   @Output() closeModal = new EventEmitter<void>();
  selectedDoctorId: number | undefined;
  hideIdField: boolean = true;
  
  assignment: Assignment = {
    id: 0,
    patientId: 0,
    // doctorId: 0,
    assignedDate: new Date(),
 
  };

  isAssignModalOpen: boolean = false;

  constructor(private assignmentService: AssignmentService,private router: Router, 
    private toastr: ToastrService,
  
  ) {}

  // openModal(patient: Patient) {
  //   this.selectedPatient = patient;
  //   this.isAssignModalOpen = true;
  // }

  onCloseModal() {
    this.closeModal.emit(); // Emit the event when close button is clicked
  }

  assign(): void {
    if (!this.selectedPatient) {
      console.error('Selected patient is undefined');
      return;
    }

   this.assignment.patientId = Number(this.selectedPatient.id);
    // this.assignment.doctorId = this.selectedDoctorId ?? 0; 
    this.assignment.assignedDate = new Date();

    this.assignmentService.createAssignment(this.assignment)
      .subscribe(
        response => {
          this.toastr.success('Item removed successfully', 'Success');
          this.closeModal.emit();
        },
        error => {
          console.error('Error creating assignment:', error);
        }
      );
  }
  
}
