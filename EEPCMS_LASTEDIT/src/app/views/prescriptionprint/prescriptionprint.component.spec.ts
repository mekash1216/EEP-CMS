import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PrescriptionprintComponent } from './prescriptionprint.component';

describe('PrescriptionprintComponent', () => {
  let component: PrescriptionprintComponent;
  let fixture: ComponentFixture<PrescriptionprintComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PrescriptionprintComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(PrescriptionprintComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
