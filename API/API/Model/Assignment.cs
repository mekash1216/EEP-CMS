using System.ComponentModel.DataAnnotations.Schema;

namespace API.Model
{
    public class Assignment
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public DateTime AssignedDate { get; set; }
        // public int PatientAge { get; set; }
        public float Weight { get; set; }
        public float SystolicPressure { get; set; }
        public float DiastolicPressure { get; set; }
        public float RespiratoryRate { get; set; }
        public float PulseRate { get; set; }
        public float Temperature { get; set; }
        public float SpO2 { get; set; }
        public string? Triage { get; set; }
        public Patient Patient { get; set; }

        [NotMapped]
        public string RespiratoryRateCategory => GetRespiratoryRateCategory(RespiratoryRate);
        [NotMapped]
        public string PulseRateCategory => GetPulseRateCategory(PulseRate);
        [NotMapped]
        public string BloodPressureCategory => GetBloodPressureCategory(SystolicPressure, DiastolicPressure);
        [NotMapped]
        public string TemperatureCategory => GetTemperatureCategory(Temperature);
        [NotMapped]
        public string SpO2Category => GetSpO2Category(SpO2);

        [NotMapped]
        public ICollection<Examiner> Examiner { get; set; }
        public ICollection<Examiner> Examiners { get; set; } = new List<Examiner>();

        private string GetRespiratoryRateCategory(float rate)
        {
            if (rate > 20) return "Tachypnea";
            if (rate >= 16 && rate <= 20) return "Normal";
            return "Bradypnea";
        }

        private string GetPulseRateCategory(float rate)
        {
            if (rate >= 100) return "Tachycardia";
            if (rate >= 60 && rate < 100) return "Normal";
            return "Bradycardia";
        }

        private string GetBloodPressureCategory(float systolic, float diastolic)
        {
            
            if (systolic >= 140 || diastolic >= 90) return "Hypertension";
            if (systolic >= 90 && diastolic >= 60) return "Normal";
            return "Hypotension";
        }

        private string GetTemperatureCategory(float temp)
        {
            if (temp > 37.5) return "Fever";
            if (temp >= 36.5 && temp <= 37.5) return "Normal";
            return "Hypothermia";
        }

        private string GetSpO2Category(float spo2)
        {
            if (spo2 < 90) return "Desaturation";
            if (spo2 >= 90 && spo2 <= 100) return "Normal";
            return "Unknown";
        }
    }
}
