import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src//environment';
import { Stock, StockCreateDto, StockUpdateDto } from 'src/Models/stock.model';

@Injectable({
  providedIn: 'root'
})
export class StockService {
  private apiBaseUrl = `${environment.apiBaseUrl}/Stocks`;
  

  constructor(private http: HttpClient) { }

  getStocks(): Observable<Stock[]> {
    return this.http.get<Stock[]>(this.apiBaseUrl);
  }

  getStockById(id: string): Observable<Stock> {
    return this.http.get<Stock>(`${this.apiBaseUrl}/${id}`);
  }

  createStock(stock: StockCreateDto): Observable<Stock> {
    return this.http.post<Stock>(this.apiBaseUrl, stock);
  }

  updateStock(id: string, stock: StockUpdateDto): Observable<void> {
    return this.http.put<void>(`${this.apiBaseUrl}/${id}`, stock);
  }

  deleteStock(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiBaseUrl}/${id}`);
  }
}
