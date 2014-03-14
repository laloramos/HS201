using NHibernate.Mapping.ByCode.Conformist;

namespace HS201_FinalAssignment.Entities
{
    public class ConferenceMapping : ClassMapping<Conference>
    {
        public ConferenceMapping()
        {
            Id(x=>x.Id);
            Property(x=>x.Name);
        } 
    }
}