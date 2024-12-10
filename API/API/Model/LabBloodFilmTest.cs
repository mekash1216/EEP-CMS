namespace API.Model
{
    public class LabBloodFilmTest
    {
        public int Id { get; set; }
        public string Category { get; set; } 
        public string Test { get; set; } 
        public string Color { get; set; }
        public string Consistency {  get; set; }
        public string Selectionone {  get; set; }
        public string CellCount { get; set; }
        public int PatientId { get; set; } 
        public Patient Patient { get; set; }

    }
}
