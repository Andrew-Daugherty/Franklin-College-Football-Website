using System;
using System.Collections.Generic;

namespace FCFootball.Models
{
    public partial class Staff
    {
        public int StaffID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Role { get; set; }
        public string? EmailAddress { get; set; }
        public string? Password { get; set; }
        public string? Image { get; set; }
    }
}
