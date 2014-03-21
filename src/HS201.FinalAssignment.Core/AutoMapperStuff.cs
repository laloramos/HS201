using AutoMapper;
using HS201.FinalAssignment.Core.Domain.Entities;
using HS201.FinalAssignment.Core.Features.Conferences;

namespace HS201.FinalAssignment.Core
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
        }
    }
}