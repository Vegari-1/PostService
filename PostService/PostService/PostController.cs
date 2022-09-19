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
            var actionName = ControllerContext.ActionDescriptor.DisplayName;
            using var scope = _tracer.BuildSpan(actionName).StartActive(true);
            scope.Span.Log("create post");
            counter.Inc();

            Post post = new()
            {
                Content = request.Content,
                TimeStamp = DateTime.Now,
                AuthorId = request.AuthorId,
            };
            if (request.Pictures != null && request.Pictures.Count() > 0)
                post.Images = request.Pictures.Select(x => new Image(x)).ToList();

            post = await _postService.Save(post);
            
            return StatusCode(StatusCodes.Status201Created, post);
        }

        [HttpGet]
        [Route("search")]
        public async Task<PagedList<PostResponse>> SearchPostByContent(
            [FromHeader(Name = "profile-id")][Required] Guid id, 
            [FromQuery] SearchReqeust reqeust)
        {
            var actionName = ControllerContext.ActionDescriptor.DisplayName;
            using var scope = _tracer.BuildSpan(actionName).StartActive(true);
            scope.Span.Log("search posts feed");
            counter.Inc();

            var posts = await _postService.SearchPostByContent(id, reqeust.Query);
            var res = await convertPostToResponse(posts, id);
            return res;
        }

        [HttpGet]
        [Route("profile/{profileId}")] 
        public async Task<PagedList<PostResponse>> FindAllProfilePosts(
            [FromHeader(Name = "profile-id")][Required] Guid id, 
            [FromQuery] PaginationParams paginationParams, Guid profileId)
        {
            var posts = await _postService.FindAllProfilePosts(paginationParams, profileId);
            var res = await convertPostToResponse(posts, id);
            return res;
        }

        [HttpGet]
        public async Task<PagedList<PostResponse>> FindAlldFollowed(
            [FromQuery] PaginationParams paginationParams,
            [FromHeader(Name = "profile-id")][Required] Guid profileId)
        {
            var actionName = ControllerContext.ActionDescriptor.DisplayName;
            using var scope = _tracer.BuildSpan(actionName).StartActive(true);
            scope.Span.Log("get posts feed");
            counter.Inc();

            var posts = await _postService.FindAllFollowed(paginationParams, profileId);
            var res = await convertPostToResponse(posts, profileId);
            return res;
        }

        [HttpPost("{id}/reaction")]
        public async Task<IActionResult> React(
            [FromBody] ReactionRequest request,
            [FromHeader(Name = "profile-id")][Required] Guid profileId,
            Guid id)
        {
            var actionName = ControllerContext.ActionDescriptor.DisplayName;
            using var scope = _tracer.BuildSpan(actionName).StartActive(true);
            scope.Span.Log("create post reaction");
            counter.Inc();

            var reaction = await _reactionService.Save(id, profileId, _mapper.Map<Reaction>(request));
            return StatusCode(StatusCodes.Status201Created, reaction);
        }

        [HttpPost("{id}/comment")]
        public async Task<IActionResult> Comment(
            [FromBody] CommentRequest request,
            [FromHeader(Name = "profile-id")][Required] Guid profileId,
            Guid id)
        {
            var actionName = ControllerContext.ActionDescriptor.DisplayName;
            using var scope = _tracer.BuildSpan(actionName).StartActive(true);
            scope.Span.Log("create post comment");
            counter.Inc();

            var comment = await _commentService.Save(id, profileId, _mapper.Map<Comment>(request));
            return StatusCode(StatusCodes.Status201Created, comment); ;
        }

        [HttpGet("{postId}/comment")]
        public async Task<List<Comment>> GetPostComments(Guid postId)
        {
            var actionName = ControllerContext.ActionDescriptor.DisplayName;
            using var scope = _tracer.BuildSpan(actionName).StartActive(true);
            scope.Span.Log("get comments by post id");
            counter.Inc();

            return await _commentService.GetComments(postId);
        }

        public async Task<PagedList<PostResponse>> convertPostToResponse(
            IReadOnlyList<Post> posts, Guid profileId)
        {
            var res = new PagedList<PostResponse>();
            foreach (var post in posts)
            {
                var profile = await _profileService.GetByIdAsync(post.AuthorId);
                var response = await CreateResponse(post, profile, profileId);
                res.Add(response);
            }
            return res;
        }

        public async Task<PostResponse> CreateResponse(Post post, Model.Sync.Profile profile, Guid profileId) {
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
                Avatar = profile.Avatar,
                Liked = post.Likes.Contains(profileId),
                Disliked = post.Dislikes.Contains(profileId),
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
                    c.Avatar,
                    c.Content,
                    c.TimeStamp
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
