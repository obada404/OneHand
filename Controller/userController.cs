using System.Net;
using Microsoft.AspNetCore.Mvc;
using OneHandTraining.Validation;
using OneHandTraining.model;
using OneHandTraining.DTO;
using OneHandTraining.Models;

namespace OneHandTraining.controller;
[ApiController]
public class userController :Controller
{
    
    private readonly IUserService _userService;
    readonly private oneHandContext _oneHandContext;
    
    public userController(IUserService userService,oneHandContext oneHandContext)
    {
        _userService = userService;
        _oneHandContext = oneHandContext;
    }

    [HttpPost]
    [Route("/users")] 
    public ActionResult PostUser([FromBody] UserRequestEnv<UserRequest> req)
    {
     

        var validator = new userValidator();
     var result = validator.Validate(req.User);
     if ( !result.IsValid)
     {
         var errors = result.Errors.Select(x => new { errors = x.ErrorMessage });
         return new JsonResult(errors);
     }

        var resp =  _userService.Adduser(req.User);
        //Json version 
        return new JsonResult(new UserRequestEnv<UserOld>(resp)){StatusCode = (int) HttpStatusCode.OK};
    
        //generic version
        return new ObjectResult(new UserRequestEnv<UserOld>(resp)){StatusCode = (int)HttpStatusCode.Accepted};
    }
    
    [HttpPost]
    [Route("/login")] 
    public ActionResult PostLogin([FromBody] UserRequestEnv<loginUserRequest> req)
    {


       var userQuery = _userService.findUserByEmail(req.User);
        
        return new ObjectResult(userQuery){StatusCode = (int)HttpStatusCode.OK};
    }
    [HttpPost]
    [Route("/users/login")] 
    public ActionResult PostUsersLogin([FromBody] UserRequestEnv<loginUserRequest> req)
    {
        
        var userQuery = _userService.findUserByEmail(req.User);

        
        
        return new ObjectResult(userQuery){StatusCode = (int)HttpStatusCode.OK};
    }
    [HttpGet]
    [Route("/user")] 
    public ActionResult GetUser()
    {


       var userQuery= _userService.findUserByToken(Request.Headers.Authorization);
        
        return new ObjectResult(userQuery){StatusCode = (int)HttpStatusCode.OK};
    }

    [HttpPut]
    [Route("/user")]
    public ActionResult PutUser( [FromBody] UserRequestEnv<emailUserRequest> req)
    {
        return new ObjectResult( _userService.updateEmail(Request.Headers.Authorization, req.User)){StatusCode = (int)HttpStatusCode.OK};
    }
}
