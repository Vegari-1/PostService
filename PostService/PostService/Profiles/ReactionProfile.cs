using PostService.Dto;
using PostService.Model;
using AutoMapper;

namespace PostService.Profiles
{
    public class ReactionProfile : AutoMapper.Profile
    {
        public ReactionProfile()
        {
            // Source -> Target
            CreateMap<ReactionRequest, Reaction>();
        }
    }
}
