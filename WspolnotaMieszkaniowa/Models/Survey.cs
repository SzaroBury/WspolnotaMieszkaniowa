using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WspolnotaMieszkaniowa.Models
{
    public class Survey
    {
        private int SurveyID { get; set; }
        private User Author { get; set; }
        private string Question { get; set; }
        private Dictionary<User, string> Answers { get; set; }
        private DateTime SurveyDate { get; set; }
        private List<Comment> Comments { get; set; }
        private List<User> Likes { get; set; }

        private bool Anonymous { get; set; }
        private bool AddingOwnAnswer { get; set; }
    }
}