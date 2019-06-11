using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Wspolnota.Models
{
    public class Survey : Post
    {
        [Key]
        public int SurveyId { get; set; }

        public List<Answer> Answers { get; set; }
        public List<Vote> Votes { get; set; }

        //public bool Anonymous { get; set; }
        //public  bool AddingOwnAnswer { get; set; }
    }
}