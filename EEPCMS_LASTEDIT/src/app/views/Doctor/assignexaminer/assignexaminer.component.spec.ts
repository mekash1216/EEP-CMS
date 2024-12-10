import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AssignexaminerComponent } from './assignexaminer.component';

describe('AssignexaminerComponent', () => {
  let component: AssignexaminerComponent;
  let fixture: ComponentFixture<AssignexaminerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AssignexaminerComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AssignexaminerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
