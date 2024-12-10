using System.ComponentModel.DataAnnotations.Schema;

namespace API.Model
{
    public class Referral
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

        [ForeignKey(nameof(Examiner))]
        public int ExaminerId { get; set; }
        public virtual Examiner Examiner { get; set; }

    }

}
