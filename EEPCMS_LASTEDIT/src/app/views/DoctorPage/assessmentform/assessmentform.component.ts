import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from 'src/app/service/user.service';
import { Assessment, PhysicalAssessment } from 'src/Models/examiner.model';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { CommonModule } from '@angular/common';
import { HttpResponse } from '@angular/common/http';
@Component({
  selector: 'app-assessmentform',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule,CommonModule],
  templateUrl: './assessmentform.component.html',
  styleUrls: ['./assessmentform.component.scss'] 
})
export class AssessmentformComponent implements OnInit {
  assessmentForm: FormGroup;
  patientId!: number;
  icd10Codes: Assessment[] = [];
  filteredCodes: Assessment[] = [];
  searchTerm: string = '';
  currentPage: number = 1;
  pageSize: number = 30;
  totalItems: number = 0;
  constructor(
    private fb: FormBuilder,
    private service: UserService,
    private router: Router,
    private route: ActivatedRoute,
    private toastr:ToastrService
  ) {
    this.assessmentForm = this.fb.group({
      heent: ['', Validators.required],
      glands: ['', Validators.required],
      chest: ['', Validators.required],
      cvs: ['', Validators.required],
      abdomen: ['', Validators.required],
      genitoUninary: ['', Validators.required],
      musculoSkeleton: ['', Validators.required],
      skin: ['', Validators.required],
      cns: ['', Validators.required],
      date: [new Date(), Validators.required],
      patientId: [this.patientId, Validators.required] ,
      code: ['',Validators.required],
    });
  }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.patientId = +params['patientId'];
      this.assessmentForm.get('patientId')?.setValue(this.patientId);
    });
    this.loadICD10Codes();
  }
  loadICD10Codes(): void {
    this.service.getICD10Codes(this.searchTerm).subscribe(data => {
      this.icd10Codes = data;
      this.filteredCodes = data; 
    });
  }

  onSearch(): void {
    this.filteredCodes = this.icd10Codes.filter(code =>
      code.shortDescription.toLowerCase().includes(this.searchTerm.toLowerCase())
    );
  }


  onSubmit(): void {
    if (this.assessmentForm.invalid) {
      this.toastr.error('Please fill out the form correctly.', 'Validation Error');
      return;
    }

    const assessmentData: PhysicalAssessment = this.assessmentForm.value;

    this.service.createAssess(assessmentData).subscribe(
      () => {
        this.toastr.success('Physical Examination added successfully.', 'Success');
        this.router.navigate(['/Assessment']);
      },
      error => {
        this.toastr.error('An error occurred while adding the Examination.', 'Error');
      }
    );
  }

  onCancel(): void {
    this.router.navigate(['/Assessment']);
  }

  navigateToAssessmentList(): void {
    this.router.navigate(['/Assessment']);
  }
  
}
