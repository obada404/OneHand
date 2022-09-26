using System.Net;
using Microsoft.AspNetCore.Mvc;
using OneHandTraining.DTO;
using OneHandTraining.model;

namespace OneHandTraining;

public interface IprofileService
{
    ProfileRequestEnv<Profile> setProfile(Profile profile);
    ActionResult FindfollowingProfiles(String username);
    ActionResult Deleteprofiles(String username);
    IEnumerable<ProfileRequestEnv<Profile>> findProfileByAuthor(String username);
}

public class profileService : IprofileService
{
     public ProfileRequestEnv<Profile> setProfile(Profile profile)
    {
        Profile pro = new Profile(profile.username, profile.bio,profile.image,profile.following);
        ProfileRequestEnv<Profile> root = new ProfileRequestEnv<Profile>(pro);
        Repo.profileDb.Add(root);
        return root;
    }

     public ActionResult FindfollowingProfiles(String username)
    {
        foreach( ProfileRequestEnv<Profile> cust in Repo.profileDb)
        {
            if (cust.profile.username.Equals(username) == true )
            {
                cust.profile.following = true;
                return new ObjectResult(cust){StatusCode = (int)HttpStatusCode.OK};
            }
        }
        return new ObjectResult("user profile "){StatusCode = (int)HttpStatusCode.NoContent};
    }

     public ActionResult Deleteprofiles(String username)
    {
        foreach( ProfileRequestEnv<Profile> cust in Repo.profileDb)
        {
            if (cust.profile.username.Equals(username) == true )
            {
                cust.profile.following = false;
                return new ObjectResult(cust){StatusCode = (int)HttpStatusCode.OK};
            }
        }
        return new ObjectResult("user profile "){StatusCode = (int)HttpStatusCode.NoContent};
    }

     public IEnumerable<ProfileRequestEnv<Profile>> findProfileByAuthor(String username)
    {
       
        IEnumerable<ProfileRequestEnv<Profile>> profileQuery =
            from Profile in Repo.profileDb
            where Profile.profile.username.Equals(username)
            select Profile;

        return profileQuery;
    }
}
