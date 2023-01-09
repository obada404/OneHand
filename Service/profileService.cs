using AutoMapper;
using OneHandTraining.DTO;
using OneHandTraining.Interface;
using OneHandTraining.model;
using Profile = OneHandTraining.model.Profile;

namespace OneHandTraining.Service;


public class ProfileService : IprofileService
{
     private readonly IMapper _mapper;

     private readonly IprofileRepository _profileRepository;
    public ProfileService(IprofileRepository profileRepository,IMapper mapper)
    {
        _profileRepository = profileRepository;
        _mapper = mapper;

    }
    public ProfileRequestEnv<Profile> FollowProfile(string username, string email)
     {
      return new ProfileRequestEnv<Profile>(_mapper.Map<Profile>(_profileRepository.FollowProfile(username,email)));
     }

     public ProfileRequestEnv<Profile> unFollowProfile(string username, string email)
     {
      return new ProfileRequestEnv<Profile>(_mapper.Map<Profile>(_profileRepository.unFollowProfile(username,email)));
     }

     public List<UserOld> findProfileByAuthor(string username)
     {
      return _profileRepository.findProfileByAuthor(username);
     }
}
