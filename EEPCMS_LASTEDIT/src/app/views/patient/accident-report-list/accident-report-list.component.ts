import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormArray } from '@angular/forms';
import { UserService } from 'src/app/service/user.service';
import { AccidentReport, Patient } from 'src/Models/patient.model';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-accident-report-list',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './accident-report-list.component.html',
  styleUrls: ['./accident-report-list.component.scss']
})
export class AccidentReportListComponent implements OnInit {
  accidentReports: AccidentReport[] = [];
  filteredReports: AccidentReport[] = [];
  searchTerm = '';
  newReportForm: FormGroup;
  showForm = false; 
  patients: Patient[] = [];
  filteredPatients: Patient[] = [];
  patientName: string = '';
  patientId: number | undefined = undefined; // Ensure this is used properly
  isEditing = false;
  currentReportId: number | null = null;
  currentPage: number = 1;
  itemsPerPage: number = 3; 
  totalPages: number = 0;
  constructor(private accidentReportService: UserService, private fb: FormBuilder) {
    this.newReportForm = this.fb.group({
      patientId: [null, Validators.required], // Make sure this is required
      department: ['', Validators.required],
      injuryDate: ['', Validators.required],
      injuryTime: ['', Validators.required],
      causeOfInjury: ['', Validators.required],
      incidentDetails: ['', Validators.required],
      preventiveAction: ['', Validators.required],
      reporterName: ['', Validators.required],
      departmentHeadName: ['', Validators.required],
      witnesses: this.fb.array([]) 
    });
  }

  ngOnInit() {
    this.loadAccidentReports();
    this.loadPatients();
  }


  loadPatients() {
    this.accidentReportService.getPatients().subscribe(patients => {
      this.patients = patients;
    });
  }
  loadAccidentReports() {
    this.accidentReportService.getAccidentReports().subscribe(reports => {
      this.accidentReports = reports;
      this.filteredReports = reports;
      this.totalPages = Math.ceil(this.filteredReports.length / this.itemsPerPage);
    });
  }

  get paginatedReports(): AccidentReport[] {
    const start = (this.currentPage - 1) * this.itemsPerPage;
    const end = start + this.itemsPerPage;
    return this.filteredReports.slice(start, end);
  }

  nextPage() {
    if (this.currentPage < this.totalPages) {
      this.currentPage++;
    }
  }

  previousPage() {
    if (this.currentPage > 1) {
      this.currentPage--;
    }
  }

  filterReports() {
    this.filteredReports = this.accidentReports.filter(report => 
      report.patient.firstName.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
      report.patient.lastName.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
      report.department.toLowerCase().includes(this.searchTerm.toLowerCase())
    );
    this.currentPage = 1; // Reset to the first page
    this.totalPages = Math.ceil(this.filteredReports.length / this.itemsPerPage);
  }

  addAccidentReport() {
    if (this.newReportForm.valid) {
      const formValue = this.newReportForm.value;
      if (this.patientId) {
        formValue.patientId = this.patientId; // Set the patient ID from the selected patient
      }

      if (this.isEditing) {
        this.updateAccidentReport();
      } else {
        this.accidentReportService.addAccidentReport(formValue).subscribe(() => {
          this.loadAccidentReports();
          this.newReportForm.reset();
          this.showForm = false;
          this.patientName = '';
          this.patientId = undefined;
        });
      }
    } else {
      alert('Please fill in all required fields, including selecting a patient.');
    }
  }

  deleteAccidentReport(id: number) {
    this.accidentReportService.deleteAccidentReport(id).subscribe(() => {
      this.loadAccidentReports();
    });
  }

  editAccidentReport(id: number) {
    const selectedReport = this.accidentReports.find(report => report.id === id);
    if (selectedReport) {
      this.showForm = true;
      this.isEditing = true;
      this.currentReportId = selectedReport.id;
  
      this.newReportForm.patchValue({
        patientId: selectedReport.patient.id,
        department: selectedReport.department,
        injuryDate: selectedReport.injuryDate,
        injuryTime: selectedReport.injuryTime,
        causeOfInjury: selectedReport.causeOfInjury,
        incidentDetails: selectedReport.incidentDetails,
        preventiveAction: selectedReport.preventiveAction,
        reporterName: selectedReport.reporterName,
        departmentHeadName: selectedReport.departmentHeadName,
      });

      this.patientName = `${selectedReport.patient.firstName} ${selectedReport.patient.lastName}`;
      this.patientId = selectedReport.patient.id;
  
      this.witnesses.clear();
      selectedReport.witnesses.forEach(witness => {
        this.witnesses.push(this.fb.group({
          id: [witness.id],
          firstName: [witness.firstName],
          lastName: [witness.lastName]
        }));
      });
    }
  }

  updateAccidentReport() {
    if (this.currentReportId && this.newReportForm.valid) {
      const formValue = this.newReportForm.value;
      formValue.id = this.currentReportId;
      formValue.witnesses = this.witnesses.controls.map(ctrl => ({
        id: ctrl.get('id')?.value,
        firstName: ctrl.get('firstName')?.value,
        lastName: ctrl.get('lastName')?.value
      }));
  
      this.accidentReportService.updateAccidentReport(this.currentReportId, formValue).subscribe(() => {
        this.loadAccidentReports();
        this.newReportForm.reset();
        this.isEditing = false;
        this.showForm = false;
        this.currentReportId = null;
      }, error => {
        console.error('Update failed', error);
        alert('Update failed: ' + (error.error || 'An error occurred'));
      });
    }
  }

  toggleForm() {
    this.showForm = !this.showForm;
    if (!this.showForm) {
      this.newReportForm.reset();
      this.patientName = '';
      this.patientId = undefined;
    }
  }

  onPatientNameChange(event: any) {
    const input = event.target.value.toLowerCase();
    this.filteredPatients = this.patients.filter(patient =>
      patient.firstName.toLowerCase().includes(input) || 
      patient.lastName.toLowerCase().includes(input)
    );
  }

  selectPatient(patient: Patient) {
    this.patientName = `${patient.firstName} ${patient.lastName}`;
    this.patientId = patient.id; // Set the selected patient's ID
    this.newReportForm.patchValue({ patientId: this.patientId }); // Update the form control
    this.filteredPatients = [];
  }

  get witnesses(): FormArray {
    return this.newReportForm.get('witnesses') as FormArray;
  }

  addWitness() {
    this.witnesses.push(this.fb.group({
      id: [0],
      firstName: [''],
      lastName: ['']
    }));
  }

  removeWitness(index: number) {
    this.witnesses.removeAt(index);
  }
  clearForm() {
    this.newReportForm.reset();
    this.patientName = '';
    this.patientId = undefined;
    this.witnesses.clear(); 
  }
  
}
