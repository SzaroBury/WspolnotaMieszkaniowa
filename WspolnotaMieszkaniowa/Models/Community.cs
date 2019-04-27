using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WspolnotaMieszkaniowa.Models
{
    public class Community
    {
        private int CommunityID { get; set; }
        private string Name { get; set; }

        private List<User> Users { get; set; }
        private List<Announcement> Announcements { get; set; }
    }
}