using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HS201_FinalAssignment.Domain.Entities;
using NHibernate.Mapping.ByCode.Conformist;

namespace HS201_FinalAssignment.Domain.Mappings
{
    public class AttendeeMapping : ClassMapping<Attendee>
    {
        public AttendeeMapping()
        {
            Id(x=>x.Id);
            Property(x=>x.FirstName);
            Property(x => x.LastName);
            Property(x => x.Email);
        }
    }
}