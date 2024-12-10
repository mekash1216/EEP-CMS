namespace API.Model.DTO
{
    public class HormoneDto
    {
        public int Id { get; set; }
        public bool TSH { get; set; }
        public bool FreeT4 { get; set; }
        public bool FreeT3 { get; set; }
        public bool TotalT4 { get; set; }
        public bool TotalT3 { get; set; }
        public bool TotalBetaHCGT3 { get; set; }
        public bool PSA { get; set; }
        public bool FSH { get; set; }
        public bool LH { get; set; }
        public bool Prolactin { get; set; }
    }
}
