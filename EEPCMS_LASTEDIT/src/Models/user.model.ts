export interface User{
    email: string;
    roles: string[];
    firstName?: string; 
    lastName?: string;
}

export interface User2 {
    id: string;
    firstName?: string; 
    lastName?: string; 
    email: string;
    phoneNumber?: string; 
    roles: string[];     
    isActive: boolean; 

  }
  // src/models/change-password.model.ts
export interface ChangePassword {
    email: string;
    oldPassword: string;
    newPassword: string;
    confirmPassword: string;
  }
  