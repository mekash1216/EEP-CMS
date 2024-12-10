// Models/PrescriptionItem.cs
using API.Models;

public class PrescriptionItem
{
    public Guid Id { get; set; }
    public Guid PrescriptionId { get; set; }
    public Prescription Prescription { get; set; }
    public Guid StockId { get; set; }
    //public string Stockname { get; set; }
    public Stock Stock { get; set; }
    public int Quantity { get; set; }
    public bool IsApproved { get; set; }
    public bool IsInternal { get; set; }
    public int ApprovedQuantity { get; set; }
    public bool StockAvailable { get; set; }
}
