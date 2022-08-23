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

        public async Task<Comment> Save(Comment comment)
        {
            Comment savedComment = (from c in _context.Comments
                                    where c.AuthorId == comment.AuthorId
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
    }
}
