using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wspolnota.Models
{
    public class Survey
    {
        private int SurveyID { get; set; }
        private ApplicationUser Author { get; set; }
        private string Question { get; set; }
        private Dictionary<ApplicationUser, string> Answers { get; set; }
        private DateTime SurveyDate { get; set; }
        private List<Comment> Comments { get; set; }
        private List<ApplicationUser> Likes { get; set; }

        private bool Anonymous { get; set; }
        private bool AddingOwnAnswer { get; set; }
    }
}