import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PatientappointmentComponent } from './patientappointment.component';

describe('PatientappointmentComponent', () => {
  let component: PatientappointmentComponent;
  let fixture: ComponentFixture<PatientappointmentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PatientappointmentComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(PatientappointmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
