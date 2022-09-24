using System.Net;
using Microsoft.AspNetCore.Mvc;
using OneHandTraining.Validation;
using OneHandTraining.model;
using OneHandTraining.DTO;
namespace OneHandTraining.controller;
[ApiController]
public class userController :Controller
{

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
        var resp =  Service.setUser(req.User);
        //Json version 
        return new JsonResult(new UserRequestEnv<UserOld>(resp)){StatusCode = (int) HttpStatusCode.OK};
    
        //generic version
        return new ObjectResult(new UserRequestEnv<UserOld>(resp)){StatusCode = (int)HttpStatusCode.Accepted};
    }
    
    [HttpPost]
    [Route("/login")] 
    public ActionResult PostLogin([FromBody] UserRequestEnv<loginUserRequest> req)
    {


       var userQuery = Service.findUserByEmail(req.User);
        
        return new ObjectResult(userQuery.Last()){StatusCode = (int)HttpStatusCode.OK};
    }
    [HttpPost]
    [Route("/users/login")] 
    public ActionResult PostUsersLogin([FromBody] UserRequestEnv<loginUserRequest> req)
    {
        
        var userQuery = Service.findUserByEmail(req.User);

        
        
        return new ObjectResult(userQuery.Last()){StatusCode = (int)HttpStatusCode.OK};
    }
    [HttpGet]
    [Route("/user")] 
    public ActionResult GetUser()
    {


       var userQuery= Service.findUserByToken(Request.Headers.Authorization);
        
        return new ObjectResult(userQuery.Last()){StatusCode = (int)HttpStatusCode.OK};
    }

    [HttpPut]
    [Route("/user")]
    public ActionResult PutUser( [FromBody] UserRequestEnv<emailUserRequest> req)
    {
        return Service.updateEmail(Request.Headers.Authorization, req.User);
    }   

    [HttpPost]
    [Route("/profiles")]
    public ActionResult postProfiles([FromBody] ProfileRequestEnv<Profile> req)
    {
      var root = Service.setProfile(req.profile);
     return new ObjectResult(root){StatusCode = (int)HttpStatusCode.Accepted};
        
    }
    [HttpGet]
    [Route("/profiles/{username}")]
    public ActionResult Getprofiles(String username)
    {

    var profileQuery=Service.findProfileByAuthor(username);
            return new ObjectResult(profileQuery.Last()){StatusCode = (int)HttpStatusCode.OK};
        }
    [HttpPost]
    [Route("/profiles/{username}/follow")]
    public ActionResult postFollowProfiles(String username)
    {
        return Service.FindfollowingProfiles(username);
    }
    [HttpDelete]
    [Route("/profiles/{username}/follow")]
    public ActionResult Deleteprofiles(String username)
    {

      return  Service.Deleteprofiles(username);
    }
    
    [HttpGet]
    [Route("/tags")]
    public ActionResult getTags(){

        return new ObjectResult( new tagseRequestEnv<Array>(Repo.tags) ){StatusCode = (int)HttpStatusCode.OK};
    }
    
    [HttpPost]
    [Route("/articles")]
    public ActionResult PostArticles([FromBody] ArticleRequestEnv<ArticleRequest> req){
        var validator = new ArticleValidator();
     var result = validator.Validate(req.article);
     if ( !result.IsValid)
     {
         var errors = result.Errors.Select(x => new { errors = x.ErrorMessage });
         return new ObjectResult( errors ){StatusCode = (int)HttpStatusCode.BadRequest};
         
         
     }

     var root = Service.setArticle(req.article);
     return new ObjectResult(root ){StatusCode = (int)HttpStatusCode.OK};
   
    }
    
    [HttpGet]
    [Route("/articles")]
    public ActionResult getaArticles(){
        if(! (string.IsNullOrEmpty(Request.Query["favorited"])))
        {
            var que = Service.getaArticlesByFavorited((Request.Query["favorited"]));
            return new ObjectResult( que ){StatusCode = (int)HttpStatusCode.OK};
        }
        
        
        if(! (string.IsNullOrEmpty(Request.Query["author"])))
        {
            var que = Service.getaArticlesByAuthor((Request.Query["author"]));
            return new ObjectResult( que ){StatusCode = (int)HttpStatusCode.OK};
        }
        
        if(! (string.IsNullOrEmpty(Request.Query["tag"])))
        {
            var que = Service.getaArticlesByTag((Request.Query["tag"]));
            return new ObjectResult( que ){StatusCode = (int)HttpStatusCode.OK};
        }
        
        return new ObjectResult( Repo.allArticle ){StatusCode = (int)HttpStatusCode.OK};
    }
    
    [HttpGet]
    [Route("/articles/feed")]
    public ActionResult getArticlesFeed()
    {



        var que = Service.getArticlesFeed(Request.Headers.Authorization);
        return new ObjectResult(que){StatusCode = (int)HttpStatusCode.OK};
    }

    
}
