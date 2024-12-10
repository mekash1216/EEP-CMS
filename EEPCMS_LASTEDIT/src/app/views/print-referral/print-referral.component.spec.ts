import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PrintReferralComponent } from './print-referral.component';

describe('PrintReferralComponent', () => {
  let component: PrintReferralComponent;
  let fixture: ComponentFixture<PrintReferralComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PrintReferralComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(PrintReferralComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
