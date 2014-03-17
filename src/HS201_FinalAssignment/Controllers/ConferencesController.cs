using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using AutoMapper;
using HS201_FinalAssignment.Domain.Entities;
using HS201_FinalAssignment.Infrastructure;
using NHibernate;
using StructureMap.Query;

namespace HS201_FinalAssignment.Controllers
{
    public class ConferencesController : Controller
    {
        private readonly IConferenceRepository _repository;
        //
        // GET: /Conferences/

        public ConferencesController(IConferenceRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Index(string searchString)
        {
            var confs = _repository
                            .GetAll()
                            .ToList();

            if (!String.IsNullOrEmpty(searchString))
                confs =
                    confs.Where(
                        x => x.Attendees.Count.ToString() == searchString
                             || x.StartDate.GetValueOrDefault().ToShortDateString() == searchString
                             || x.Name == searchString).ToList();
            var model = new ConferenceIndexModel()
            {
                Conferences = Mapper.Map<List<ConferenceListItem>>(confs)
            };

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var conf = _repository.Load(id);

            var model = Mapper.Map<ConferenceEditModel>(conf);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ConferenceEditModel model)
        {
            if (ModelState.IsValid)
            {
                var conference = _repository.Load(model.Id);

                conference.ChangeName(model.Name);
                conference.ChangeCost(model.Cost.Value);
                conference.ChangeDates(model.StartDate.Value, model.EndDate.Value);
                conference.ChangeHashTag(model.HashTag);

                return RedirectToAction("Index");
            }

            return View(model);
        }
    }

    public class ConferenceIndexModel
    {
        public List<ConferenceListItem> Conferences { get; set; }
    }

    public class ConferenceListItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string HashTag { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal Cost { get; set; }
        public int AttendeeCount { get; set; }
        public int SessionCount { get; set; }
    }

    public class ConferenceEditModel
    {
        public int Id { get; set; }

        [DisplayName("Name")]
        public string Name { get; set; }

        [DisplayName("Hash Tag")]
        public string HashTag { get; set; }

        [DisplayName("Start Date")]
        public DateTime? StartDate { get; set; }

        [DisplayName("End Date")]
        public DateTime? EndDate { get; set; }

        public decimal? Cost { get; set; }
    }
}
