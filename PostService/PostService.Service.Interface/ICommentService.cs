using PostService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostService.Service.Interface
{
    public interface ICommentService
    {
        Task<Comment> Save(Guid postId, Guid profileId, Comment comment);

        Task<List<Comment>> GetComments(Guid postId);
    }
}
