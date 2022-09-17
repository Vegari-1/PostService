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
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _profileRepository;

        public ProfileService(IProfileRepository IPostRepository)
        {
            _profileRepository = IPostRepository;
        }

        public Task<Profile> FindById(Guid id)
        {
            return _profileRepository.FindById(id);
        }
    }
}
