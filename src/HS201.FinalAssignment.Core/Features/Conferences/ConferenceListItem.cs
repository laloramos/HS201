using System;

namespace HS201.FinalAssignment.Core.Features.Conferences
{
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
}