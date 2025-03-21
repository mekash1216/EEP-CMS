import { Component, Input,NgModule  } from '@angular/core';
import { Examiner, PatientAppointment, PhysicalExamination, Referral } from '../../../../Models/examiner.model';
import { FormBuilder, FormGroup, FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ExaminerService } from '../../../service/examiner.service'
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { CardBodyComponent, CardComponent, DropdownComponent,
  DropdownDividerDirective,
  DropdownHeaderDirective,
  DropdownItemDirective,
  DropdownMenuDirective,
  DropdownToggleDirective, } from '@coreui/angular';
import { PrescriptionComponent } from "../../prescriptionform/prescription/prescription.component";
import { PatientappointmentComponent } from '../../appointment/patientappointment/patientappointment.component';
import { PrescriptionService } from '../../../service/prescription.service';
import { Patient } from '../../../../Models/patient.model';
import { UserService } from '../../../../app/service/user.service';
import { ReferralComponent } from "../../referral/referral.component";
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { PatienthistoryComponent } from '../../../views/DoctorPage/patienthistory/patienthistory.component';
import { ExaminationComponent } from '../../../views/DoctorPage/examination/examination.component';
import{ SickLeaveFormComponent } from '../../../views/patient/sick-leave-form/sick-leave-form.component'
@Component({
    selector: 'app-listofpatient',
    standalone: true,
    templateUrl: './listofpatient.component.html',
    styleUrl: './listofpatient.component.scss',
    imports: [
    FormsModule,
    CommonModule,
    CardBodyComponent,
    CardComponent,
    DropdownComponent,
    DropdownDividerDirective,
    DropdownHeaderDirective,
    DropdownItemDirective,
    DropdownMenuDirective,
    DropdownToggleDirective,
    RouterModule,
    PrescriptionComponent,
    PatientappointmentComponent,
    ReactiveFormsModule,
    PatienthistoryComponent,
    ExaminationComponent,
    SickLeaveFormComponent
    
]
})
export class ListofpatientComponent {
onSearch($event: Event) {
throw new Error('Method not implemented.');
}
navigateToPatientList() {
  this.router.navigate(['/referral']);
}
onPageSizeChange($event: Event) {
throw new Error('Method not implemented.');
}
  patients: Patient[] = [];
  selectedPatient1: Patient | null = null;
  isReferralModalOpen: boolean = false;
  referralForm: FormGroup;
isRegisterPhysicalExaminationModalOpen = false;
selectedPatientId!: number;
  isAppointmentModalOpen = false;
  selectedAppointment: any;
  isAssignModalOpen = false; 

selectedAssignment: Examiner | null = null;
  examiners: Examiner[] = [];
  selectedPatient: Examiner | null = null;
selectedReferralId: number | null = null; 

  @Input() popperOptions: any;
  isSickLeaveModalOpen = false;


  openpreModal(examiner: Examiner) {
    this.selectedAssignment = examiner;
    this.isAssignModalOpen = true;
    
  }

  constructor(private examinerService: ExaminerService,
    private appointmentService: PrescriptionService,
    private toastr: ToastrService,private router: Router,
    private patientService: UserService,
    private referralService: PrescriptionService,
    private fb: FormBuilder,
    private modalService: NgbModal
  ) {
    {
      this.referralForm = this.fb.group({
        referralDate: [''],
        investigationResult: [''],
        examinerId: [''],
        From: [''],             
        Clinicalfinding: [''],
        Diagnosis: [''],
        Rxgiven: [''],
        Reason: [''],
        To: [''],
      });
    }
   }
 

  navigateTo(route: string) {
    this.router.navigate([route]);
  }

  ngOnInit(): void {
    this.loadExaminers();
  }

  loadExaminers(): void {
    this.examinerService.getExaminers().subscribe(
      (data: Examiner[]) => {
        this.examiners = data;
  
      },
      (error) => {
        console.error('Error fetching examiners', error);
      }
    );
  }

  deleteExaminer(examiner: Examiner): void {
    if (confirm(`Are you sure you want to delete Patient ${examiner.patientFirstName} ${examiner.patientLastName}?`)) {
      this.examinerService.deleteExamine(examiner.id).subscribe(
        () => {
          this.examiners = this.examiners.filter(e => e !== examiner); // Remove deleted examiner from the list
          this.toastr.success(`Patient ${examiner.patientFirstName} ${examiner.patientLastName} deleted successfully.`);
          console.log(`Examiner ${examiner.id} deleted successfully.`);
        },
        (error) => {
          console.error('Error deleting examiner', error);
          this.toastr.error('Failed to delete examiner.');
        }
      );
    }
  }

  editExaminer(examiner: Examiner): void {
    // Implement edit functionality if needed
  }

  openPatientHistoryForm(examiner: Examiner) {
    this.selectedPatient = examiner;
    // Open the patient history modal here
    // const modalRef = this.modalService.open(PatienthistoryComponent);
    // modalRef.componentInstance.examiner = this.selectedPatient;
    this.router.navigate(['/history', examiner.patientId]);
  }
 
  
  openReferral(examiner: Examiner): void {
    this.selectedPatient = examiner;
    this.referralForm.patchValue({
      referralDate: '',
      investigationResult: '',
      Reason: '',
      To:'',
      From: '',           
      Clinicalfinding: '',
      Diagnosis: '',
      Rxgiven: '',
      examinerId: examiner.id 
    });
    this.isReferralModalOpen = true;

    
  }
  
  


  saveReferral(): void {
    if (this.selectedPatient) {
      const referral: Referral = {
        referralDate: this.referralForm.get('referralDate')?.value,
        investigationResult: this.referralForm.get('investigationResult')?.value,
        reason: this.referralForm.get('Reason')?.value,
        examinerId: Number(this.selectedPatient.id),
        rxgiven: this.referralForm.get('Rxgiven')?.value,
        diagnosis: this.referralForm.get('Diagnosis')?.value,
        from: this.referralForm.get('From')?.value,
        clinicalfinding: this.referralForm.get('Clinicalfinding')?.value,
        to: this.referralForm.get('To')?.value,
      };
  
      this.referralService.createReferral(referral).subscribe({
        next: () => {
          this.toastr.success('Referral saved successfully!'); 
          this.closeModal(); 
        },
        error: (error) => {
          console.error('Error saving referral', error);
          this.toastr.error('Failed to save referral.'); 
        }
      });
    }
  }
  
  
  
  

  openAppointmentForm(examiner: any): void {
    this.selectedAppointment = examiner;
    this.isAppointmentModalOpen = true;
  }
  closeModal(): void {
    this.isAppointmentModalOpen = false;
  }
close():void{
  this.isReferralModalOpen = false;
}
  
  handleSave(appointment: PatientAppointment): void {
    this.appointmentService.createAppointment(appointment).subscribe(() => {
      this.loadExaminers(); 
      this.closeModal();
    });
  }


  getFormattedPatientName(patient: Examiner | null): string {
    if (patient) {
      return `${patient.patientFirstName} ${patient.patientLastName}`;
    }
    return 'N/A'; // Return a default value if no patient is selected
  }

  openRegisterPhysicalExamination(examiner: Examiner): void {
    console.log('Selected Examiner ID:', examiner.id);
    this.selectedPatientId = Number(examiner.id);
    this.isRegisterPhysicalExaminationModalOpen = true;
  }

  openRegisterPhysicalAssessment(examiner: Examiner): void {
    const patientId = examiner.patientId;
this.router.navigate(['/Assessmentform', patientId]);
  }
  

  handleSavePhysicalExamination(examination: PhysicalExamination): void {
    console.log('Physical Examination:', examination);
    if (this.selectedPatientId !== undefined) {
      examination.patientId = this.selectedPatientId;

      // Call your service to save the examination data
      this.appointmentService.addPhysicalExamination(examination).subscribe({
        next: () => {
          this.toastr.success('Physical Examination Saved Successfully!');
          this.closePhysicalModal();
        },
        error: (error) => {
          console.error('Error saving physical examination', error);
          this.toastr.error('Failed to save physical examination.');
        }
      });
    } else {
      console.error('Selected Patient ID is undefined');
      this.toastr.error('Selected Patient ID is undefined');
    }
  }


  closePhysicalModal(): void {
    this.isRegisterPhysicalExaminationModalOpen = false;
  }

 
  openLabRequest(examiner: Examiner): void {
  this.selectedPatient = examiner;
  this.router.navigate(['/labrequest', examiner.patientId]);
}

    

    closePrescriptionModal(): void {
      this.isAssignModalOpen = false;
    }
    handleClosePrescriptionModal(): void {
      this.closePrescriptionModal();
    }


    openAddSickLeaveForm(examiner: Examiner) {
      this.selectedPatient = examiner;
      this.isSickLeaveModalOpen = true;
    }
  
    closeSickLeaveModal() {
      this.isSickLeaveModalOpen = false;
    }
  

    handleSickLeaveSave(sickLeaveData: any) {
      if (this.selectedPatient) {
        sickLeaveData.patientId = this.selectedPatient.id;  
        this.patientService.addSickLeave(sickLeaveData).subscribe(
          response => {
            this.toastr.success('Sick leave saved successfully!');

          },
          error => {
            this.toastr.error('Failed to save sick leave.');
           
          }
        );
      } else {
        console.error('No patient selected');
      }
    }
  }
  

