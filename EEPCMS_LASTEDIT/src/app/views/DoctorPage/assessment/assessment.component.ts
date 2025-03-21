import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../../../service/user.service';
import { PhysicalAssessment } from '../../../../Models/examiner.model';
import { CommonModule } from '@angular/common';
import { User, User2 } from '../../../../Models/user.model';

@Component({
  selector: 'app-assessment',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './assessment.component.html',
  styleUrls: ['./assessment.component.scss']
})
export class AssessmentComponent implements OnInit {
  physicalAssessments: PhysicalAssessment[] = [];
  filteredAssessments: PhysicalAssessment[] = [];
  paginatedAssessments: PhysicalAssessment[] = [];
  pageSize: number = 5;
  pageSizes: number[] = [5, 10, 25];
  currentPage: number = 1;
  totalPages: number = 1;
  pageNumbers: number[] = [];
  patientNames: { [key: number]: string } = {};
  constructor(public service: UserService, private router: Router) { }
  currentUser: User | null = null;

  ngOnInit(): void {

    this.service.currentUser.subscribe(user => {
      this.currentUser = user;
    });
    this.loadPhysicalAssessments();

  }


  loadPatientNames(): void {
    const patientIds = [...new Set(this.physicalAssessments.map(a => a.patientId))];
    patientIds.forEach(id => {
      this.service.getPatientBiId(id).subscribe(patient => { 
        this.patientNames[id] = patient.firstName;
      });
    });
  }
  
  loadPhysicalAssessments(): void {
    this.service.getAllAssess().subscribe(data => {
      this.physicalAssessments = data;
      this.filteredAssessments = data;
      this.updatePagination();
      this.loadPatientNames();
    });
  }

  edit(id: number): void {
    this.router.navigate(['/Assessmentedit', id]);
  }

  delete(id: number): void {
    if (confirm('Are you sure you want to delete this item?')) {
      this.service.deleteAssess(id).subscribe(() => {
        this.loadPhysicalAssessments();
      });
    }
  }

  filterAssessments(event: Event): void {
    const input = event.target as HTMLInputElement;
    const searchTerm = input.value.toLowerCase();
    this.filteredAssessments = this.physicalAssessments.filter(assessment =>
      assessment.heent.toLowerCase().includes(searchTerm) ||
      assessment.glands.toLowerCase().includes(searchTerm) ||
      assessment.chest.toLowerCase().includes(searchTerm) ||
      assessment.cvs.toLowerCase().includes(searchTerm) ||
      assessment.abdomen.toLowerCase().includes(searchTerm) ||
      assessment.genitoUninary.toLowerCase().includes(searchTerm) ||
      assessment.musculoSkeleton.toLowerCase().includes(searchTerm) ||
      assessment.skin.toLowerCase().includes(searchTerm) ||
      assessment.cns.toLowerCase().includes(searchTerm)
    );
    this.updatePagination();
  }

  onPageSizeChange(event: Event): void {
    const select = event.target as HTMLSelectElement;
    this.pageSize = +select.value;
    this.updatePagination();
  }

  changePage(page: number): void {
    if (page < 1 || page > this.totalPages) return;
    this.currentPage = page;
    this.updatePagination();
  }

  updatePagination(): void {
    const start = (this.currentPage - 1) * this.pageSize;
    const end = start + this.pageSize;
    this.paginatedAssessments = this.filteredAssessments.slice(start, end);
    this.totalPages = Math.ceil(this.filteredAssessments.length / this.pageSize);
    this.pageNumbers = Array.from({ length: this.totalPages }, (_, i) => i + 1);
  }



}
