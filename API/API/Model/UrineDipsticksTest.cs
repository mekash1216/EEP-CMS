namespace API.Model
{
    public class UrineDipsticksTest
    {
        public int Id { get; set; }
        public decimal PH { get; set; }
        public decimal SpecificGravity { get; set; }
        public string LeukocyteEsterase { get; set; } 
        public string? LeukocyteEsteraseIntensity { get; set; }  

        public string Nitrite { get; set; }
        public string? NitriteIntensity { get; set; }
        public string Urobilinogen { get; set; }
        public string? UrobilinogenIntensity { get; set; }
        public string Protein { get; set; }  
        public string? ProteinIntensity { get; set; } 

        public string Glucose { get; set; }  
        public string? GlucoseIntensity { get; set; }  

        public string BloodHemoglobin { get; set; }  
        public string? BloodHemoglobinIntensity { get; set; }  

        public string Ketones { get; set; } 
        public string? KetonesIntensity { get; set; }  

        public string Bilirubin { get; set; } 
        public string? BilirubinIntensity { get; set; }  
        public DateTime TestDate { get; set; }

    }

}
