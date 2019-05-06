using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Wspolnota.Models
{
    public class Community
    {
        public int CommunityID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Upload)]
        public byte[] Image { get; set; }

        public List<Announcement> Announcements { get; set; }
        public List<ApplicationUser> Users { get; set; }
    }
}