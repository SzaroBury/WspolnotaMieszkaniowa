using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Wspolnota.Models
{
    public class Community
    {
        public int CommunityID { get; set; }

        [Required]
        public string Name { get; set; }

        public string Image { get; set; }

        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
    }
}