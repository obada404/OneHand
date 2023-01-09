using Microsoft.AspNetCore.Mvc;
using OneHandTraining.DTO;
using OneHandTraining.model;

namespace OneHandTraining.Interface;

public interface IprofileRepository                                                                  
{
    UserOld? FollowProfile(string username, string email);
    UserOld? unFollowProfile(string username, string email);
    List<UserOld> findProfileByAuthor(string username);
}