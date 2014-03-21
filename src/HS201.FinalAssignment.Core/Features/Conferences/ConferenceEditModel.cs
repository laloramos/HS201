using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Attributes;
using ShortBus;

namespace HS201.FinalAssignment.Core.Features.Conferences
{
    [Validator(typeof(ConferenceEditModelValidator))]
    public class ConferenceEditModel : ICommand
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
