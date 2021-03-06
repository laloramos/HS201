﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using HS201_FinalAssignment.Domain.Entities;
using NHibernate;
using NHibernate.Linq;

namespace HS201_FinalAssignment.Infrastructure
{
    public interface ISessionRepository
    {
        IEnumerable<Session> GetAll();
        IQueryable<Session> Query();
        Session Load(int id);
        Session FindByTitle(string title);
        void Insert(Session session);
    }
    
    public class SessionRepository : ISessionRepository
    {
        private readonly ISession _session;

        public SessionRepository(ISession session)
        {
            _session = session;
        }

        public Session Load(int id)
        {
            return _session.Load<Session>(id);
        }

        public Session FindByTitle(string title)
        {
            return _session.Query<Session>()
                .FirstOrDefault(x => x.Title == title);
        }

        public IQueryable<Session> Query()
        {
            return _session.Query<Session>();
        }

        public IEnumerable<Session> GetAll()
        {
            return _session.Query<Session>().ToList();
        }

        public void Insert(Session session)
        {
            _session.Save(session);
        }
    }
}