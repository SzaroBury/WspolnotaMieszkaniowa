using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace WspolnotaMieszkaniowa.Models
{
    public class Announcement
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        [Display(Name = "Creating Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public User Author { get; set; }

        [DataType(DataType.Upload)]
        public byte[] Image { get; set; }

        public List<User> Likes { get; set; }
        public List<Comment> Comments { get; set; }
    }
}