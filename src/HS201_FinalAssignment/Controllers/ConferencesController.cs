using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using FluentValidation;
using FluentValidation.Attributes;
using HS201_FinalAssignment.Domain.Entities;
using HS201_FinalAssignment.Infrastructure;

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
                conference.ChangeCost(model.Cost);
                conference.ChangeDates(model.StartDate.Value, model.EndDate.Value);
                conference.ChangeHashTag(model.HashTag);

                _repository.Save(conference);

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
                Conferences = Mapper.Map<List<ConferenceListItem>>(confs)
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
                    conference.ChangeCost(conf.Cost);
                    conference.ChangeDates(conf.StartDate.Value, conf.EndDate.Value);
                    conference.ChangeHashTag(conf.HashTag);
                    _repository.Save(conference);
                }

                return RedirectToAction("Index");
            }

            return View(model);
        }
    }

    public class ConferenceIndexModel
    {
        public List<ConferenceListItem> Conferences { get; set; }
    }

    public class ConferenceBulkEditModel
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

    [Validator(typeof(ConferenceEditModelValidator))]
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

        public decimal Cost { get; set; }
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

        public decimal Cost { get; set; }
    }

    public class ConferenceEditModelValidator : AbstractValidator<ConferenceEditModel>
    {
        private IConferenceRepository _conferenceRepository;

        public ConferenceEditModelValidator()
        {
            _conferenceRepository = DependencyResolver.Current.GetService<IConferenceRepository>();

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

        public bool BeAUniqueName(ConferenceEditModel model, string name)
        {
            var c = _conferenceRepository.FindByName(name);
            return c == null || model.Id == c.Id;
        }
    }


    public class ConferenceAddModelValidator : AbstractValidator<ConferenceAddModel>
    {
        private IConferenceRepository _conferenceRepository;

        public ConferenceAddModelValidator()
        {
            _conferenceRepository = DependencyResolver.Current.GetService<IConferenceRepository>();

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
            return _conferenceRepository.FindByName(name) == null;
        }
    }
}
