using System.Collections.Generic;
using System.Linq;
using HS201_FinalAssignment.Domain.Entities;
using NHibernate;
using NHibernate.Linq;

namespace HS201_FinalAssignment.Infrastructure
{
    public interface IConferenceRepository
    {
        IEnumerable<Conference> GetAll();
        IQueryable<Conference> Query();
        Conference Load(int id);
        Conference FindByName(string name);
        void Insert(Conference conference);
    }

    public class ConferenceRepository : IConferenceRepository
    {
        private readonly ISession _session;

        public ConferenceRepository(ISession session)
        {
            _session = session;
        }

        public Conference Load(int id)
        {
            return _session.Load<Conference>(id);
        }

        public Conference FindByName(string name)
        {
            return _session.Query<Conference>()
                .FirstOrDefault(x => x.Name == name);
        }

        public IQueryable<Conference> Query()
        {
            return _session.Query<Conference>();
        }

        public IEnumerable<Conference> GetAll()
        {
            return _session.Query<Conference>().ToList();
        }

        public void Insert(Conference conference)
        {
            _session.Save(conference);
        }
    }
}