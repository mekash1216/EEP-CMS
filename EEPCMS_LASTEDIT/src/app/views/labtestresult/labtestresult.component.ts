import { HttpClient } from '@angular/common/http';
import { ChangeDetectorRef, Component, Input, OnInit } from '@angular/core';
import { LabtestresultdetailComponent } from '../labtestresultdetail/labtestresultdetail.component';
import { CommonModule } from '@angular/common';
import { NgxPaginationModule } from 'ngx-pagination'; // Import ngx-pagination module
import { UserService } from '../../../app/service/user.service';

@Component({
  selector: 'app-labtestresult',
  standalone: true,
  imports: [LabtestresultdetailComponent, CommonModule, NgxPaginationModule], // Add NgxPaginationModule here
  templateUrl: './labtestresult.component.html',
  styleUrls: ['./labtestresult.component.scss'] 
})
export class LabtestresultComponent implements OnInit {
  specialTests = ['bloodFilm', 'peripheralMorphology', 'bloodGroup', 'malariaByAgCard', 'weilFelix', 'widal', 'afb', 'gramStrin', 'koh'];
  specialTestData: any[] = [];
  nonSpecialTestData: any[] = [];
  combinedData: any[] = [];
  filteredData: any[] = [];
  selectedTest: any;
  isSpecial: boolean | null = null;
  currentPage: number = 1;
  pageSize: number = 5;
  totalItems: number = 0;
  filterQuery: string = '';
  isDetailVisible: boolean = false;
  patientNames: { [key: number]: string } = {}; 

  constructor(private http: HttpClient,private cdr: ChangeDetectorRef, private patientService: UserService) {}

  ngOnInit(): void {
    this.loadPatients();
    this.loadSpecialTests();
    this.loadNonSpecialTests();
  }

  loadSpecialTests(): void {
    this.http.get<any[]>('http://localhost:5153/api/LabBloodFilmTest').subscribe(data => {
      this.specialTestData = data;
      this.combineData();
    });
  }

  loadNonSpecialTests(): void {
    this.http.get<any[]>('http://localhost:5153/api/LaboratoryTestResult').subscribe(data => {
        this.nonSpecialTestData = data;
        this.combineData();
  
    });
}


  combineData(): void {
    this.combinedData = [
        ...this.specialTestData.map(test => ({ ...test, isSpecial: true })),
        ...this.nonSpecialTestData.map(test => ({ ...test, isSpecial: false }))
    ];
    this.totalItems = this.combinedData.length;
    this.applyFilters();
}
loadPatients(): void {
  this.patientService.getPatients().subscribe(patients => {
    patients.forEach(patient => {
      if (patient.id !== undefined && patient.id !== null) { 
        this.patientNames[patient.id] = `${patient.firstName} ${patient.lastName}`;
      }
    });
  });
}


applyFilters(): void {
    let filtered = this.combinedData;
    if (this.filterQuery) {
        filtered = filtered.filter(test =>
            test.Category.toLowerCase().includes(this.filterQuery.toLowerCase()) ||
            test.TestName.toLowerCase().includes(this.filterQuery.toLowerCase())
        );
    }
    this.filteredData = filtered;
   
}


  viewDetails(test: any): void {
    this.selectedTest = test;
    this.isSpecial = test.isSpecial;
  }

  onPageChange(page: number): void {
    this.currentPage = page;
  }

  onPageSizeChange(event: Event): void {
    const target = event.target as HTMLSelectElement;
    this.pageSize = +target.value;
    this.currentPage = 1; // Reset to first page on page size change
    this.applyFilters();
  }

  onFilterChange(event: Event): void {
    const target = event.target as HTMLInputElement;
    this.filterQuery = target.value;
    this.applyFilters();
  }

  toggleDetails(test: any): void {
    if (this.selectedTest === test) {
      this.isDetailVisible = !this.isDetailVisible;
    } else {
      this.selectedTest = test;
      this.isSpecial = test.isSpecial;
   
      this.isDetailVisible = true;
    }
    
    this.cdr.detectChanges(); 
  }
  
  
  checkIfSpecial(test: any): boolean {
    // Replace 'specialCriteria' with the correct property that determines if the test is special
    return test.specialCriteria === true;
  }
  onDeleteTest(): void {
    if (this.selectedTest) {
      const apiUrl = this.selectedTest.isSpecial
        ? `http://localhost:5153/api/LabBloodFilmTest/${this.selectedTest.id}`
        : `http://localhost:5153/api/LaboratoryTestResult/${this.selectedTest.id}`;

      this.http.delete(apiUrl).subscribe({
        next: () => {
          alert('Test deleted successfully');
          this.removeDeletedTestFromList();
          this.selectedTest = null; // Hide the details
          this.isDetailVisible = false; // Hide the detail component
        },
        error: () => {
          alert('Failed to delete the test');
        }
      });
    }
  }

  removeDeletedTestFromList(): void {
    if (this.selectedTest) {
      this.combinedData = this.combinedData.filter(test => test.id !== this.selectedTest.id);
      this.applyFilters(); // Reapply filters to update the displayed list
    }
  }
}
