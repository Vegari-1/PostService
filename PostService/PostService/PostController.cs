using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OpenTracing;
using PostService.Dto;
using PostService.Model;
using PostService.Repository.Interface.Pagination;
using PostService.Service.Interface;
using PostService.Service.Interface.Sync;
using Prometheus;
using System.ComponentModel.DataAnnotations;

namespace PostService.Controllers
{
        
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IReactionService _reactionService;
        private readonly ICommentService _commentService;
        private readonly IProfileSyncService _profileService;
        private readonly IMapper _mapper;
        private readonly ITracer _tracer;

        Counter counter = Metrics.CreateCounter("post_service_counter", "post counter");


        public PostController(IPostService postService,
                                IReactionService reactionService,
                                ICommentService commentService,
                                IProfileSyncService profileService,
                                IMapper mapper,
                                ITracer tracer)
        {
            _postService = postService;
            _reactionService = reactionService;
            _commentService = commentService;
            _profileService = profileService;
            _mapper = mapper;
            _tracer = tracer;
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] PostRequest request)
        {
            var post = await _postService.Save(new Post()
            {
                Content = request.Content,
                TimeStamp = request.TimeStamp,
                AuthorId = new Guid(request.AuthorId),
                Images = request.Pictures.Select(x => new Image(x)).ToList()
            });
            return StatusCode(StatusCodes.Status201Created, post);
        }

        
        public async Task<PagedList<PostResponse>> FindAll([FromQuery] PaginationParams paginationParams, Guid profileId)
        {
            
            var posts = await _postService.FindAll(paginationParams);
            var res = await convertPostToResponse(posts);
            return res;
        }


        [HttpGet]
        [Route("search")]
        public async Task<PagedList<PostResponse>> SearchPostByContent(
            [FromHeader(Name = "profile-id")][Required] Guid id, 
            [FromQuery] SearchReqeust reqeust)
        {
            var posts = await _postService.SearchPostByContent(id, reqeust.Query);
            var res = await convertPostToResponse(posts);
            return res;
        }

        [HttpGet]
        [Route("profile/{profileId}")] 
        public async Task<PagedList<PostResponse>> FindAllProfilePosts([FromQuery] PaginationParams paginationParams, Guid profileId)
        {
            var posts = await _postService.FindAllProfilePosts(paginationParams, profileId);
            var res = await convertPostToResponse(posts);
            return res;
        }

        [HttpGet("")]
        public async Task<PagedList<PostResponse>> FindAlldFollowed(
            [FromQuery] PaginationParams paginationParams,
            [FromHeader(Name = "profile-id")][Required] Guid profileId)
        {
            var posts = await _postService.FindAllFollowed(paginationParams, profileId);
            var res = await convertPostToResponse(posts);
            return res;
        }

        [HttpPost("{id}/reaction")]
        public async Task<IActionResult> React(
            [FromBody] ReactionRequest request,
            [FromHeader(Name = "profile-id")][Required] Guid profileId,
            Guid id)
        {
            var reaction = await _reactionService.Save(id, profileId, _mapper.Map<Reaction>(request));
            return StatusCode(StatusCodes.Status201Created, reaction);
        }

        [HttpPost("{id}/comment")]
        public async Task<IActionResult> Comment(
            [FromBody] CommentRequest request,
            [FromHeader(Name = "profile-id")][Required] Guid profileId,
            Guid id)
        {
            var comment = await _commentService.Save(id, profileId, _mapper.Map<Comment>(request));
            return StatusCode(StatusCodes.Status201Created, comment); ;
        }

        [HttpGet("{postId}/comment")]
        public async Task<List<Comment>> GetPostComments(Guid postId)
        {
            return await _commentService.GetComments(postId);
        }

        public async Task<PagedList<PostResponse>> convertPostToResponse(IReadOnlyList<Post> posts)
        {
            var res = new PagedList<PostResponse>();
            foreach (var post in posts)
            {
                var profile = await _profileService.GetByIdAsync(post.AuthorId);
                var response = await CreateResponse(post, profile);
                res.Add(response);
            }
            return res;
        }

        public async Task<PostResponse> CreateResponse(Post post, Model.Sync.Profile profile) {
            return new PostResponse()
            {
                Id = post.Id,
                Likes = post.LikesNumber,
                Dislikes = post.DislikesNumber,
                Content = post.Content,
                Timestamp = post.TimeStamp,
                Username = profile.Username,
                Name = profile.Name,
                Surname = profile.Surname,
                Liked = post.Likes.Contains(profile.Id),
                Disliked = post.Dislikes.Contains(profile.Id),
                Comments = await MapComments(post.Comments ?? new List<Comment>()),
                CommentCount = post.Comments.Count(),
                Pictures = await MapImagesToStringList(post.Images)
            };
        }

        public async Task<List<CommentResponse>> MapComments(List<Comment> comments)
        {
            if (comments is null)
            {
                return new List<CommentResponse>();
            }
            return comments.Select(c => new CommentResponse(
                    c.Id,
                    c.Name?? "",
                    c.Surname,
                    c.Username,
                    c.Image?.Content,
                    c.Content,
                    c.TimeStamp.Value
                ))
                .ToList();
        }

        public async Task<List<string>> MapImagesToStringList(List<Image> images)
        {
            if (images is null)
            {
                return new List<string>();
            }
            return images.Select(x => x.Content).ToList();
        }

    }
}
