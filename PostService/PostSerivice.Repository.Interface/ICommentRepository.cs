using PostService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostService.Repository.Interface
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Task<Comment> Save(Guid postId, Guid profileId, Comment comment);

        Task<List<Comment>> GetComments(Guid postId);
    }
}
