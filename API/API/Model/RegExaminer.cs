using System.ComponentModel.DataAnnotations.Schema;

namespace API.Model
{
    public class RegExaminer
    {


        public int Id { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Specialization { get; set; }

        [NotMapped]
        public ICollection<Examiner> Examiner { get; set; }
        public ICollection<Examiner> Examiners { get; set; } = new List<Examiner>();


    }

}

