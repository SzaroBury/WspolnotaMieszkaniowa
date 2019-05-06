using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Wspolnota.Models
{
    public class Announcement
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public ApplicationUser Author { get; set; }

        [Display(Name = "Creating Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [DataType(DataType.Upload)]
        public byte[] Image { get; set; }


        public Community Community { get; set; }
    }
}