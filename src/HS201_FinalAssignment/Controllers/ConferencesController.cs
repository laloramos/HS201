using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HS201_FinalAssignment.Domain.Entities;
using NHibernate;

namespace HS201_FinalAssignment.Controllers
{
    public class ConferencesController : Controller
    {
        private readonly ISession _session;
        //
        // GET: /Conferences/

        public ConferencesController(ISession session)
        {
            _session = session;
        }

        public ActionResult Index(int? id)
        {
            //var conference = _session.Get<Conference>(id);

            var newConf = new Conference();

            newConf.Name = "Test";
            newConf.StartDate = DateTime.Now;
            newConf.EndDate = DateTime.Now;

            _session.SaveOrUpdate(newConf);
            
            return View();
        }

    }
}
