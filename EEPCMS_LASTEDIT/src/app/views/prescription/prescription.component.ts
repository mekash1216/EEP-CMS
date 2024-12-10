import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { PrescriptionService } from 'src/app/service/prescription.service';
import { Prescription, PrescriptionItem } from 'src/Models/prescription.model';
import { NgxPaginationModule } from 'ngx-pagination';
import { Stock } from 'src/Models/stock.model';
import { StockPrescriptionService } from 'src/app/service/stock-prescription-service.service'
import { Route, Router, Routes } from '@angular/router';
import { StockService } from 'src/app/service/stock.service';
import { NavigationExtras } from '@angular/router';
@Component({
  selector: 'app-prescription',
  standalone: true,
  imports: [FormsModule, CommonModule, NgxPaginationModule],
  templateUrl: './prescription.component.html',
  styleUrl: './prescription.component.scss'
})
export class PrescriptionComponent implements OnInit {
  prescription: Prescription | undefined; 
  stocks: Stock[] = [];
  prescriptions: Prescription[] = []; 
  pagedPrescriptions: Prescription[] = []; 
  approvePrescriptions: PrescriptionItem[] = [];
  searchTerm: string = ''; 
  isModalOpen = false; 
  selectedPrescription: Prescription | null = null;
  selectedFilter: 'all' | 'internal' | 'external' = 'all';
  itemsPerPage: number = 10;
  totalPages: number = 0; 
  externalItemsModalOpen: boolean = false;
  externalItems: PrescriptionItem[] = []; 
currentPage: number = 1;
setPage(page: number): void {
  this.currentPage = page;
  const startIndex = (this.currentPage - 1) * this.itemsPerPage;
  const endIndex = Math.min(startIndex + this.itemsPerPage - 1, this.prescriptions.length - 1);

  // Slice the prescriptions array to display the current page
  this.pagedPrescriptions = this.prescriptions.slice(startIndex, endIndex + 1);
}


editPrescription(_t29: Prescription) {
throw new Error('Method not implemented.');
}

  constructor(private prescriptionService: PrescriptionService,
    private stockPrescriptionService: StockPrescriptionService,
    private router: Router,
    private stockservice: StockService
  ) {}

  ngOnInit(): void {
    this.loadstock()
    this.loadPrescriptions();

  
    this.stockPrescriptionService.stockUpdated$.subscribe(() => {
      this.updateStockAvailability();
    });
  }

  updateStockAvailability(): void {
    if (this.selectedPrescription && this.selectedPrescription.prescriptionItems) {
      this.selectedPrescription.prescriptionItems.forEach(item => {
        this.checkStockAvailability(item);
      });
    }
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

  
  loadPrescriptions(): void {
    this.prescriptionService.getAllPrescriptions().subscribe(
      (response: Prescription[]) => {
        this.prescriptions = response;
        this.setPage(this.currentPage); 
      },
      (error) => {
        console.error('Error fetching prescriptions:', error);
      }
    );
  }

 

  openPrescription(prescription: Prescription): void {
    this.selectedPrescription = prescription;
    this.isModalOpen = true;
    this.getApprovePrescriptions();
  }

  getApprovePrescriptions(): void {
    this.approvePrescriptions = this.selectedPrescription?.prescriptionItems || [];
  }

  approvePrescriptionItem(id: string) {
    this.prescriptionService.approvePrescriptionItem(id).subscribe(
      () => {
        alert(`Prescription item  approved successfully.`);
        if (this.selectedPrescription && this.selectedPrescription.prescriptionItems) {
          this.selectedPrescription.prescriptionItems = this.selectedPrescription.prescriptionItems.map(item =>
            item.id === id ? { ...item, isApproved: true } : item
          );
        }
        this.loadPrescriptions();
      },
      (error) => {
        console.error(`Failed to approve prescription item with ID ${id}:`, error);
      }
    );
  }

  // Total number of items for pagination
get totalItems(): number {
  return this.prescriptions.length;
}


  closePrescriptionModal(): void {
    this.isModalOpen = false;
  }

  createPrescription(form: NgForm): void {
  
    console.log(form.value); 

    this.closePrescriptionModal(); 
  }


  deletePrescription(prescription: Prescription): void {
    if (confirm('Are you sure you want to delete this prescription?')) {
      this.prescriptionService.deletePrescription(prescription.id).subscribe(
        () => {
          this.prescriptions = this.prescriptions.filter(p => p.id !== prescription.id);
        
          this.setPage(this.currentPage);
          
          console.log('Prescription deleted successfully.');
        },
        error => {
          console.error('Error deleting prescription:', error);
        }
      );
    }
  }
  


filterPrescriptions(): void {
  this.currentPage = 1;
  this.setPage(this.currentPage);
}


filterItems(item: PrescriptionItem): boolean {
  if (this.selectedFilter === 'internal') {
    return item.isInternal === true;
  }
  if (this.selectedFilter === 'external') {
    return item.isInternal === false;
  }
  return true; 
}


checkStockAvailability(item: PrescriptionItem): void {
  const stockItem = this.stocks.find(stock => stock.id === item.stockId);
  if (stockItem) {
    item.isInternal = item.quantity <= stockItem.quantity;
  } else {
    item.isInternal = false; 
  }
}



viewExternalItems(prescription: Prescription): void {
  if (prescription && prescription.prescriptionItems) {
    // Filter external items
    this.externalItems = prescription.prescriptionItems.filter(item => !item.isInternal);
  } else {
    this.externalItems = []; 
  }
}



openExternalItemsModal(prescription: Prescription): void {
  this.selectedPrescription = prescription;

  // Check if selectedPrescription and prescriptionItems are defined
  if (this.selectedPrescription && this.selectedPrescription.prescriptionItems) {
    this.externalItems = this.selectedPrescription.prescriptionItems.filter(item => !item.isInternal);
  } else {
    // Handle the case where selectedPrescription or prescriptionItems is undefined
    this.externalItems = []; // Or handle accordingly based on your application logic
  }

  const navigationExtras: NavigationExtras = {
    state: {
      items: this.externalItems
    }
  };

  this.router.navigate(['/external'], navigationExtras);
}


closeExternalItemsModal(): void {
  this.externalItemsModalOpen = false;
}
getStockName(stockId: string): string {
  const stock = this.stocks.find(s => s.id === stockId);
  return stock ? stock.name : 'Unknown';
}

}