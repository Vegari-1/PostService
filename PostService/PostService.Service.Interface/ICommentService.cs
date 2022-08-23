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
        Task<Comment> Save(Comment comment);
    }
}
