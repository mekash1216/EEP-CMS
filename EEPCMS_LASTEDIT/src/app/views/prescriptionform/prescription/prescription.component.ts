import { CommonModule } from '@angular/common';
import { Component, ElementRef, EventEmitter, Input, OnInit, Output, ViewChild, viewChild } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Prescription, PrescriptionItem } from 'src/Models/prescription.model';
import { PrescriptionService } from 'src/app/service/prescription.service';
import { PrescriptionItemComponent } from 'src/app/views/prescriptionform/prescription-item/prescription-item.component';
import { RouterModule } from '@angular/router';
import { Examiner } from 'src/Models/examiner.model';
import { Stock } from 'src/Models/stock.model';
import { StockService } from 'src/app/service/stock.service';
@Component({
  selector: 'app-prescription',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    PrescriptionItemComponent,
    RouterModule ],
  templateUrl: './prescription.component.html',
  styleUrl: './prescription.component.scss'
})
export class PrescriptionComponent implements OnInit {
goBack() {
  this.closeModal();
}

  @Input() selectedPatient: Examiner | null = null;
  @Output() onClose: EventEmitter<void> = new EventEmitter<void>();
 prescriptionForm: FormGroup;
 createdPrescriptionId: string | null = null;
 savedItems: any[] = []; // To store saved items
  showPrescriptionItemForm: boolean = false;
  modalOpen: boolean = false;
  stocks: Stock[] = [];
  selectedAssignment: PrescriptionItem | null = null;
  @ViewChild(PrescriptionItemComponent) addItemDialog!: PrescriptionItemComponent
  prescriptionItems: any[] = [];

  constructor(
    private fb: FormBuilder,
    private prescriptionService: PrescriptionService,
    private toastr: ToastrService,
    private stockservice: StockService
   
  ) {
   
    // Initialize the form group in the constructor
    this.prescriptionForm = this.fb.group({
      patientId:[''],
      date: ['', Validators.required],
      weight: [''],
      isInpatient: [false],
      isOutpatient: [false],
      isEmergency: [false],
      diagnosis: ['', Validators.required]
    });
  }


 
  ngOnInit(): void {
  this.loadstock()
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

  onSubmit(): void {
    if (this.prescriptionForm.valid) {
      this.prescriptionForm.patchValue({
        patientId: this.selectedPatient?.patientId,
        // nameOfPatient: `${this.selectedPatient?.patientFirstName} ${this.selectedPatient?.patientLastName}`,
        // age: this.selectedPatient?.patientAge,
         weight: this.selectedPatient?.weight,
      });
  
      console.log(this.prescriptionForm);
      this.savePrescriptionAndItemForms(); 
    }
  }
  
  

  addPrescriptionItem() {
    this.modalOpen = true;
    const sub = this.addItemDialog.formSubmitted.subscribe((data: any) => {
    console.log(data)
      this.prescriptionItems.push(data);
      sub.unsubscribe();
    });
  }



  savePrescriptionAndItemForms(): void {
     // Save prescription form
    const prescription: Prescription = this.prescriptionForm.value;
    prescription.prescriptionItems = this.prescriptionItems;
    this.prescriptionService.createPrescription(prescription).subscribe(response => {
      this.createdPrescriptionId = response.id; // Assuming the response contains the created prescription ID
      this.toastr.success('Prescription created successfully', 'Success');
     
    });
  }
  close(): void {
    this.onClose.emit();
  }
  closeModal() {
    this.modalOpen = false;
  }

  private savePrescriptionForm(): void {
    // Update the existing prescription if already created
    const prescriptionId = this.createdPrescriptionId!;
    const updatedPrescription: Prescription = this.prescriptionForm.value;

    this.prescriptionService.updatePrescription(prescriptionId, updatedPrescription).subscribe(response => {
      this.toastr.success('Prescription updated successfully', 'Success');
    });
  }
  // Method to remove a saved item
  removeSavedItem(item: any): void {
    // Logic to remove the saved item, e.g., by making an API call to delete it
    this.prescriptionService.deletePrescriptionItem(item.id).subscribe(() => {
      this.savedItems = this.savedItems.filter(i => i !== item);
      this.toastr.success('Item removed successfully', 'Success');
    }, error => {
      this.toastr.error('Failed to remove item', 'Error');
      console.error('Error removing item:', error);
    });
  }
    // Method to edit a saved item
    editSavedItem(item: any): void {
      const index = this.savedItems.indexOf(item);
      if (index !== -1) {
        this.prescriptionItems.at(0).patchValue(item); // Assuming single item form for editing
        this.removeSavedItem(item); // Remove the item from saved items list for editing
      }
}

getStockName(stockId: string ): string {
  const stock = this.stocks.find(s => s.id === stockId);
  return stock ? stock.name : 'Unknown';
}

removePrescriptionItem(index: number): void {
  if (index > -1 && index < this.prescriptionItems.length) {
    this.prescriptionItems.splice(index, 1);
  }
}


}