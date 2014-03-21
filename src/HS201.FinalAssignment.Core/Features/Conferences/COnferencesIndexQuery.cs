using ShortBus;

namespace HS201.FinalAssignment.Core.Features.Conferences
{
    public class ConferencesIndexQuery : IQuery<ConferenceIndexModel>
    {
        public string SearchString { get; set; }
    }
}
