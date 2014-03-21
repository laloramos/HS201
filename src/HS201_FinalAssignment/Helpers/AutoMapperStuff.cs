using AutoMapper;
using HS201.FinalAssignment.Controllers;
using HS201.FinalAssignment.Core.Domain.Entities;

namespace HS201.FinalAssignment.Helpers
{

    public static class AutoMapperBootstrapper
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg => cfg.AddProfile<WebAutoMapperProfile>());
        }
    }

    public class WebAutoMapperProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Conference, ConferenceListItem>();
            Mapper.CreateMap<Conference, ConferenceEditModel>();
            Mapper.CreateMap<Conference, ConferenceAddModel>();
            Mapper.CreateMap<ConferenceAddModel, Conference>();
        }
    }
}