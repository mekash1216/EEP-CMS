import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Doctor, SecondDoctor } from 'src/Models/doctor.model';
import { Examinerpage } from 'src/Models/examiner.model';
import { CommonModule } from '@angular/common';
import { ExaminerService } from 'src/app/service/examiner.service';
import { FormsModule } from '@angular/forms';
import { Assign } from 'src/Models/assign.model';
import { DoctorService } from 'src/app/service/doctor.service';
import { User2 } from 'src/Models/user.model';

@Component({
  selector: 'app-assignexaminer',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './assignexaminer.component.html',
  styleUrl: './assignexaminer.component.scss'
})
export class AssignexaminerComponent {
  private _doctors: User2[] = [];
  @Input() set doctors(allDoctors: User2[]) {
    this._doctors = allDoctors.filter(doctor => doctor.roles.includes('doctor')); 
  }
  get doctors(): User2[] {
    return this._doctors;
  }
  @Input() selectedAssignment: Assign | null = null;
  @Output() oncloseModal = new EventEmitter<void>();
  isAssignModalOpen: boolean =true;
  selectedDoctorId: number | null = null; 
 
  constructor(private examService: ExaminerService,private doctorService : DoctorService) {}




  AAA: Examinerpage = {
    id: 0,
    patientId: 0,
    doctorId: 0,
    assignmentId: 0,
    assignedDate: new Date(),
    regExaminerId: 0
  };

 
  assignExaminer(): void {
      if (!this.selectedAssignment) {
        console.error('Selected patient is undefined');
        return;
      }
  
      this.AAA.assignmentId= this.selectedAssignment.id;
      this.AAA.patientId= this.selectedAssignment.patientId;
      this.AAA.doctorId= this.selectedAssignment.doctorId;
       this.AAA.regExaminerId = this.selectedDoctorId ?? 0; 
   
      this.AAA.assignedDate = new Date();
  
      this.examService.createAssignment(this.AAA)
        .subscribe(
          response => {
            console.log('Assignment created successfully!', response);
            alert('successfully assigned');
            this.oncloseModal.emit();
          },
          error => {
            console.error('Error creating assignment:', error);
          }
        );
    }
  

  ngOnInit(): void {
    console.log('Modal initialized with:', this.selectedAssignment, this.doctors);
  }

  closeModal() {
    this.oncloseModal.emit();
  }
}
