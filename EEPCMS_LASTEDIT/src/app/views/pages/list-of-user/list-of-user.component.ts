import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/service/user.service';
import { CommonModule } from '@angular/common';
import { User, User2 } from 'src/Models/user.model';
import { FormsModule } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-list-of-user',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './list-of-user.component.html',
  styleUrls: ['./list-of-user.component.scss']
})
export class ListOfUserComponent implements OnInit {
  users: User2[] = [];
  filteredUsers: User2[] = [];
  paginatedUsers: User2[] = [];
  loading: boolean = true;
  showModal: boolean = false;
  selectedUserId!: string;
  isPasswordVisible: boolean = false;
  currentUser: User | null = null;
  currentPage: number = 1;
  itemsPerPage: number = 4; 
  totalPages: number = 0;
  searchTerm: string = '';

  newUser: any = {
    email: '',
    firstName: '',
    lastName: '',
    phoneNumber: '',
    role: ''
  };
  roles: string[] = ['Admin', 'manager','StockMan','lab','doctor','reception','preliminary'];

  constructor(private authService: UserService, private toastr: ToastrService) {}

  ngOnInit() {
    this.authService.currentUser.subscribe(user => {
      this.currentUser = user;
    });
    this.fetchUsers();
  }


  fetchUsers() {
    this.authService.getAllUsers().subscribe(
      (data: User2[]) => {
        this.users = data;
        this.filteredUsers = data; 
        this.loading = false;
        this.calculateTotalPages();
        this.updatePaginatedUsers();
      },
      (error) => {
        console.error('Error fetching users:', error);
        this.loading = false;
      }
    );
  }

  filterUsers() {
    if (this.searchTerm) {
      this.filteredUsers = this.users.filter(user =>
        user.email.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
        user.firstName?.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
        user.lastName?.toLowerCase().includes(this.searchTerm.toLowerCase())
      );
    } else {
      this.filteredUsers = this.users;
    }
    this.currentPage = 1; 
    this.calculateTotalPages();
    this.updatePaginatedUsers();
  }

  calculateTotalPages() {
    this.totalPages = Math.ceil(this.filteredUsers.length / this.itemsPerPage);
  }

  updatePaginatedUsers() {
    const start = (this.currentPage - 1) * this.itemsPerPage;
    this.paginatedUsers = this.filteredUsers.slice(start, start + this.itemsPerPage);
  }

  nextPage() {
    if (this.currentPage < this.totalPages) {
      this.currentPage++;
      this.updatePaginatedUsers();
    }
  }

  prevPage() {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.updatePaginatedUsers();
    }
  }

  togglePasswordVisibility() {
    this.isPasswordVisible = !this.isPasswordVisible; 
  }

  openModal() {
    this.resetForm();
    this.showModal = true;
  }

  closeModal() {
    this.showModal = false;
    this.resetForm();
  }

  resetForm() {
    this.newUser = {
      email: '',
      firstName: '',
      lastName: '',
      phoneNumber: '',
      role: ''
    };
  }

  registerUser() {
    const userToRegister = {
      ...this.newUser,
      isActive: true
    };

    const observable = this.selectedUserId 
      ? this.authService.updateProfile(this.selectedUserId, userToRegister) 
      : this.authService.registerloginUser(userToRegister);

    observable.subscribe(
      (response: any) => {
        this.toastr.success(response.message || 'Operation successful', 'Success');
        this.closeModal();
        this.fetchUsers();

        if (this.selectedUserId) {
          this.users = this.users.map(user => 
            user.id === this.selectedUserId ? { ...user, ...userToRegister } : user
          );
        } else {
          this.users.push({ ...userToRegister, id: response.id }); 
        }
      },
      (error) => {
        console.error('Error updating user:', error);
        this.toastr.error('Error updating user: ' + (error.error?.message || error.message || 'Unknown error'));
      }
    );
  }

  toggleActivation(user: User2) {
    const updatedStatus = !user.isActive;
    this.authService.updateUserStatus(user.id, updatedStatus).subscribe(
      (response) => {
        user.isActive = updatedStatus;
        this.toastr.success(`User status updated to ${updatedStatus ? 'Active' : 'Inactive'}.`, 'Success');
      },
      (error) => {
        console.error('Error updating user status:', error);
        user.isActive = !updatedStatus; // Revert status on error
        this.toastr.error('Error updating user status', 'Error');
      }
    );
  }

  editUser(user: User2) {
    this.selectedUserId = user.id; 
    this.newUser = {
      email: user.email,
      firstName: user.firstName,
      lastName: user.lastName,
      phoneNumber: user.phoneNumber,
      role: user.roles && user.roles.length > 0 ? user.roles[0] : ''
    };
    this.showModal = true;
  }

  confirmDelete(userEmail: string) {
    if (confirm('Are you sure you want to delete this user?')) {
      this.deleteUser(userEmail); 
    }
  }

  deleteUser(userEmail: string): void {
    this.authService.deletelogUser(userEmail).subscribe(
      (response: string) => {
        this.toastr.success('Successfully Deleted');
        this.fetchUsers(); 

        const currentUserEmail = localStorage.getItem('user-email');
        if (currentUserEmail === userEmail) {
          this.authService.logout(); 
        }
      },
      (error) => {
        console.error('Error deleting user:', error);
        this.toastr.error('Error deleting user: ' + (error.error.message || 'Unknown error.'));
      }
    );
  }
}
