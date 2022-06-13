using PostService.Dto;
using PostService.Model;
using AutoMapper;

namespace PostService.Profiles
{
    public class PostProfile : AutoMapper.Profile
    {
        public PostProfile()
        {
            // Source -> Target
            CreateMap<PostRequest, Post>();
        }

    }
}
