using System.ComponentModel.DataAnnotations;

namespace Wspolnota.Models
{
    public class SurveyAnswer
    {
        [Key]
        public int AnswerId { get; set; }
        public int SurveyId { get; set; }
        public int Answer { get; set; }
        public Survey Survey { get; set; }
        public int AuthorId { get; set; }
        public ApplicationUser Author { get; set; }
    }
}