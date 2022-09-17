using PostService.Model;
using PostService.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostService.Repository
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(AppDbContext context) : base(context) { }

        public async Task<Comment> Save(Guid postId, string username, Comment comment)
        {
            var authorId = _context.Profiles
                           .Where(x => x.Username == username)
                           .Select(x => x.Id)
                           .FirstOrDefault();


            var savedComment = (from c in _context.Comments
                                where c.AuthorId == authorId && c.PostId == postId
                                select c).FirstOrDefault();
                                    

            if (savedComment == null)
            {
                _context.Comments.Add(comment);
            } else
            {
                savedComment.Content = comment.Content;
            }

            _context.SaveChanges();

            return comment;
        }

        public async Task<List<Comment>> GetComments(Guid postId)
        {
            return _context.Comments
                    .Where(x => x.PostId == postId)
                    .ToList();
        }
    }
}
