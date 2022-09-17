//using PostService.Dto;
//using PostService.Model;
//using PostService.Repository;
//using Newtonsoft.Json;
//using System;
//using System.Net;
//using System.Net.Http;
//using System.Text;
//using System.Threading.Tasks;
//using Xunit;
//using Xunit.Abstractions;
//using Microsoft.Extensions.Logging;
//using System.Collections.Generic;
//using PostService.Repository.Interface.Pagination;

//namespace PostService.IntegrationTests
//{
//    public class PostControllerTests : IClassFixture<IntegrationWebApplicationFactory<Program, AppDbContext>>
//    {
//        private readonly IntegrationWebApplicationFactory<Program, AppDbContext> _factory;
//        private readonly HttpClient _client;
//        private readonly ITestOutputHelper _testOutputHelper;

//        public PostControllerTests(IntegrationWebApplicationFactory<Program, AppDbContext> factory, ITestOutputHelper testOutputHelper)
//        {
//            _factory = factory;
//            _client = _factory.CreateClient();
//            _testOutputHelper = testOutputHelper;
//        }

//        private static readonly string postsTableName = "Posts";
//        private static readonly string commentsTableName = "Comments";
//        private static readonly string reactionsTableName = "Reactions";
//        private static readonly string profilesTableName = "Profiles";
//        private static readonly string connectionsTableName = "Connections";
//        private static readonly Guid id = Guid.NewGuid();
//        private static readonly Guid authorId = Guid.NewGuid();
//        private static readonly Guid profileId = Guid.NewGuid();
//        private static readonly Guid postId = Guid.NewGuid();
//        private static readonly Guid profile1 = Guid.NewGuid();
//        private static readonly Guid profile2 = Guid.NewGuid();
//        private static readonly string authorIdString = Guid.NewGuid().ToString();
//        private static readonly string content = "test content";
//        private static readonly DateTime timeStamp = new();
//        private static readonly List<Guid> likes = new List<Guid>();
//        private static readonly List<Guid> dislikes = new List<Guid>();
//        private static readonly string name = "test name";
//        private static readonly string surname = "test surname";
//        private static readonly bool positive = true;
//        private static readonly bool publicPost = true;
//        private static readonly int pageNumber = 1;
//        private static readonly int pageSize = 5;
//        private static readonly string actionName = "test";


//        [Fact]
//        public async Task SavePost_CorrectData_ReturnedPost()
//        {
//            // Given
//            PostRequest postRequest = new PostRequest()
//            {
//                Content = content,
//                TimeStamp = timeStamp,
//                AuthorId = authorIdString
//            };
//            var requestContent = new StringContent(JsonConvert.SerializeObject(postRequest), Encoding.UTF8, "application/json");

//            // When
//            var response = await _client.PostAsync("/api/post", requestContent);

//            // Then
//            response.EnsureSuccessStatusCode();
//            var responseContentString = await response.Content.ReadAsStringAsync();
//            var responseContentObject = JsonConvert.DeserializeObject<Post>(responseContentString);
//            Assert.NotNull(responseContentObject);
//            Assert.Equal(content, responseContentObject.Content);
//            Assert.Equal(authorIdString, responseContentObject.AuthorId.ToString());
//            Assert.Equal(timeStamp, responseContentObject.TimeStamp);
//            Assert.Equal(1L, _factory.CountTableRows(postsTableName));

//            // Rollback
//            _factory.DeleteById(postsTableName, responseContentObject.Id);
//        }


//        [Fact]
//        public async void SaveComment_CorrectData_ReturnedComment()
//        {
//            // Given
//            CommentRequest commentRequest = new CommentRequest()
//            {
//                Content = content
//            };
//            var requestContent = new StringContent(JsonConvert.SerializeObject(commentRequest), Encoding.UTF8, "application/json");

//            // When
//            var response = await _client.PostAsync("/api/post/comment", requestContent);

//            // Then
//            response.EnsureSuccessStatusCode();
//            var responseContentString = await response.Content.ReadAsStringAsync();
//            var responseContentObject = JsonConvert.DeserializeObject<Comment>(responseContentString);
//            Assert.NotNull(responseContentObject);
//            Assert.Equal(content, responseContentObject.Content);
//            Assert.Equal(authorId, responseContentObject.AuthorId);
//            Assert.Equal(postId, responseContentObject.PostId);
//            Assert.Equal(1L, _factory.CountTableRows(commentsTableName));

//            // Rollback
//            _factory.DeleteById(commentsTableName, responseContentObject.Id);
//        }


//        [Fact]
//        public async void SaveReaction_CorrectData_ReturnedReaction()
//        {
//            // Given
//            Post post = new Post()
//            {
//                Id = postId,
//                Content = content,
//                TimeStamp = timeStamp,
//                AuthorId = authorId,
//                Likes = likes,
//                Dislikes = dislikes
//            };

//            ReactionRequest reactionRequest = new ReactionRequest()
//            {
//                Positive = positive
//            };
//            _factory.InsertPost(postsTableName, post);
//            var requestContent = new StringContent(JsonConvert.SerializeObject(reactionRequest), Encoding.UTF8, "application/json");

//            // When
//            var response = await _client.PostAsync("/api/post/reaction", requestContent);

//            // Then
//            response.EnsureSuccessStatusCode();
//            var responseContentString = await response.Content.ReadAsStringAsync();
//            var responseContentObject = JsonConvert.DeserializeObject<Reaction>(responseContentString);
//            Assert.NotNull(responseContentObject);
//            Assert.Equal(positive, responseContentObject.Positive);
//            Assert.Equal(authorId, responseContentObject.AuthorId);
//            Assert.Equal(postId, responseContentObject.PostId);
//            Assert.Equal(1L, _factory.CountTableRows(reactionsTableName));

//            // Rollback
//            _factory.DeleteById(reactionsTableName, responseContentObject.Id);
//        }

//        [Fact]
//        public async void GetAllPosts_CorrectRequest_ReturnedAllPosts()
//        {
//            // Given
//            Post post = new Post()
//            {
//                Id = id,
//                AuthorId = authorId,
//                Content = content,
//                Likes = likes,
//                Dislikes = dislikes

//            };
//            _factory.InsertPost(postsTableName, post);

//            // When
//            var response = await _client.GetAsync("/api/post?pageNumber=1&pageSize=5");

//            // Then
//            response.EnsureSuccessStatusCode();
//            var responseContentString = await response.Content.ReadAsStringAsync();
//            var responseContentObject = JsonConvert.DeserializeObject<PagedList<Post>>(responseContentString);
//            Assert.NotNull(responseContentObject);
//            Assert.Equal(1, responseContentObject.Count);
//            Assert.Equal(1L, _factory.CountTableRows(postsTableName));

//            // Rollback
//            _factory.DeleteById(postsTableName, post.Id);
//        }

//        [Fact]
//        public async void GetPublicPosts_CorrectRequest_ReturnedPublicPosts()
//        {
//            // Given
//            Post post = new Post()
//            {
//                Id = id,
//                AuthorId = authorId,
//                Content = content,
//                Likes = likes,
//                Dislikes = dislikes
                   
//            };
//            Profile profile = new Profile()
//            {
//                Id = authorId,
//                Public = publicPost,
//                Name = name,
//                Surname = surname

//            };
//            _factory.InsertProfile(profilesTableName, profile);
//            _factory.InsertPost(postsTableName, post);

//            // When
//            var response = await _client.GetAsync("/api/post/public?pageNumber=1&pageSize=5");

//            // Then
//            response.EnsureSuccessStatusCode();
//            var responseContentString = await response.Content.ReadAsStringAsync();
//            var responseContentObject = JsonConvert.DeserializeObject<PagedList<Post>>(responseContentString);
//            Assert.NotNull(responseContentObject);
//            Assert.Equal(1, responseContentObject.Count);
//            Assert.Equal(1L, _factory.CountTableRows(postsTableName));

//            // Rollback
//            _factory.DeleteById(postsTableName, post.Id);
//            _factory.DeleteById(profilesTableName, profile.Id);
//        }

//        [Fact]
//        public async void GetFollowedPosts_CorrectRequest_ReturnedFollowedPosts()
//        {
//            // Given
//            FollowedProfilePostsRequest followedRequest = new FollowedProfilePostsRequest()
//            {
//                ProfileId = id
//            };

//            Post post = new Post()
//            {
//                Id = id,
//                AuthorId = authorId,
//                Content = content,
//                Likes = likes,
//                Dislikes = dislikes

//            };
//            Profile profile = new Profile()
//            {
//                Id = authorId,
//                Public = publicPost,
//                Name = name,
//                Surname = surname

//            };
//            Connection connection = new Connection()
//            {
//                Id = id,
//                Profile1 = id,
//                Profile2 = authorId
//            };
//            _factory.InsertProfile(profilesTableName, profile);
//            _factory.InsertPost(postsTableName, post);
//            _factory.InsertConnection(connectionsTableName, connection);
//            var requestContent = new StringContent(JsonConvert.SerializeObject(followedRequest), Encoding.UTF8, "application/json");

//            // When
//            var response = await _client.PostAsync("/api/post/followed?pageNumber=1&pageSize=5", requestContent);

//            // Then
//            response.EnsureSuccessStatusCode();
//            var responseContentString = await response.Content.ReadAsStringAsync();
//            var responseContentObject = JsonConvert.DeserializeObject<PagedList<Post>>(responseContentString);
//            Assert.NotNull(responseContentObject);
//            Assert.Equal(1, responseContentObject.Count);
//            Assert.Equal(1L, _factory.CountTableRows(postsTableName));

//            // Rollback
//            _factory.DeleteById(postsTableName, post.Id);
//            _factory.DeleteById(profilesTableName, profile.Id);
//            _factory.DeleteById(connectionsTableName, connection.Id);
//        }


//        [Fact]
//        public async void GetPublicAndFollowedPosts_CorrectRequest_ReturnedPublicAndFollowedPosts()
//        {
//            // Given
//            FollowedProfilePostsRequest followedRequest = new FollowedProfilePostsRequest()
//            {
//                ProfileId = id
//            };

//            Post post = new Post()
//            {
//                Id = id,
//                AuthorId = authorId,
//                Content = content,
//                Likes = likes,
//                Dislikes = dislikes

//            };
//            Profile profile = new Profile()
//            {
//                Id = authorId,
//                Public = publicPost,
//                Name = name,
//                Surname = surname

//            };
//            Connection connection = new Connection()
//            {
//                Id = id,
//                Profile1 = id,
//                Profile2 = authorId
//            };
//            _factory.InsertProfile(profilesTableName, profile);
//            _factory.InsertPost(postsTableName, post);
//            _factory.InsertConnection(connectionsTableName, connection);
//            var requestContent = new StringContent(JsonConvert.SerializeObject(followedRequest), Encoding.UTF8, "application/json");

//            // When
//            var response = await _client.PostAsync("/api/post/public-followed?pageNumber=1&pageSize=5", requestContent);

//            // Then
//            response.EnsureSuccessStatusCode();
//            var responseContentString = await response.Content.ReadAsStringAsync();
//            var responseContentObject = JsonConvert.DeserializeObject<PagedList<Post>>(responseContentString);
//            Assert.NotNull(responseContentObject);
//            Assert.Equal(1, responseContentObject.Count);
//            Assert.Equal(1L, _factory.CountTableRows(postsTableName));

//            // Rollback
//            _factory.DeleteById(postsTableName, post.Id);
//            _factory.DeleteById(profilesTableName, profile.Id);
//            _factory.DeleteById(connectionsTableName, connection.Id);
//        }
//    }
//}
