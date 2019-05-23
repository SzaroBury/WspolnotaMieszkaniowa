using System.ComponentModel.DataAnnotations;

namespace Wspolnota.Models
{
    public class Brochure : Post
    {
        [Key]
        public int BrochureId { get; set; }
        public string Link { get; set; }
        public string Image { get; set; }

    }
}