using HS201_FinalAssignment.Domain.Entities;
using NHibernate.Mapping.ByCode.Conformist;

namespace HS201_FinalAssignment.Domain.Mappings
{
    public class SpeakerMapping : ClassMapping<Speaker>
    {
        public SpeakerMapping()
        {
            Id(x => x.Id);
            Property(x => x.LastName);
            Property(x => x.FirstName);
            Property(x => x.Bio);
        }
    }
}