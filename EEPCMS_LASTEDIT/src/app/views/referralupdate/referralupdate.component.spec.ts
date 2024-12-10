import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReferralupdateComponent } from './referralupdate.component';

describe('ReferralupdateComponent', () => {
  let component: ReferralupdateComponent;
  let fixture: ComponentFixture<ReferralupdateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ReferralupdateComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ReferralupdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
