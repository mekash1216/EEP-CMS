import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LabeditComponent } from './labedit.component';

describe('LabeditComponent', () => {
  let component: LabeditComponent;
  let fixture: ComponentFixture<LabeditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LabeditComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(LabeditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
