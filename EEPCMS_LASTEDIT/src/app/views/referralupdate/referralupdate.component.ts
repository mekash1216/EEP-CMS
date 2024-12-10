import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { PrescriptionService } from 'src/app/service/prescription.service';
import { Referral } from 'src/Models/examiner.model';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-referralupdate',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './referralupdate.component.html',
  styleUrls: ['./referralupdate.component.scss']
})
export class ReferralupdateComponent implements OnInit {
  @Input() referral: Referral | null = null;
  referralForm!: FormGroup;

  constructor(
    private fb: FormBuilder,
    public activeModal: NgbActiveModal,
    private referralService: PrescriptionService
  ) {}

  ngOnInit(): void {
    this.referralForm = this.fb.group({
      referralDate: [this.referral?.referralDate || '', Validators.required],
      investigationResult: [this.referral?.investigationResult || '', Validators.required],
      reason: [this.referral?.reason || '', Validators.required],
      rxgiven: [this.referral?.rxgiven || '', Validators.required],
      diagnosis: [this.referral?.diagnosis || '', Validators.required],
      from: [this.referral?.from || '', Validators.required],
      clinicalfinding: [this.referral?.clinicalfinding || '', Validators.required],
      to: [this.referral?.to || '', Validators.required]
      // Do not include examinerId in the form
    });
  }

  saveChanges(): void {
    if (this.referralForm.valid) {
      const updatedReferral: Referral = {
        id: this.referral?.id || 0,
        ...this.referralForm.value,
        examinerId: this.referral?.examinerId || '' // Include examinerId in submission
      };

      // Ensure date is in ISO format
      if (updatedReferral.referralDate) {
        updatedReferral.referralDate = new Date(updatedReferral.referralDate);
      }

      this.referralService.updateReferral(updatedReferral).subscribe(
        (response) => {
          console.log('Referral updated successfully:', response);
          this.activeModal.close('Updated');
        },
        (error) => {
          console.error('Error updating referral:', error);
        }
      );
    } else {
      console.log('Form is invalid');
      Object.keys(this.referralForm.controls).forEach(key => {
        const control = this.referralForm.get(key);
        if (control?.invalid) {
          console.log(`Errors for ${key}:`, control.errors);
        }
      });
    }
  }

  close(): void {
    this.activeModal.dismiss('Cross click');
  }
}