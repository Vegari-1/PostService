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
            //CreateMap<PostRequest, Post>()
            //    .ForMember(dest => dest.Images, src => src.MapFrom(s => s.Pictures));

            //CreateMap<PostRequest, Post>()
            //    .ForMember(dest => dest.Image, src => src.MapFrom(s => s.Picture));
        }

    }
}
