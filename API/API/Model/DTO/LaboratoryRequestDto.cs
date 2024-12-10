namespace API.Model.DTO
{
    public class LaboratoryRequestDto
    {
        public int Id { get; set; }
        public DateTime DateOfRequest { get; set; }
        public bool IsUrinalysisChecked { get; set; }
        public int PatientId { get; set; }
        //public PatientDTO Patient { get; set; }
        public string hospitalName { get; set; }
        public ParasitologyDto Parasitology { get; set; }
        public bool IsParasitologyChecked { get; set; }
        public HematologyDto Hematology { get; set; }
        public bool IsHematologyChecked { get; set; }
        public BiochemistryDto Biochemistry { get; set; }
        public bool IsBiochemistryChecked { get; set; }
        public BacteriologyDto Bacteriology { get; set; }
        public bool IsBacteriologyChecked { get; set; }
        public SerologyDto Serology { get; set; }
        public bool IsSerologyChecked { get; set; }
        public bool IsElectrolyteChecked { get; set; }
        public ElectrolyteDto Electrolyte { get; set; }
        public bool IsCancerMarkerChecked { get; set; }

        public CancerMarkerDto CancerMarker { get; set; }
        public bool IsCardiacMarkerChecked { get; set; }
        public CardiacMarkerDto CardiacMarker { get; set; }
        public bool IsCoagulationChecked { get; set; }

        public CoagulationDto Coagulation { get; set; }
        public bool IsHormoneChecked { get; set; }
        public HormoneDto Hormone { get; set; }
        public bool IsOtherLabChecked { get; set; }

        public OtherLabDto OtherLab { get; set; }


    }
}