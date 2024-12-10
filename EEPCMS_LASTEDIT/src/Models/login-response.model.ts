export interface LoginResponse{
    token: string;
    email: string;
    roles: string[];
    isActive: boolean;
    firstName?: string; 
    lastName?: string;
   
}