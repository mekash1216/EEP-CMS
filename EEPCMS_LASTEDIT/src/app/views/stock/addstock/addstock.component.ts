import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Stock } from '../../../../Models/stock.model';
import { StockService } from '../../../service/stock.service';
import { FormsModule } from '@angular/forms';
@Component({
  selector: 'app-addstock',
  standalone: true,
  imports: [
    FormsModule,
    

  ],
  templateUrl: './addstock.component.html',
  styleUrl: './addstock.component.scss'
})
export class AddstockComponent {

  newStock: Stock = {
    id: '',
    name: '',
    code: '',
    price: 0,
    quantity: 0,
    manufacturer: '',
    expiryDate: new Date(),
    totalPrice: 0
  }; 

  constructor(
    private stockService: StockService, 
    private router: Router,
    private toastr: ToastrService 
  ) {}

  onSubmit(): void {
    this.stockService.createStock(this.newStock).subscribe(
      () => {
        this.toastr.success('Stock added successfully.', 'Success'); 
        // this.router.navigate(['/stocks']); 
      },
      error => {
        console.error('Error adding stock:', error);
        this.toastr.error('Failed to add stock.', 'Error'); 
      }
    );
  }
}
