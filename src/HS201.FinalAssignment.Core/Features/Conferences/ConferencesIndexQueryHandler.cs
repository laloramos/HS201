using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using HS201.FinalAssignment.Core.Infrastructure;
using ShortBus;

namespace HS201.FinalAssignment.Core.Features.Conferences
{
    public class ConferencesIndexQueryHandler : IQueryHandler<ConferencesIndexQuery,ConferenceIndexModel>
    {
        private readonly IConferenceRepository _repository;

        public ConferencesIndexQueryHandler(IConferenceRepository repository)
        {
            _repository = repository;
        }

        public ConferenceIndexModel Handle(ConferencesIndexQuery request)
        {
            var searchString = request.SearchString;

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

            return model;
        }
    }
}