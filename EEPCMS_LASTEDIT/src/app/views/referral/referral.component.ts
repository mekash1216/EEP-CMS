import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { PrescriptionService } from 'src/app/service/prescription.service';
import { ReferralupdateComponent } from 'src/app/views/referralupdate/referralupdate.component';
import { CommonModule } from '@angular/common';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'app-referral',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './referral.component.html',
  styleUrls: ['./referral.component.scss']
})
export class ReferralComponent implements OnInit {
  @Input() referralId: number | null = null;
  referrals: any[] = [];
  paginatedReferrals: any[] = [];
  selectedReferral: any | null = null;
  closeResult = '';
  editModal: NgbModalRef | null = null;
  referralForm!: FormGroup;
  pageSize = 10; 
  currentPage = 1;
  totalPages = 1;
  pages: number[] = [];
  pageSizes = [5, 10, 15, 20]; 

  constructor(
    private referralService: PrescriptionService,
    private modalService: NgbModal,
    private router: Router,
  ) { }

  ngOnInit(): void {
    this.getReferrals();
  }

  getReferrals(): void {
    this.referralService.getReferrals().subscribe((data: any[]) => {
      this.referrals = data;
      this.totalPages = Math.ceil(this.referrals.length / this.pageSize);
      this.pages = Array.from({ length: this.totalPages }, (_, i) => i + 1);
      this.updatePaginatedReferrals();
    });
  }

  updatePaginatedReferrals(): void {
    const start = (this.currentPage - 1) * this.pageSize;
    const end = start + this.pageSize;
    this.paginatedReferrals = this.referrals.slice(start, end);
  }

  onSearch(event: any): void {
    const query = event.target.value.toLowerCase();
    if (query) {
      this.paginatedReferrals = this.referrals.filter(referral =>
        referral.examiner.patientName.toLowerCase().includes(query) ||
        referral.from.toLowerCase().includes(query)
      ).slice(0, this.pageSize);
    } else {
      this.updatePaginatedReferrals();
    }
  }

  onPageSizeChange(event: any): void {
    this.pageSize = +event.target.value; // Convert to number
    this.totalPages = Math.ceil(this.referrals.length / this.pageSize);
    this.pages = Array.from({ length: this.totalPages }, (_, i) => i + 1);
    this.updatePaginatedReferrals();
  }

  changePage(page: number): void {
    if (page >= 1 && page <= this.totalPages) {
      this.currentPage = page;
      this.updatePaginatedReferrals();
    }
  }

  viewReferral(id: number) {
    const selectedReferral = this.referrals.find(referral => referral.id === id);
    if (selectedReferral) {
      this.referralService.setSelectedReferral(selectedReferral);
      this.router.navigate(['/print-referral', id]);
    }
  }

  openEditModal(referral: any): void {
    const modalRef = this.modalService.open(ReferralupdateComponent, { centered: true });
    modalRef.componentInstance.referral = referral;
    modalRef.result.then(
      (result) => {
        console.log('Modal closed with:', result);
        this.getReferrals();
      },
      (reason) => {
        console.log('Modal dismissed with:', reason);
      }
    );
  }

  deleteReferral(id: number): void {
    if (confirm('Are you sure you want to delete this referral?')) {
      this.referralService.deleteReferral(id).subscribe(
        () => {
          this.referrals = this.referrals.filter(r => r.id !== id);
          this.totalPages = Math.ceil(this.referrals.length / this.pageSize);
          this.pages = Array.from({ length: this.totalPages }, (_, i) => i + 1);
          this.updatePaginatedReferrals();
          console.log('Referral deleted successfully.');
        },
        (error) => {
          console.error('Error deleting referral:', error);
        }
      );
    }
  }
  navigateToPatientList(): void {
    this.router.navigate(['/examiner']);
  }
}
