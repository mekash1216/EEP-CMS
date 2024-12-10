import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AccidentReportListComponent } from './accident-report-list.component';

describe('AccidentReportListComponent', () => {
  let component: AccidentReportListComponent;
  let fixture: ComponentFixture<AccidentReportListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AccidentReportListComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AccidentReportListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
