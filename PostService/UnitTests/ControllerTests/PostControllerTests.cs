//using System;
//using PostService.Dto;
//using PostService.Model;
//using PostService.Service.Interface;
//using PostService.Service.Interface.Exceptions;
//using AutoMapper;
//using Microsoft.AspNetCore.Mvc;
//using Moq;
//using OpenTracing.Mock;
//using Xunit;
//using PostService.Controllers;
//using PostService.Repository.Interface.Pagination;
//using System.Collections.Generic;
//using Microsoft.AspNetCore.Mvc.Controllers;

//namespace UnitTests.ControllerTests { 
//    public class PostControllerTests
//    {
//        private static readonly Guid id = Guid.NewGuid();
//        private static readonly Guid authorId = Guid.NewGuid();
//        private static readonly Guid profileId = Guid.NewGuid();
//        private static readonly Guid postId = Guid.NewGuid();
//        private static readonly Guid profile1 = Guid.NewGuid();
//        private static readonly Guid profile2 = Guid.NewGuid();
//        private static readonly string authorIdString = "";
//        private static readonly string content = "test content";
//        private static readonly DateTime timeStamp = new DateTime();
//        private static readonly int likes = 1;
//        private static readonly int dislikes = 2;
//        private static readonly string name = "test name";
//        private static readonly string surname = "test surname";
//        private static readonly bool positive = true;
//        private static readonly bool publicPost = true;
//        private static readonly int pageNumber = 1;
//        private static readonly int pageSize = 5;
//        private static readonly string actionName = "test";

//        private static Post post;
//        private static Comment comment;
//        private static PostService.Model.Profile profile;
//        private static Reaction reaction;
//        private static Connection connection;
//        private static PostRequest postRequest;
//        private static FollowedProfilePostsRequest followedProfilePostsRequest;
//        private static CommentRequest commentRequest;
//        private static ReactionRequest reactionRequest;
//        private static PaginationParams paginationParams;
//        private static List<Post> posts;
//        private static PagedList<Post> pagedListPosts;

//        private static Mock<IPostService> mockPostService = new Mock<IPostService>();
//        private static Mock<IReactionService> mockReactionService = new Mock<IReactionService>();
//        private static Mock<ICommentService> mockCommentService = new Mock<ICommentService>();
//        private static Mock<IMapper> mockMapper = new Mock<IMapper>();
//        private static MockTracer mockTracer = new MockTracer();
//        private static ControllerActionDescriptor controllerActionDescriptor = new ControllerActionDescriptor() { ActionName = actionName };
//        private static ControllerContext controllerContext = new ControllerContext() { ActionDescriptor = controllerActionDescriptor};

//        PostController postController = new PostController(mockPostService.Object, mockReactionService.Object, mockCommentService.Object, mockMapper.Object, mockTracer) { ControllerContext = controllerContext };
        

//        private static void SetUp()
//        {
//            post = new Post() {
//                Id = id,
//                Content = content,
//                TimeStamp = timeStamp,
//                AuthorId = authorId,
//                Likes = likes,
//                Dislikes = dislikes
//            };

//            comment = new Comment()
//            {
//                Id = id,
//                Content = content,
//                AuthorId = authorId,
//                PostId = postId
//            };

//            profile = new PostService.Model.Profile()
//            {
//                Id = id,
//                Public = publicPost,
//                Name = name,
//                Surname = surname
//            };

//            reaction = new Reaction()
//            {
//                Id = id,
//                Positive = positive,
//                AuthorId = authorId,
//                PostId = postId
//            };

//            connection = new Connection()
//            {
//                Id = id,
//                Profile1 = profile1,
//                Profile2 = profile2
//            };

//            postRequest = new PostRequest()
//            {
//                Content = content,
//                TimeStamp = timeStamp,
//                AuthorId = authorIdString
//             };

//            followedProfilePostsRequest = new FollowedProfilePostsRequest()
//            {
//                ProfileId = profileId
//            };

//            commentRequest = new CommentRequest()
//            {
//                Content = content,
//                AuthorId = authorId,
//                PostId = postId
//            };

//            reactionRequest = new ReactionRequest()
//            {
//                Positive = positive,
//                AuthorId = authorId,
//                PostId = postId
//            };

//            paginationParams = new PaginationParams()
//            {
//                PageNumber = pageNumber,
//                PageSize = pageSize
//            };

//            posts = new List<Post>() { post, post, post};

//            pagedListPosts = new PagedList<Post>(posts, posts.Count, pageNumber, pageSize);
//        }

//        [Fact]
//        public async void Save_ValidRequest_CreatePost()
//        {
//            SetUp();

//            mockPostService
//                .Setup(service => service.Save(post))
//                .ReturnsAsync(post);

//            mockMapper
//                .Setup(x => x.Map<Post>(postRequest))
//                .Returns((PostRequest source) =>  
//                {
//                    return post;
//                });

//            var response = await postController.Save(postRequest);

//            var actionResult = Assert.IsType<ObjectResult>(response);
//            var actionValue = Assert.IsType<Post>(actionResult.Value);
//            Assert.Equal(actionValue, post);
//        }

//        [Fact]
//        public async void FindAllPost_ValidRequest_FindPosts()
//        {
//            SetUp();
            
//            mockPostService
//                .Setup(service => service.FindAll(paginationParams))
//                .ReturnsAsync(pagedListPosts);

//            var response = await postController.FindAll(paginationParams);

//            var actionResult = Assert.IsType<PagedList<Post>>(response);
//            var actionValue = Assert.IsType<List<Post>>(actionResult.Items);
//            Assert.Equal(actionValue.Count, posts.Count);
//        }

//        [Fact]
//        public async void FindPublicPosts_ValidRequest_FindPosts()
//        {
//            SetUp();

//            mockPostService
//                .Setup(service => service.FindAllPublic(paginationParams))
//                .ReturnsAsync(pagedListPosts);   

//            var response = await postController.FindAllPublic(paginationParams);

//            var actionResult = Assert.IsType<PagedList<Post>>(response);
//            var actionValue = Assert.IsType<List<Post>>(actionResult.Items);
//            Assert.Equal(actionValue.Count, posts.Count);
//        }

//        [Fact]
//        public async void FindAllFollowedPosts_ValidRequest_FindPosts()
//        {
//            SetUp();

//            mockPostService
//                .Setup(service => service.FindAllFollowed(paginationParams, profileId))
//                .ReturnsAsync(pagedListPosts);

//            var response = await postController.FindAlldFollowed(paginationParams, followedProfilePostsRequest);

//            var actionResult = Assert.IsType<PagedList<Post>>(response);
//            var actionValue = Assert.IsType<List<Post>>(actionResult.Items);
//            Assert.Equal(actionValue.Count, posts.Count);
//        }

//        [Fact]
//        public async void FindPublicAndFollowedPosts_ValidRequest_FindPosts()
//        {
//            SetUp();

//            mockPostService
//                .Setup(service => service.FindAllPublicAndFollowed(paginationParams, profileId))
//                .ReturnsAsync(pagedListPosts);

//            var response = await postController.FindAllPublicAndFollowed(paginationParams, followedProfilePostsRequest);

//            var actionResult = Assert.IsType<PagedList<Post>>(response);
//            var actionValue = Assert.IsType<List<Post>>(actionResult.Items);
//            Assert.Equal(actionValue, posts);
//        }

//        [Fact]
//        public async void Save_ValidComment_CreateComment()
//        {
//            SetUp();

//            mockCommentService
//                .Setup(service => service.Save(comment))
//                .ReturnsAsync(comment);

//            mockMapper
//                .Setup(x => x.Map<Comment>(commentRequest))
//                .Returns((CommentRequest source) =>
//                {
//                    return comment;
//                });

//            var response = await postController.Comment(commentRequest);

//            var actionResult = Assert.IsType<ObjectResult>(response);
//            var actionValue = Assert.IsType<Comment>(actionResult.Value);
//            Assert.Equal(actionValue, comment);
//        }

//        [Fact]
//        public async void Save_ValidReaction_CreateReaction()
//        {
//            SetUp();

//            mockReactionService
//                .Setup(service => service.Save(reaction))
//                .ReturnsAsync(reaction);

//            mockMapper
//                .Setup(x => x.Map<Reaction>(reactionRequest))
//                .Returns((ReactionRequest source) =>
//                {
//                    return reaction;
//                });

//            var response = await postController.React(reactionRequest);

//            var actionResult = Assert.IsType<ObjectResult>(response);
//            var actionValue = Assert.IsType<Reaction>(actionResult.Value);
//            Assert.Equal(actionValue, reaction);
//        }
//    }
//}
