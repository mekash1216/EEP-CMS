import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Stock } from 'src/Models/stock.model';
import { PrescriptionService } from 'src/app/service/prescription.service';
import { FormsModule } from '@angular/forms';
import { PrescriptionItem } from 'src/Models/prescription.model';
@Component({
  selector: 'app-prescription-item',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule,
    FormsModule

  ],
  templateUrl: './prescription-item.component.html',
  styleUrls: ['./prescription-item.component.scss']
})
export class PrescriptionItemComponent implements OnInit {
  @Input() prescriptionId: string | null = null;
  @Output() formSubmitted: EventEmitter<any> = new EventEmitter<any>();
  @Input() stock: Stock[] = [];
  selectedStockId: string  | null = null; 
  stocks: Stock[] = []; 
  selectedStock: Stock | null = null; 
  prescriptionItemForm: FormGroup;
  isApproved: boolean = false;
  modalOpen: boolean = false;
  savedItems: any[] = []; 
  @Input() selectedAssignment: PrescriptionItem | null = null; 
  hideStockId: boolean = false; 
  isExpired = false;
  isExpiringSoon = false;
  constructor(
    private fb: FormBuilder,
    private prescriptionService: PrescriptionService,
    private toastr: ToastrService
  ) {
    this.prescriptionItemForm = this.fb.group({
      prescriptionItems: this.fb.array([])
    });
  }

  ngOnInit(): void {
    this.hideStockId = true; 
    this.addPrescriptionItem();

  }
  
  checkExpiry(event: Event) {
    const target = event.target as HTMLSelectElement;
    const stockId = target.value;

    this.isExpired = false;
    this.isExpiringSoon = false;
    const selectedStock = this.stock.find(s => s.id === stockId);
    if (selectedStock) {
      const today = new Date();
      const expiryDate = new Date(selectedStock.expiryDate);
  
      this.isExpired = expiryDate < today; 
      this.isExpiringSoon = !this.isExpired && (expiryDate.getTime() - today.getTime()) / (1000 * 3600 * 24) <= 10; 
    }
  }
  

  get prescriptionItems(): FormArray {
    return this.prescriptionItemForm.get('prescriptionItems') as FormArray;
  }

  // Method to add a prescription item to the FormArray
  addPrescriptionItem(): void {
    const prescriptionItemGroup = this.fb.group({
      stockId: ['', Validators.required],
      // stockname:[''],
      quantity: ['', Validators.required]
    });
    
    this.prescriptionItems.push(prescriptionItemGroup);
  }

  editPrescriptionItem(index: number): void {
    console.log(`Editing item at index ${index}`);
  }

  // Method to remove a prescription item from the FormArray
  removePrescriptionItem(index: number): void {
    this.prescriptionItems.removeAt(index);
  }

  // Method to remove a saved item
  removeSavedItem(item: any): void {
    this.prescriptionService.deletePrescriptionItem(item.id).subscribe(() => {
      this.savedItems = this.savedItems.filter(i => i !== item);
      this.toastr.success('Item removed successfully', 'Success');
    }, error => {
      this.toastr.error('Failed to remove item', 'Error');
      console.error('Error removing item:', error);
    });
  }

  getStockName(stockId: string ): string {
    const stock = this.stocks.find(s => s.id === stockId);
    return stock ? stock.name : 'Unknown';
  }

  // Method to edit a saved item
  editSavedItem(item: any): void {
    const index = this.savedItems.indexOf(item);
    if (index !== -1) {
      this.prescriptionItems.at(0).patchValue(item);
      this.removeSavedItem(item); 
    }
  }

  onSubmit(): void {
 
    if (this.prescriptionItemForm.valid) {
      
      this.formSubmitted.emit(this.prescriptionItemForm.value.prescriptionItems[0]);
      // // Process each prescription item
      // const prescriptionItems = this.prescriptionItemForm.value.prescriptionItems;
      // const savePromises = prescriptionItems.map((item: any) => {
      //   // Add prescriptionId to each item
      //   item.prescriptionId = this.prescriptionId;
      //   return this.prescriptionService.createPrescriptionItem(item).toPromise();
      // });

      // // Wait for all items to be saved
      // Promise.all(savePromises).then((savedItems) => {
      //   // Display success message
      //   this.toastr.success('Prescription items added successfully', 'Success');

      //   // Update savedItems with the response
      //   this.savedItems = [...this.savedItems, ...savedItems];

      //   // Clear the form array and add a new empty item
      //   this.prescriptionItems.clear();
      //   this.addPrescriptionItem();

      //   // Close modal or perform additional actions as needed
      //   this.modalOpen = false;
      // }).catch(error => {
      //   this.toastr.error('Failed to add prescription items', 'Error');
      //   console.error('Error adding prescription items:', error);
      // });


      this.closeModal();
    }
    // this.formSubmitted.emit();
  }

  closeModal() {
    this.modalOpen = false;
  }
  
}
