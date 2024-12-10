import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/service/user.service';
import { SickLeave } from 'src/Models/SickLeave.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-sick-leave',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './sick-leave.component.html',
  styleUrls: ['./sick-leave.component.scss']
})
export class SickLeaveComponent implements OnInit {
  sickLeaves: SickLeave[] = [];
  paginatedSickLeaves: SickLeave[] = [];
  searchTerm: string = '';
  currentPage: number = 1;
  itemsPerPage: number = 3; 
  totalPages: number = 1;
  pageNumbers: number[] = []; 
  isModalOpen: boolean = false;
  isSickLeaveModalOpen = false;
  selectedSickLeave: SickLeave = {
    id: 0,
    startDate: '',
    endDate: '',
    reason: '',
    patient: {
      firstName: '',
      lastName: '',
      cardNo: '',
      phoneNumber: '',
      gender: '',
      dateOfBirth: new Date,
      age: 0,
      patientDepartment: '',
      email: '',
      workplace: '',
      position: '',
      assigneddate: ''
    },
    isExpired: false,
    patientId: 0
  };

  constructor(private sickLeaveService: UserService, 
    private cd: ChangeDetectorRef,
    private router: Router,
    public  modalService: NgbModal,
    private toastr: ToastrService) {}

  ngOnInit(): void {
    this.loadSickLeaves();
  }

  loadSickLeaves(): void {
    this.sickLeaveService.getSickLeaves().subscribe((data: SickLeave[]) => {
      this.sickLeaves = data;
      this.totalPages = Math.ceil(this.sickLeaves.length / this.itemsPerPage);
      this.updatePageNumbers();
      this.applyFilter();
      this.cd.detectChanges();
    });
  }

  applyFilter(): void {
    // Apply search filter
    const filteredLeaves = this.sickLeaves.filter(sickLeave => 
      sickLeave.reason.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
      `${sickLeave.patient.firstName} ${sickLeave.patient.lastName}`.toLowerCase().includes(this.searchTerm.toLowerCase())
    );

    // Apply pagination
    const startIndex = (this.currentPage - 1) * this.itemsPerPage;
    this.paginatedSickLeaves = filteredLeaves.slice(startIndex, startIndex + this.itemsPerPage);
    this.totalPages = Math.ceil(filteredLeaves.length / this.itemsPerPage);
    this.updatePageNumbers();
  }

  updatePageNumbers(): void {
    this.pageNumbers = Array.from({ length: this.totalPages }, (_, i) => i + 1);
  }

  isExpired(sickLeave: SickLeave): boolean {
    return sickLeave.isExpired;
  }

  goToPage(page: number): void {
    if (page > 0 && page <= this.totalPages) {
      this.currentPage = page;
      this.applyFilter();
    }
  }

  previousPage(): void {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.applyFilter();
    }
  }

  nextPage(): void {
    if (this.currentPage < this.totalPages) {
      this.currentPage++;
      this.applyFilter();
    }
  }

  viewPatientList(): void {
    this.router.navigate(['/examiner']); 
  }

  openSickLeaveForm(sickLeave: any) {
    this.selectedSickLeave = sickLeave;
    this.selectedSickLeave.startDate = this.formatDate(sickLeave.startDate);
    this.selectedSickLeave.endDate = this.formatDate(sickLeave.endDate);
    this.isSickLeaveModalOpen = true;
    
  }
  formatDate(dateString: string): string {
    const date = new Date(dateString);
    return date.toISOString().split('T')[0];
  }
  closeSickLeaveModal() {
    this.isSickLeaveModalOpen = false;
  }
  submitEditForm(): void {
    if (this.selectedSickLeave) {
      this.sickLeaveService.updateSickLeave(this.selectedSickLeave).subscribe({
        next: () => {
          this.loadSickLeaves();
          this.modalService.dismissAll();
          this.toastr.success('Sick leave updated successfully', 'Success');
        },
        error: () => {
          this.toastr.error('Failed to update sick leave', 'Error');
        }
      });
    }
  }
  
  deleteSickLeave(id: number): void {
    if (confirm('Are you sure you want to delete this sick leave entry?')) {
      this.sickLeaveService.deleteSickLeave(id).subscribe({
        next: () => {
          this.loadSickLeaves();
          this.toastr.success('Sick leave deleted successfully', 'Success');
        },
        error: () => {
          this.toastr.error('Failed to delete sick leave', 'Error');
        }
      });
    }
  }
}
