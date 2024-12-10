import { TestBed } from '@angular/core/testing';

import { StockPrescriptionServiceService } from './stock-prescription-service.service';

describe('StockPrescriptionServiceService', () => {
  let service: StockPrescriptionServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(StockPrescriptionServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
