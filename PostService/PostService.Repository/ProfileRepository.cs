using PostService.Model;
using PostService.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostService.Repository
{
    public class ProfileRepository : Repository<Profile>, IProfileRepository
    {
        public ProfileRepository(AppDbContext context) : base(context) { }

        public async Task<List<Profile>> FindAll()
        {
            return _context.Profiles.ToList();
        }

    }
}
