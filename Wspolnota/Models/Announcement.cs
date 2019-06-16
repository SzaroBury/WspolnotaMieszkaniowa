using System.ComponentModel.DataAnnotations;

namespace Wspolnota.Models
{
    public class Announcement : Post
    {
        [Key]
        public string Content { get; set; }
    }
}