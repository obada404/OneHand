using System.Net;
using Microsoft.AspNetCore.Mvc;
using OneHandTraining.Validation;
using OneHandTraining.model;
using OneHandTraining.DTO;
using OneHandTraining.Models;

namespace OneHandTraining.controller;
[ApiController]
public class profileController:Controller
{
    private readonly IprofileService _profileService;
    readonly private oneHandContext _oneHandContext;

    public profileController(IprofileService profileService,oneHandContext oneHandContext)
    {
        
     
            _profileService = profileService;
            _oneHandContext = oneHandContext;
        
    }

    [HttpPost]
    [Route("/profiles")]
    public ActionResult postProfiles([FromBody] ProfileRequestEnv<Profile> req)
    {
        var root = _profileService.setProfile(req.profile);
        return new ObjectResult(root){StatusCode = (int)HttpStatusCode.Accepted};
        
    }

    [HttpGet]
    [Route("/profiles/{username}")]
    public ActionResult Getprofiles(String username)
    {

        var profileQuery=_profileService.findProfileByAuthor(username);
        return new ObjectResult(profileQuery.Last()){StatusCode = (int)HttpStatusCode.OK};
    }

    [HttpPost]
    [Route("/profiles/{username}/follow")]
    public ActionResult postFollowProfiles(String username)
    {
        return _profileService.FindfollowingProfiles(username);
    }

    [HttpDelete]
    [Route("/profiles/{username}/follow")]
    public ActionResult Deleteprofiles(String username)
    {

        return  _profileService.Deleteprofiles(username);
    }

    [HttpGet]
    [Route("/tags")]
    public ActionResult getTags(){

        return new ObjectResult( new tagseRequestEnv<Array>(Repo.tags) ){StatusCode = (int)HttpStatusCode.OK};
    }
}