using PostService.Model;
using PostService.Repository.Interface;
using PostService.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostService.Service
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public Task<Comment> Save(Guid postId, Guid profileId, Comment comment)
        {
            return _commentRepository.Save(postId, profileId, comment);
        }

        public Task<List<Comment>> GetComments(Guid postId)
        {
            return _commentRepository.GetComments(postId);
        }
    }
}
