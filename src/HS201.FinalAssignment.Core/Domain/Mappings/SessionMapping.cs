using HS201.FinalAssignment.Core.Domain.Entities;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace HS201.FinalAssignment.Core.Domain.Mappings
{
    public class SessionMapping : ClassMapping<Session>
    {
        public SessionMapping()
        {
            Id(x => x.Id, map => map.Generator(Generators.Identity));
            Property(x => x.Title);
            Property(x => x.Abstract);
            ManyToOne(x => x.Speaker, x => x.Column("SpeakerId"));
        }
    }
}