using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Wspolnota.Models
{
    public class Community
    {
        [Key]
        public int CommunityID { get; set; }

        [Required]
        public string Name { get; set; }

        public string Image { get; set; }

        public List<Announcement> Announcements { get; set; }
    }
}