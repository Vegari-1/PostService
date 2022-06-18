using System;
using System.Collections.Generic;
using PostService.Model;
using System.Threading.Tasks;

namespace PostService.Repository.Interface
{
    public interface IConnectionRepository
    {
        Task<List<Connection>> FindAll();
    }
}
