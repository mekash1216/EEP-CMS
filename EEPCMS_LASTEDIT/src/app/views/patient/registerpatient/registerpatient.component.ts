import { Component, EventEmitter, Output } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, Validators } from '@angular/forms';
import { CardBodyComponent, CardComponent } from '@coreui/angular';
import { Patient } from 'src/Models/patient.model';
import { ReactiveFormsModule } from '@angular/forms';
import { UserService } from 'src/app/service/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-registerpatient',
  standalone: true,
  imports: [FormsModule, CardBodyComponent, CardComponent, ReactiveFormsModule],
  templateUrl: './registerpatient.component.html',
  styleUrls: ['./registerpatient.component.scss']
})
export class RegisterpatientComponent {
  registerForm: FormGroup;

  @Output() PatientCreated = new EventEmitter<Patient>();
  
  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private router: Router // Inject Router here
  ) {
    this.registerForm = this.fb.group({
      cardNo: ['', Validators.required],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      gender: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      dateOfBirth: ['', Validators.required],
      age: ['', Validators.required],
    });
  }

  onSubmit() {
    if (this.registerForm.valid) {
      const newPatient: Patient = this.registerForm.value;
      this.userService.addPatient(newPatient).subscribe(
        response => {
          console.log('User registered successfully', response);
          window.alert('User registered successfully'); // Show success message
          this.PatientCreated.emit(newPatient); // Emit the newly registered user
          this.router.navigate(['/patientlist']);
        },
        error => {
          console.error('Error registering user', error);
          window.alert('Error registering user');
        }
      );
    }
  }

  clearForm() {
    this.registerForm.reset();
  }
}
