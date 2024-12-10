import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { Assign } from 'src/Models/assign.model';
import { AssignmentService } from 'src/app/service/assignment.service';
import { Router } from '@angular/router';

import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-editdoctor',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    CommonModule
  ],
  templateUrl: './editdoctor.component.html',
  styleUrl: './editdoctor.component.scss'
})
export class EditdoctorComponent {

  @Input() assignment: Assign | null=null;
  @Output() patientUpdated = new EventEmitter<Assign>();
  
  patientForm: FormGroup;
  showEditModal: boolean = false;

  constructor(private fb: FormBuilder, private assignmentservice: AssignmentService,
     private router: Router,
     private toastr: ToastrService) {
    this.patientForm = this.fb.group({
      id: [''],
      patientId: [{value: '', disabled: true}, Validators.required],
      // doctorId: [{value: '', disabled: true}, Validators.required],
      patientCardNumber: [''],
      patientAge:[''],
      patientFirstName: [''],
      patientLastName: [''],
      doctorName: [''],
      assignedDate: [''],
      weight: ['', Validators.required],
       systolicPressure:['', Validators.required],
       diastolicPressure: ['', Validators.required],
       respiratoryRate: ['', Validators.required],
       pulseRate:['', Validators.required],
       temperature: ['', Validators.required],
       spO2: ['', Validators.required],
       triage: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    if (this.assignment) {
      
      this.patientForm.patchValue(this.assignment);
    }
  }

  ngOnChanges(): void {
    if (this.assignment) {
      this.patientForm.patchValue(this.assignment);
    }
  }

  onSubmit() {
    if (this.patientForm.valid) {
      const updatedAssign: Assign = this.patientForm.getRawValue(); // getRawValue to include disabled fields
      console.log('Submitting:', updatedAssign); // Debugging
      this.assignmentservice.updateAssign(updatedAssign).subscribe(
        response => {
          this.toastr.success('Updated successfully!', 'Success');
          this.patientUpdated.emit(); 
        },
        error => {
          console.error('Error updating assignment', error);
          this.toastr.error('Error updating assignment', 'Error');
        }
      );
    } else {
      console.warn('Form is invalid');
    }
  }
  
  

  onCancel(): void {
    this.patientUpdated.emit(undefined);
  }

}
