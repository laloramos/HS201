using System;
using Iesi.Collections.Generic;

namespace HS201.FinalAssignment.Core.Domain.Entities
{
    public class Conference
    {
        private ISet<Session> _sessions = new HashedSet<Session>();
        private ISet<Attendee> _attendees = new HashedSet<Attendee>();
        public virtual int Id { get; set; }
        public virtual string Name { get;  set; }
        public virtual string HashTag { get;  set; }
        public virtual DateTime? StartDate { get;  set; }
        public virtual DateTime? EndDate { get;  set; }
        public virtual decimal Cost { get;  set; }
        public virtual int AttendeeCount {
            get { return _attendees.Count; }
        }
        public virtual int SessionCount{get { return _sessions.Count; }}

        public virtual ISet<Session> Sessions
        {
            get { return _sessions; }
            set { _sessions = value; }
        }

        public virtual ISet<Attendee> Attendees
        {
            get { return _attendees; }
            set { _attendees = value; }
        }

        public virtual void ChangeName(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            if (name == string.Empty)
                throw new ArgumentOutOfRangeException("name", "Must be a non-empty string.");

            Name = name;
        }

        public virtual void ChangeHashTag(string hashTag)
        {
            if (hashTag == null)
                throw new ArgumentNullException("hashTag");

            if (hashTag == string.Empty)
                throw new ArgumentOutOfRangeException("hashTag", "Must be a non-empty string.");

            HashTag = hashTag;
        }

        public virtual void ChangeDates(DateTime? startDate, DateTime? endDate)
        {
            if (endDate < startDate)
                throw new ArgumentOutOfRangeException("endDate", "Must be on or after startDate");

            StartDate = startDate;
            EndDate = endDate;
        }

        public virtual void ChangeCost(decimal cost)
        {
            Cost = cost;
        }
    }
}