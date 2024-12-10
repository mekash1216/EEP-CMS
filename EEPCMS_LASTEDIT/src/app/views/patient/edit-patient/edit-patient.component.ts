import { Component, EventEmitter, Input, OnInit, Output, SimpleChanges } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Patient } from 'src/Models/patient.model';
import { UserService } from 'src/app/service/user.service';
import { FormsModule } from '@angular/forms';
import { ToasterService } from '@coreui/angular';
@Component({
  selector: 'app-edit-patient',
  standalone:true,
  imports:[
    FormsModule,
    ReactiveFormsModule
  ],
  templateUrl: './edit-patient.component.html',
  styleUrls: ['./edit-patient.component.scss']
})
export class EditPatientComponent implements OnInit {
  @Input() patient: Patient | null = null;
  @Output() patientUpdated: EventEmitter<Patient> = new EventEmitter<Patient>(); // Emit patient object after update
  editForm: FormGroup;

  constructor(private fb: FormBuilder, private userService: UserService) {
    this.editForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      gender: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      dateOfBirth: ['', Validators.required],
      // Add more fields as needed
    });
  }

  ngOnInit(): void {
    this.initializeForm();
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['patient']) {
      this.initializeForm();
    }
  }

  initializeForm(): void {
    if (this.patient) {
      this.editForm.patchValue({
        firstName: this.patient.firstName,
        lastName: this.patient.lastName,
        gender: this.patient.gender,
        phoneNumber: this.patient.phoneNumber,
        dateOfBirth: this.patient.dateOfBirth,
        // Patch other form controls with patient data
      });
    }
  }

  saveChanges() {
    if (this.editForm.valid && this.patient) {
      const updatedPatient: Patient = { ...this.patient, ...this.editForm.value };
      this.userService.updatePatient(updatedPatient).subscribe(
        response => {
          console.log('Patient updated successfully', response);
          this.userService.showSuccess(' Updated successfully', 'Update Success');
          this.patientUpdated.emit(updatedPatient); // Emit the updated patient object
        
        },
        error => {
          console.error('Error updating patient', error);
          this.userService.showError('Failed to update patient', 'Update Failed');
        }
      );
    }
  }

  cancel() {
    // Optionally handle cancel action
  }
}