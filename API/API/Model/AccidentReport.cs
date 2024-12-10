namespace API.Model
{
    public class AccidentReport
    {
        public int Id { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public string Department { get; set; }
        public DateTime InjuryDate { get; set; }
        public string InjuryTime { get; set; }
        public string CauseOfInjury { get; set; }
        public string IncidentDetails { get; set; }
        public string PreventiveAction { get; set; }
        public string ReporterName { get; set; }
        public string DepartmentHeadName { get; set; }
        public ICollection<Witness> Witnesses { get; set; }
    }
}
