import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { LaboratoryRequest } from 'src/Models/labrequest.model';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from 'src/Models/user.model';
import { UserService } from 'src/app/service/user.service';

@Component({
  selector: 'app-labdetails',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './labdetails.component.html',
  styleUrl: './labdetails.component.scss'
})
export class LabdetailsComponent implements OnInit{
  request?: LaboratoryRequest;
  editMode: boolean = false;
  currentUser: User | null = null;

  constructor(private http: HttpClient, private route: ActivatedRoute, private router: Router,private service: UserService) {}

  ngOnInit(): void {
    this.service.currentUser.subscribe(user => {
      this.currentUser = user;
    });
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.fetchRequest(id);
  }
  
  fetchRequest(id: number): void {
    this.http.get<LaboratoryRequest>(`https://localhost:7292/api/LaboratoryRequests/${id}`)
      .subscribe(data => {
        this.request = data;
      });
  }

  saveChanges(): void {
    if (this.request) {
      this.http.put(`https://localhost:7292/api/LaboratoryRequests/${this.request.id}`, this.request)
        .subscribe(() => {
          this.editMode = false;
          alert('Changes saved successfully');
        });
    }
  }

  getCheckedItems(category?: any): { key: string, value: boolean }[] {
    if (!category) return [];
    return Object.keys(category)
      .filter(key => category[key] === true)
      .map(key => ({ key, value: category[key] }));
  }
  navigateToEdit(id: number): void {
    this.router.navigate(['/labedit', id]);
  }

  

  getEditableItems(category?: any): { key: string, value: boolean }[] {
    if (!category) return [];
    return Object.keys(category).map(key => ({ key, value: category[key] }));
  }

  printPage() {
    window.print();
  }
  
}