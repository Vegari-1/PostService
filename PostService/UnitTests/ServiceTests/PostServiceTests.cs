//using System;
//using PostService.Dto;
//using PostService.Model;
//using PostService.Service.Interface;
//using PostService.Service;
//using AutoMapper;
//using Microsoft.AspNetCore.Mvc;
//using Moq;
//using OpenTracing.Mock;
//using Xunit;
//using PostService.Controllers;
//using PostService.Repository.Interface.Pagination;
//using System.Collections.Generic;
//using Microsoft.AspNetCore.Mvc.Controllers;
//using PostService.Repository.Interface;

//namespace UnitTests.ServiceTests
//{
//    public class PostServiceTests
//    {
//        private static readonly Guid id = Guid.NewGuid();
//        private static readonly Guid authorId = Guid.NewGuid();
//        private static readonly Guid profileId = Guid.NewGuid();
//        private static readonly string content = "test content";
//        private static readonly DateTime timeStamp = new DateTime();
//        private static readonly int likes = 1;
//        private static readonly int dislikes = 2;
//        private static readonly int pageNumber = 1;
//        private static readonly int pageSize = 5;

//        private static Post post;
//        private static PaginationParams paginationParams;
//        private static List<Post> posts;
//        private static PagedList<Post> pagedListPosts;

//        private static Mock<IPostRepository> mockPostRepository = new Mock<IPostRepository>();

//        private PostsService postService = new PostsService(mockPostRepository.Object);


//        private static void SetUp()
//        {
//            post = new Post()
//            {
//                Id = id,
//                Content = content,
//                TimeStamp = timeStamp,
//                AuthorId = authorId,
//                Likes = likes,
//                Dislikes = dislikes
//            };

//            FollowedProfilePostsRequest followedProfilePostsRequest = new FollowedProfilePostsRequest()
//            {
//                ProfileId = profileId
//            };

//            paginationParams = new PaginationParams()
//            {
//                PageNumber = pageNumber,
//                PageSize = pageSize
//            };

//            posts = new List<Post>() { post, post, post };

//            pagedListPosts = new PagedList<Post>(posts, posts.Count, pageNumber, pageSize);
//        }


//        [Fact]
//        public async void Save_ValidRequest_CreatePost()
//        {
//            SetUp();

//            mockPostRepository
//                .Setup(repository => repository.Save(post))
//                .ReturnsAsync(post);


//            var response = await postService.Save(post);

//            var actionValue = Assert.IsType<Post>(response);
//            Assert.Equal(actionValue, post);
//        }

//        [Fact]
//        public async void FindAllPost_ValidRequest_FindPosts()
//        {
//            SetUp();

//            mockPostRepository
//                 .Setup(repository => repository.FindAll(paginationParams))
//                 .ReturnsAsync(pagedListPosts);

//            var response = await postService.FindAll(paginationParams);

//            var actionResult = Assert.IsType<PagedList<Post>>(response);
//            var actionValue = Assert.IsType<List<Post>>(actionResult.Items);
//            Assert.Equal(actionValue.Count, posts.Count);
//        }

//        [Fact]
//        public async void FindPublicPosts_ValidRequest_FindPosts()
//        {
//            SetUp();

//            mockPostRepository
//                .Setup(repository => repository.FindAllPublic(paginationParams))
//                .ReturnsAsync(pagedListPosts);

//            var response = await postService.FindAllPublic(paginationParams);

//            var actionResult = Assert.IsType<PagedList<Post>>(response);
//            var actionValue = Assert.IsType<List<Post>>(actionResult.Items);
//            Assert.Equal(actionValue.Count, posts.Count);
//        }

//        [Fact]
//        public async void FindAllFollowedPosts_ValidRequest_FindPosts()
//        {
//            SetUp();

//            mockPostRepository
//                .Setup(repository => repository.FindAllFollowed(paginationParams, profileId))
//                .ReturnsAsync(pagedListPosts);

//            var response = await postService.FindAllFollowed(paginationParams, profileId);

//            var actionResult = Assert.IsType<PagedList<Post>>(response);
//            var actionValue = Assert.IsType<List<Post>>(actionResult.Items);
//            Assert.Equal(actionValue.Count, posts.Count);
//        }

//        [Fact]
//        public async void FindPublicAndFollowedPosts_ValidRequest_FindPosts()
//        {
//            SetUp();

//            mockPostRepository
//                .Setup(repository => repository.FindAllPublicAndFollowed(paginationParams, profileId))
//                .ReturnsAsync(pagedListPosts);

//            var response = await postService.FindAllPublicAndFollowed(paginationParams, profileId);

//            var actionResult = Assert.IsType<PagedList<Post>>(response);
//            var actionValue = Assert.IsType<List<Post>>(actionResult.Items);
//            Assert.Equal(actionValue, posts);
//        }
//    }
//}
