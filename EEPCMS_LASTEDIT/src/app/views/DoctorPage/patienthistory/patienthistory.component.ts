import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { ExaminerService } from 'src/app/service/examiner.service';
import { PrescriptionService } from 'src/app/service/prescription.service';
import { StockService } from 'src/app/service/stock.service';
import { UserService } from 'src/app/service/user.service';
import { Stock } from 'src/Models/stock.model';
import { Patient } from 'src/Models/patient.model';
import { AppointmentList, LaboratoryTestResult, PhysicalExamination, Referral, SpecialLaboratoryTestResult } from 'src/Models/examiner.model';
import { Prescription } from 'src/Models/prescription.model';
import { LabtestresultComponent } from "../../labtestresult/labtestresult.component";
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-patienthistory',
  standalone: true,
  imports: [CommonModule, FormsModule, LabtestresultComponent],
  templateUrl: './patienthistory.component.html',
  styleUrls: ['./patienthistory.component.scss']
})
export class PatienthistoryComponent implements OnInit {
  filterOption: string = 'recent'; 
  searchQuery: string = '';
  startDate: string = '';
  endDate: string = '';
  referrals: Referral[] = [];
  labresults: LaboratoryTestResult[] = []; 
  specialLabResults: SpecialLaboratoryTestResult[] = [];
  examination: PhysicalExamination[] = [];
  appointments: AppointmentList[] = [];
  prescriptions: Prescription[] = [];
  filteredData: any[] = [];
  selectedPrescription: Prescription | null = null;
  isModalOpen: boolean = false;
  stocks: Stock[] = [];
  patient: Patient[] = [];
  recentData: any[] = [];


  constructor(
    private examinerService: PrescriptionService,
    private prescriptionService: PrescriptionService,
    private stockService: StockService,
    private userService: UserService,
    private route: ActivatedRoute,
    private http: HttpClient
  ) {}
 
  ngOnInit(): void {
    this.loadStock();
    this.loadPatient();
    this.route.paramMap.subscribe(params => {
      const patientId = params.get('id');
      if (patientId) {
        this.loadPatientHistory(+patientId);
  
      }
    });
  }

 
  loadPatientHistory(patientId: number): void {
    this.examinerService.getReferralsByPatient(patientId).subscribe(referrals => {
      this.referrals = referrals;
      this.updateRecentData();
      this.filterData();
    });

    this.examinerService.getAppointmentsByPatient(patientId).subscribe(appointments => {
      this.appointments = appointments;
      this.updateRecentData();
      this.filterData();
    });
    this.examinerService.getLabResultsByPatientId(patientId).subscribe(labresults => {
      this.labresults = labresults;
      this.updateRecentData();
      this.filterData();
    });
    this.examinerService.getSpecialTestsByPatient(patientId).subscribe(specialTests => {
      this.specialLabResults = specialTests; 
      this.updateRecentData();
      this.filterData();
      console.log(this.labresults); 
    });
    this.prescriptionService.getPrescriptionsByPatient(patientId).subscribe(prescriptions => {
      this.prescriptions = prescriptions;
      this.updateRecentData();
      this.filterData();
    });

    this.examinerService.getExaminationsByPatientId(patientId).subscribe(examination => {
      this.examination = examination;
      this.updateRecentData();
      this.filterData();
    });
    this.filterData();
  }

  updateRecentData(): void {
    const getMostRecent = (items: any[], dateKey: string) => {
      if (!items.length) return [];
      const mostRecentDate = Math.max(...items.map(item => new Date(item[dateKey]).getTime()));
      return items.find(item => new Date(item[dateKey]).getTime() === mostRecentDate) || null;
    };
  
    this.recentData = [
      getMostRecent(this.referrals, 'referralDate'),
      getMostRecent(this.appointments, 'appointmentDateTime'),
      getMostRecent(this.prescriptions, 'date'),
      getMostRecent(this.examination, 'examinationDate'),
      getMostRecent(this.labresults, 'testDate')
    ].filter(item => item !== null)
    .map(item => ({ ...item, type: this.getItemType(item) }));
  }
  
  getItemType(item: any): string {
    if (item.referralDate) return 'Referral';
    if (item.appointmentDateTime) return 'Appointment';
    if (item.date) return 'Prescription';
    if (item.examinationDate) return 'Examination';
    if (item.testDate) return 'Labresults';
    return 'No History';
  }

  
  filterData(): void {
    switch (this.filterOption) {
      case 'referrals':
        this.filteredData = this.filterByQuery(this.referrals);
        break;
      case 'examination':
        this.filteredData = this.filterByQuery(this.examination);
        break;
      case 'appointments':
        this.filteredData = this.filterByQuery(this.appointments);
        break;
      case 'prescriptions':
        this.filteredData = this.filterByQuery(this.prescriptions);
        break;
        case 'labresults': 
        this.filteredData = this.filterByQuery(this.labresults);
        break;
           
      case 'recent':
        this.filteredData = this.recentData;
        break;
      default:
        this.filteredData = [];
    }
    this.filteredData = this.filterByDateRange(this.filteredData);
  }

  filterByQuery(items: any[]): any[] {
    if (!this.searchQuery) return items;
  
    const query = this.searchQuery.toLowerCase();
    return items.filter(item =>
      Object.values(item).some(value =>
        (value as string).toLowerCase().includes(query)
      )
    );
  }

  filterByDateRange(items: any[]): any[] {
    if (!this.startDate && !this.endDate) return items;
    const start = this.startDate ? new Date(this.startDate).setHours(0, 0, 0, 0) : -Infinity;
    const end = this.endDate ? new Date(this.endDate).setHours(23, 59, 59, 999) : Infinity;

    return items.filter(item => {
        const itemDateStr = item.testDate || item.date || item.appointmentDateTime || item.referralDate;
        if (!itemDateStr) return false;

      
        const itemDate = new Date(itemDateStr).setHours(0, 0, 0, 0);
        return itemDate >= start && itemDate <= end;
    });
}

  onFilterChange(event: any): void {
    this.filterOption = event.target.value;
    this.filterData();
  }

  openPrescriptionItemsModal(prescription: Prescription): void {
    this.selectedPrescription = prescription;
    this.isModalOpen = true;
  }

  closePrescriptionItemsModal(): void {
    this.isModalOpen = false;
    this.selectedPrescription = null;
  }

  loadStock(): void {
    this.stockService.getStocks().subscribe(
      (stocks: Stock[]) => this.stocks = stocks,
      error => console.error('Error fetching stocks:', error)
    );
  }

  loadPatient(): void {
    this.userService.getPatients().subscribe(
      (patients: Patient[]) => this.patient = patients,
      error => console.error('Error fetching patients:', error)
    );
  }

  getPatientName(patientId: number): string {
    const patient = this.patient.find(p => p.id === patientId);
    return patient ? patient.firstName : 'Unknown';
  }

  getStockName(stockId: string): string {
    const stock = this.stocks.find(s => s.id === stockId);
    return stock ? stock.name : 'Unknown';
  }


  
}
