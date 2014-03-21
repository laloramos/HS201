using HS201.FinalAssignment.Core.Domain.Entities;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace HS201.FinalAssignment.Core.Domain.Mappings
{
    public class AttendeeMapping : ClassMapping<Attendee>
    {
        public AttendeeMapping()
        {
            Id(x => x.Id, map => map.Generator(Generators.Identity)); 
            Property(x=>x.FirstName);
            Property(x => x.LastName);
            Property(x => x.Email);
        }
    }
}