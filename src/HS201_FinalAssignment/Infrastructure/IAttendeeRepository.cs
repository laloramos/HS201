using System.Collections.Generic;
using System.Linq;
using HS201_FinalAssignment.Domain.Entities;
using NHibernate;
using NHibernate.Linq;

namespace HS201_FinalAssignment.Infrastructure
{
    public interface IAttendeeRepository
    {
        IEnumerable<Attendee> GetAll();
        IQueryable<Attendee> Query();
        Attendee Load(int id);
        Attendee FindByName(string lastName, string firstname);
        void Insert(Attendee attendee);
    }

    public class AttendeeRepository : IAttendeeRepository
    {
        private readonly ISession _session;

        public AttendeeRepository(ISession session)
        {
            _session = session;
        }

        public Attendee Load(int id)
        {
            return _session.Load<Attendee>(id);
        }

        public Attendee FindByName(string lastName, string firstName)
        {
            return _session.Query<Attendee>()
                .FirstOrDefault(x => x.LastName == lastName && x.FirstName == firstName);
        }

        public IQueryable<Attendee> Query()
        {
            return _session.Query<Attendee>();
        }

        public IEnumerable<Attendee> GetAll()
        {
            return _session.Query<Attendee>().ToList();
        }

        public void Insert(Attendee attendee)
        {
            _session.Save(attendee);
        }
    }
}