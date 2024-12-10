import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LabtestresultComponent } from './labtestresult.component';

describe('LabtestresultComponent', () => {
  let component: LabtestresultComponent;
  let fixture: ComponentFixture<LabtestresultComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LabtestresultComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(LabtestresultComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
