using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PostService.Dto;
using PostService.Model;
using PostService.Repository.Interface.Pagination;
using PostService.Service.Interface;

namespace PostService.Controllers
{

    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;


        public PostController(IPostService postService, IMapper mapper)
        {
            _postService = postService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] PostRequest request)
        {
            await _postService.Save(_mapper.Map<Post>(request));

            return Ok("ok");
        }

        [HttpGet]
        public async Task<PagedList<Post>> FindAll([FromQuery] PaginationParams paginationParams)
        {
            return await _postService.FindAll(paginationParams);
        }

        [HttpPost("public-followed")]
        public async Task<PagedList<Post>> FindAllPublicAndFollowed([FromQuery] PaginationParams paginationParams, [FromBody] FollowedProfilePostsRequest request)
        {
            return await _postService.FindAllPublicAndFollowed(paginationParams, request.ProfileId);
        }

        [HttpGet("public")]
        public async Task<PagedList<Post>> FindAllPublic([FromQuery] PaginationParams paginationParams)
        {
            return await _postService.FindAllPublic(paginationParams);
        }

        [HttpPost("followed")]
        public async Task<PagedList<Post>> FindAlldFollowed([FromQuery] PaginationParams paginationParams, [FromBody] FollowedProfilePostsRequest request)
        {
            return await _postService.FindAllFollowed(paginationParams, request.ProfileId);
        }

    }
}
