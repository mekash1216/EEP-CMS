import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { UserService } from 'src/app/service/user.service';
import { PhysicalAssessment } from 'src/Models/examiner.model';

@Component({
  selector: 'app-assessmentedit',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './assessmentedit.component.html',
  styleUrls: ['./assessmentedit.component.scss']
})
export class AssessmenteditComponent implements OnInit {

  assessmentForm: FormGroup;
  id: number | undefined;
  patientId: number | undefined; // Add this to keep track of patientId

  constructor(
    private fb: FormBuilder,
    private service: UserService,
    private route: ActivatedRoute,
    private router: Router
  ) { 
    const today = new Date().toISOString().split('T')[0];
    this.assessmentForm = this.fb.group({
      heent: [''],
      glands: [''],
      chest: [''],
      cvs: [''],
      abdomen: [''],
      genitoUninary: [''],
      musculoSkeleton: [''],
      skin: [''],
      cns: [''],
      date: [today]
    });
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.id = +params.get('id')!;
      if (this.id) {
        this.loadAssessment(this.id);
      }
    });
  }

  loadAssessment(id: number): void {
    this.service.getByIdAssess(id).subscribe(data => {
      this.assessmentForm.patchValue({
        heent: data.heent,
        glands: data.glands,
        chest: data.chest,
        cvs: data.cvs,
        abdomen: data.abdomen,
        genitoUninary: data.genitoUninary,
        musculoSkeleton: data.musculoSkeleton,
        skin: data.skin,
        cns: data.cns,
        date: data.date || new Date().toISOString().split('T')[0]
      });
      this.patientId = data.patientId; // Set patientId
    });
  }

  save(): void {
    if (this.assessmentForm.valid && this.id) {
      const updatedAssessment: PhysicalAssessment = {
        ...this.assessmentForm.value,
        id: this.id,
        patientId: this.patientId! 
      };
      console.log(this.assessmentForm)
      this.service.updateAssess(this.id, updatedAssessment).subscribe(() => {
        this.router.navigate(['/Assessment']);
      });
    }
  }

  cancel(): void {
    this.router.navigate(['/Assessment']);
  }

  navigateToAssessmentList(): void {
    this.router.navigate(['/Assessment']);
  }
}
