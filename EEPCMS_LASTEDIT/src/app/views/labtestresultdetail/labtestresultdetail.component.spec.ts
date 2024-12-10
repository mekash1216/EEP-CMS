import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LabtestresultdetailComponent } from './labtestresultdetail.component';

describe('LabtestresultdetailComponent', () => {
  let component: LabtestresultdetailComponent;
  let fixture: ComponentFixture<LabtestresultdetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LabtestresultdetailComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(LabtestresultdetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
