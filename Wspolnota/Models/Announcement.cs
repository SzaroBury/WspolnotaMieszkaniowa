using System.ComponentModel.DataAnnotations;

namespace Wspolnota.Models
{
    public class Announcement : Post
    {
        [Key]
        public int AnnouncementId { get; set; }
        public string Content { get; set; }
    }
}