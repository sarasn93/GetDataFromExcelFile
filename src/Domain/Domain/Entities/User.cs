using System;

namespace Domain.Entities
{
    public class User : EntityBase
    {
        public double UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Country { get; set; }
        public double Age { get; set; }
        public string Date { get; set; }

    }
}
