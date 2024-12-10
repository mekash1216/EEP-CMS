import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { PrescriptionService } from 'src/app/service/prescription.service';
import { CommonModule } from '@angular/common';
import { PhysicalExamination } from 'src/Models/examiner.model';
import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-examination',
  standalone: true,
  imports: [ReactiveFormsModule,CommonModule],
  templateUrl: './examination.component.html',
  styleUrl: './examination.component.scss'
})
export class ExaminationComponent implements OnInit {
  @Input() patientId!: number; // Patient ID passed from parent component
  @Output() onSave = new EventEmitter<any>();
  physicalExaminationForm!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private physicalExaminationService: PrescriptionService,
    private router: Router,
    private toastr: ToastrService,
  ) {
    this.physicalExaminationForm = this.fb.group({
      generalAppearance: ['', Validators.required],
      vitalSigns: ['', Validators.required],
      headAndNeck: [''],
      cardiovascular: [''],
      respiratory: [''],
      abdomen: [''],
      examinationDate: ['', Validators.required]
    });
  }

  ngOnInit(): void {}

  get formControls() {
    return this.physicalExaminationForm.controls;
  }

  loading = false;

  onSubmit() {
    if (this.physicalExaminationForm.valid) {
      const formData = this.physicalExaminationForm.value;
      formData.patientId = this.patientId; // Ensure patientId is included
  
      // Call the service to save the data
      this.physicalExaminationService.addPhysicalExamination(formData).subscribe(
        (response) => {
          // Success handling
          console.log('Physical examination registered successfully!', response);
          this.toastr.success('Physical Examination Saved Successfully!');
        },
        (error) => {
          // Error handling
          console.error('Error registering physical examination', error);
          if (error.status === 500) {
            alert('Server error occurred while saving the physical examination.');
          } else {
            alert('Error occurred: ' + error.message);
          }
        }
      );
    }
  }
  


 
  
}