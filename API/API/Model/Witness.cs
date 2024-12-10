namespace API.Model
{
    public class Witness
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int AccidentReportId { get; set; }
        public AccidentReport AccidentReport { get; set; }
    }
}
