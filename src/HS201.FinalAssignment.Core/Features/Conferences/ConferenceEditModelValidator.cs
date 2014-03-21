using FluentValidation;
using HS201.FinalAssignment.Core.Domain.Entities;
using Microsoft.Practices.ServiceLocation;
using NHibernate;

namespace HS201.FinalAssignment.Core.Features.Conferences
{
    public class ConferenceEditModelValidator : AbstractValidator<ConferenceEditModel>
    {
        public ConferenceEditModelValidator()
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
        public bool BeAUniqueName(ConferenceEditModel model, string name)
        {
            var conf =
                ServiceLocator.Current.GetInstance<ISession>().QueryOver<Conference>().Where(x => x.Name == name).List();
            return conf.Count == 0;
        }
    }
}