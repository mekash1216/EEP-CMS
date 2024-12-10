namespace API.Model.DTO
{
    public class ReferralDto
    {
        public int Id { get; set; }
        public DateTime ReferralDate { get; set; }
        public string InvestigationResult { get; set; }
        public string Reason { get; set; }
        public string Rxgiven { get; set; }
        public string Diagnosis { get; set; }
        public string Clinicalfinding { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public ExaminerDto Examiner { get; set; }
    }

    public class ReferralCreateDto
    {
        public int Id { get; set; }
        public DateTime ReferralDate { get; set; }
        public string InvestigationResult { get; set; }
        public string Reason { get; set; }
        public string Rxgiven { get; set; }
        public string Diagnosis { get; set; }
        public string Clinicalfinding { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public int ExaminerId { get; set; }
    }


    public class ReferralupdateDto
    {
        public int Id { get; set; }
        public DateTime ReferralDate { get; set; }
        public string InvestigationResult { get; set; }
        public string Reason { get; set; }
        public string Rxgiven { get; set; }
        public string Diagnosis { get; set; }
        public string Clinicalfinding { get; set; }

    }

}
