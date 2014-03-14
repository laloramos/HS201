using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HS201_FinalAssignment.Domain.Entities
{
    public class Attendee
    {
        public virtual int Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Email { get; set; }
    }
}