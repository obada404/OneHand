using OneHandTraining.DTO;
using OneHandTraining.Interface;
using OneHandTraining.model;

namespace OneHandTraining.Service;


public class ProfileService : IprofileService
{
 
    private readonly IprofileRepository _profileRepository;
    public ProfileService(IprofileRepository profileRepository)
    {
        _profileRepository = profileRepository;
    }
    public ProfileRequestEnv<Profile> FollowProfile(string username, string email)
     {
      return _profileRepository.FollowProfile(username,email);
     }

     public ProfileRequestEnv<Profile> unFollowProfile(string username, string email)
     {
      return _profileRepository.unFollowProfile(username,email);
     }

     public List<UserOld> findProfileByAuthor(string username)
     {
      return _profileRepository.findProfileByAuthor(username);
     }
}
