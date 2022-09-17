using PostService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostService.Repository.Interface
{
    public interface IProfileRepository
    {
        Task<List<Profile>> FindAll();

        Task<Profile> FindById(Guid id);
    }
}
