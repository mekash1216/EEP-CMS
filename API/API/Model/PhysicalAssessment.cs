namespace API.Model
{
    public class PhysicalAssessment
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string HEENT { get; set; }
        public string Glands { get; set; }
        public string Chest { get; set; }
        public string CVS { get; set; }
        public string Abdomen { get; set; }
        public string GenitoUninary { get; set; }
        public string MusculoSkeleton { get; set; }
        public string Skin { get; set; }
        public string CNS { get; set; }
        public string? Assessment { get; set; }
        public DateTime Date { get; set; }
    }
}
