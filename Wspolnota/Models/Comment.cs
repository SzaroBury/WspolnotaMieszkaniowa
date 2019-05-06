using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wspolnota.Models
{
    public class Comment
    {
        public int ID { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }

        public ApplicationUser Author { get; set; }
        public List<ApplicationUser> Likes { get; set; }
    }
}