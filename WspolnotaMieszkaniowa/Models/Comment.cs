using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WspolnotaMieszkaniowa.Models
{
    public class Comment
    {
        private int ID { get; set; }
        private string Content { get; set; }
        private DateTime Date { get; set; }

        private User Author { get; set; }
        private List<User> Likes { get; set; }
    }
}