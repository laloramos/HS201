using HS201.FinalAssignment.Core.Infrastructure;
using ShortBus;

namespace HS201.FinalAssignment.Core.Features.Conferences
{
    public class ConferenceEditModelHandler : ICommandHandler<ConferenceEditModel>
    {
        private readonly IConferenceRepository _repository;

        public ConferenceEditModelHandler(IConferenceRepository repository)
        {
            _repository = repository;
        }

        public void Handle(ConferenceEditModel message)
        {
            var conference = _repository.Load(message.Id);

            conference.ChangeName(message.Name);
            conference.ChangeCost(message.Cost.Value);
            conference.ChangeDates(message.StartDate.Value, message.EndDate.Value);
            conference.ChangeHashTag(message.HashTag);

            _repository.Save(conference);
        }
    }
}