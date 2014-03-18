using AutoMapper;
using HS201_FinalAssignment.Controllers;
using HS201_FinalAssignment.Domain.Entities;

namespace HS201_FinalAssignment.Helpers
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
        }
    }
}