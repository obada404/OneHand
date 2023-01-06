using System.Net;
using AdventureWorks.Filter;
using Microsoft.AspNetCore.Mvc;
using OneHandTraining.model;
using OneHandTraining.DTO;
using OneHandTraining.Interface;

namespace OneHandTraining.controller;
[ApiController]
public class ProfileController:Controller
{
    private readonly IprofileService _profileService;

    public ProfileController(IprofileService profileService)
    {
        _profileService = profileService;
    }

    [HttpGet]
    [TypeFilter(typeof(LogFilter))]
    [Route("/profiles/{username}")]
    public ActionResult Getprofiles(String username)
    {

        var profileQuery=_profileService.findProfileByAuthor(username);
        return new ObjectResult(profileQuery.Last()){StatusCode = (int)HttpStatusCode.OK};
    }

    [HttpGet]
    [Route("/profiles/{username}/follow")]
    public ActionResult FollowProfile(String username,[FromBody] emailUserRequest emailUserRequest)
    {
        var result = _profileService.FollowProfile(username, emailUserRequest.Email);
        return new ObjectResult(result){StatusCode = (int)HttpStatusCode.OK};
    }

    [HttpDelete]
    [Route("/profiles/{username}/follow")]
    public ActionResult unFollowProfile(String username,[FromBody] emailUserRequest emailUserRequest)
    {
        var result = _profileService.unFollowProfile(username,emailUserRequest.Email);
        return new ObjectResult(result){StatusCode = (int)HttpStatusCode.OK};
 
    }

    [HttpGet]
    [Route("/tags")]
    public ActionResult getTags(){
        return new ObjectResult( new tagseRequestEnv<Array>(Repo.tags) ){StatusCode = (int)HttpStatusCode.OK};
    }
}