using System.Net;
using AdventureWorks;
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
    private readonly JwtManager _jwtManager;
    public UserController(IConfiguration config,IUserService userService,userValidator userValidator)
    {
        _userService = userService;
        _userValidator = userValidator;
        _jwtManager = new JwtManager(config["Jwt:Key"]);
    }
    [HttpPost]
    [Route("/users")] 
    public ActionResult userSingIn([FromBody] UserRequestEnv<UserRequest> req)
    {
        var result = _userValidator.Validate(req.User);
     if ( !result.IsValid)
     {
         var errors = result.Errors.Select(x => new { errors = x.ErrorMessage });
         return new JsonResult(errors);
     }
        var resp =  _userService.Adduser(req.User);
        //Json version 
        return new JsonResult(new UserRequestEnv<UserRequest>(resp)){StatusCode = (int) HttpStatusCode.OK};
    
        //generic version
        //return new ObjectResult(new UserRequestEnv<UserOld>(resp)){StatusCode = (int)HttpStatusCode.Accepted};
    }
    [HttpPost]
    [Route("/login")] 
    public ActionResult userLogin([FromBody] UserRequestEnv<loginUserRequest> req)
    {
        var userOld = _userService.findUserByEmail(req.User);
        return new ObjectResult(userOld){StatusCode = (int)HttpStatusCode.OK};
    }
    [HttpPost]
    [Route("/users/login")] 
    public ActionResult usersLogin([FromBody] UserRequestEnv<loginUserRequest> req)
    {
        var userOld = _userService.findUserByEmail(req.User);
        var jwt = _jwtManager.GenerateJwt(userOld.Id+"", userOld.Username!, userOld.Email!,"Admin");
        userOld.Token = jwt;
        return new ObjectResult(userOld) { StatusCode = (int)HttpStatusCode.OK };
    }
    [HttpGet]
    [Route("/user")] 
    [TypeFilter(typeof(LogFilter))]
    public ActionResult GetUser()
    {
        var userQuery= _userService.findUserByToken(Request.Headers.Authorization);
        return new ObjectResult(userQuery){StatusCode = (int)HttpStatusCode.OK};
    }
    [HttpPut]
    [Route("/user")]
    [TypeFilter(typeof(LogFilter))]
    public ActionResult updateUser( [FromBody] UserRequestEnv<emailUserRequest> req)
    {
        return new ObjectResult( _userService.updateEmail(Request.Headers.Authorization, req.User)){StatusCode = (int)HttpStatusCode.OK};
    }
}
