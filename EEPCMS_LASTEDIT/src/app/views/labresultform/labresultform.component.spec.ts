import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LabresultformComponent } from './labresultform.component';

describe('LabresultformComponent', () => {
  let component: LabresultformComponent;
  let fixture: ComponentFixture<LabresultformComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LabresultformComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(LabresultformComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
