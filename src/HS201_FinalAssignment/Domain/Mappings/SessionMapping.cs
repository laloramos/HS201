using HS201_FinalAssignment.Domain.Entities;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace HS201_FinalAssignment.Domain.Mappings
{
    public class SessionMapping : ClassMapping<Session>
    {
        public SessionMapping()
        {
            Id(x => x.Id, map => map.Generator(Generators.Identity)); 
            Property(x => x.Title);
            Property(x => x.Abstract);
            Property(x => x.Speaker);
        }
    }
}