using PostService.Dto;
using PostService.Model;
using AutoMapper;

namespace PostService.Profiles
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            // Source -> Target
            CreateMap<PostRequest, Post>();
        }

    }
}
