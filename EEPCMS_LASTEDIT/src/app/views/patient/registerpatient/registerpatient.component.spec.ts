import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegisterpatientComponent } from './registerpatient.component';

describe('RegisterpatientComponent', () => {
  let component: RegisterpatientComponent;
  let fixture: ComponentFixture<RegisterpatientComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RegisterpatientComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(RegisterpatientComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
