using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Wspolnota.Models
{
    public abstract class Post
    {
        [Key]
        public int PostId { get; set; }
        public string Title { get; set; }
        public string AuthorId { get; set; }
        public ApplicationUser Author { get; set; }
        [Display(Name = "Creating Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedAt { get; set; }
        public int CommunityId { get; set; }
        public Community Community { get; set; }

        public List<Comment> Comments { get; set; }
        public List<ApplicationUser> Likes { get; set; }
    }
}