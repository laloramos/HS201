using HS201_FinalAssignment.Domain.Entities;
using NHibernate.Mapping.ByCode.Conformist;

namespace HS201_FinalAssignment.Domain.Mappings
{
    public class SessionMapping : ClassMapping<Session>
    {
        public SessionMapping()
        {
            Id(x => x.Id);
            Property(x => x.Title);
            Property(x => x.Abstract);
        }
    }
}