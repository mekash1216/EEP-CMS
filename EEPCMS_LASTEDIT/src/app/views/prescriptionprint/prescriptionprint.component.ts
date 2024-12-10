import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PrescriptionItem } from 'src/Models/prescription.model';
import { CommonModule } from '@angular/common';
import { Stock } from 'src/Models/stock.model';
import { StockService } from 'src/app/service/stock.service';
@Component({
  selector: 'app-prescriptionprint',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './prescriptionprint.component.html',
  styleUrl: './prescriptionprint.component.scss'
})
export class PrescriptionprintComponent implements OnInit {
  externalItems: PrescriptionItem[] = [];
  stocks: Stock[] = []; 
  constructor(private route: ActivatedRoute,
    private stockservice:StockService
  ) {}

  ngOnInit(): void {
    this.loadstock();
    this.externalItems = history.state.items || [];
  }

  printPage(): void {
   
    window.print();
  }
  
  loadstock() {
    this.stockservice.getStocks().subscribe(
      (stock: Stock[]) => {
        this.stocks = stock;
      },
     (error) => {
        console.error('Error fetching doctors:', error);
      }
    );
  }

  getStockName(stockId: string ): string {
    const stock = this.stocks.find(s => s.id === stockId);
    return stock ? stock.name : 'Unknown';
  }
  
  
  
  
}


