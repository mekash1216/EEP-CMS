import { Component, OnInit } from '@angular/core';
import { PrescriptionService } from 'src/app/service/prescription.service';
import { PhysicalExamination } from 'src/Models/examiner.model';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ToastrService } from 'ngx-toastr';
import { FormsModule } from '@angular/forms';
@Component({
  selector: 'app-examinationlist',
  standalone: true,
  imports: [ReactiveFormsModule,CommonModule,FormsModule],
  templateUrl: './examinationlist.component.html',
  styleUrl: './examinationlist.component.scss'
})
export class ExaminationlistComponent implements OnInit {
  examinations: any[] = [];
  physicalExaminationForm: FormGroup;
  editingExamination: boolean = false;
  currentExaminationId: number | null = null;
  loading: boolean = true;
  isModalOpen: boolean = false;
  selectedPatientId: number | undefined; 
  searchTerm: string = '';
  itemsPerPage: number = 5;
  currentPage: number = 1;
  constructor(
    private fb: FormBuilder,
    private examinationService: PrescriptionService,
    private toastr: ToastrService
  ) {
    this.physicalExaminationForm = this.fb.group({
      generalAppearance: ['', Validators.required],
      vitalSigns: ['', Validators.required],
      headAndNeck: [''],
      cardiovascular: [''],
      respiratory: [''],
      abdomen: [''],
      examinationDate: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    this.loadExaminations();
  }

  loadExaminations(): void {
    this.examinationService.getAllExaminations().subscribe({
      next: (data) => {
        this.examinations = data;
        this.loading = false;
      },
      error: (err) => {
        console.error('Error fetching examinations', err);
        this.toastr.error('Failed to load examinations.');
        this.loading = false;
      }
    });
  }

  openModal(examination: any): void {
    this.editingExamination = true;
    this.currentExaminationId = examination.id;
    this.selectedPatientId = examination.patientId; // Set the patient ID here
    this.physicalExaminationForm.patchValue({
      generalAppearance: examination.generalAppearance,
      vitalSigns: examination.vitalSigns,
      headAndNeck: examination.headAndNeck,
      cardiovascular: examination.cardiovascular,
      respiratory: examination.respiratory,
      abdomen: examination.abdomen,
      examinationDate: new Date(examination.examinationDate).toISOString().substring(0, 16) // Format for <input type="datetime-local">
    });
    this.isModalOpen = true;
  }

  closeModal(): void {
    this.isModalOpen = false;
  }



  paginatedExaminations(): any[] {
    const filteredExaminations = this.examinations.filter(examination =>
      Object.values(examination).some(value =>
        String(value).toLowerCase().includes(this.searchTerm.toLowerCase())
      )
    );
    
    const start = (this.currentPage - 1) * this.itemsPerPage;
    return filteredExaminations.slice(start, start + this.itemsPerPage);
  }

  totalPages(): number[] {
    const filteredExaminations = this.examinations.filter(examination =>
      Object.values(examination).some(value =>
        String(value).toLowerCase().includes(this.searchTerm.toLowerCase())
      )
    );

    const pageCount = Math.ceil(filteredExaminations.length / this.itemsPerPage);
    return Array.from({ length: pageCount }, (_, index) => index + 1);
  }

  changePage(page: number): void {
    if (page < 1 || page > this.totalPages().length) return;
    this.currentPage = page;
  }

  onItemsPerPageChange(): void {
    this.currentPage = 1; // Reset to first page when items per page change
  }

  saveExamination(): void {
    if (this.physicalExaminationForm.invalid) {
      return;
    }

    const formValue = this.physicalExaminationForm.value;
    const examinationData: PhysicalExamination = {
      id: this.currentExaminationId!,
      generalAppearance: formValue.generalAppearance,
      vitalSigns: formValue.vitalSigns,
      headAndNeck: formValue.headAndNeck,
      cardiovascular: formValue.cardiovascular,
      respiratory: formValue.respiratory,
      abdomen: formValue.abdomen,
      examinationDate: new Date(formValue.date),
      patientId: this.selectedPatientId! 
    };

    if (this.editingExamination && this.currentExaminationId !== null) {
      // Update existing examination
      this.examinationService.updateExamination(this.currentExaminationId, examinationData).subscribe({
        next: () => {
          this.toastr.success('Examination updated successfully!');
          this.loadExaminations(); // Reload the list after update
          this.closeModal(); // Close the modal
        },
        error: (err) => {
          console.error('Error updating examination', err);
          this.toastr.error('Failed to update examination.');
        }
      });
    } else {
      console.error('No examination ID available for update or editing is not enabled');
      this.toastr.error('Unable to update examination. No ID available.');
    }
  }

  deleteExamination(id: number): void {
    if (id === undefined || id === null) {
      console.error('Invalid ID provided for deletion');
      return;
    }

    this.examinationService.deleteExamination(id).subscribe({
      next: () => {
        this.toastr.success('Examination deleted successfully!');
        this.loadExaminations(); // Reload the list after deletion
      },
      error: (err) => {
        console.error('Error deleting examination', err);
        this.toastr.error('Failed to delete examination.');
      }
    });
  }
}