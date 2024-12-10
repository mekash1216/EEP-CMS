import { DOCUMENT, NgStyle } from '@angular/common';
import { ChangeDetectorRef, Component, DestroyRef, effect, inject, OnInit, Renderer2, signal, WritableSignal } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { ChartOptions } from 'chart.js';
import { CommonModule } from '@angular/common';
import {
  AvatarComponent,
  ButtonDirective,
  ButtonGroupComponent,
  CardBodyComponent,
  CardComponent,
  CardFooterComponent,
  CardHeaderComponent,
  ColComponent,
  FormCheckLabelDirective,
  GutterDirective,
  ProgressBarDirective,
  ProgressComponent,
  RowComponent,
  TableDirective,
  TextColorDirective
} from '@coreui/angular';
import { ChartjsComponent } from '@coreui/angular-chartjs';
import { IconDirective } from '@coreui/icons-angular';
import { DashboardChartsData, IChartProps } from './dashboard-charts-data';
import { UserService } from 'src/app/service/user.service';
import { Patient } from 'src/Models/patient.model';
import { Observable, forkJoin } from 'rxjs';
import { map } from 'rxjs/operators';
import { PrescriptionService } from 'src/app/service/prescription.service';
import { PatientReport } from 'src/Models/prescription.model';
import * as XLSX from 'xlsx';
@Component({
  templateUrl: 'dashboard.component.html',
  styleUrls: ['dashboard.component.scss'],
  standalone: true,
  imports: [TextColorDirective, CardComponent, CardBodyComponent, 
    RowComponent, ColComponent, ButtonDirective, IconDirective, 
    ReactiveFormsModule, ButtonGroupComponent, FormCheckLabelDirective, 
    ChartjsComponent, NgStyle, CardFooterComponent, GutterDirective, 
    ProgressBarDirective, ProgressComponent, CardHeaderComponent, 
    TableDirective, AvatarComponent, CommonModule]
})
export class DashboardComponent implements OnInit {
  readonly #destroyRef: DestroyRef = inject(DestroyRef);
  readonly #document: Document = inject(DOCUMENT);
  readonly #renderer: Renderer2 = inject(Renderer2);
  readonly #chartsData: DashboardChartsData = inject(DashboardChartsData);
  numberOfPatients: number = 0;
  public mainChart: IChartProps = { type: 'line' };
  public mainChartRef: WritableSignal<any> = signal(undefined);
  #mainChartRefEffect = effect(() => {
    if (this.mainChartRef()) {
      // Handle chart reference if needed
    }
  });
  prescriptionReports: PatientReport[] = [];
  trafficForm = new FormGroup({
    monthControl: new FormControl(new Date().getMonth() + 1),
    yearControl: new FormControl(new Date().getFullYear()),
  });

  months: string[] = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
  years: number[] = Array.from({ length: 10 }, (_, i) => new Date().getFullYear() - i); // Last 10 years

  constructor(private userService: UserService, 
              private chartsData: DashboardChartsData,
              private cd: ChangeDetectorRef,
            private priscriptionservice:PrescriptionService) { }
              numberOfAppointments: number = 0;
              numberOfUsers: number = 0;
              numberOfReferrals: number = 0;
  selectedYear: number = new Date().getFullYear(); 

  ngOnInit(): void {
    this.fetchCurrentWeekData(); 
    this.selectedYear = this.trafficForm.get('yearControl')?.value || new Date().getFullYear();

    this.trafficForm.get('monthControl')?.valueChanges.subscribe(() => {
      this.fetchMonthlyData();
    });

    this.trafficForm.get('yearControl')?.valueChanges.subscribe(year => {
      this.selectedYear = year ?? new Date().getFullYear(); 
      this.fetchYearlyData();
    });

    this.fetchDashboardData();
    this.fetchPrescriptionReports();
  }

  fetchCurrentWeekData(): void {
    const startOfWeek = this.getStartOfWeek();
    const endOfWeek = this.getEndOfWeek();
    this.userService.getPatientsByWeek(startOfWeek, endOfWeek).subscribe(patients => {
      this.numberOfPatients = patients.length;
      this.chartsData.initMainChart('Week', patients);
      this.mainChart = this.chartsData.mainChart; // Update chart data
      this.cd.detectChanges(); // Notify Angular to update the view
    });
  }

  fetchWeeklyData(): void {
    this.fetchCurrentWeekData(); // Re-fetch weekly data if needed
  }

  updateMonth(event: Event): void {
    const month = Number((event.target as HTMLSelectElement).value);
    this.trafficForm.get('monthControl')?.setValue(month);
    this.fetchMonthlyData();
  }

  updateYear(event: Event): void {
    const year = Number((event.target as HTMLSelectElement).value);
    this.trafficForm.get('yearControl')?.setValue(year);
    this.fetchYearlyData();
  }

  fetchMonthlyData(): void {
    const month = Number(this.trafficForm.get('monthControl')?.value);
    const year = Number(this.trafficForm.get('yearControl')?.value);

    if (!isNaN(month) && !isNaN(year)) {
      this.userService.getPatientsByMonth(year, month).subscribe(patients => {
        this.numberOfPatients = patients.length;
        this.chartsData.initMainChart('Month', patients);
        this.mainChart = this.chartsData.mainChart; 
        this.cd.detectChanges(); 
      });
    } else {
      console.error('Invalid month or year values');
    }
  }

  fetchYearlyData(): void {
    const year = Number(this.trafficForm.get('yearControl')?.value);

    if (!isNaN(year)) {
      this.userService.getPatientsByYear(year).subscribe(patients => {
        this.numberOfPatients = patients.length;
        this.chartsData.initMainChart('Year', patients);
        this.mainChart = this.chartsData.mainChart; 
        this.cd.detectChanges(); 
      });
    } else {
      console.error('Invalid year value');
    }
  }


  fetchDashboardData(): void {
    // Using forkJoin to execute multiple HTTP requests simultaneously
    forkJoin({
      patients: this.userService.getPatients(), 
      activeAppointments: this.userService.getActiveAppointments(),
      users: this.userService.getAllUsers(),
      referrals: this.priscriptionservice.getReferrals()
    }).subscribe({
      next: (data) => {
        this.numberOfPatients = data.patients.length; 
        this.numberOfAppointments = data.activeAppointments.length; 
        this.numberOfUsers = data.users.length; 
        this.numberOfReferrals = data.referrals.length; 
      },
      error: (error) => {
        console.error('Error fetching dashboard data:', error);
      }
    });
  }

  getStartOfWeek(): Date {
    const date = new Date();
    const day = date.getDay() || 7; // Make Sunday (0) the last day of the week
    return new Date(date.setDate(date.getDate() - day + 1)); // Get Monday
  }

  getEndOfWeek(): Date {
    const date = new Date();
    const day = date.getDay() || 7;
    return new Date(date.setDate(date.getDate() + (7 - day))); // Get Sunday
  }
  fetchPrescriptionReports(): void {
    this.userService.getPrescriptionReports().subscribe(
      (data: PatientReport[]) => {
        this.prescriptionReports = data;
      },
      (error) => {
        console.error('Error fetching reports:', error); 
      }
    );
  }
  exportToExcel(): void {
    const formattedData = this.prescriptionReports.map(report => ({
      'ID': report.patientCardNo,
      Date: new Date(report.date).toLocaleDateString(),
      Gender: report.gender,
      Diagnosis: report.diagnosis,
    }));
  
    const worksheet: XLSX.WorkSheet = XLSX.utils.json_to_sheet(formattedData);
    const workbook: XLSX.WorkBook = { Sheets: { 'Patient Reports': worksheet }, SheetNames: ['Patient Reports'] };
  
    XLSX.writeFile(workbook, 'PatientReports.xlsx');
  }
  
}
