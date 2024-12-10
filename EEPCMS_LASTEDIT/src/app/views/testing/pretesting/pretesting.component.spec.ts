import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PretestingComponent } from './pretesting.component';

describe('PretestingComponent', () => {
  let component: PretestingComponent;
  let fixture: ComponentFixture<PretestingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PretestingComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(PretestingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
