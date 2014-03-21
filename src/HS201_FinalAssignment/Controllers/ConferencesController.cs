using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using FluentValidation;
using FluentValidation.Attributes;
using HS201.FinalAssignment.Core.Domain.Entities;
using HS201.FinalAssignment.Core.Features.Conferences;
using HS201.FinalAssignment.Core.Infrastructure;
using Microsoft.Practices.ServiceLocation;
using NHibernate;
using ShortBus;

namespace HS201.FinalAssignment.Controllers
{
    public class ConferencesController : Controller
    {
        private readonly IConferenceRepository _repository;
        private readonly IMediator _mediator;
        //
        // GET: /Conferences/

        public ConferencesController(IConferenceRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public ActionResult Index(string searchString)
        {
            var model = _mediator.Request(new ConferencesIndexQuery {SearchString = searchString});
            return View(model.Data);
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
                _mediator.Send(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Add()
        {
            return View(new ConferenceAddModel());
        }

        [HttpPost]
        public ActionResult Add(ConferenceAddModel model)
        {
            if (ModelState.IsValid)
            {
                _repository.Insert(Mapper.Map<Conference>(model));

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult BulkEdit(string searchString)
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
            var model = new ConferenceBulkEditModel()
            {
                Conferences = Mapper.Map<List<ConferenceEditModel>>(confs)
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult BulkEdit(ConferenceBulkEditModel model)
        {
            if (ModelState.IsValid)
            {
                foreach (var conf in model.Conferences)
                {
                    var conference = _repository.Load(conf.Id);

                    conference.ChangeName(conf.Name);
                    conference.ChangeCost(conf.Cost.Value);
                    conference.ChangeDates(conf.StartDate.Value, conf.EndDate.Value);
                    conference.ChangeHashTag(conf.HashTag);
                    _repository.Save(conference);
                }

                return RedirectToAction("Index");
            }

            return View(model);
        }
    }



    public class ConferenceBulkEditModel
    {
        public List<ConferenceEditModel> Conferences { get; set; } 
    }





    [Validator(typeof (ConferenceAddModelValidator))]
    public class ConferenceAddModel
    {
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

    



    public class ConferenceAddModelValidator : AbstractValidator<ConferenceAddModel>
    {
        public ConferenceAddModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .Must(BeAUniqueName)
                .WithMessage("{PropertyName} is already in use.");

            RuleFor(x => x.HashTag)
                .NotEmpty();

            RuleFor(x => x.StartDate)
                .NotEmpty();

            RuleFor(x => x.EndDate)
                .NotEmpty()
                .GreaterThanOrEqualTo(x => x.StartDate)
                    .WithMessage("{PropertyName} must be after or equal to {ComparisonValue}.", x => x.StartDate);

            RuleFor(x => x.Cost)
                .NotEmpty();

            RuleFor(x => x.Cost)
                .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} must be at least $0.00");
        }

        public bool BeAUniqueName(ConferenceAddModel model, string name)
        {
            var conf =
                ServiceLocator.Current.GetInstance<ISession>().QueryOver<Conference>().Where(x => x.Name == name).List();
            return conf.Count == 0;
        }
    }
}
