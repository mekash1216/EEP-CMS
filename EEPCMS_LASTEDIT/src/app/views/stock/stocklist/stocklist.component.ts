import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { StockService } from '../../../service/stock.service';
import { Stock } from '../../../../Models/stock.model';
import { CommonModule } from '@angular/common';
import { ToastrService } from 'ngx-toastr';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { Router } from '@angular/router';
import { AddstockComponent } from '../../stock/addstock/addstock.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { CardModule } from '@coreui/angular';
import { Prescription, PrescriptionItem } from '../../../../Models/prescription.model';
import { StockPrescriptionService } from '../../../service/stock-prescription-service.service';

@Component({
  selector: 'app-stocklist',
  standalone: true,
  imports: [
    CommonModule,
    AddstockComponent,
    ReactiveFormsModule,
    FormsModule,
    CardModule
  ],
  templateUrl: './stocklist.component.html',
  styleUrls: ['./stocklist.component.scss']
})
export class StockListComponent implements OnInit {
  stocks: Stock[] = [];
  searchTerm: string = '';
  filteredStocks: Stock[] = [];
  prescriptions: Prescription[] = [];
  pagedStocks: Stock[] = [];
  currentPage: number = 1;
  itemsPerPage: number = 5;
  totalPages: number = 0;
  pages: number[] = [];
  selectedStock: Stock = {
    id: '',
    name: '',
    code: '',
    price: 0,
    quantity: 0,
    manufacturer: '',
    expiryDate: new Date(),
    totalPrice: 0
  };

  @ViewChild('editStockModal', { static: false }) editStockModal!: TemplateRef<any>;
  editModalRef!: NgbModalRef;

  @ViewChild('addStockModal', { static: false }) addStockModal!: TemplateRef<any>;

  constructor(
    private stockService: StockService,
    private toastr: ToastrService,
    private modalService: NgbModal,
    private router: Router,
    private stockPrescriptionService: StockPrescriptionService
  ) { }

  ngOnInit(): void {
    this.loadStocks();
  }

  // Open Add Stock Modal
  openAddStockModal(): void {
    this.modalService.open(this.addStockModal, { centered: true, size: 'lg' });
  }

  // Handle Stock Added
  onStockAdded(event: any): void {
    this.modalService.dismissAll();
    this.toastr.success('Stock added successfully.', 'Success');
    this.loadStocks();
    this.stockPrescriptionService.notifyStockUpdate();
  }

  // Load all Stocks
  loadStocks(): void {
    this.stockService.getStocks().subscribe(
      (response: Stock[]) => {
        this.stocks = response;
        this.filteredStocks = this.stocks; // Initialize filteredStocks
        this.setPagination();
      },
      (error) => {
        console.error('Error fetching stocks:', error);
      }
    );
  }

  // Set Pagination
  setPagination(): void {
    this.totalPages = Math.ceil(this.filteredStocks.length / this.itemsPerPage);
    this.pages = Array.from({ length: this.totalPages }, (v, k) => k + 1);
    this.setPage(this.currentPage);
  }

  // Set Current Page
  setPage(page: number): void {
    if (page < 1 || page > this.totalPages) return;
    this.currentPage = page;
    const startIndex = (page - 1) * this.itemsPerPage;
    this.pagedStocks = this.filteredStocks.slice(startIndex, startIndex + this.itemsPerPage);
  }

  // Filter Stocks and remove pagination when searching
  filterStocks(): void {
    if (this.searchTerm) {
      this.filteredStocks = this.stocks.filter(stock =>
        stock.name.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
        stock.code.toLowerCase().includes(this.searchTerm.toLowerCase())
      );
      this.totalPages = 1;
      this.setPage(1);
    } else {
      this.filteredStocks = this.stocks;
      this.setPagination();
    }
  }

  // Delete Stock
  deleteStock(id: string): void {
    if (confirm('Are you sure you want to delete this stock?')) {
      this.stockService.deleteStock(id).subscribe(
        () => {
          this.loadStocks();
          this.toastr.success('Stock deleted successfully!', 'Success');
        },
        (error) => {
          console.error('Error deleting stock:', error);
          this.toastr.error('Failed to delete stock!', 'Error');
        }
      );
    }
  }

  // Open Edit Stock Modal
  openEditStockModal(stock: Stock): void {
    this.selectedStock = { ...stock };
    this.editModalRef = this.modalService.open(this.editStockModal);
  }

  // Handle Edit Submit
  onEditSubmit(): void {
    this.stockService.updateStock(this.selectedStock.id, this.selectedStock).subscribe(() => {
      this.loadStocks();
      this.updatePrescriptionItems();
      this.stockPrescriptionService.notifyStockUpdate();
      this.toastr.success('Stock updated successfully');
      this.editModalRef.close(); // Close modal after update
    }, error => {
      this.toastr.error('Failed to update stock');
    });
  }

  // Update Prescription Items
  updatePrescriptionItems(): void {
    this.prescriptions.forEach((prescription: Prescription) => {
      prescription.prescriptionItems?.forEach((item: PrescriptionItem) => {
        this.checkStockAvailability(item);
      });
    });
  }

  // Check Stock Availability
  checkStockAvailability(item: PrescriptionItem): void {
    const stockItem = this.stocks.find(stock => stock.id === item.stockId);
    if (stockItem) {
      item.isInternal = item.quantity <= stockItem.quantity;
    } else {
      item.isInternal = false; // or handle accordingly
    }
  }
}
