using System;

namespace WspolnotaMieszkaniowa.Models
{
    public class User
    {
        private int UserID { get; set; }
        private string FirstName { get; set; }
        private string LastName { get; set; }
        private string Address { get; set; }
        private string Email { get; set; }
        private DateTime BirthDate { get; set; }
        private char Gender { get; set; }

        private Community Community { get; set; }
    }
}