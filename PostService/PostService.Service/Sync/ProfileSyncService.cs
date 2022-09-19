using BusService;
using BusService.Contracts;
using Microsoft.Extensions.Logging;
using PostService.Model;
using PostService.Model.Sync;
using PostService.Repository.Interface;
using PostService.Repository.Interface.Sync;
using PostService.Service.Interface.Sync;
using System;
using System.Threading.Tasks;

namespace PostService.Service.Sync
{
    public class ProfileSyncService : ConsumerBase<Profile, ProfileContract>, IProfileSyncService
    {
        private readonly IMessageBusService _messageBusService;
        private readonly IProfileRepository _profileRepository;

        public ProfileSyncService(IMessageBusService messageBusService, 
            IProfileRepository profileRepository,
            ILogger<ProfileSyncService> logger) : base(logger)
        {
            _messageBusService = messageBusService;
            _profileRepository = profileRepository;
        }

        public override Task PublishAsync(Profile entity, string action)
        {
            throw new NotImplementedException();
        }

        public override Task SynchronizeAsync(ProfileContract entity, string action)
        {
            if (action == Events.Created)
            {
                Profile profile = new Profile
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Surname = entity.Surname,
                    Username = entity.Username,
                    Avatar = entity.Avatar
                };
                _profileRepository.Save(profile);
            }
            if (action == Events.Updated)
            {
                Profile dbProfile = _profileRepository.GetById(entity.Id);
                dbProfile.Name = entity.Name;
                dbProfile.Surname = entity.Surname;
                dbProfile.Username = entity.Username;
                dbProfile.Avatar = entity.Avatar;
                _profileRepository.SaveChanges();
            }
            return Task.CompletedTask;
        }

        public Profile GetById(Guid id)
        {
            return _profileRepository.GetById(id);
        }

        public Task<Profile> GetByIdAsync(Guid id)
        {
            return _profileRepository.GetByIdAsync(id);
        }
    }
}
