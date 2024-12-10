namespace API.Model
{
    public class LaboratoryRequest
    {
        public int Id { get; set; }
        public DateTime DateOfRequest { get; set; }
        public string hospitalName { get; set; }
        public bool IsUrinalysisChecked { get; set; }
        public bool IsBacteriologyChecked { get; set; }
        public bool IsBiochemistryChecked { get; set; }
        public bool IsHematologyChecked { get; set; }
        public bool IsParasitologyChecked { get; set; }
        public bool IsSerologyChecked { get; set; }
        public bool IsElectrolyteChecked { get; set; }
        public bool IsCancerMarkerChecked { get; set; }
        public bool IsCardiacMarkerChecked { get; set; }
        public bool IsCoagulationChecked { get; set; }
        public bool IsHormoneChecked { get; set; }
        public bool IsOtherLabChecked { get; set; }
        public int PatientId { get; set; }
        public Patient Patients { get; set; }

        public int? ParasitologyId { get; set; } 
        public Parasitology Parasitology { get; set; }

        public int? HematologyId { get; set; } 
        public Hematology Hematology { get; set; }

        public int? BiochemistryId { get; set; }
        public Biochemistry Biochemistry { get; set; }

        public int? BacteriologyId { get; set; } 
        public Bacteriology Bacteriology { get; set; }

        public int? SerologyId { get; set; } 
        public Serology Serology { get; set; }


        public int? Electrolyteid { get; set; }
        public Electrolyte Electrolyte { get; set; }


        public int? CancerMarkerid { get; set; }
        public CancerMarker CancerMarker { get; set; }

        public int? CardiacMarkerid { get; set; }
        public CardiacMarker CardiacMarker { get; set; }

        public int? Coagulationid { get; set; }
        public Coagulation Coagulation { get; set; }

        public int? Hormoneid { get; set; }
        public Hormone Hormone { get; set; }

        public int? OtherLabid { get; set; }
        public OtherLab OtherLab { get; set; }
    }

}
