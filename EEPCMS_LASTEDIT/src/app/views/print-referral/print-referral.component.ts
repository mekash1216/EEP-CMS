import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { PrescriptionService } from 'src/app/service/prescription.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
@Component({
  selector: 'app-print-referral',
  standalone: true,
  imports: [FormsModule,CommonModule,RouterModule],
  templateUrl: './print-referral.component.html',
  styleUrl: './print-referral.component.scss'
})
// print-referral.component.ts
export class PrintReferralComponent implements OnInit {
printPage() {
  window.print();
}
  selectedReferral: any;

  constructor(
    private route: ActivatedRoute,
    private referralSharingService: PrescriptionService,
    private router:Router
  ) {}

  ngOnInit() {
    const id = +this.route.snapshot.paramMap.get('id')!;
    this.referralSharingService.getSelectedReferral().subscribe(
      (referral: any) => {
        this.selectedReferral = referral as any;
      },
      error => {
        console.error('Error fetching referral', error);
      }
    );
  }
}