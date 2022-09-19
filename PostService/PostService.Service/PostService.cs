using PostService.Model;
using PostService.Repository.Interface;
using PostService.Repository.Interface.Pagination;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using PostService.Service.Interface.Sync;
using BusService;

namespace PostService.Service.Interface
{
    public class PostsService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IPostSyncService _postSyncService;

        public PostsService(IPostRepository IPostRepository, IPostSyncService postSyncService)
        {
            _postRepository = IPostRepository;
            _postSyncService = postSyncService;
        }

        public async Task<Post> Save(Post post)
        {
            await _postRepository.SaveAsync(post);
            _postSyncService.PublishAsync(post, Events.Created);
            return post;
        }

        public Task<PagedList<Post>> FindAll(PaginationParams paginationParams)
        {
            return _postRepository.FindAll(paginationParams);
        }

        public Task<PagedList<Post>> FindAllProfilePosts(PaginationParams paginationParams, Guid profileId)
        {
            return _postRepository.FindAllProfilePosts(paginationParams, profileId);
        }

        public Task<PagedList<Post>> FindAllFollowed(PaginationParams paginationParams, Guid profileId)
        {
            return _postRepository.FindAllFollowedByUsername(paginationParams, profileId);
        }

        public Task<IReadOnlyList<Post>> SearchPostByContent(Guid id, string query)
        {
            return _postRepository.SearchPostByContent(id, query);
        }
    }
}
