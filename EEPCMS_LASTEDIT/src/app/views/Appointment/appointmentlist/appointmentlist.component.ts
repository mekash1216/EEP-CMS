import { Component, OnInit } from '@angular/core';
import { PrescriptionService } from '../../../service/prescription.service';
import { AppointmentList, PatientAppointment } from '../../../../Models/examiner.model';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { RouterModule } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms'; 
import { FormsModule } from '@angular/forms';

declare var $: any;
@Component({
  selector: 'app-appointmentlist',
  standalone: true,
  imports: [CommonModule,RouterModule,ReactiveFormsModule,FormsModule],
  templateUrl: './appointmentlist.component.html',
  styleUrl: './appointmentlist.component.scss'
})
export class AppointmentlistComponent implements OnInit {
  appointments: any[] = [];
  editForm: FormGroup;
  selectedAppointment: any;
  showModal = false;
  filteredAppointments: any[] = [];
  paginatedAppointments: any[] = [];
  currentPage = 1;
  pageSize = 10; // Number of items per page
  totalPages: number[] = [];
  searchQuery = '';

  constructor(private http: HttpClient, private router: Router, private fb: FormBuilder) {
    this.editForm = this.fb.group({
      appointmentDateTime: ['', Validators.required],
      examinerId: [''],
      reason: [''],
    });
  }

  ngOnInit(): void {
    this.loadAppointments();
  }

  loadAppointments(): void {
    this.http.get<any[]>('http://localhost:5153/api/Appointment')
      .subscribe(data => {
        console.log('Appointments loaded:', data); 
        this.appointments = data;
        this.filterAppointments(); 
      });
  }
  

  isFuture(dateString: string): boolean {
    return new Date(dateString) > new Date();
  }

  openEditModal(appointment: PatientAppointment): void {
    this.selectedAppointment = appointment;
    this.editForm.patchValue({
      appointmentDateTime: appointment.appointmentDateTime,
      examinerId: appointment.examinerId,
      reason:appointment.reason,
    });
    this.showModal = true;
  }

  closeModal(event?: Event): void {
    if (event) {
      event.stopPropagation(); // Prevent event from bubbling up
    }
    this.showModal = false;
  }

  stopPropagation(event: Event): void {
    event.stopPropagation(); // Prevent click inside modal from closing it
  }
  saveAppointment(): void {
    if (this.editForm.valid) {
      const updatedAppointment = { ...this.selectedAppointment, ...this.editForm.value };
      this.http.put(`http://localhost:5153/api/Appointment/${updatedAppointment.id}`, updatedAppointment)
        .subscribe(() => {
          this.loadAppointments();
          this.closeModal();
        });
    }
  }

  deleteAppointment(id: number): void {
    this.http.delete(`http://localhost:5153/api/Appointment/${id}`)
      .subscribe(() => {
        this.loadAppointments();
      });
  }

  filterAppointments(): void {
    if (this.searchQuery) {
      this.filteredAppointments = this.appointments.filter(appointment =>
        appointment.patientFirstName.toLowerCase().includes(this.searchQuery.toLowerCase()) ||
        appointment.patientLastName.toLowerCase().includes(this.searchQuery.toLowerCase())
      );
    } else {
      this.filteredAppointments = [...this.appointments];
    }
    this.paginateAppointments();
  }

  paginateAppointments(): void {
    const start = (this.currentPage - 1) * this.pageSize;
    const end = start + this.pageSize;
    this.paginatedAppointments = this.filteredAppointments.slice(start, end);
    this.totalPages = Array(Math.ceil(this.filteredAppointments.length / this.pageSize)).fill(0).map((_, i) => i + 1);
  }
  changePage(page: number): void {
    if (page >= 1 && page <= this.totalPages.length) {
      this.currentPage = page;
      this.paginateAppointments();
    }
  }
  
 
}