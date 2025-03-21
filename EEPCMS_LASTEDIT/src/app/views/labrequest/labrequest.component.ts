import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserService } from '../../../app/service/user.service';
import { LaboratoryRequest } from '../../../Models/labrequest.model';
import { Patient } from '../../../Models/patient.model';

@Component({
  selector: 'app-labrequest',
  standalone: true,
  templateUrl: './labrequest.component.html',
  imports: [ReactiveFormsModule, CommonModule]
})
export class LabrequestComponent implements OnInit {

  laboratoryForm!: FormGroup;
  patient: Patient | null = null;
  isHematologySelectAllChecked = false;
  isBiochemistrySelectAllChecked = false;
  isBacteriologySelectAllChecked = false;
  isSerologySelectAllChecked = false;
  isElectrolyteSelectAllChecked = false;
  isCancerMarkerSelectAllChecked = false;
  isCardiacMarkerSelectAllChecked = false;
  isCoagulationSelectAllChecked = false;
  isHormoneSelectAllChecked = false;
  isOtherLabSelectAllChecked = false;
  isParasitologySelectAllChecked = false;
  constructor(private fb: FormBuilder, private http: HttpClient, private toastr: ToastrService,private route: ActivatedRoute,private patientservice:UserService) {}
  hospitals: string[] = ['Inside','Hospital A', 'Hospital B', 'Hospital C', 'Hospital D'];
  ngOnInit(): void {
    this.laboratoryForm = this.fb.group({
      dateOfRequest: [''],
      patientId: [''],
      patientName: [''],
      patientGender:[''],
      patientAge: [''],
      hospitalName:[''],
      isParasitologyChecked: [false],
      parasitology: this.fb.group({
        stoolDirect: [false],
        urinalysis: [false]
      }),
      isHematologyChecked: [false],
      hematology: this.fb.group({
        cbc: [false],
        hgb: [false],
        esr: [false],
        bloodGroup: [false],
        hba1c: [false],
        bloodFilm: [false],
        peripheralMorphology: [false],
        malariaByAgCard: [false]
      }),
      isBiochemistryChecked: [false],
      biochemistry: this.fb.group({
        fbs: [false],
        rbs: [false],
        bun: [false],
        creatinine: [false],
        sgot: [false],
        sgpt: [false],
        alkaPho: [false],
        tBilirubin: [false],
        dBilirubin: [false],
        albumin: [false],
        uricAcid: [false],
        tProtein: [false],
        tCholesterol: [false],
        hdl: [false],
        ldl: [false],
        triglyceride: [false],
        amylase: [false],
        ggt: [false],
        lipase: [false],
        ldh: [false]
      }),
      isBacteriologyChecked: [false],
      bacteriology: this.fb.group({
        afb: [false],
        wetSmear: [false],
        gramStrin: [false],
        koh: [false]
      }),
      isSerologyChecked: [false],
      serology: this.fb.group({
        vdrl: [false],
        widal: [false],
        weilFelix: [false],
        bloodGroup: [false],
        xMatch: [false],
        hBsAg: [false],
        hBsAb: [false],
        hPyloriAb: [false],
        hPyloriAgStool: [false],
        rf: [false],
        aso: [false],
        crp: [false],
        hiv: [false],
        fob: [false],
        hcg: [false],
        hepatitisCViralLoad: [false],
        hepatitisBViralLoad: [false]
      }),
      isElectrolyteChecked: [false],
      electrolyte: this.fb.group({
        sodium: [false],
        potassium: [false],
        chloride: [false],
        calcium: [false],
        magnesium: [false],
        phosphorus: [false]
      }),
      isCancerMarkerChecked: [false],
      cancerMarker: this.fb.group({
        cA125: [false],
        cA199: [false],
        cA153: [false],
        cea: [false],
        afp: [false]
      }),
      isCardiacMarkerChecked: [false],
      cardiacMarker: this.fb.group({
        ckmb: [false],
        troponinT: [false],
        dDimer: [false]
      }),
      isCoagulationChecked: [false],
      coagulation: this.fb.group({
        pt: [false],
        aptt: [false],
        inr: [false]
      }),
      isHormoneChecked: [false],
      hormone: this.fb.group({
        tsh: [false],
        freeT4: [false],
        freeT3: [false],
        totalT4: [false],
        totalT3: [false],
        totalBetaHCGT3: [false],
        psa: [false],
        fsh: [false],
        lh: [false],
        prolactin: [false]
      }),
      isOtherLabChecked: [false],
      otherLab: this.fb.group({
        vitB12e: [false],
        vitD: [false]
      })
    });

    this.route.paramMap.subscribe((params: ParamMap) => {
      const patientId = Number(params.get('patientId'));
      if (patientId) {
        this.fetchPatient(patientId);
      }
    });
    
  }
  fetchPatient(id: number): void {
    this.patientservice.getPatientBiId(id).subscribe({
      next: (patient: Patient) => {
        console.log('Fetched Patient:', patient);
        this.patient = patient;
        this.laboratoryForm.patchValue({
          patientId: patient.id,
          patientAge:patient.age,
          patientGender:patient.gender,
          patientName: `${patient.firstName} ${patient.lastName}`
        });
      },
      error: (error) => {
        console.error('Error fetching patient details', error);
      }
    });
  }
  
  onSubmit(): void {
    if (this.laboratoryForm.valid) {
      const formData: LaboratoryRequest = this.laboratoryForm.value;
      console.log('Form Data:', formData);
      this.http.post('http://localhost:5153/api/LaboratoryRequests', formData).subscribe({
        next: (response) => {
          this.toastr.success('Form submitted successfully!', 'Success');
        },
        error: (error) => {
          this.toastr.error('There was an error submitting the form. Please try again.', 'Error');
        }
      });
    } else {
      this.toastr.error('Form is invalid. Please correct the errors and try again.', 'Error');
    }
  }
  
  
  toggleHematologySelection(event: Event) {
    const isChecked = (event.target as HTMLInputElement).checked;
    this.setAllControls('hematology', isChecked);
    this.isHematologySelectAllChecked = isChecked;
  }

  toggleBiochemistrySelection(event: Event) {
    const isChecked = (event.target as HTMLInputElement).checked;
    this.setAllControls('biochemistry', isChecked);
    this.isBiochemistrySelectAllChecked = isChecked;
  }

  onHematologyCategoryChange(event: Event) {
    const isChecked = (event.target as HTMLInputElement).checked;
    if (!isChecked) {
      this.setAllControls('hematology', false);
      this.isHematologySelectAllChecked = false;
    }
  }

  onBiochemistryCategoryChange(event: Event) {
    const isChecked = (event.target as HTMLInputElement).checked;
    if (!isChecked) {
      this.setAllControls('biochemistry', false);
      this.isBiochemistrySelectAllChecked = false;
    }
  }

  private setAllControls(groupName: string, value: boolean) {
    const group = this.laboratoryForm.get(groupName) as FormGroup;
    if (group) {
      Object.keys(group.controls).forEach(controlName => {
        group.get(controlName)?.setValue(value);
      });
    }
  }
  toggleBacteriologySelection(event: Event) {
    const isChecked = (event.target as HTMLInputElement).checked;
    this.isBacteriologySelectAllChecked = isChecked;
    this.setAllControls('bacteriology', isChecked);
  }

  toggleSerologySelection(event: Event) {
    const isChecked = (event.target as HTMLInputElement).checked;
    this.isSerologySelectAllChecked = isChecked;
    this.setAllControls('serology', isChecked);
  }

  toggleElectrolyteSelection(event: Event) {
    const isChecked = (event.target as HTMLInputElement).checked;
    this.isElectrolyteSelectAllChecked = isChecked;
    this.setAllControls('electrolyte', isChecked);
  }

  toggleCancerMarkerSelection(event: Event) {
    const isChecked = (event.target as HTMLInputElement).checked;
    this.isCancerMarkerSelectAllChecked = isChecked;
    this.setAllControls('cancerMarker', isChecked);
  }

  toggleCardiacMarkerSelection(event: Event) {
    const isChecked = (event.target as HTMLInputElement).checked;
    this.isCardiacMarkerSelectAllChecked = isChecked;
    this.setAllControls('cardiacMarker', isChecked);
  }

  toggleCoagulationSelection(event: Event) {
    const isChecked = (event.target as HTMLInputElement).checked;
    this.isCoagulationSelectAllChecked = isChecked;
    this.setAllControls('coagulation', isChecked);
  }

  toggleHormoneSelection(event: Event) {
    const isChecked = (event.target as HTMLInputElement).checked;
    this.isHormoneSelectAllChecked = isChecked;
    this.setAllControls('hormone', isChecked);
  }

  toggleOtherLabSelection(event: Event) {
    const isChecked = (event.target as HTMLInputElement).checked;
    this.isOtherLabSelectAllChecked = isChecked;
    this.setAllControl('otherLab', isChecked);
  }

  toggleParasitologySelection(event: Event) {
    const isChecked = (event.target as HTMLInputElement).checked;
    this.isParasitologySelectAllChecked = isChecked;
    this.setAllControls('parasitology', isChecked);
  }

  private setAllControl(groupName: string, isChecked: boolean) {
    const group = this.laboratoryForm.get(groupName) as FormGroup;
    const controls = group.controls;
    Object.keys(controls).forEach(key => {
      controls[key].setValue(isChecked);
    });
  }
}  
