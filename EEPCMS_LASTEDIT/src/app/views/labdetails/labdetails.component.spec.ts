import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LabdetailsComponent } from './labdetails.component';

describe('LabdetailsComponent', () => {
  let component: LabdetailsComponent;
  let fixture: ComponentFixture<LabdetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LabdetailsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(LabdetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
