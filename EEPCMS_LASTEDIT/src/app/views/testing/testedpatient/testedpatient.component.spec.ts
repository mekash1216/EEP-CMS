import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TestedpatientComponent } from './testedpatient.component';

describe('TestedpatientComponent', () => {
  let component: TestedpatientComponent;
  let fixture: ComponentFixture<TestedpatientComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TestedpatientComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TestedpatientComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
