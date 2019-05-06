using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WspolnotaMieszkaniowa.Models
{
    public class Community
    {
        public int CommunityID { get; set; }
        public string Name { get; set; }

        public List<User> Users { get; set; }
        public List<Announcement> Announcements { get; set; }
    }
}