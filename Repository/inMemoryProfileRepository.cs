using System.Net;
using Microsoft.AspNetCore.Mvc;
using OneHandTraining.DTO;
using OneHandTraining.Interface;
using OneHandTraining.model;

namespace OneHandTraining.Repository;

public class inMemoryProfileRepository:IprofileRepository
{
    public ProfileRequestEnv<Profile> addProfile(Profile profile)
    {
        Profile profileTmp = new Profile(profile.username, profile.bio,profile.image,profile.following);
        ProfileRequestEnv<Profile> root = new ProfileRequestEnv<Profile>(profileTmp);
        return root;    }
    
    

    public ProfileRequestEnv<Profile> FollowProfile(string username, string email)
    {
        foreach( ProfileRequestEnv<Profile> requestEnv in Repo.profileDb)
        {
            if (requestEnv.profile.username.Equals(username) == true )
            {
                requestEnv.profile.following = true;
                return requestEnv;
            }
        }
        return null;
    }

    public ProfileRequestEnv<Profile> unFollowProfile(string username, string email)
    {
        foreach( ProfileRequestEnv<Profile> profileRequest in Repo.profileDb)
        {
            if (profileRequest.profile.username.Equals(username) == true )
            {
                profileRequest.profile.following = false;
                return profileRequest;
            }
        }
        return null;
    }

    public List<UserOld> findProfileByAuthor(string username)
    {
        IEnumerable<ProfileRequestEnv<Profile>> profileQuery =
            from Profile in Repo.profileDb
            where Profile.profile.username.Equals(username)
            select Profile;
        return null;
    }
    
}