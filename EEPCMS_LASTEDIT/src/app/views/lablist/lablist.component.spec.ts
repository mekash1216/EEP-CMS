import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LablistComponent } from './lablist.component';

describe('LablistComponent', () => {
  let component: LablistComponent;
  let fixture: ComponentFixture<LablistComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LablistComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(LablistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
