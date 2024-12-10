import { Component, OnInit } from '@angular/core';
import { NgStyle } from '@angular/common';
import { IconDirective } from '@coreui/icons-angular';
import { ContainerComponent, RowComponent, ColComponent, CardGroupComponent, TextColorDirective, CardComponent, CardBodyComponent, FormDirective, InputGroupComponent, InputGroupTextDirective, FormControlDirective, ButtonDirective } from '@coreui/angular';
import { FormsModule } from '@angular/forms';
import { LoginRequest } from 'src/models/login-request.model';
import { UserService } from 'src/app/service/user.service';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';
import { LoginResponse } from 'src/models/login-response.model';
import { ToastrService } from 'ngx-toastr';
import { CommonModule } from '@angular/common';
import { ChangePassword } from 'src/Models/user.model';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  standalone: true,
  imports: [
    ContainerComponent,
    RowComponent,
    ColComponent,
    CardGroupComponent,
    TextColorDirective,
    CardComponent,
    CardBodyComponent,
    FormDirective,
    InputGroupComponent,
    InputGroupTextDirective,
    IconDirective,
    FormControlDirective,
    ButtonDirective,
    NgStyle,
    FormsModule,
    CommonModule
    
  ]
})
export class LoginComponent implements OnInit{
  model: LoginRequest = { email: '', password: '' };
  loading: boolean = false; // Loading state
  showChangePasswordModal: boolean = false; // Flag to show modal
  changePasswordDto: ChangePassword = {
    oldPassword: '',
    newPassword: '',
    confirmPassword: '',
    email: '' 
  };
  email: string = ''; 

  constructor(public userService: UserService, private router: Router, private toastr: ToastrService) {}

  ngOnInit(): void {
    localStorage.clear(); 
    
  }

 
  onFormSubmit(): void {
    this.userService.login(this.model).subscribe({
      next: (response) => {
        if (response && response.token) {
          // Check for default password
          if (this.model.password === 'Eep@123') {
            this.changePasswordDto.email = this.model.email; 
            this.changePasswordDto.oldPassword = this.model.password; 
            this.showChangePasswordModal = true; 
          } else {
            this.router.navigate(['dashboard']);
          }
        } else {
          this.toastr.error('Login failed. Please check your credentials.');
        }
      },
      error: () => {
        this.toastr.error('An error occurred during login.');
      }
    });
  }

  changePassword(): void {
    this.userService.changePassword(this.changePasswordDto).subscribe({
      next: (response) => {
        this.toastr.success(response.message);
        this.showChangePasswordModal = false;
  
      
        this.model.password = this.changePasswordDto.newPassword; 
        this.onFormSubmit();
      },
      error: (error) => {
        const message = error.error?.message || 'An error occurred while changing the password.';
        this.toastr.error(message);
        console.error('Change password error:', error);
      }
    });
  }
  
}