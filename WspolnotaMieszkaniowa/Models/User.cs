using System;

namespace WspolnotaMieszkaniowa.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public char Gender { get; set; }

        public Community Community { get; set; }
    }
}