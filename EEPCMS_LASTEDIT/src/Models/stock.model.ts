export interface Stock {
  id: string;
    name: string;
    code: string;
    price: number;
    quantity: number;
    manufacturer: string;
    expiryDate: Date;
    totalPrice: number;
  }
  
  export interface StockCreateDto {
    name: string;
    code: string;
    price: number;
    quantity: number;
    manufacturer: string;
    expiryDate: Date;
  }
  
  export interface StockUpdateDto {
    name: string;
    code: string;
    price: number;
    quantity: number;
    manufacturer: string;
    expiryDate: Date;
  }
  