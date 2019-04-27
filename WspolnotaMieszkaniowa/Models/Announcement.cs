using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WspolnotaMieszkaniowa.Models
{
    public class Announcement:Comment
    {
        private List<Comment> Comments { get; set; }
        private string Image { get; set; }
    }
}