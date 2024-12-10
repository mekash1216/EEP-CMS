import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { LaboratoryRequest } from 'src/Models/labrequest.model';
import { DoctorService } from 'src/app/service/doctor.service';

@Component({
  selector: 'app-labedit',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './labedit.component.html',
  styleUrls: ['./labedit.component.scss']
})
export class LabeditComponent implements OnInit {
  request?: LaboratoryRequest;
  biochemistryItems: any[] = [];
  bacteriologyItems: any[] = [];
  serologyItems: any[] = [];
  electrolyteItems: any[] = [];
  cancerMarkerItems: any[] = [];
  cardiacMarkerItems: any[] = [];
  coagulationItems: any[] = [];
  hormoneItems: any[] = [];
  otherLabItems: any[] = [];
  parasitologyItems: any[] = [];
  hematologyItems: any[] = [];

  constructor(
    private route: ActivatedRoute,
    private labRequestService: DoctorService,
    private router: Router
  ) {}

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    if (id) {
      this.labRequestService.getRequestById(id).subscribe(
        data => {
          this.request = data;
          this.initializeItems();
        },
        error => {
          console.error('Error fetching request data', error);
        }
      );
    } else {
      console.error('Invalid ID');
    }
  }

  initializeItems(): void {
    if (this.request) {
      this.biochemistryItems = this.getCheckedItems(this.request.biochemistry);
      this.bacteriologyItems = this.getCheckedItems(this.request.bacteriology);
      this.serologyItems = this.getCheckedItems(this.request.serology);
      this.electrolyteItems = this.getCheckedItems(this.request.electrolyte);
      this.cancerMarkerItems = this.getCheckedItems(this.request.cancerMarker);
      this.cardiacMarkerItems = this.getCheckedItems(this.request.cardiacMarker);
      this.coagulationItems = this.getCheckedItems(this.request.coagulation);
      this.hormoneItems = this.getCheckedItems(this.request.hormone);
      this.otherLabItems = this.getCheckedItems(this.request.otherLab);
      this.hematologyItems = this.getCheckedItems(this.request.hematology);
      this.parasitologyItems = this.getCheckedItems(this.request.parasitology);
    }
  }

  getCheckedItems(items: any): any[] {
    if (!items) return [];
    return Object.keys(items).map(key => ({ key, value: items[key] }));
  }

  saveChanges(): void {
    if (!this.request) {
      console.error('No request data available to save');
      return;
    }

    const updatedRequest: LaboratoryRequest = {
      id: this.request.id,
      dateOfRequest: this.request.dateOfRequest,
      isBiochemistryChecked: this.request.isBiochemistryChecked,
      biochemistry: this.mapItemsToObject(this.biochemistryItems),
      isBacteriologyChecked: this.request.isBacteriologyChecked,
      bacteriology: this.mapItemsToObject(this.bacteriologyItems),
      isSerologyChecked: this.request.isSerologyChecked,
      serology: this.mapItemsToObject(this.serologyItems),
      isElectrolyteChecked: this.request.isElectrolyteChecked,
      electrolyte: this.mapItemsToObject(this.electrolyteItems),
      isCancerMarkerChecked: this.request.isCancerMarkerChecked,
      cancerMarker: this.mapItemsToObject(this.cancerMarkerItems),
      isCardiacMarkerChecked: this.request.isCardiacMarkerChecked,
      cardiacMarker: this.mapItemsToObject(this.cardiacMarkerItems),
      isCoagulationChecked: this.request.isCoagulationChecked,
      coagulation: this.mapItemsToObject(this.coagulationItems),
      isHormoneChecked: this.request.isHormoneChecked,
      hormone: this.mapItemsToObject(this.hormoneItems),
      isOtherLabChecked: this.request.isOtherLabChecked,
      otherLab: this.mapItemsToObject(this.otherLabItems),
      isParasitologyChecked: this.request.isParasitologyChecked,
      parasitology: this.mapItemsToObject(this.parasitologyItems),
      isHematologyChecked: this.request.isHematologyChecked,
      hematology: this.mapItemsToObject(this.hematologyItems),
      isUrinalysisChecked: this.request.isUrinalysisChecked,
      patientId: this.request.patientId || 0,
      hospitalName:this.request.hospitalName
    };

    console.log('Updated Request:', updatedRequest);

    this.labRequestService.updateRequest(this.request.id, updatedRequest).subscribe(
      response => {
        console.log('Request updated successfully', response);
        this.router.navigate(['/lablist']);
      },
      error => {
        console.error('Error updating request', error);
      }
    );
  }

  mapItemsToObject(items: any[]): any {
    const result: any = {};
    items.forEach(item => {
      result[item.key] = item.value;
    });
    return result;
  }
}
