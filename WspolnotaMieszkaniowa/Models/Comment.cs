using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WspolnotaMieszkaniowa.Models
{
    public class Comment
    {
        public int ID { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }

        public User Author { get; set; }
        public List<User> Likes { get; set; }
    }
}