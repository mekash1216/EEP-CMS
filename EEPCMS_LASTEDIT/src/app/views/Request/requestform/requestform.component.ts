import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from 'src/app/service/user.service'; // Ensure correct service path
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { ClinicRequest } from 'src/Models/ClinicRequest.model';
import { cardNoValidator } from 'src/app/views/Validation/forms';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-requestform',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './requestform.component.html',
  styleUrls: ['./requestform.component.scss']
})
export class RequestformComponent implements OnInit {
  requestForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private requestService: UserService,
    private router: Router
  ) {
    const today = new Date().toISOString().split('T')[0];
    this.requestForm = this.fb.group({
      id: [''],
      employeeId: ['', Validators.required],
      patientFirstName: ['', Validators.required],
      patientLastName: ['', Validators.required],
      patientDepartment: ['', Validators.required],
      gender: [''],
      phoneNumber: [''],
      email: ['', [Validators.required, Validators.email]],
      workplace: [''],
      isManager: [false],
      position: [''],
      birthdate: [''],
      age: [''],
      status: ['Pending', Validators.required] ,
      requestDate: [today, Validators.required],
    });
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const requestId = params.get('id');
      if (requestId) {
        this.loadRequest(requestId);
      }
    });

    this.requestForm.get('birthdate')?.valueChanges.subscribe(birthdate => {
      this.calculateAge(birthdate);
    });
  }
  calculateAge(birthdate: string): void {
    if (birthdate) {
      const birthDateObj = new Date(birthdate);
      const today = new Date();
      let age = today.getFullYear() - birthDateObj.getFullYear();
      const monthDiff = today.getMonth() - birthDateObj.getMonth();
      if (monthDiff < 0 || (monthDiff === 0 && today.getDate() < birthDateObj.getDate())) {
        age--;
      }
      
      this.requestForm.patchValue({ age: age }); 
    } else {
      this.requestForm.patchValue({ age: '' }); 
    }
  }

  loadRequest(id: string): void {
    this.requestService.getById(id).subscribe(request => {
      this.requestForm.patchValue(request);

      if (request.employeeId) {
        this.fetchEmployeeInfo(request.employeeId.toString()); 
      }
    });
  }

  fetchEmployeeInfo(employeeId: string): void {
    if (employeeId) {
      this.requestService.getEmployeeById(employeeId).subscribe(employee => {
        this.requestForm.patchValue({
          patientFirstName: employee.patientFirstName,
          patientLastName: employee.patientLastName,
          patientDepartment: employee.patientDepartment,
          gender: employee.gender,
          phoneNumber: employee.phoneNumber,
          email: employee.email,
          workplace: employee.workplace,
          isManager: employee.isManager,
          position: employee.position,
          birthdate: employee.birthdate,
          
        });
      }, (error: HttpErrorResponse) => {
        console.error('Error fetching employee info:', error);
      });
    } else {
      console.warn('Employee ID is required to fetch employee info.');
    }
  }

  onSubmit(): void {
    if (this.requestForm.valid) {
      const requestData: ClinicRequest = this.requestForm.value;
      this.requestService.create(requestData).subscribe(() => {
        this.router.navigate(['/requestlist']); 
      }, (error: HttpErrorResponse) => {
        console.error('Error submitting request:', error);
      });
    } else {
      console.warn('Form is invalid. Please fill out all required fields.');
    }
  }
}
