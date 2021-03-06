﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Wspolnota.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
        public int AuthorId { get; set; }
        public ApplicationUser Author { get; set; }
        public string Content { get; set; }
        [Display(Name = "Creating Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedAt { get; set; }

        public List<ApplicationUser> Likes { get; set; }
    }
}