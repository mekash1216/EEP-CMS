import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StockListComponent } from './stocklist.component';

describe('StocklistComponent', () => {
  let component: StockListComponent;
  let fixture: ComponentFixture<StockListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [StockListComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(StockListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
