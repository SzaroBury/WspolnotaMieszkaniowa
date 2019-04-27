using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace WspolnotaMieszkaniowa.Models
{
    public class Brochure
    {
        private int BrochureID { get; set; }
        private string Author { get; set; }
        private string Location { get; set; }
        private Image Image { get; set; }

    }
}