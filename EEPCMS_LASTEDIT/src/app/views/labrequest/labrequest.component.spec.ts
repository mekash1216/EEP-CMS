import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LabrequestComponent } from './labrequest.component';

describe('LabrequestComponent', () => {
  let component: LabrequestComponent;
  let fixture: ComponentFixture<LabrequestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LabrequestComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(LabrequestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
