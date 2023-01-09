using System.Net;
using AdventureWorks.Filter;
using Microsoft.AspNetCore.Mvc;
using OneHandTraining.Validation;
using OneHandTraining.DTO;
using OneHandTraining.Interface;

namespace OneHandTraining.controller;
[ApiController]
public class UserController :Controller
{
    
    private readonly IUserService _userService;
    private readonly userValidator _userValidator;
    public UserController(IUserService userService,userValidator userValidator)
    {
        _userService = userService;
        _userValidator = userValidator;
    }
    [HttpPost]
    [Route("/users")] 
    public async Task<JsonResult> UserSingIn([FromBody] UserRequestEnv<UserRegistrationRequest> req)
    {
        var validationResult = await _userValidator.ValidateAsync(req.User);
     if ( !validationResult.IsValid)
     {
         var errors = validationResult.Errors.Select(x => new { errors = x.ErrorMessage });
         return new JsonResult(errors);
     }
        var result = await _userService.AdduserAsync(req.User);

        return new JsonResult(new UserRequestEnv<UserRequest>(result)) { StatusCode = (int)HttpStatusCode.OK };
    }
    [HttpPost]
    [Route("/users/login")] 
    public async Task<ActionResult> UsersLogin([FromBody] UserRequestEnv<loginUserRequest> req)
    {        
        var userOld = await _userService.findUserByEmail(req.User);
        return new ObjectResult(new UserRequestEnv<UserRequest>(userOld)) { StatusCode = (int)HttpStatusCode.OK };
    }
    [HttpGet]
    [Route("/user")] 
    [TypeFilter(typeof(LogFilter))]
    public ActionResult GetCurrentUser()
    {
        var userQuery= _userService.findUserByEmailAndId(Request.Headers["email"]!,int.Parse(Request.Headers["id"]!));
        return new ObjectResult(userQuery){StatusCode = (int)HttpStatusCode.OK};
    }
    [HttpPut]
    [Route("/user")]
    [TypeFilter(typeof(LogFilter))]
    public ActionResult UpdateUser( [FromBody] UserRequestEnv<emailUserRequest> req)
    {
        return new ObjectResult( _userService.updateEmail( req.User,int.Parse(Request.Headers["id"]!))){StatusCode = (int)HttpStatusCode.OK};
    }
}
