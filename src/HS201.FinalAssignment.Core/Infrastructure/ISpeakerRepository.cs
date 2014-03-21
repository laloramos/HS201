using System.Collections.Generic;
using System.Linq;
using HS201.FinalAssignment.Core.Domain.Entities;
using NHibernate;
using NHibernate.Linq;

namespace HS201.FinalAssignment.Core.Infrastructure
{
    public interface ISpeakerRepository
    {
        IEnumerable<Speaker> GetAll();
        IQueryable<Speaker> Query();
        Speaker Load(int id);
        Speaker FindByName(string lastName, string firstName);
        void Insert(Speaker speaker);
    }

    public class SpeakerRepository : ISpeakerRepository
    {
        private readonly ISession _session;

        public SpeakerRepository(ISession session)
        {
            _session = session;
        }

        public Speaker Load(int id)
        {
            return _session.Load<Speaker>(id);
        }

        public Speaker FindByName(string lastName, string firstName)
        {
            return _session.Query<Speaker>()
                .FirstOrDefault(x => x.LastName == lastName && x.FirstName == firstName);
        }

        public IQueryable<Speaker> Query()
        {
            return _session.Query<Speaker>();
        }

        public IEnumerable<Speaker> GetAll()
        {
            return _session.Query<Speaker>().ToList();
        }

        public void Insert(Speaker speaker)
        {
            _session.Save(speaker);
        }
    }
}