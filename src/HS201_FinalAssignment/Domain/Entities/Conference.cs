using System;
using Iesi.Collections.Generic;

namespace HS201_FinalAssignment.Domain.Entities
{
    public class Conference
    {
        private Iesi.Collections.Generic.ISet<Session> _sessions = new HashedSet<Session>();
        private Iesi.Collections.Generic.ISet<Attendee> _attendees = new HashedSet<Attendee>();
        public virtual int Id { get; set; }
        public virtual string Name { get;  set; }
        public virtual string HashTag { get;  set; }
        public virtual DateTime? StartDate { get;  set; }
        public virtual DateTime? EndDate { get;  set; }
        public virtual decimal Cost { get;  set; }

        public virtual Iesi.Collections.Generic.ISet<Session> Sessions
        {
            get { return _sessions; }
            set { _sessions = value; }
        }

        public virtual Iesi.Collections.Generic.ISet<Attendee> Attendees
        {
            get { return _attendees; }
            set { _attendees = value; }
        }
    }
}