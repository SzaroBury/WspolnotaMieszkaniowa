using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WspolnotaMieszkaniowa.Models
{
    public class AppDbContext:DbContext
    {
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Community> Communities { get; set; }
        public DbSet<User> Users { get; set; }
    }
}