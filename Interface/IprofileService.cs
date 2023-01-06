using Microsoft.AspNetCore.Mvc;
using OneHandTraining.DTO;
using OneHandTraining.model;

namespace OneHandTraining.Interface;

public interface IprofileService
{
    ProfileRequestEnv<Profile> FollowProfile(string username, string email);
    ProfileRequestEnv<Profile> unFollowProfile(string username, string email);
    List<UserOld> findProfileByAuthor(string username);
}
