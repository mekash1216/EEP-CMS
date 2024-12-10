import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AssessmenteditComponent } from './assessmentedit.component';

describe('AssessmenteditComponent', () => {
  let component: AssessmenteditComponent;
  let fixture: ComponentFixture<AssessmenteditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AssessmenteditComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AssessmenteditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
