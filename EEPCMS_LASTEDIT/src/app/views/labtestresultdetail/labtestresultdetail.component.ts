import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-labtestresultdetail',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './labtestresultdetail.component.html',
  styleUrls: ['./labtestresultdetail.component.scss']
})
export class LabtestresultdetailComponent implements OnInit {
  @Input() test: any;
  @Input() isSpecial: boolean | null = null;
  @Output() delete = new EventEmitter<void>();

  pagedSubTests: any[] = [];
  currentPage: number = 1;
  itemsPerPage: number = 5;

  ngOnInit(): void {
    this.updatePagedSubTests();
  }

  // Updates the sub-tests for the current page
  updatePagedSubTests(): void {
    if (this.test && this.test.test === 'cbc') {
      const startIndex = (this.currentPage - 1) * this.itemsPerPage;
      const endIndex = startIndex + this.itemsPerPage;
      this.pagedSubTests = this.test.subTests.slice(startIndex, endIndex);
    }
  }

  // Navigate to next page
  nextPage(): void {
    if (this.hasNextPage()) {
      this.currentPage++;
      this.updatePagedSubTests();
    }
  }

  // Navigate to previous page
  previousPage(): void {
    if (this.hasPreviousPage()) {
      this.currentPage--;
      this.updatePagedSubTests();
    }
  }

  // Check if next page is available
  hasNextPage(): boolean {
    return this.test && this.test.test === 'cbc' && this.currentPage * this.itemsPerPage < this.test.subTests.length;
  }

  // Check if previous page is available
  hasPreviousPage(): boolean {
    return this.currentPage > 1;
  }

  // Calculate total pages
  get totalPages(): number {
    if (this.test && this.test.test === 'cbc') {
      return Math.ceil(this.test.subTests.length / this.itemsPerPage);
    }
    return 1;
  }

  onDelete(): void {
    this.delete.emit(); // Emit an event to notify the parent component about the delete action
  }
}
