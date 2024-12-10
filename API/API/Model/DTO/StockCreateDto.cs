// API.Model.DTO.StockCreateDto.cs
using System;

namespace API.Model.DTO
{
    public class StockCreateDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Manufacturer { get; set; }
        public DateTime ExpiryDate { get; set; }
    }



    public class StockReadDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Manufacturer { get; set; }
        public DateTime ExpiryDate { get; set; }
        public decimal TotalPrice { get; set; }  // Added for total price display
    }



    public class StockUpdateDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Manufacturer { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
