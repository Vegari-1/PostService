using PostService.Dto;
using PostService.Model;
using AutoMapper;

namespace PostService.Profiles
{
    public class CommentProfile: AutoMapper.Profile
    {
        public CommentProfile()
        {
            // Source -> Target
            CreateMap<CommentRequest, Comment>();
        }
    }
}
