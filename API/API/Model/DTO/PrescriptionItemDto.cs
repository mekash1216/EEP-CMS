// DTOs/PrescriptionItemDto.cs
public class PrescriptionItemDto
{
    public Guid Id { get; set; }
    public Guid PrescriptionId { get; set; }
    public Guid StockId { get; set; }
    //public string Stockname { get; set; }
    public int Quantity { get; set; }
    public bool StockAvailable { get; set; }
    public bool IsInternal { get; set; }
    public bool IsApproved { get; set; }
}

public class PrescriptionItemCreateDto
{
    public Guid? PrescriptionId { get; set; }
    public Guid StockId { get; set; }
    public int Quantity { get; set; }
}
