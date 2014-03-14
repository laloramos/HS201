using HS201_FinalAssignment.Domain.Entities;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace HS201_FinalAssignment.Domain.Mappings
{
    public class ConferenceMapping : ClassMapping<Conference>
    {
        public ConferenceMapping()
        {
            Id(x=>x.Id);
            Property(x => x.Name);
            Property(x => x.HashTag);
            Property(x => x.StartDate);
            Property(x => x.EndDate);
            Property(x => x.Cost);

            Set(x => x.Sessions,
                map => {map.Access(Accessor.Field);
                           map.Key(x => x.Column("ConferenceId"));
                },
                relation => relation.OneToMany());

            Set(x => x.Attendees,
                map =>
                {
                    map.Access(Accessor.Field);
                    map.Key(x => x.Column("ConferenceId"));
                },
                relation => relation.OneToMany());
        } 
    }
}