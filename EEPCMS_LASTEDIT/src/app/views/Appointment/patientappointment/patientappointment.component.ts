import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PatientAppointment } from 'src/Models/examiner.model';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-patientappointment',
  standalone: true,
  imports: [ReactiveFormsModule,CommonModule],
  templateUrl: './patientappointment.component.html',
  styleUrl: './patientappointment.component.scss'
})
export class PatientappointmentComponent implements OnInit,OnChanges {
  @Input() patient: any;
  @Output() onSave = new EventEmitter<PatientAppointment>();

  appointmentForm: FormGroup;

  constructor(private fb: FormBuilder) {
    this.appointmentForm = this.fb.group({
      appointmentDateTime: ['', Validators.required],
      examinerId: ['', Validators.required],
      reason: ['', Validators.required],
      //regExaminerId: ['', Validators.required]
    });
  }

  ngOnInit(): void {}

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['patient'] && this.patient) {
      this.appointmentForm.patchValue({
        examinerId: this.patient.id
      });
    }
  }

  save(): void {
    if (this.appointmentForm.valid) {
      const appointment = this.appointmentForm.value;
      console.log('Saving appointment:', appointment);  
      this.onSave.emit(appointment);
    }
  }
}