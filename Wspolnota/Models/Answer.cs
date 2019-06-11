using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Wspolnota.Models
{
    public class Answer
    {
        [Key]
        public int AnswerId { get; set; }
        public int SurveyId { get; set; }
        public Survey Survey { get; set; }
        public string Content { get; set; }
    }
}