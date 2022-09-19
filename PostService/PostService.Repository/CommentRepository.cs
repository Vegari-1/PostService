using PostService.Model;
using PostService.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Repository
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(AppDbContext context) : base(context) { }

        public async Task<Comment> Save(Guid postId, Guid profileId, Comment comment)
        {
            var author = _context.Profiles
                           .Where(x => x.Id == profileId)
                           .FirstOrDefault();

            var savedComment = (from c in _context.Comments
                                where c.AuthorId == profileId && c.PostId == postId
                                select c).FirstOrDefault();
                                    

            comment.TimeStamp = DateTime.Now;
            comment.PostId = postId;
            comment.AuthorId = profileId;
            comment.Name = author.Name;
            comment.Surname = author.Surname;
            comment.Username = author.Username;
            comment.Avatar = author.Avatar;

            _context.Comments.Add(comment);
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
