import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StockPrescriptionService {
  private stockUpdatedSource = new BehaviorSubject<void>(undefined);
  stockUpdated$ = this.stockUpdatedSource.asObservable();

  notifyStockUpdate() {
    this.stockUpdatedSource.next(); // Emit an event without a parameter
  }
}
