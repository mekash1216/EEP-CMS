import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExaminationlistComponent } from './examinationlist.component';

describe('ExaminationlistComponent', () => {
  let component: ExaminationlistComponent;
  let fixture: ComponentFixture<ExaminationlistComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ExaminationlistComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ExaminationlistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
